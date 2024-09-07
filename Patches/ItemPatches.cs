using bbpfer.FundamentalManagers;
using System.Collections.Generic;
using HarmonyLib;
using System.Collections;
using UnityEngine;
using bbpfer.Enums;
using System.Linq;
using MTM101BaldAPI;
using bbpfer.CustomLoaders;
using System.Reflection.Emit;

namespace bbpfer.Patches
{

    [HarmonyPatch(typeof(NPC), nameof(NPC.SentToDetention))]
	internal class UnStuckInDetetion
	{
		static void Prefix(NPC __instance) => 
			__instance.Navigator.Entity.StartCoroutine(InDentetion(__instance));
		

		static IEnumerator InDentetion(NPC npc)
		{
			npc.Navigator.Entity.SetFrozen(true);
			float cooldown = 15f;
			while (cooldown > 0f)
			{
				cooldown -= npc.ec.EnvironmentTimeScale * Time.deltaTime;
				yield return null;
			}

			npc.Navigator.Entity.SetFrozen(false);
			yield break;
		}
	}

	[HarmonyPatch(typeof(ItemManager), nameof(ItemManager.RemoveItem))]
	internal class TapeUseful
	{
	}

	[HarmonyPatch(typeof(Principal), nameof(Principal.SendToDetention))]
	internal class FreeDetetion
	{
		static void Postfix()
		{
			if (Singleton<CoreGameManager>.Instance.GetPlayer(0).itm.Has(EnumExtensions.GetFromExtendedName<Items>(CustomItems.HallPass.ToString())))
			{
				foreach (RoomController office in Singleton<BaseGameManager>.Instance.Ec.offices)
					FundamentalCodingHelper.SetValue<float>(office.functionObject.GetComponent<DetentionRoomFunction>(), "time", 1);

				foreach (DetentionUi ui in GameObject.FindObjectsOfType<DetentionUi>())
					ui.gameObject.SetActive(false);

				foreach (RoomController office in Singleton<BaseGameManager>.Instance.Ec.offices)
				{
					foreach (Door door in office.doors)
						door.Unlock();
				}

				Singleton<CoreGameManager>.Instance.GetPlayer(0).itm.Remove(EnumExtensions.GetFromExtendedName<Items>(CustomItems.HallPass.ToString()));
			}
		}

		[HarmonyPatch(typeof(Bully), nameof(Bully.StealItem))]
		internal class BullyPresentPatch
		{
			static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
			{
				var code = new List<CodeInstruction>(instructions);
				var hasMethod = typeof(ItemManager).GetMethod("Has");
				var getFromExtendedNameMethod = typeof(EnumExtensions).GetMethod("GetFromExtendedName").MakeGenericMethod(typeof(Items));
				var removeMethod = typeof(ItemManager).GetMethod("Remove");

				for (int i = 0; i < code.Count; i++)
				{
					if (code[i].opcode == OpCodes.Call && code[i].operand.Equals(hasMethod))
					{
						code.Insert(i - 1, new CodeInstruction(OpCodes.Ldarg_1));
						code.Insert(i, new CodeInstruction(OpCodes.Call, hasMethod));

						code[i + 1] = new CodeInstruction(OpCodes.Ldarg_1);
						code[i + 2] = new CodeInstruction(OpCodes.Ldarg_1);
						code.Insert(i + 3, new CodeInstruction(OpCodes.Ldc_I4, (int)CustomItems.BullyPresent));
						code.Insert(i + 4, new CodeInstruction(OpCodes.Call, getFromExtendedNameMethod));
						code.Insert(i + 5, new CodeInstruction(OpCodes.Callvirt, removeMethod));
					}
				}
				return code.AsEnumerable();
			}
		}
	}
}
