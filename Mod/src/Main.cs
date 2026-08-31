using HarmonyLib;
using MelonLoader;
using BoneLib;
using Il2CppSLZ.Marrow;
using UnityEngine;
using jlib;

namespace novirtualcrouch
{
	public class NoVirtualCrouch : MelonMod
	{
		public const string Version = "1.1.0";

		private static MelonPreferences_Entry<bool> virtualCrouch;

		public override void OnInitializeMelon()
		{
			var menu = JLib.Register("NoVirtualCrouch", Color.blue);
			
			virtualCrouch = menu.Bool("Virtual Crouch", false, Color.green);
		}

		[HarmonyPatch(typeof(OpenController), nameof(OpenController.GetThumbStickAxis))]
		private static class SuppressVirtualCrouch
		{
			private static void Postfix(OpenController __instance, ref Vector2 __result)
			{
				if (!virtualCrouch.Value && __instance == Player.RightController)
				{
					__result.y = 0f;
				}
			}
		}
	}
}
