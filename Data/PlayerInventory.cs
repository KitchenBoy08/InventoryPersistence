using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoneLib;
using InventoryPersistence.Bonemenu;
using MelonLoader;

using SLZ;
using SLZ.Interaction;
using SLZ.Marrow.Pool;

using UnityEngine;

namespace InventoryPersistence
{
    public class PlayerInventory
    {
        public int LightAmmoCount = 0;
        public int MediumAmmoCount = 0;
        public int HeavyAmmoCount = 0;

        public Dictionary<string, string> InventoryBarcodes { get; set; }

        public static Dictionary<string, string> GetPlayerInventoryBarcodes()
        {
            if (Player.rigManager == null)
                return null;

            Dictionary<string, string> inventory = new();

            foreach (var bodySlot in Player.rigManager.inventory.bodySlots)
            {
                if (!bodySlot.inventorySlotReceiver)
                    continue;

                if (!bodySlot.inventorySlotReceiver._slottedWeapon)
                    continue;

                AssetPoolee assetpoolee;
                if (bodySlot.inventorySlotReceiver._slottedWeapon.interactableHost.manager)
                    assetpoolee = bodySlot.inventorySlotReceiver._slottedWeapon.interactableHost.manager.transform.GetComponent<AssetPoolee>();
                else
                    assetpoolee = bodySlot.inventorySlotReceiver._slottedWeapon.interactableHost.transform.GetComponent<AssetPoolee>();

                if (!assetpoolee)
                    continue;

                string barcode = assetpoolee.spawnableCrate.Barcode.ID;

                inventory.Add(bodySlot.name, barcode);
            }

            return inventory;
        }

        public static PlayerInventory GetPlayerInventory()
        {
            PlayerInventory inventory = new();

            if (BoneMenuPreferences.SaveAmmo.Value)
            {
                inventory.LightAmmoCount = Player.rigManager.AmmoInventory.GetCartridgeCount("light");
                inventory.MediumAmmoCount = Player.rigManager.AmmoInventory.GetCartridgeCount("medium");
                inventory.HeavyAmmoCount = Player.rigManager.AmmoInventory.GetCartridgeCount("heavy");
            }

            if (BoneMenuPreferences.SaveItems.Value)
                inventory.InventoryBarcodes = GetPlayerInventoryBarcodes();

            return inventory;
        }
    }
}
