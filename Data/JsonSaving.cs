using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MelonLoader;

using Newtonsoft.Json;

namespace InventoryPersistence.Data
{
    public static class JsonSaving
    {
        public static readonly string SavePath = $"{MelonUtils.UserDataDirectory}/InventoryPersistenceData.json";

        public static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            CheckAdditionalContent = true,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        internal static void SaveInventory(PlayerInventory inventory)
        {
            string jsonData = JsonConvert.SerializeObject(inventory, SerializationSettings);

            File.WriteAllText(SavePath, jsonData);
        }

        public static PlayerInventory LoadInventory()
        {
            if (!File.Exists(SavePath))
                return null;
            
            string jsonData = File.ReadAllText(SavePath);

            PlayerInventory inventory = JsonConvert.DeserializeObject<PlayerInventory>(jsonData, SerializationSettings);

            return inventory;
        }
    }
}
