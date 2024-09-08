using System;
using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI.ObjectCreation;
using bbpfer.CustomContent.CustomNPCs;
using bbpfer.Extessions;
using MTM101BaldAPI;
using bbpfer.CustomLoaders;
using System.IO;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
    public partial class NPCsCreator
    {
        public static void Load()
        {
            NPC kawa = new NPCBuilder<Kawa>(BasePlugin.instance.Info)
            .SetName("Kawa")
            .SetEnum(bbpfer.Enums.CustomNPCs.Kawa.ToString())
            .SetPoster("pri_kawa", "PST_PRI_Kawa1", "PST_PRI_Kawa2")
            .AddTrigger()
            .IgnoreBelts()
            .SetMinMaxAudioDistance(80, 100)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Faculty, RoomCategory.Special })
            .Build()
            .SetInitialSprite("Kawa", "KawaV2_Idle", 40, -1.1f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F1", "F2", "END" }, new int[] { 40, 70, 80 });

            NPC klip = new NPCBuilder<Klip>(BasePlugin.instance.Info)
            .SetName("Klip")
            .SetEnum(bbpfer.Enums.CustomNPCs.Klip.ToString())
            .SetPoster("pri_klip", "PST_PRI_Klip1", "PST_PRI_Klip2")
            .AddTrigger()
            .SetMinMaxAudioDistance(30, 60)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Hall, EnumExtensions.GetFromExtendedName<RoomCategory>(bbpfer.Enums.Rooms.Artistic.ToString()) })
            .AddLooker()
            .SetMaxSightDistance(500)
            .Build()
            .SetInitialSprite("Klip", "Klip1", 28, -1.83f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F1", "F2", "F3", "END" }, new int[] { 15, 25, 24, 35 });
            
            NPC checkupRobot = new NPCBuilder<CheckupRobot>(BasePlugin.instance.Info)
            .SetName("CheckupRobot")
            .SetEnum(bbpfer.Enums.CustomNPCs.CheckupRobot.ToString())
            .SetPoster("pri_checkupRobot", "PST_PRI_CR1", "PST_PRI_CR2")
            .AddTrigger()
            .SetMinMaxAudioDistance(90, 210)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Hall, RoomCategory.Faculty, RoomCategory.Special })
            .AddLooker()
            .SetMaxSightDistance(100)
            .Build()
            .SetInitialSprite("ChekupRobot", "CheckupRobot_0", 45, -1.28f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F2", "F3", "F4", "END" }, new int[] { 35, 45, 55, 74 });          

            /*
            NPC bonjur = new NPCBuilder<Bonjur>(BasePlugin.instance.Info)
            .SetName("Bonjur")
            .SetEnum(bbpfer.Enums.CustomNPCs.Bonjur.ToString())
            .SetPoster("pri_bonjur", "PST_PRI_Bonjur1", "PST_PRI_Bonjur2")
            .AddTrigger()
            .SetMinMaxAudioDistance(90, 210)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Hall })
            .Build()
            .SetInitialSprite("Bonjur", "bonjur_smile", 50, -2.83f)
            .CreateForcedNPC(new string[] { "F1", "END" });
            //.AddCustomData<CD_CheckupRobot>()
            //.CreateNPC(new string[] { "F2", "F3", "F4", "END" }, new int[] { 40, 50, 60, 75 });
            */

            NPC stellaLog = new NPCBuilder<StellaLog>(BasePlugin.instance.Info)
            .SetName("StellaLog")
            .SetMetaName("PST_PRI_StellaLog1")
            .SetEnum(bbpfer.Enums.CustomNPCs.StellaLog.ToString())
            .SetPoster("pri_stellaLog", "PST_PRI_StellaLog1", "PST_PRI_StellaLog2")
            .AddTrigger()
            .SetMinMaxAudioDistance(100, 200)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Hall, RoomCategory.Faculty })
            .DisableNavigationPrecision()
            .EnableAcceleration()
            .Build()
            .SetInitialSprite("StellaLog", "StellaLog", 37, -1.45f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 64, 23, 28, 38, 62 });

            NPC handleKlap = new NPCBuilder<HandleKlap>(BasePlugin.instance.Info)
            .SetName("HandleKlap")
            .SetEnum(bbpfer.Enums.CustomNPCs.HandleKlap.ToString())
            .SetPoster("pri_handleKlap", "PST_PRI_HandleKlap1", "PST_PRI_HandleKlap2")
            .AddTrigger()
            .SetMinMaxAudioDistance(90, 210)
            .AddLooker()
            .SetMaxSightDistance(540)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Hall, RoomCategory.Special })
            .Build()
            .SetInitialSprite("HandleKlap", "HandleKlap_Idle", 40, -1.258f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F1", "F2", "F3", "END" }, new int[] { 25, 52, 34, 64 });


            NPC turtiwille = new NPCBuilder<Turtiwille>(BasePlugin.instance.Info)
            .SetName("Turtiwille")
            .SetEnum(bbpfer.Enums.CustomNPCs.Turtiwille.ToString())
            .SetPoster("pri_turtiwille", "PST_PRI_Turtiwille1", "PST_PRI_Turtiwille2")
            .AddTrigger()
            .SetMinMaxAudioDistance(50, 100)
            .AddLooker()
            .AddHeatmap()
            .SetMaxSightDistance(float.MaxValue)
            .EnableAcceleration()
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Hall })
            .Build()
            .SetInitialSprite("Turtiwille", "Tortuguito_0", 30, -1.05f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F1", "F2", "F3", "END" }, new int[] { 14, 35, 12, 38 });
           
            NPC ballador = new NPCBuilder<Ballador>(BasePlugin.instance.Info)
            .SetName("Ballador")
            .SetEnum(bbpfer.Enums.CustomNPCs.Ballador.ToString())
            .SetPoster("pri_ballador", "PST_PRI_Ballador1", "PST_PRI_Ballador2")
            .AddTrigger()
            .EnableAcceleration()
            .SetMinMaxAudioDistance(44, 110)
            .AddSpawnableRoomCategories(new RoomCategory[] { RoomCategory.Class })
            .Build()
            .SetInitialSprite("Ballador", "Ballador", 80, -1.17f)
            .InitializeCustomData()
            //.CreateForcedNPC(new string[] { "F1", "END" });
            .CreateNPC(new string[] { "F2", "F3", "END" }, new int[] { 30, 33, 62 });

            NPC secretAdmirer = new NPCBuilder<SecretAdmirer>(BasePlugin.instance.Info)
            .SetName("Secret Admirer")
            .SetEnum(bbpfer.Enums.CustomNPCs.SecretAdmirer.ToString())
            .SetPoster("pri_secretadmierer", "PST_PRI_ScrAd1", "PST_PRI_ScrAd2")
            .AddTrigger()
            .AddLooker()
            .SetMaxSightDistance(440)
            .SetMinMaxAudioDistance(50, 120)
            .AddPotentialRoomAsset(AssetsCreator.Get<RoomAsset>("SecretAdmier1"), 100)
            .AddPotentialRoomAsset(AssetsCreator.Get<RoomAsset>("SecretAdmier2"), 65)
            .AddSpawnableRoomCategories(EnumExtensions.GetFromExtendedName<RoomCategory>(bbpfer.Enums.Rooms.AdmirerSecret.ToString()))
            .EnableAcceleration()
            .Build()
            .SetInitialSprite("SecretAdmirer", "SecretAdmirer_Idle", 50, -1f, 0)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F2", "F3", "END" }, new int[] { 50, 45, 74 });

            NPC dustPan = new NPCBuilder<DustPan>(BasePlugin.instance.Info)
            .SetName("Dust Pan")
            .SetEnum(bbpfer.Enums.CustomNPCs.DustPan.ToString())
            .SetPoster("pri_dustPan", "PST_PRI_DustPan1", "PST_PRI_DustPan2")
            .AddTrigger()
            .SetMinMaxAudioDistance(50, 120)
            .AddPotentialRoomAsset(AssetsCreator.Get<RoomAsset>("DustPan1"), 96)
            .AddPotentialRoomAsset(AssetsCreator.Get<RoomAsset>("DustPan2"), 100)
            .AddSpawnableRoomCategories(EnumExtensions.GetFromExtendedName<RoomCategory>(bbpfer.Enums.Rooms.DustPan.ToString()))
            .Build()
            .SetInitialSprite("DustPan", "DustPan_0", 20, -0.2f)
            .InitializeCustomData()
            .CreateNPC(new string[] { "F2", "F4", "END" }, new int[] { 50, 75, 62 });
        }
    }

    public static partial class npcExtessions
    {
        public static NPCBuilder<T> SetPoster<T>(this NPCBuilder<T> builder, string texture, string nameKey, string descKey) where T : NPC
        {
            builder.SetPoster(ObjectCreators.CreateCharacterPoster(AssetsCreator.CreateTexture(texture, "Posters"), nameKey, descKey));
            
            builder.SetMetaName(nameKey);
            return builder;
        }

        public static NPC SetInitialSprite(this NPC npc, string folder, string initialSpriteName, int pixelPorUnit, float spriteHeight, int posterTextReduce = 0)
        {
            AssetsCreator.CreateSprites(pixelPorUnit, Path.Combine("Characters", folder));
            npc.spriteRenderer[0].sprite = AssetsCreator.Get<Sprite>(initialSpriteName);
            npc.spriteRenderer[0].transform.position = new Vector3(0, spriteHeight, 0);
            npc.Poster.textData[1].fontSize -= posterTextReduce;
            return npc;
        }

        public static NPC CreateNPC(this NPC npc, string[] floors, int[] weights)
        {
            int weight = 0;
            foreach (string data in floors)
            {
                FundamentalManager.GetFloorByName(data).npcs.Add(new WeightedNPC { selection = npc, weight = weights[weight] });
                weight++;
            }

            return npc;
        }

        public static NPC CreateForcedNPC(this NPC npc, string[] floors)
        {
            foreach (string data in floors)           
                FundamentalManager.GetFloorByName(data).forcedNPCs.Add(npc);          

            return npc;
        }

        public static NPC InitializeCustomData(this NPC npc)
        {
            if (npc.GetComponent<CustomDataNPC>() == null)
            {
                Debug.LogError(npc.name);
                return npc;
            }

            var _npc = npc.GetComponent<CustomDataNPC>();
            _npc.Setup();

            npc.Navigator.npc = npc;
            npc.Navigator.ec = npc.ec;
            npc.spriteRenderer[0].ResizeCollider(npc.baseTrigger[0]);
            return npc;
        }
    }
}
