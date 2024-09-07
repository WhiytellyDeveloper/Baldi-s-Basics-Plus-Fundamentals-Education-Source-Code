using System;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.Registers;
using bbpfer.FundamentalManagers;
using System.Collections.Generic;
using UnityEngine;
using bbpfer.Enums;
using bbpfer.Extessions;
using System.Linq;

namespace bbpfer
{
    [BepInDependency("mtm101.rulerp.bbplus.baldidevapi", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pixelguy.pixelmodding.baldiplus.pixelinternalapi", BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModInfo.streamingPath, ModInfo.name, ModInfo.version)]
    public class BasePlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            instance = this;
            Harmony harmony = new Harmony(ModInfo.streamingPath);
            harmony.PatchAll();

            LoadingEvents.RegisterOnAssetsLoaded(Info, FundamentalManager.LoadAll(), false);
            LoadingEvents.RegisterOnAssetsLoaded(Info, PostLoadAssets, true);

            GeneratorManagement.Register(this, GenerationModType.Base, (floorName, floorNum, ld) =>
            {
                if (f1 == null)
                f1 = FundamentalCodingHelper.FindResourceObjectContainingName<LevelObject>("Main1");
                switch (floorName)
                {
                    case "F1":
                        f1 = ld;
                        ld.roomGroup.First(x => x.name == "Class").maxRooms = 5;
                        ld.roomGroup.First(x => x.name == "Class").minRooms = 5;
                        ld.roomGroup.First(x => x.name == "Faculty").minRooms += 1;
                        ld.roomGroup.First(x => x.name == "Faculty").maxRooms += 3;
                        ld.maxEvents += 1;
                        ld.minSpecialBuilders += 1;
                        ld.maxSpecialBuilders += 2;
                        ld.additionalNPCs += 3;
                        List<WeightedNPC> npcsToRemove = new List<WeightedNPC>();

                        foreach (WeightedNPC npc in ld.potentialNPCs)
                        {
                            if (npc.selection.Character == Character.Sweep)                            
                                npcsToRemove.Add(npc);                            
                        }

                        foreach (WeightedNPC npc in npcsToRemove)                       
                            ld.potentialNPCs.Remove(npc);
                      
                        ld.forcedNpcs = ld.forcedNpcs.AddItem(NPCMetaStorage.Instance.Get(Character.Sweep).value).ToArray();

                        break;
                    case "F2":
                        ld.maxExtraRooms += 1;
                        ld.roomGroup.First(x => x.name == "Faculty").minRooms += 2;
                        ld.roomGroup.First(x => x.name == "Faculty").maxRooms += 5;
                        ld.minEvents += 1;
                        ld.maxEvents += 2;
                        ld.minSpecialBuilders += 2;
                        ld.maxSpecialBuilders += 3;
                        ld.additionalNPCs += 5;

                        List<WeightedNPC> _npcsToRemove = new List<WeightedNPC>();

                        foreach (WeightedNPC npc in ld.potentialNPCs)
                        {
                            if (npc.selection.Character == Character.Sweep || npc.selection.Character == Character.DrReflex)
                                _npcsToRemove.Add(npc);
                        }

                        foreach (WeightedNPC npc in _npcsToRemove)
                            ld.potentialNPCs.Remove(npc);

                        ld.forcedNpcs = ld.forcedNpcs.AddItem(NPCMetaStorage.Instance.Get(Character.DrReflex).value).ToArray();

                        ld.roomGroup.First(x => x.name == "Class").stickToHallChance = 0.8f;
                        foreach (WeightedRoomAsset classRoom in f1.roomGroup.First(x => x.name == "Class").potentialRooms)
                        {
                            ld.roomGroup.First(x => x.name == "Class").potentialRooms = HarmonyLib.CollectionExtensions.AddItem<WeightedRoomAsset>(ld.roomGroup.First(x => x.name == "Class").potentialRooms, new WeightedRoomAsset
                            {
                                selection = classRoom.selection,
                                weight = classRoom.weight / 2
                            }).ToArray();
                        }
                        break;
                    case "F3":
                        ld.minExtraRooms += 1;
                        ld.maxExtraRooms += 2;
                        ld.roomGroup.First(x => x.name == "Faculty").minRooms += 1;
                        ld.roomGroup.First(x => x.name == "Faculty").maxRooms += 3;
                        ld.facultyStickToHallChance += 0.75f;
                        ld.minEvents += 3;
                        ld.maxEvents += 4;
                        ld.minSpecialBuilders += 4;
                        ld.maxSpecialBuilders += 5;
                        ld.additionalNPCs += 9;

                        List<WeightedNPC> __npcsToRemove = new List<WeightedNPC>();

                        foreach (WeightedNPC npc in ld.potentialNPCs)
                        {
                            if (npc.selection.Character == Character.Sweep || npc.selection.Character == Character.DrReflex)
                                __npcsToRemove.Add(npc);
                        }

                        foreach (WeightedNPC npc in __npcsToRemove)
                            ld.potentialNPCs.Remove(npc);

                        ld.roomGroup.First(x => x.name == "Class").stickToHallChance = 0.8f;
                        foreach (WeightedRoomAsset classRoom in f1.roomGroup.First(x => x.name == "Class").potentialRooms)
                        {
                            ld.roomGroup.First(x => x.name == "Class").potentialRooms = HarmonyLib.CollectionExtensions.AddItem<WeightedRoomAsset>(ld.roomGroup.First(x => x.name == "Class").potentialRooms, new WeightedRoomAsset
                            {
                                selection = classRoom.selection,
                                weight = classRoom.weight / 2
                            }).ToArray();
                        }

                        ld.exitCount = 3;
                        ld.roomGroup.First(x => x.name == "Class").stickToHallChance = 0.5f;
                        foreach (WeightedRoomAsset classRoom in f1.roomGroup.First(x => x.name == "Class").potentialRooms)
                        {
                            ld.roomGroup.First(x => x.name == "Class").potentialRooms = HarmonyLib.CollectionExtensions.AddItem<WeightedRoomAsset>(ld.roomGroup.First(x => x.name == "Class").potentialRooms, new WeightedRoomAsset
                            {
                                selection = classRoom.selection,
                                weight = classRoom.weight / 3
                            }).ToArray();
                        }
                        break;
                    case "END":
                        ld.maxExtraRooms += 1;
                        ld.roomGroup.First(x => x.name == "Faculty").minRooms += 3;
                        ld.roomGroup.First(x => x.name == "Faculty").maxRooms += 5;
                        ld.minEvents += 2;
                        ld.maxEvents += 5;
                        ld.minSpecialBuilders += 2;
                        ld.maxSpecialBuilders += 5;
                        ld.additionalNPCs += 5;
                        ld.roomGroup.First(x => x.name == "Class").stickToHallChance = 0.6f;
                        foreach (WeightedRoomAsset classRoom in f1.roomGroup.First(x => x.name == "Class").potentialRooms)
                        {
                            ld.roomGroup.First(x => x.name == "Class").potentialRooms = HarmonyLib.CollectionExtensions.AddItem<WeightedRoomAsset>(ld.roomGroup.First(x => x.name == "Class").potentialRooms, new WeightedRoomAsset
                            {
                                selection = classRoom.selection,
                                weight = classRoom.weight / 2
                            }).ToArray();
                        }
                        break;
                }

                foreach (FloorData floorData in FundamentalManager.floors)
                {
                    if (FundamentalManager.GetFloorByName(floorName) == floorData)
                    {
                        ld.hallWallTexs = ld.hallWallTexs.AddRangeToArray(floorData.wallHallTextures.ToArray());
                        ld.hallFloorTexs = ld.hallFloorTexs.AddRangeToArray(floorData.floorHallTextures.ToArray());
                        ld.hallCeilingTexs = ld.hallCeilingTexs.AddRangeToArray(floorData.ceilingHallTextures.ToArray());

                        ld.roomGroup.First(x => x.name == "Class").wallTexture = ld.roomGroup.First(x => x.name == "Class").wallTexture.AddRangeToArray(floorData.wallClassTextures.ToArray());
                        ld.roomGroup.First(x => x.name == "Class").floorTexture = ld.roomGroup.First(x => x.name == "Class").floorTexture.AddRangeToArray(floorData.floorClassTextures.ToArray());
                        ld.roomGroup.First(x => x.name == "Class").ceilingTexture = ld.roomGroup.First(x => x.name == "Class").ceilingTexture.AddRangeToArray(floorData.ceilingClassTextures.ToArray());

                        ld.roomGroup.First(x => x.name == "Faculty").wallTexture = ld.roomGroup.First(x => x.name == "Faculty").wallTexture.AddRangeToArray(floorData.wallFacultyTextures.ToArray());
                        ld.roomGroup.First(x => x.name == "Faculty").floorTexture = ld.roomGroup.First(x => x.name == "Faculty").floorTexture.AddRangeToArray(floorData.floorFacultyTextures.ToArray());
                        ld.roomGroup.First(x => x.name == "Faculty").ceilingTexture = ld.roomGroup.First(x => x.name == "Faculty").ceilingTexture.AddRangeToArray(floorData.ceilingFacultyTextures.ToArray());

                        ld.roomGroup.First(x => x.name == "Office").wallTexture = ld.roomGroup.First(x => x.name == "Office").wallTexture.AddRangeToArray(floorData.wallFacultyTextures.ToArray());
                        ld.roomGroup.First(x => x.name == "Office").floorTexture = ld.roomGroup.First(x => x.name == "Office").floorTexture.AddRangeToArray(floorData.floorFacultyTextures.ToArray());
                        ld.roomGroup.First(x => x.name == "Office").ceilingTexture = ld.roomGroup.First(x => x.name == "Office").ceilingTexture.AddRangeToArray(floorData.ceilingFacultyTextures.ToArray());

                        ld.roomGroup.First(x => x.name == "Faculty").potentialRooms = ld.roomGroup.First(x => x.name == "Faculty").potentialRooms.AddRangeToArray(floorData.customsFacultyrooms.ToArray());
                        ld.potentialExtraRooms = ld.potentialExtraRooms.AddRangeToArray(floorData.customsExtrarooms.ToArray());


                        ld.specialHallBuilders = ld.specialHallBuilders.AddRangeToArray(floorData.specialObjectBuilders.ToArray());
                        ld.forcedSpecialHallBuilders = ld.forcedSpecialHallBuilders.AddRangeToArray(floorData.forcedObjectBuilders.ToArray());

                        ld.roomGroup = ld.roomGroup.AddRangeToArray(floorData.roomGroups.ToArray());

                        ld.randomEvents.AddRange(floorData.randomEvents);

                        ld.potentialItems = ld.potentialItems.AddRangeToArray(floorData.items.ToArray());
                        ld.forcedItems.AddRange(floorData.forcedItems);

                        ld.potentialNPCs.AddRange(floorData.npcs);
                        ld.forcedNpcs = ld.forcedNpcs.AddRangeToArray(floorData.forcedNPCs.ToArray());

                        ld.posters = ld.posters.AddRangeToArray(floorData.posters.ToArray());
                    }
                }
            });
        }

        public void PostLoadAssets()
        {
            NPCMetaStorage.Instance.Get(Character.Crafters).value.spawnableRooms.Add(EnumExtensions.GetFromExtendedName<RoomCategory>(Rooms.Artistic.ToString()));

            foreach (RoomAsset room in FundamentalCodingHelper.FindResourceObjects<RoomAsset>())
            {
                switch (room.category)
                {
                    case RoomCategory.Store:
                        room.ceilTex = AssetsCreator.CreateTexture("concreteceiling", "CellTextures");
                        room.florTex = AssetsCreator.Get<Texture2D>("GenericWoodFloor2_New");
                        break;
                    case RoomCategory.Null:
                        if (room.name.Contains("reflex") || room.name.Contains("Reflex"))
                        {
                            room.wallTex = AssetsCreator.CreateTexture("DrReflexNotFoundWall", "CellTextures");
                            room.florTex = AssetsCreator.CreateTexture("Calpert_DrReflex", "CellTextures");
                            room.ceilTex = AssetsCreator.assetMan.Get<Texture2D>("NyanCeiling");
                            room.doorMats = ObjectCreators.CreateDoorDataObject("ReflexDoor", AssetsCreator.CreateTexture("ClincDoorStandard_Open", "Doors"), AssetsCreator.CreateTexture("ClincDoorStandard_Closed", "Doors"));
                        }
                            break;
                    case RoomCategory.Special:
                        if (room.name.Contains("Cafeteria_"))
                        {
                            room.wallTex = AssetsCreator.CreateTexture("BuluerAnimatedBrick", "CellTextures");
                            room.florTex = FundamentalCodingHelper.FindResourceObjectContainingName<Texture2D>("BasicFloor");
                            room.ceilTex = AssetsCreator.assetMan.Get<Texture2D>("NyanCeiling");
                            room.basicSwaps[0].potentialReplacements = HarmonyLib.CollectionExtensions.AddItem<WeightedTransform>(room.basicSwaps[0].potentialReplacements, new WeightedTransform { selection = AssetsCreator.assetMan.Get<SodaMachine>("sodaMachine").transform, weight = 80 }).ToArray();
                            room.basicSwaps[0].potentialReplacements = HarmonyLib.CollectionExtensions.AddItem<WeightedTransform>(room.basicSwaps[0].potentialReplacements, new WeightedTransform { selection = AssetsCreator.assetMan.Get<SodaMachine>("cookieMachine").transform, weight = 110 }).ToArray();
                            room.basicSwaps[0].potentialReplacements = HarmonyLib.CollectionExtensions.AddItem<WeightedTransform>(room.basicSwaps[0].potentialReplacements, new WeightedTransform { selection = AssetsCreator.assetMan.Get<SodaMachine>("genericSodaMachine").transform, weight = 100 }).ToArray();
                            room.basicSwaps[0].potentialReplacements = HarmonyLib.CollectionExtensions.AddItem<WeightedTransform>(room.basicSwaps[0].potentialReplacements, new WeightedTransform { selection = AssetsCreator.assetMan.Get<SodaMachine>("iceCreamMachine").transform, weight = 75 }).ToArray();
                            room.basicSwaps[0].potentialReplacements = HarmonyLib.CollectionExtensions.AddItem<WeightedTransform>(room.basicSwaps[0].potentialReplacements, new WeightedTransform { selection = AssetsCreator.assetMan.Get<SodaMachine>("teaMachine").transform, weight = 78 }).ToArray();

                            break;
                        }
                        break;

                }
            }
            foreach (SceneObject scene in FundamentalCodingHelper.FindResourceObjects<SceneObject>())
            {
                switch (scene.levelTitle)
                {
                    case "PIT":
                        scene.levelAsset.posters[0].poster = Posters.exitPoster;
                        scene.levelAsset.rooms[0].wallTex = AssetsCreator.Get<Texture2D>("WlwightAnimatedBrick");
                        scene.levelAsset.rooms[0].florTex = AssetsCreator.Get<Texture2D>("AspargoFloor2");
                        break;
                }          

                foreach (FloorData floorData in FundamentalManager.floors)
                {
                    if (FundamentalManager.GetFloorByName(scene.levelTitle) == floorData)                   
                        scene.shopItems = scene.shopItems.AddRangeToArray(floorData.shopItems.ToArray());                
                }
            }
            foreach (ItemObject item in FundamentalCodingHelper.FindResourceObjects<ItemObject>())
            {
                switch (item.itemType)
                {
                    case Items.DietBsoda:
                        item.item.GetComponentInChildren<Transform>().GetComponentInChildren<SpriteRenderer>().sprite = AssetsCreator.CreateSprite("DietBsodaSprite", "Items", 12);
                        FundamentalCodingHelper.SetValue<int>(item.item.GetComponentInChildren<ITM_BSODA>(), "time", 5);
                        FundamentalCodingHelper.SetValue<float>(item.item.GetComponentInChildren<ITM_BSODA>(), "speed", 40);
                        break;
                }
            }
        }

        public static BepInEx.BaseUnityPlugin instance;
        public bool Loaded;
        protected LevelObject f1;
    }

    public class ModInfo
    {
        public const string streamingPath = "whiytellydeveloper.mod.baldiplus.fundamentalseducation";
        public const string name = "Baldi's basics plus fundamentals education";
        public const string version = "0.1";
    }
}
