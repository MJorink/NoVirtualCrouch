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
		public const string Version = "2.0.0";

		private static MelonPreferences_Entry<bool> virtualCrouch;

		public override void OnInitializeMelon()
		{
			var menu = JLib.Register("NoVirtualCrouch", Color.blue);
			
			virtualCrouch = menu.Bool("Virtual Crouch", false, Color.green);
		}

		[HarmonyPatch(typeof(OpenControllerRig), nameof(OpenControllerRig.ProcessSecondaryThumbstick))]
		private static class SuppressVirtualCrouch
		{
			private static void Prefix(ref Vector2 axis)
			{
				if (virtualCrouch.Value == false)
				{
					axis.y = 0f;
				}
			}
		}
	}
}
