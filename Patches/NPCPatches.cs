using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HarmonyLib;
using bbpfer.CustomLoaders;

namespace bbpfer.Patches
{

    [HarmonyPatch(typeof(NPC), nameof(NPC.Initialize))]
    public static class NPCIntializerData
    {
        public static void Prefix(NPC __instance)
        {
            if (__instance.GetComponent<CustomDataNPC>() != null)
                __instance.GetComponent<CustomDataNPC>().InGameSetup();
        }
    }

    [HarmonyPatch(typeof(BeltManager), "OnTriggerEnter")]
    public static class BeltFix
    {
        public static bool Prefix(BeltManager __instance, Collider other)
        {
            NPC npc = other.GetComponent<NPC>();
            if (npc != null)
            {
                if (npc.Navigator.Speed > __instance.Speed)
                    return false;
            }

            return true;
        }
    }
}
