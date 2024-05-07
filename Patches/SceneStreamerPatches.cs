using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;

using BoneLib;

using MelonLoader;

using SLZ;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Warehouse;
using InventoryPersistence.Data;
using InventoryPersistence.Bonemenu;

namespace InventoryPersistence.Patches
{
    [HarmonyPatch(typeof(SceneStreamer))]
    public class SceneStreamerPatches
    {
        [HarmonyPatch(nameof(SceneStreamer.Load), new[] { typeof(LevelCrateReference), typeof(LevelCrateReference) })]
        [HarmonyPrefix]
        public static void Load(LevelCrateReference level, LevelCrateReference loadLevel)
        {
            OnLoadLevel(level.Barcode.ID);
        }

        [HarmonyPatch(nameof(SceneStreamer.Load), new[] { typeof(string), typeof(string) })]
        [HarmonyPrefix]
        public static void Load(string levelBarcode, string loadLevelBarcode)
        {
#if DEBUG
            MelonLogger.Msg("Hello World from string Load!");
#endif

            OnLoadLevel(levelBarcode);
        }

        private static void OnLoadLevel(string levelBarcode)
        {
            if (!BoneMenuPreferences.LevelBlacklist.Value.Contains(SceneStreamer.Session.Level.Barcode.ID))
                JsonSaving.SaveInventory(PlayerInventory.GetPlayerInventory());
        }

    }
}
