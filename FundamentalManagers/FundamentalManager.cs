using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bbpfer.FundamentalManagers.ExternalLoaders;

namespace bbpfer.FundamentalManagers
{
    public static class FundamentalManager
    {
        public static IEnumerator LoadAll()
        {
            yield return 8;
            yield return "Loading Tile Textures";
            TileTexturesLoaders.Load();
            yield return "Loading Events";
            EventCreator.Load();
            yield return "Loading Items";
            ItemCreator.Load();
            yield return "Loading Posters";
            PosterCreator.Load();
            yield return "Loading Structure";
            StructuresCreator.Load();
            yield return "Loading Rooms (With Old System)";
            RoomCreator.Load();
            yield return "Loading Rooms (With New System)";
            RoomLoader.Load();
            yield return "Loading Npcs";
            NPCsCreator.Load();        
        }

        public static FloorData GetFloorByName(string name)
        {
            foreach (FloorData data in FundamentalManager.floors)
            {
                if (data.name.Contains(name))               
                    return data;               
            }
            return null;
        }

        public static List<FloorData> floors = new List<FloorData>()
        {
            new FloorData("F1"),
            new FloorData("F2"),
            new FloorData("F3"),
            new FloorData("F4"),
            new FloorData("END")
        };
    }

    public class FloorData
    {
        public string name;
        public FloorData(string _name) { name = _name; }


        public List<WeightedNPC> npcs = new List<WeightedNPC>();
        public List<NPC> forcedNPCs = new List<NPC>();


        public List<WeightedTexture2D> wallHallTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> floorHallTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> ceilingHallTextures = new List<WeightedTexture2D>();

        public List<WeightedTexture2D> wallClassTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> floorClassTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> ceilingClassTextures = new List<WeightedTexture2D>();

        public List<WeightedTexture2D> wallFacultyTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> floorFacultyTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> ceilingFacultyTextures = new List<WeightedTexture2D>();

        public List<WeightedTexture2D> wallStorageTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> floorStorageTextures = new List<WeightedTexture2D>();
        public List<WeightedTexture2D> ceilingStorageTextures = new List<WeightedTexture2D>();

        public List<WeightedRoomAsset> customsFacultyrooms = new List<WeightedRoomAsset>();
        public List<WeightedRoomAsset> customsExtrarooms = new List<WeightedRoomAsset>();

        public List<WeightedItemObject> items = new List<WeightedItemObject>();
        public List<WeightedItemObject> shopItems = new List<WeightedItemObject>();
        public List<ItemObject> forcedItems = new List<ItemObject>();

        public List<WeightedRandomEvent> randomEvents = new List<WeightedRandomEvent>();

        public List<WeightedPosterObject> posters = new List<WeightedPosterObject>();

        public List<RoomGroup> roomGroups = new List<RoomGroup>();

        public List<ObjectBuilder> forcedObjectBuilders = new List<ObjectBuilder>();
        public List<WeightedObjectBuilder> specialObjectBuilders = new List<WeightedObjectBuilder>();
    }
}
