using HarmonyLib;
using MelonLoader;
using BoneLib;
using Il2CppSLZ.Marrow;
using UnityEngine;

namespace NoVirtualCrouch
{
    public class NoVirtualCrouchMod : MelonMod
    {
    	public const string Title = "NoVirtualCrouch";
    	public const string Description = "A BoneLab mod that disables virtual crouching input.";
    	public const string Version = "1.1.0";

        private static BaseController GetController() => Player.RightController;

        [HarmonyPatch(typeof(OpenController), nameof(OpenController.GetThumbStickAxis))]
        private static class SuppressVirtualCrouch
        {
            private static void Postfix(OpenController __instance, ref Vector2 __result)
            {
                if (__instance == GetController())
                {
                	__result.y = 0f;
                }
            }
        }
    }
}
