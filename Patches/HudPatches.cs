using System;
using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using bbpfer.FundamentalManagers;
using System.Linq;
using System.Reflection.Emit;

namespace bbpfer.Patches
{
	[HarmonyPatch(typeof(BaseGameManager), nameof(BaseGameManager.BeginPlay))]
	internal class MoreItemSlots
	{
		static void Postfix()
        {
			Singleton<CoreGameManager>.Instance.GetHud(0).UpdateInventorySize(6);
			Singleton<CoreGameManager>.Instance.GetPlayer(0).itm.maxItem = 5;
		}
			
	}
}
