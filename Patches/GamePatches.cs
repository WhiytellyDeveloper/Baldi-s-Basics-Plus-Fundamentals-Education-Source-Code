using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using bbpfer.FundamentalManagers;
using System.Linq;
using System.Reflection.Emit;

namespace bbpfer.Patches
{
	[HarmonyPatch(typeof(BaseGameManager), "Initialize")]
	internal class BaseGameManagerPatches
	{
		static void Prefix() =>
			new GameObject("InGameManager").AddComponent<InGameManager>();
		
	}

	
	[HarmonyPatch(typeof(BaseGameManager), nameof(BaseGameManager.LoadNextLevel))]
	internal class BaseGameManagerPatchesReset
	{
		static void Prefix() =>
			Singleton<InGameManager>.Instance.Reset();
	}

	/*
	[HarmonyPatch(typeof(BaseGameManager), nameof(BaseGameManager.CollectNotebook))]
	internal class BaseGameManagerPatchesNotebooksCollect
	{
		static void Postfix() =>
			Singleton<InGameManager>.Instance.notebooks++;

	}
	*/

	[HarmonyPatch(typeof(RoomFunction), nameof(RoomFunction.OnPlayerEnter))]
	internal class MapFix
	{
		static void Prefix(RoomFunction __instance)
		{
			for (int i = 0; i < __instance.Room.cells.Count; i++)
				if (!__instance.Room.ec.cells[__instance.Room.cells[i].position.x, __instance.Room.cells[i].position.z].hideFromMap && !__instance.Room.ec.map.foundTiles[__instance.Room.cells[i].position.x, __instance.Room.cells[i].position.z])
				__instance.Room.ec.map.Find(__instance.Room.cells[i].position.x, __instance.Room.cells[i].position.z, __instance.Room.cells[i].ConstBin, __instance.Room);
		}
	}

	[HarmonyPatch(typeof(PitstopGameManager), nameof(PitstopGameManager.BeginPlay))]
	internal class PitStopMusic
	{
		static void Prefix()
		{
			Singleton<MusicManager>.Instance.PlayMidi(AssetsCreator.LoadMidi("PitStop"), true);
			Singleton<MusicManager>.Instance.SetSpeed(0.75f);
		}
	}

	[HarmonyPatch(typeof(HappyBaldi), "SpawnWait", MethodType.Enumerator)]
	internal class ChangeCount
	{
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			bool changedCount = false;
			using (var enumerator = instructions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					var instruction = enumerator.Current;
					if (!changedCount && instruction.Is(OpCodes.Ldc_I4_S, 9))
					{
						changedCount = true;
						instruction.operand = 0;
					}
					yield return instruction;
				}
			}
		}

		[HarmonyPatch(typeof(Notebook), "Start")]
		internal class NotebooksColor
		{
			static void Postfix(Notebook __instance)
			{
				List<Sprite> sprites = AssetsCreator.CreateSprites(100, "Notebooks", false).ToList();
				sprites.RemoveAt(0);

				FundamentalCodingHelper.GetVariable<SpriteRenderer>(__instance, "sprite").sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
			}
		}

		[HarmonyPatch(typeof(Map), "Update")]
		internal class FullMinimapInSmallMinimap
		{
			static void Postfix(Notebook __instance) {
				Shader.EnableKeyword("_KEYMAPSHOWBACKGROUND");
			}
		}
	}
}
