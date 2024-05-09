using BoneLib;
using BoneLib.Nullables;

using InventoryPersistence.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using SLZ.Marrow.Warehouse;
using SLZ.Marrow.Data;
using SLZ.Marrow.Pool;
using SLZ.Interaction;
using MelonLoader;
using BoneLib.Notifications;
using InventoryPersistence.Bonemenu;
using System.Reflection;
using LabFusion.Network;
using LabFusion.Utilities;
using LabFusion.Data;
using LabFusion.Representation;
using Steamworks.Ugc;

namespace InventoryPersistence
{
    internal class Hooking
    {
        internal static void InitializeHooks()
        {
            BoneLib.Hooking.OnLevelInitialized += OnLevelInitializedHook;
        }

        private static void OnLevelInitializedHook(LevelInfo levelInfo)
        {
            if (BoneMenuPreferences.LevelBlacklist.Value.Contains(levelInfo.barcode))
            {
                BoneMenuPreferences.BlacklistLevelElement.SetName("Whitelist Current Level");
                BoneMenuPreferences.BlacklistLevelElement.SetColor(Color.green);

                return;
            } else
            {
                BoneMenuPreferences.BlacklistLevelElement.SetName("Blacklist Current Level");
                BoneMenuPreferences.BlacklistLevelElement.SetColor(Color.red);
            }

            PlayerInventory inventory = JsonSaving.LoadInventory();

            if (inventory == null)
                return;

            Player.rigManager.AmmoInventory.ClearAmmo();

            Player.rigManager.AmmoInventory.AddCartridge(Player.rigManager.AmmoInventory.lightAmmoGroup, inventory.LightAmmoCount);
            Player.rigManager.AmmoInventory.AddCartridge(Player.rigManager.AmmoInventory.mediumAmmoGroup, inventory.MediumAmmoCount);
            Player.rigManager.AmmoInventory.AddCartridge(Player.rigManager.AmmoInventory.heavyAmmoGroup, inventory.HeavyAmmoCount);


            if (Main.HasFusion && NetworkInfo.HasServer)
            {
                foreach (var item in inventory.InventoryBarcodes)
                {
                    SendFusionSpawn(item.Value);
                }

                return;
            }

            foreach (var item in inventory.InventoryBarcodes)
            {

                var slotContainer = Player.rigManager.inventory.bodySlots.Where(x => x.name == item.Key).First();

                var reference = new SpawnableCrateReference(item.Value);

                var spawnable = new Spawnable()
                {
                    crateRef = reference
                };

                AssetSpawner.Register(spawnable);

                var head = Player.playerHead;
                AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(null), false, new BoxedNullable<int>(null), (Action<GameObject>)OnSpawn);

                void OnSpawn(GameObject go)
                {
                    InteractableHost host;

                    if (go.GetComponent<InteractableHost>() != null)
                        host = go.GetComponent<InteractableHost>();
                    else
                        host = go.transform.GetComponentInChildren<InteractableHost>();

                    if (slotContainer.inventorySlotReceiver._slottedWeapon)
                        slotContainer.inventorySlotReceiver.DespawnContents();

                    slotContainer.inventorySlotReceiver.InsertInSlot(host);
                }
            }
        }

        private static void SendFusionSpawn(string barcode)
        {
            PooleeUtilities.RequestSpawn(barcode, new SerializedTransform(Player.playerHead.position + Player.playerHead.forward * 2.5f, Quaternion.identity), PlayerIdManager.LocalSmallId);
        }
    }
}
