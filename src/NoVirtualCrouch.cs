using HarmonyLib;
using MelonLoader;
using BoneLib;
using BoneLib.BoneMenu;
using Il2CppSLZ.Marrow;
using UnityEngine;

[assembly: MelonInfo(typeof(NoVirtualCrouch.Core), "NoVirtualCrouch", "1.0.0", "jorink")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace NoVirtualCrouch
{
    public class Core : MelonMod
    {
        private static MelonPreferences_Category category;
        private static MelonPreferences_Entry<bool> crouchEntry;

        private static BaseController GetController() => Player.RightController;

        public override void OnInitializeMelon()
        {
            SetupMelonPreferences();
            SetupBoneMenu();
        }

        private void SetupBoneMenu()
        {
            Page defaultPage = Page.Root.CreatePage("Jorink", Color.red).CreatePage("NoVirtualCrouch", Color.yellow);

            defaultPage.CreateBool("Virtual Crouch", Color.yellow, crouchEntry.Value, (value) => crouchEntry.Value = value);
            defaultPage.CreateFunction("Save Settings", Color.cyan, () => MelonPreferences.Save());
        }

        private void SetupMelonPreferences()
        {
            category = MelonPreferences.CreateCategory("NoVirtualCrouch");
            crouchEntry = category.CreateEntry("Virtual Crouch", true);
            MelonPreferences.Save();
            category.SaveToFile();
        }

        [HarmonyPatch(typeof(OpenController), nameof(OpenController.GetThumbStickAxis))]
        private static class SuppressVirtualCrouch
        {
            private static void Postfix(OpenController __instance, ref Vector2 __result)
            {
                if (!crouchEntry.Value && __instance == GetController())
                {
                    __result.y = 0f;
                }
            }
        }
    }
}
