using System;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI;
using MTM101BaldAPI.Registers;
using bbpfer.FundamentalManagers;
using System.Collections.Generic;
using UnityEngine;
using bbpfer.Enums;
using bbpfer.Extessions;
using BaldiLevelEditor;
using PlusLevelLoader;
using PlusLevelFormat;
using System.IO;
using System.Linq;
using UnityEngine.Rendering;

namespace bbpfer.ModsCompabilty.Editor
{
    [BepInDependency(ModInfo.streamingPath, BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin(ModInfo.streamingPath + ".editor", ModInfo.name + " editor", ModInfo.version)]
    public class EditorBasePlugin : BaseUnityPlugin
    {
        public void Awake()
        {
            AssetsCreator.CreateSprites(40, Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Textures", "LevelEditor", "NPCs"));
            AssetsCreator.CreateSprites(40, Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Textures", "LevelEditor", "Rooms"));
            AssetsCreator.CreateSprites(40, Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Textures", "LevelEditor", "Objects"));

            LoadingEvents.RegisterOnAssetsLoaded(BasePlugin.instance.Info, PostLoad, true);
        }

        public void PostLoad()
        {
            AddRoom("Artistic");
            AddRoom("Storage");
            AddRoom("Placeholder");
            AddRoom("AdmirerSecretRoom");
            AddRoom("Security");
            AddRoom("DustPan");

            AddNpc("kawa", CustomNPCs.Kawa);
            AddNpc("klip", CustomNPCs.Klip);
            AddNpc("checkupbot", CustomNPCs.CheckupRobot);
            AddNpc("stellalog", CustomNPCs.StellaLog);
            AddNpc("handleklap", CustomNPCs.HandleKlap);
            AddNpc("turtiwille", CustomNPCs.Turtiwille);
            AddNpc("ballador", CustomNPCs.Ballador);
            AddNpc("SCR", CustomNPCs.SecretAdmirer);
            AddNpc("dustpan", CustomNPCs.DustPan);

            AddItem(CustomItems.CommonTeleporter);
            AddItem(CustomItems.CoffeAndSugar);
            AddItem(CustomItems.GenericHammer);
            AddItem(CustomItems.IceCream, true);
            AddItem(CustomItems.Soda);
            AddItem(CustomItems.Cookie, true);
            AddItem(CustomItems.GPS);
            AddItem(CustomItems.SweepWhistle);
            AddItem(CustomItems.BanHammer);
            AddItem(CustomItems.PlasticPercussionHammer);
            AddItem(CustomItems.PercussionHammer);
            AddItem(CustomItems.Pretzel);
            AddItem(CustomItems.GenericSoda);
            AddItem(CustomItems.Glue);
            AddItem(CustomItems.Gum);
            AddItem(CustomItems.DietGum);
            AddItem(CustomItems.Present);
            AddItem(CustomItems.BullyPresent);
            AddItem(CustomItems.AdvertenceBook);
            AddItem(CustomItems.ConnectedAddvertenceBook);
            AddItem(CustomItems.IceCreamMask);
            AddItem(CustomItems.Tea);
            AddItem(CustomItems.DietTea);
            AddItem(CustomItems.Cheese);
            AddItem(CustomItems.Horn);
            AddItem(CustomItems.EntityTeleporter);
            AddItem(CustomItems.Potion);
            AddItem(CustomItems.Bag);
            AddItem(CustomItems.Shovel);
            AddItem(CustomItems.HallPass);
            AddItem(CustomItems.WhiteZesty);
            AddItem(CustomItems.SoupInCan);
            AddItem(CustomItems.DuctTape);
        }

        public void AddRoom(string room) =>
            rooms.Add(new CustomRoomEditor(room));

        public void AddNpc(string npc, CustomNPCs npcObj)
        {
            npcs.Add(new CustomNPCEditor(npc));
            NPC NPC = NPCMetaStorage.Instance.Get(EnumExtensions.GetFromExtendedName<Character>(npcObj.ToString())).value;
            BaldiLevelEditorPlugin.characterObjects.Add(npc, BaldiLevelEditorPlugin.StripAllScripts(NPC.gameObject));
            PlusLevelLoaderPlugin.Instance.npcAliases.Add(npc, NPC);
        }

        public void AddItem(CustomItems item, bool maxUse = false)
        {
            items.Add(new CustomItemEditor(item));

            if (!maxUse)
            {
                BaldiLevelEditorPlugin.itemObjects.Add(item.ToString(), ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(item.ToString())).value);
                PlusLevelLoaderPlugin.Instance.itemObjects.Add(item.ToString(), ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(item.ToString())).value);
            }
            else
            {
                Items itemEnum = EnumExtensions.GetFromExtendedName<Items>(item.ToString());
                ItemObject _item = null;

                foreach (var itemObject in ItemMetaStorage.Instance.FindByEnum(itemEnum).itemObjects)
                {
                    if (ItemMetaStorage.Instance.FindByEnum(itemEnum).tags.Contains("maxItemUse"))
                    {
                        _item = ItemMetaStorage.Instance.FindByEnum(itemEnum).value;
                        break;
                    }
                }

                BaldiLevelEditorPlugin.itemObjects.Add(item.ToString(), _item);
                PlusLevelLoaderPlugin.Instance.itemObjects.Add(item.ToString(), _item);


            }
        }

        public static List<CustomNPCEditor> npcs = new List<CustomNPCEditor>();
        public static List<CustomItemEditor> items = new List<CustomItemEditor>();
        public static List<CustomRoomEditor> rooms = new List<CustomRoomEditor>();


        [HarmonyPatch(typeof(PlusLevelEditor), "Initialize")]
        internal class LevelEditorPatch
        {
            private static void Postfix(PlusLevelEditor __instance)
            {
                __instance.toolCats.Find((ToolCategory x) => x.name == "halls").tools.AddRange(rooms);
                __instance.toolCats.Find((ToolCategory x) => x.name == "characters").tools.AddRange(npcs);
                __instance.toolCats.Find((ToolCategory x) => x.name == "items").tools.AddRange(items);

                __instance.toolCats.Find((ToolCategory x) => x.name == "objects").tools.AddRange(new List<EditorTool>(new EditorTool[]
                {
                    new CustomRotateAndPlaceEditor("Painting"),
                    new CustomRotateAndPlaceEditor("Piano"),
                    new CustomRotateAndPlaceEditor("Pedestal"),
                    new CustomRotateAndPlaceEditor("TrashBag")
                }));

            }
        }

        [HarmonyPatch(typeof(EditorLevel), "InitializeDefaultTextures")]
        internal class DefaulTextureInRooms
        {
            private static void Postfix(EditorLevel __instance)
            {
                __instance.defaultTextures.Add("Artistic", new TextureContainer { wall = "ArtWall", floor = "ArtRoomCarpet", ceiling = "FancyCeiling" });
                __instance.defaultTextures.Add("Security", new TextureContainer { wall = "SecurityWall", floor = "DiamongPlateFloor", ceiling = "SecurityCeiling" });
                __instance.defaultTextures.Add("Placeholder", new TextureContainer { wall = "PlaceholderWall", floor = "PlaceholderFloor", ceiling = "PlaceholderCeiling" });
                __instance.defaultTextures.Add("Storage", new TextureContainer { wall = "StorageWall", floor = "StorageFloor", ceiling = "StorageCeiling" });
                __instance.defaultTextures.Add("AdmirerSecretRoom", new TextureContainer { wall = "SecretAdmirerWall", floor = "SecretAdmirerFloor", ceiling = "SecretAdmirerCeiling" });
                __instance.defaultTextures.Add("DustPan", new TextureContainer { wall = "DustPanWall", floor = "DustPanFloor", ceiling = "DustPanCeiling" });
            }
        }

        public class CustomNPCEditor : NpcTool
        {
            string obj;
            public CustomNPCEditor(string obj) : base(obj) =>
                this.obj = obj;

            public override Sprite editorSprite {
                get { return AssetsCreator.Get<Sprite>("npc_" + obj); }
            }
        }

        public class CustomItemEditor : ItemTool
        {
            CustomItems obj;
            public CustomItemEditor(CustomItems obj) : base(obj.ToString()) =>
                this.obj = obj;

            public override Sprite editorSprite {
                get { return ItemMetaStorage.Instance.FindByEnum(EnumExtensions.GetFromExtendedName<Items>(obj.ToString())).value.itemSpriteSmall; }
            }
        }

        public class CustomRoomEditor : FloorTool
        {
            string obj;
            public override Sprite editorSprite {
                get { return AssetsCreator.Get<Sprite>("Floor_" + obj); }
            }

            public CustomRoomEditor(string obj) : base(obj) =>
                this.obj = obj;

        }
    }
}
