using InventoryPersistence.Bonemenu;

using MelonLoader;

namespace InventoryPersistence
{
    internal partial class Main : MelonMod
    {
        public override void OnLateInitializeMelon()
        {
            BoneMenuPreferences.InitializeBonemneu();

            Hooking.InitializeHooks();
        }
    }
}
