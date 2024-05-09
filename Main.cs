using BoneLib;
using InventoryPersistence.Bonemenu;

using MelonLoader;

namespace InventoryPersistence
{
    internal partial class Main : MelonMod
    {
        internal static bool HasFusion;

        public override void OnLateInitializeMelon()
        {
            BoneMenuPreferences.InitializeBonemneu();

            Hooking.InitializeHooks();

            HasFusion = HelperMethods.CheckIfAssemblyLoaded("labfusion");
        }
    }
}
