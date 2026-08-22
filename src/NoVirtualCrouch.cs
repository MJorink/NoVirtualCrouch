using HarmonyLib;
using MelonLoader;
using BoneLib;
using Il2CppSLZ.Marrow;
using UnityEngine;

[assembly: MelonInfo(typeof(NoVirtualCrouch.Core), "NoVirtualCrouch", "1.0.0", "jorink")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace NoVirtualCrouch
{
    public class Core : MelonMod
    {
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
