using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using EditorCustomRooms;
using System.IO;
using PlusLevelLoader;
using bbpfer.Enums;
using bbpfer.CustomRooms.CustomObjects;
using bbpfer.CustomRooms.Functions;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;
using bbpfer.Extessions;
using bbpfer.FundamentalManagers;
using System.Linq;
using MTM101BaldAPI.Registers;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
    public partial class RoomLoader
    {
        public static void Load()
        {
            Color artColor = new Color();
            ColorUtility.TryParseHtmlString("#5946AF", out artColor);

            PlusLevelLoaderPlugin.Instance.roomSettings.Add("Artistic", new RoomSettings(
                EnumExtensions.ExtendEnum<RoomCategory>(Rooms.Artistic.ToString()),
                RoomType.Room,
                artColor,
                ObjectCreators.CreateDoorDataObject("ArtisticDoor", AssetsCreator.CreateTexture("ArtClassStandard_Open", "Doors"), AssetsCreator.CreateTexture("ArtClassStandard_Closed", "Doors"))
            ));

            Sprite[] canvaSprites = TextureExtensions.LoadSpriteSheet(12, 1, 30, Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Textures", "Rooms", "PaintingsSpriteSheet.png"));
            List<Sprite> rankingSprites = TextureExtensions.LoadSpriteSheet(3, 1, 30, Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Textures", "Rooms", "RankingsSpriteShhet.png")).ToList();

            var artPaint = ObjectCreationExtensions.CreateSpriteBillboard(canvaSprites[0]).gameObject.AddComponent<Painting>();
            artPaint.sprites = canvaSprites;
            artPaint.sr = artPaint.GetComponent<SpriteRenderer>();
            artPaint.gameObject.AddToEditor("Painting", new Vector3(0, 4.3f, 0));

            var piano = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("Piano", "Rooms", 20));
            piano.gameObject.AddBoxCollider(Vector3.zero, new Vector3(8, 10, 8), false);
            piano.gameObject.AddToEditor("Piano", new Vector3(0, 3.5f, 0));

            RoomFunctionContainer artFuncContainer = CreateRoomFunctionContainer("Artistic");
            artFuncContainer.AddRoomFunctionToContainer<ArtRoomFunction>().paintRankings = rankingSprites;
            PosterTextData[] nothing = new PosterTextData[] { };
            artFuncContainer.AddRoomFunctionToContainer<ForcePosterFunction>().posters = new PosterObject[] { ObjectCreators.CreatePosterObject(AssetsCreator.CreateTexture("ArtPoster", "Posters"), nothing) };

            PlusLevelLoaderPlugin.Instance.roomSettings["Artistic"].container = artFuncContainer;

            Dictionary<string, RoomAsset> artRooms = CreateRooms("Artistic", 0, true, artFuncContainer, false, false, null, false);

            PlusLevelLoaderPlugin.Instance.textureAliases.Add("ArtWall", AssetsCreator.Get<Texture2D>("ArtWall"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("ArtRoomCarpet", AssetsCreator.Get<Texture2D>("ArtRoomCarpet"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("FancyCeiling", AssetsCreator.Get<Texture2D>("FancyCeiling"));

            FundamentalManager.GetFloorByName("F2").roomGroups.Add(new RoomGroup
            {
                name = "ArtisticRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = artRooms["ArtRoom1"], weight = 100 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom2"], weight = 75 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom3"], weight = 55 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom4"], weight = 60 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom5"], weight = 49 }
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 0,
                maxRooms = 1,
                stickToHallChance = 1,
                wallTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.CreateTexture("ArtWall", "CellTextures"), weight = 100 } },
                floorTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.CreateTexture("ArtRoomCarpet", "CellTextures"), weight = 100 } },
                ceilingTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.Get<Texture2D>("FancyCeiling"), weight = 100 } }
            });

            FundamentalManager.GetFloorByName("F3").roomGroups.Add(new RoomGroup
            {
                name = "ArtisticRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = artRooms["ArtRoom1"], weight = 80 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom2"], weight = 75 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom3"], weight = 100 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom4"], weight = 25 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom5"], weight = 45 }
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 1,
                maxRooms = 1,
                stickToHallChance = 0.8f,
                wallTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.CreateTexture("ArtWall", "CellTextures"), weight = 100 } },
                floorTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.CreateTexture("ArtRoomCarpet", "CellTextures"), weight = 100 } },
                ceilingTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.Get<Texture2D>("FancyCeiling"), weight = 100 } }
            });

            FundamentalManager.GetFloorByName("END").roomGroups.Add(new RoomGroup
            {
                name = "ArtisticRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = artRooms["ArtRoom1"], weight = 84 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom2"], weight = 95 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom3"], weight = 75 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom4"], weight = 55 },
                    new WeightedRoomAsset { selection = artRooms["ArtRoom5"], weight = 67 }
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 0,
                maxRooms = 1,
                stickToHallChance = 0.9f,
                wallTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.CreateTexture("ArtWall", "CellTextures"), weight = 100 } },
                floorTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.CreateTexture("ArtRoomCarpet", "CellTextures"), weight = 100 } },
                ceilingTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.Get<Texture2D>("FancyCeiling"), weight = 100 } }
            });

            Color securityColor = new Color();
            ColorUtility.TryParseHtmlString("#324049", out securityColor);

            PlusLevelLoaderPlugin.Instance.roomSettings.Add("Security", new RoomSettings(
                EnumExtensions.ExtendEnum<RoomCategory>(Rooms.SecureGuard.ToString()),
                RoomType.Room,
                securityColor,
                ObjectCreators.CreateDoorDataObject("SecurityDoor", AssetsCreator.CreateTexture("SecurityStandard_Open", "Doors"), AssetsCreator.CreateTexture("SecurityStandard_Closed", "Doors"))
            ));

            AssetsCreator.CreateTexture("SecurityWall", "CellTextures");
            AssetsCreator.CreateTexture("SecurityCeiling", "CellTextures");

            RoomFunctionContainer securityGuardFuncContainer = CreateRoomFunctionContainer("SecurityGuard");
            securityGuardFuncContainer.AddRoomFunctionToContainer<LessItemsFunction>().chance = 1;

            PlusLevelLoaderPlugin.Instance.textureAliases.Add("SecurityWall", AssetsCreator.Get<Texture2D>("SecurityWall"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("DiamongPlateFloor", AssetsCreator.Get<Texture2D>("DiamongPlateFloor"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("SecurityCeiling", AssetsCreator.Get<Texture2D>("SecurityCeiling"));

            Dictionary<string, RoomAsset> securityGuardRooms = CreateRooms("SecurityGuard", 0, true, securityGuardFuncContainer, false, false, null, false);

            /*
            FundamentalManager.GetFloorByName("F1").roomGroups.Add(new RoomGroup
            {
                name = "SecurityGuardRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = securityGuardRooms["SecurityGuard1"], weight = 100 }
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 1,
                maxRooms = 1,
                stickToHallChance = 1,
                wallTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.Get<Texture2D>("SecurityWall"), weight = 100 } },
                floorTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.Get<Texture2D>("DiamongPlateFloor"), weight = 100 } },
                ceilingTexture = new WeightedTexture2D[] { new WeightedTexture2D { selection = AssetsCreator.Get<Texture2D>("SecurityCeiling"), weight = 100 } }
            });
            */

            Color storageColor = new Color();
            ColorUtility.TryParseHtmlString("#B6B1E2", out storageColor);

            PlusLevelLoaderPlugin.Instance.roomSettings.Add("Storage", new RoomSettings(
                EnumExtensions.ExtendEnum<RoomCategory>(Rooms.Storage.ToString()),
                RoomType.Room,
                storageColor,
                ObjectCreators.CreateDoorDataObject("StorageDoor", AssetsCreator.CreateTexture("StorageStandard_Open", "Doors"), AssetsCreator.CreateTexture("StorageStandard_Closed", "Doors"))
            ));

            RoomFunctionContainer storageFuncContainer = CreateRoomFunctionContainer("Storage");
            storageFuncContainer.AddRoomFunctionToContainer<StorageFunction>();

            PlusLevelLoaderPlugin.Instance.roomSettings["Storage"].container = storageFuncContainer;

            Dictionary<string, RoomAsset> storageRooms = CreateRooms("Storage", 35, true, storageFuncContainer, false, false, null, false);

            PlusLevelLoaderPlugin.Instance.textureAliases.Add("StorageWall", AssetsCreator.Get<Texture2D>("WlwightAnimatedBrick"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("StorageFloor", AssetsCreator.Get<Texture2D>("Carpet_Red"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("StorageCeiling", AssetsCreator.Get<Texture2D>("HapracoNewCeiling"));

            FundamentalManager.GetFloorByName("F1").roomGroups.Add(new RoomGroup
            {
                name = "StorageRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = storageRooms["Storage1"], weight = 100 },
                    new WeightedRoomAsset { selection = storageRooms["Storage2"], weight = 85 },
                    new WeightedRoomAsset { selection = storageRooms["Storage3"], weight = 90 },
                    new WeightedRoomAsset { selection = storageRooms["Storage4"], weight = 60 },
                    new WeightedRoomAsset { selection = storageRooms["Storage5"], weight = 70 },
                    new WeightedRoomAsset { selection = storageRooms["Storage6"], weight = 50 },
                    new WeightedRoomAsset { selection = storageRooms["Storage7"], weight = 30 },
                    new WeightedRoomAsset { selection = storageRooms["Storage8"], weight = 45 },
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 1,
                maxRooms = 2,
                stickToHallChance = 0.7f,
                wallTexture =  FundamentalManager.GetFloorByName("F1").wallStorageTextures.ToArray(),
                floorTexture = FundamentalManager.GetFloorByName("F1").floorStorageTextures.ToArray(),
                ceilingTexture = FundamentalManager.GetFloorByName("F1").ceilingStorageTextures.ToArray()
            });

            FundamentalManager.GetFloorByName("F2").roomGroups.Add(new RoomGroup
            {
                name = "StorageRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = storageRooms["Storage1"], weight = 40 },
                    new WeightedRoomAsset { selection = storageRooms["Storage2"], weight = 35 },
                    new WeightedRoomAsset { selection = storageRooms["Storage3"], weight = 48 },
                    new WeightedRoomAsset { selection = storageRooms["Storage4"], weight = 10 },
                    new WeightedRoomAsset { selection = storageRooms["Storage5"], weight = 30 },
                    new WeightedRoomAsset { selection = storageRooms["Storage6"], weight = 20 },
                    new WeightedRoomAsset { selection = storageRooms["Storage7"], weight = 15 },
                    new WeightedRoomAsset { selection = storageRooms["Storage8"], weight = 38 },
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 1,
                maxRooms = 4,
                stickToHallChance = 0.8f,
                wallTexture = FundamentalManager.GetFloorByName("F2").wallStorageTextures.ToArray(),
                floorTexture = FundamentalManager.GetFloorByName("F2").floorStorageTextures.ToArray(),
                ceilingTexture = FundamentalManager.GetFloorByName("F2").ceilingStorageTextures.ToArray()
            });

            FundamentalManager.GetFloorByName("F3").roomGroups.Add(new RoomGroup
            {
                name = "StorageRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = storageRooms["Storage1"], weight = 15 },
                    new WeightedRoomAsset { selection = storageRooms["Storage2"], weight = 50 },
                    new WeightedRoomAsset { selection = storageRooms["Storage3"], weight = 40 },
                    new WeightedRoomAsset { selection = storageRooms["Storage4"], weight = 30 },
                    new WeightedRoomAsset { selection = storageRooms["Storage5"], weight = 32 },
                    new WeightedRoomAsset { selection = storageRooms["Storage6"], weight = 60 },
                    new WeightedRoomAsset { selection = storageRooms["Storage7"], weight = 50 },
                    new WeightedRoomAsset { selection = storageRooms["Storage8"], weight = 62 },
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 1,
                maxRooms = 5,
                stickToHallChance = 0.4f,
                wallTexture = FundamentalManager.GetFloorByName("F3").wallStorageTextures.ToArray(),
                floorTexture = FundamentalManager.GetFloorByName("F3").floorStorageTextures.ToArray(),
                ceilingTexture = FundamentalManager.GetFloorByName("F3").ceilingStorageTextures.ToArray()
            });

            FundamentalManager.GetFloorByName("END").roomGroups.Add(new RoomGroup
            {
                name = "StorageRooms",
                potentialRooms = new WeightedRoomAsset[]
                {
                    new WeightedRoomAsset { selection = storageRooms["Storage1"], weight = 60 },
                    new WeightedRoomAsset { selection = storageRooms["Storage2"], weight = 50 },
                    new WeightedRoomAsset { selection = storageRooms["Storage3"], weight = 40 },
                    new WeightedRoomAsset { selection = storageRooms["Storage4"], weight = 30 },
                    new WeightedRoomAsset { selection = storageRooms["Storage5"], weight = 70 },
                    new WeightedRoomAsset { selection = storageRooms["Storage6"], weight = 30 },
                    new WeightedRoomAsset { selection = storageRooms["Storage7"], weight = 40 },
                    new WeightedRoomAsset { selection = storageRooms["Storage8"], weight = 22 },
                },
                light = new WeightedTransform[] { new WeightedTransform { selection = FundamentalCodingHelper.FindResourceObjectContainingName<Transform>("HangingLight"), weight = 100 } },
                minRooms = 0,
                maxRooms = 5,
                stickToHallChance = 0.7f,
                wallTexture = FundamentalManager.GetFloorByName("END").wallStorageTextures.ToArray(),
                floorTexture = FundamentalManager.GetFloorByName("END").floorStorageTextures.ToArray(),
                ceilingTexture = FundamentalManager.GetFloorByName("END").ceilingStorageTextures.ToArray()
            });

            PlusLevelLoaderPlugin.Instance.roomSettings.Add("Placeholder", new RoomSettings(
                EnumExtensions.ExtendEnum<RoomCategory>(Rooms.Placeholder.ToString()),
                RoomType.Room,
                Color.black,
                ObjectCreators.CreateDoorDataObject("PlaceholderDoor", AssetsCreator.CreateTexture("Door_Open", "Doors"), AssetsCreator.CreateTexture("Door_Closed", "Doors"))
            ));

            PlusLevelLoaderPlugin.Instance.textureAliases.Add("PlaceholderWall", FundamentalCodingHelper.FindResourceObjectWithName<Texture2D>("Placeholder_Wall_W"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("PlaceholderFloor", FundamentalCodingHelper.FindResourceObjectWithName<Texture2D>("Placeholder_Floor"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("PlaceholderCeiling", FundamentalCodingHelper.FindResourceObjectWithName<Texture2D>("Placeholder_Celing"));

            Color minimapAdmirerSecretColor = new Color();
            ColorUtility.TryParseHtmlString("#BF40BF", out minimapAdmirerSecretColor);

            PlusLevelLoaderPlugin.Instance.roomSettings.Add("AdmirerSecretRoom", new RoomSettings(
                EnumExtensions.ExtendEnum<RoomCategory>(Rooms.AdmirerSecret.ToString()),
                RoomType.Room,
                minimapAdmirerSecretColor,
                ObjectCreators.CreateDoorDataObject("AdmirerSecretDoor", AssetsCreator.CreateTexture("AdmierSecretDoor_Open", "Doors"), AssetsCreator.CreateTexture("AdmierSecretDoor_Closed", "Doors"))
            ));

            AssetsCreator.CreateTexture("SecretAdmirerWall", "CellTextures");
            AssetsCreator.CreateTexture("SecretAdmirerFloor", "CellTextures");
            AssetsCreator.CreateTexture("SecretAdmirerCeiling", "CellTextures");
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("SecretAdmirerWall", AssetsCreator.Get<Texture2D>("SecretAdmirerWall"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("SecretAdmirerFloor", AssetsCreator.Get<Texture2D>("SecretAdmirerFloor"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("SecretAdmirerCeiling", AssetsCreator.Get<Texture2D>("SecretAdmirerCeiling"));

            var pilarSprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("Pedestal", "Rooms", 20));
            var pilar = new GameObject("Pilar");
            var itemSprite = ObjectCreationExtensions.CreateSpriteBillboard(FundamentalCodingHelper.FindResourceObjectContainingName<Sprite>("BsodaIcon_Large"));
            var item = new GameObject("Pickup");
            item.transform.localPosition = new Vector3(0, 3.5f, 0);
            itemSprite.transform.SetParent(item.transform);
            itemSprite.gameObject.AddComponent<PickupBob>();
            pilarSprite.transform.SetParent(pilar.transform);
            item.transform.SetParent(pilar.transform);
            pilar.gameObject.AddBoxCollider(Vector3.zero, new Vector3(1, 10, 1), false); //Colision
            pilar.gameObject.AddBoxCollider(Vector3.zero, new Vector3(8, 10, 8), true); //Trigger
            pilar.gameObject.AddComponent<ItemPilar>();
            pilar.gameObject.GetComponent<ItemPilar>().sprite = itemSprite;
            pilar.gameObject.AddToEditor("Pedestal", new Vector3(0, 3.2f, 0));

            CreateRooms(path:"SecretAdmirer", maxValue:30, isOffLimits:true, cont:null, isAHallway:false, secretRoom:false, mapBg:AssetsCreator.CreateTexture("MapBG_Hearts", "Minimap"), true, new Texture2D[] { AssetsCreator.Get<Texture2D>("SecretAdmirerWall"), AssetsCreator.Get<Texture2D>("SecretAdmirerFloor"), AssetsCreator.Get<Texture2D>("SecretAdmirerCeiling") }, new WeightedPosterObject[] { new WeightedPosterObject { selection = Posters.secretAdmirer10LikesPoster, weight = 100 }, new WeightedPosterObject { selection = Posters.secretAdimirerLovePlayer, weight = 80 } }, 0.5f);

            Color minimapDustPanColor = new Color();
            ColorUtility.TryParseHtmlString("#793B42", out minimapDustPanColor);

            AssetsCreator.CreateTexture("HappyWallDustPan", "CellTextures");

            PlusLevelLoaderPlugin.Instance.roomSettings.Add("DustPan", new RoomSettings(
                EnumExtensions.ExtendEnum<RoomCategory>(Rooms.DustPan.ToString()),
                RoomType.Room,
                minimapDustPanColor,
                ObjectCreators.CreateDoorDataObject("DustPanDoor", AssetsCreator.CreateTexture("DustPanRoomDoor_Open", "Doors"), AssetsCreator.CreateTexture("DustPanRoomDoor_Closed", "Doors"))
            ));

            RoomFunctionContainer dustPanFuncContainer = CreateRoomFunctionContainer("DustPan");
            dustPanFuncContainer.AddRoomFunctionToContainer<ForcePosterFunction>().posters = new PosterObject[] { Posters.proibithedGottaSweep };
            dustPanFuncContainer.AddRoomFunctionToContainer<NanaPeelRoomFunction>();
            FundamentalCodingHelper.SetValue<ITM_NanaPeel>(dustPanFuncContainer.GetComponent<NanaPeelRoomFunction>(), "bananaPrefab", FundamentalCodingHelper.FindResourceObject<ITM_NanaPeel>());
            FundamentalCodingHelper.SetValue<int>(dustPanFuncContainer.GetComponent<NanaPeelRoomFunction>(), "minBananas", 4);
            FundamentalCodingHelper.SetValue<int>(dustPanFuncContainer.GetComponent<NanaPeelRoomFunction>(), "maxBananas", 8);

            PlusLevelLoaderPlugin.Instance.roomSettings["DustPan"].container = dustPanFuncContainer;

            var trashbagSprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("trashbag", "Rooms", 80)).AddSpriteHolder(2.2f);
            var trashBag = new GameObject("TrashBag").AddComponent<TrashBag>();
            trashBag.nanaPeelPref = FundamentalCodingHelper.FindResourceObject<ITM_NanaPeel>();
            trashbagSprite.transform.SetParent(trashBag.transform);
            trashBag.gameObject.AddBoxCollider(Vector3.zero, new Vector3(1, 10, 1), false);
            trashBag.gameObject.AddToEditor("TrashBag", new Vector3(0, 0f, 0));

            PlusLevelLoaderPlugin.Instance.textureAliases.Add("DustPanWall", AssetsCreator.Get<Texture2D>("HappyWallDustPan"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("DustPanFloor", AssetsCreator.Get<Texture2D>("Calpert"));
            PlusLevelLoaderPlugin.Instance.textureAliases.Add("DustPanCeiling", AssetsCreator.Get<Texture2D>("GenericCeiling1"));

            CreateRooms(path: "DustPan", maxValue: 0, isOffLimits: true, cont: dustPanFuncContainer, isAHallway: false, secretRoom: false, mapBg: null, keepTextures:true,  roomsTextures: new Texture2D[] { AssetsCreator.Get<Texture2D>("HappyWallDustPan"), AssetsCreator.Get<Texture2D>("Calpert"), AssetsCreator.Get<Texture2D>("GenericCeiling1") });
        }

        private static RoomFunctionContainer CreateRoomFunctionContainer(string prefix)
        {
            RoomFunctionContainer rfc = new GameObject($"{prefix}RoomFunction").AddComponent<RoomFunctionContainer>();
            FundamentalCodingHelper.SetValue<List<RoomFunction>>(rfc, "functions", new List<RoomFunction>());
            rfc.gameObject.ConvertToPrefab(true);
            return rfc;
        }

        private static Dictionary<string, RoomAsset> CreateRooms(string path, int maxValue, bool isOffLimits = false, RoomFunctionContainer cont = null, bool isAHallway = false, bool secretRoom = false, Texture2D mapBg = null, bool keepTextures = true, Texture2D[] roomsTextures = null, WeightedPosterObject[] posters = null, float posterChance = 0, bool squaredShape = false)
        {
            Dictionary<string, RoomAsset> assets = new Dictionary<string, RoomAsset>();
            RoomFunctionContainer container = cont;
            foreach (var file in Directory.GetFiles(Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Rooms", path)))
            {
                if (File.ReadAllBytes(file).Length == 0) continue;
                try
                {
                    var asset = RoomFactory.CreateAssetsFromPath(file, maxValue, isOffLimits, container, isAHallway, secretRoom, mapBg, keepTextures, squaredShape);
                    foreach (var room in asset)
                    {
                        Debug.Log("Room_" + Path.GetFileNameWithoutExtension(file));
                        if (roomsTextures != null)
                        {
                            room.wallTex = roomsTextures[0];
                            room.florTex = roomsTextures[1];
                            room.ceilTex = roomsTextures[2];
                        }
                        if (posters != null)  
                            room.posters = posters.ToList();

                        room.posterChance = posterChance;
                        
                            string roomName = Path.GetFileNameWithoutExtension(file);
                        if (!assets.ContainsKey(roomName))
                        {
                            assets.Add(roomName, room);
                            AssetsCreator.assetMan.Add<RoomAsset>(roomName, room);
                        }
                        
                    }
                }
                catch { }
            }

            return assets;
        }

    }
}
