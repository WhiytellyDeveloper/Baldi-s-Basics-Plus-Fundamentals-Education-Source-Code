using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomRooms
{
    public class Room
    {
        public Room(string file) { }

        public virtual RoomAsset Build()
        {
            return Build();
        }

        public static RoomAsset Load(Room room, string[] floors, int[] weighted, RoomCategory category, bool extraRoom = false)
        {
            RoomAsset room2 = room.Build();

            switch (category)
            {
                case RoomCategory.Faculty:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalManager.GetFloorByName(floors[i]).customsFacultyrooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                    /*
                case RoomCategory.Class:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalsFunctions.GetExtraFloorData(floors[i])._classRooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                case RoomCategory.Office:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalsFunctions.GetExtraFloorData(floors[i])._officeRooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                case RoomCategory.Hall:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalsFunctions.GetExtraFloorData(floors[i])._hallFormation.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                    */
            }

            if (extraRoom)
                for (int i = 0; i < floors.Length; i++)
                    FundamentalManager.GetFloorByName(floors[i]).customsExtrarooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });

            return room2;
        }


        public static void Load(RoomAsset room, string[] floors, int[] weighted, RoomCategory category)
        {
            RoomAsset room2 = room;

            switch (category)
            {
                case RoomCategory.Faculty:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalManager.GetFloorByName(floors[i]).customsFacultyrooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                    /*
                case RoomCategory.Class:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalsFunctions.GetExtraFloorData(floors[i])._classRooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                case RoomCategory.Office:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalsFunctions.GetExtraFloorData(floors[i])._officeRooms.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                case RoomCategory.Hall:
                    for (int i = 0; i < floors.Length; i++)
                        FundamentalsFunctions.GetExtraFloorData(floors[i])._hallFormation.Add(new WeightedRoomAsset { selection = room2, weight = weighted[i] });
                    break;
                    */
            }
        }

        public static void LoadFile(string room)
        {
            string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "Modded", ModInfo.streamingPath, "ExternalFiles", "Rooms", room + ".txt"));
            data = JsonUtility.FromJson<RoomFileData>(json);
        }

        public static void Setup(RoomAsset roomAsset, string room, string activity = "none")
        {
            LoadFile(room);
            roomAsset.cells = data.cells;
            roomAsset.blockedWallCells = data.blockedWallCells;
            roomAsset.entitySafeCells = data.entitySafeCells;
            roomAsset.eventSafeCells = data.eventSafeCells;
            roomAsset.forcedDoorPositions = data.forcedDoorPositions;
            roomAsset.requiredDoorPositions = data.requiredDoorPositions;
            roomAsset.secretCells = data.secretCells;
            roomAsset.standardLightCells = data.standardLightCells;
            roomAsset.potentialDoorPositions = data.potentialDoorPositions;
            roomAsset.basicObjects = GetBasicObjects();

            if (activity != "none")     
                roomAsset.activity = new ActivityData { position = data.activity.position, direction = data.activity.direction, prefab = GetObject(activity).GetComponent<Activity>() };       
        }

        public static void SetAllItemsSpawnPoints(RoomAsset roomAsset, int maxVaule, int minVaule, float chance, int weight)
        {
            roomAsset.itemSpawnPoints.Clear();
            for (int i = 0; i < data.itemSpawnPoints.Count; i++)           
                roomAsset.itemSpawnPoints.Add(new ItemSpawnPoint { position = data.itemSpawnPoints[i], chance = chance, minValue = minVaule, maxValue = maxVaule, weight = weight });
            
        }

        public static List<BasicObjectData> GetBasicObjects()
        {
            for (int i = 0; i < data.basicObjects.Count; i++)
            {
                data.basicObjects[i].prefab = GetObject(data.basicObjectsNames[i]);
            }
            return data.basicObjects;
        }

        public static void AddBasicsObjects(RoomAsset room, BasicObjectData[] objects)
        {
            room.basicObjects.AddRange(objects);
        }

        public static void AddSwapsObjects(RoomAsset room, BasicObjectSwapData[] objects)
        {
            room.basicSwaps.AddRange(objects);
        }

        public static Transform GetObject(string str)
        {
            return Resources.FindObjectsOfTypeAll<Transform>().First((Transform x) => x.name == str);
        }

        //OLD
        public static Quaternion RotateObject(float i)
        {
            return new Quaternion
            {
                eulerAngles = new Vector3(0f, i, 0f)
            };
        }

        public RoomAsset room;
        public static RoomFileData data;
    }
}
