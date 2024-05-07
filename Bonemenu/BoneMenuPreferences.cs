using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using MelonLoader;
using SLZ.Marrow.SceneStreaming;
using UnityEngine;

namespace InventoryPersistence.Bonemenu
{
    internal class BoneMenuPreferences
    {
        internal static MenuCategory MainCategory;

        internal static MelonPreferences_Category PreferenceCategory;

        internal static MelonPreferences_Entry<bool> SaveAmmo;
        internal static MelonPreferences_Entry<bool> SaveItems;
        internal static MelonPreferences_Entry<List<string>> LevelBlacklist;

        internal static BoolElement SaveAmmoElement;
        internal static BoolElement SaveItemsElement;
        internal static FunctionElement BlacklistLevelElement;

        internal static void InitializeBonemneu()
        {
            MainCategory = MenuManager.CreateCategory("Inventory Persistence", Color.white);
            PreferenceCategory = MelonPreferences.CreateCategory("InventoryPersistence", "Inventory Persistence", false, true);

            SaveAmmo = PreferenceCategory.CreateEntry("SaveAmmo", true);
            SaveItems = PreferenceCategory.CreateEntry("SaveItems", true);
            LevelBlacklist = PreferenceCategory.CreateEntry("LevelBlacklist", new List<string>());

            SaveAmmoElement = MainCategory.CreateBoolElement("Save Ammo", Color.white, true, (save) =>
            {
                SaveAmmo.Value = save;
                SaveAmmo.Save();
            });
            SaveItemsElement = MainCategory.CreateBoolElement("Save Items", Color.white, true, (save) =>
            {
                SaveItems.Value = save;
                SaveItems.Save();
            });

            BlacklistLevelElement = MainCategory.CreateFunctionElement("Blacklist Current Level", Color.red, () =>
            {
                if (LevelBlacklist.Value.Contains(SceneStreamer.Session.Level.Barcode.ID))
                {
                    LevelBlacklist.Value.Remove(SceneStreamer.Session.Level.Barcode.ID);
                    BlacklistLevelElement.SetName("Blacklist Current Level");
                    BlacklistLevelElement.SetColor(Color.red);
                } 
                else
                {
                    LevelBlacklist.Value.Add(SceneStreamer.Session.Level.Barcode.ID);
                    BlacklistLevelElement.SetName("Whitelist Current Level");
                    BlacklistLevelElement.SetColor(Color.green);
                }

                LevelBlacklist.Save();
            });
        }
    }
}
