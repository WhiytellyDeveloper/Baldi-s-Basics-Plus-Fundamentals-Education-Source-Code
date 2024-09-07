using System;
using System.Collections.Generic;
using UnityEngine;
using bbpfer.FundamentalManagers;
using bbpfer.CustomRooms;
using System.Linq;
using MTM101BaldAPI;
using MTM101BaldAPI.Registers;
using bbpfer.Extessions;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
	public partial class RoomCreator
	{
		public static void Load()
		{
			
			Room.Load(new CustomFaculty("CustomFaculty1", 0, 100, 0.75f), new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 60, 55, 72, 42, 66 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty2", 0, 130, 5.4f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 40, 60, 75, 120 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty3", 0, 100, 0.5f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 68, 80, 100, 95 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty4", 0, 80, 1), new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 59, 68, 76, 55, 78 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty5", 0, 100, 2.5f), new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 72, 55, 36, 50, 57 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty6", 0, 100, 0.5f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 50, 70, 80, 100 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty7", 0, 100, 1f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 80, 75, 60, 100 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty8", 0, 100, 0.5f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 90, 120, 35, 75 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty9", 0, 150, 0.5f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 50, 70, 80, 85 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty10", 0, 100, 0.5f), new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 75, 80, 95, 60, 125 }, RoomCategory.Faculty); 
			Room.Load(new CustomFaculty("CustomFaculty11", 0, 100, 5f), new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 50, 60, 70, 80, 100 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty12", 0, 100, 5f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 50, 76, 30, 80 }, RoomCategory.Faculty);
			Room.Load(new CustomFaculty("CustomFaculty13", 0, 100, 0.1f), new string[] { "F2", "F3", "F4", "END" }, new int[] { 70, 60, 80, 110 }, RoomCategory.Faculty);
			Room.Load(new CustomDebugRoom("DebugRoom"), new string[] { "F2", "F3", "F4", "END" }, new int[] { 40, 50, 80, 0 }, RoomCategory.Buffer, true);
			
		}


	}

	public class CustomFaculty : Room
	{
		protected string _file;
		protected int max, min;
		protected float chance;

		public CustomFaculty(string file, int min, int max, float chance) : base(file)
		{
			_file = file;
			this.min = min;
			this.max = max;
			this.chance = chance;
		}


		public override RoomAsset Build()
		{
			RoomAsset roomAsset = ScriptableObject.CreateInstance<RoomAsset>();
			Setup(roomAsset, _file);
			SetAllItemsSpawnPoints(roomAsset, max, min, chance, 35);

			roomAsset.name = _file;
			roomAsset.category = RoomCategory.Faculty;
			roomAsset.type = RoomType.Room;
			roomAsset.color = Color.red;
			roomAsset.mapMaterial = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.mapMaterial;
			roomAsset.doorMats = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.doorMats;
			roomAsset.windowObject = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.windowObject;
			roomAsset.hasActivity = false;
			roomAsset.offLimits = true;
			roomAsset.keepTextures = false;
			roomAsset.activity = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.activity;
			roomAsset.roomFunction = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.roomFunction;
			roomAsset.roomFunctionContainer = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.roomFunctionContainer;
			roomAsset.posters = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.posters;
			roomAsset.posterChance = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.posterChance;
			roomAsset.items = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.items;
			roomAsset.itemList = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.itemList;
			roomAsset.minItemValue = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.minItemValue;
			roomAsset.maxItemValue = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.maxItemValue;
			roomAsset.lightPre = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.lightPre;
			roomAsset.windowChance = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.windowChance;

			if (_file.EndsWith("1") || _file.EndsWith("9"))
			{
				AddSwapsObjects(roomAsset, new BasicObjectSwapData[] {
				new BasicObjectSwapData { chance = 100, prefabToSwap = Resources.FindObjectsOfTypeAll<Transform>().First((Transform x) => x.name == "SodaMachine"), potentialReplacements = new WeightedTransform[] {
					new WeightedTransform { selection = Resources.FindObjectsOfTypeAll<Transform>().First((Transform x) => x.name == "DietSodaMachine"), weight = 30 },
					new WeightedTransform { selection = Resources.FindObjectsOfTypeAll<Transform>().First((Transform x) => x.name == "SodaMachine"), weight = 10 },
					new WeightedTransform { selection = Resources.FindObjectsOfTypeAll<Transform>().First((Transform x) => x.name == "ZestyMachine"), weight = 30 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("cookieMachine").transform, weight = 48 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("sodaMachine").transform, weight = 28 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("genericSodaMachine").transform, weight = 37 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("iceCreamMachine").transform, weight = 15 },
				}
			}
			});
			}

			if (_file.EndsWith("3"))
			{
				AddSwapsObjects(roomAsset, new BasicObjectSwapData[] {
				new BasicObjectSwapData { chance = 100, prefabToSwap = GetObject("Decor_Papers"),
					potentialReplacements = new WeightedTransform[] {
						new WeightedTransform { selection = GetObject("Decor_Notebooks"), weight = 40 },
						new WeightedTransform { selection = GetObject("Decor_Globe"), weight = 57 },
						new WeightedTransform { selection = GetObject("Decor_PencilNotes"), weight = 60 },
						new WeightedTransform { selection = GetObject("Decor_Papers"), weight = 61 },
					}
				 }
				});
			}

			if (_file.EndsWith("4"))
			{
				AddSwapsObjects(roomAsset, new BasicObjectSwapData[] {
				new BasicObjectSwapData {
					prefabToSwap = GetObject("SodaMachine"),
					chance = 50,
					potentialReplacements = new WeightedTransform[] {
						new WeightedTransform { selection = GetObject("SodaMachine"), weight = 10 },
						new WeightedTransform { selection = GetObject("DietSodaMachine"), weight = 35 },
						new WeightedTransform { selection = GetObject("ZestyMachine"), weight = 42 },
						new WeightedTransform { selection = GetObject("WaterFountain"), weight = 46 },
				    	new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("cookieMachine").transform, weight = 48 },
					    new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("sodaMachine").transform, weight = 28 },
				      	new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("genericSodaMachine").transform, weight = 37 },
				    	new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("iceCreamMachine").transform, weight = 15 },
					}
				}
			  });

				roomAsset.posters.Clear();
			}

			if (_file.EndsWith("5"))
			{
				AddSwapsObjects(roomAsset, new BasicObjectSwapData[]
{
				new BasicObjectSwapData
				{
					prefabToSwap = GetObject("SodaMachine"),
					chance = 100,
					potentialReplacements = new WeightedTransform[]
					{
						new WeightedTransform { selection = GetObject("SodaMachine"), weight = 100 },
						new WeightedTransform { selection = GetObject("DietSodaMachine"), weight = 30 },
						new WeightedTransform { selection = GetObject("ZestyMachine"), weight = 80 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("cookieMachine").transform, weight = 90 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("sodaMachine").transform, weight = 50 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("genericSodaMachine").transform, weight = 43 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("iceCreamMachine").transform, weight = 20 },
					}
				}
});
			}

			if (_file.EndsWith("6"))
			{

				for (int i = 0; i < roomAsset.basicObjects.Count; i++)
				{
					if (roomAsset.basicObjects[i].prefab.name.Contains("Computer"))
						roomAsset.basicObjects[i].rotation = RotateObject(180);
				}

				AddSwapsObjects(roomAsset, new BasicObjectSwapData[]
				{
				new BasicObjectSwapData
				{
					prefabToSwap = GetObject("SodaMachine"),
					chance = 100,
					potentialReplacements = new WeightedTransform[]
					{
						new WeightedTransform { selection = GetObject("DietSodaMachine"), weight = 100 },
						new WeightedTransform { selection = GetObject("SodaMachine"), weight = 30 },
						new WeightedTransform { selection = GetObject("ZestyMachine"), weight = 80 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("cookieMachine").transform, weight = 90 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("sodaMachine").transform, weight = 50 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("genericSodaMachine").transform, weight = 43 },
					new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("iceCreamMachine").transform, weight = 20 },
						new WeightedTransform { selection = GetObject("WaterFountain"), weight = 110 },
					}
				}
				});

				AddSwapsObjects(roomAsset, new BasicObjectSwapData[]
{
				new BasicObjectSwapData
				{
					prefabToSwap = GetObject("MyComputer"),
					chance = 100,
					potentialReplacements = new WeightedTransform[]
					{
						new WeightedTransform { selection = GetObject("MyComputer_Off"), weight = 100 },
						new WeightedTransform { selection = GetObject("MyComputer"), weight = 80 },
					}
				}
});
			}

			if (_file.EndsWith("8") || _file.EndsWith("10") || _file.EndsWith("12"))
			{

				AddSwapsObjects(roomAsset, new BasicObjectSwapData[]
				{
				new BasicObjectSwapData
				{
					prefabToSwap = GetObject("SodaMachine"),
					chance = 100,
					potentialReplacements = new WeightedTransform[]
					{
						new WeightedTransform { selection = GetObject("DietSodaMachine"), weight = 75 },
						new WeightedTransform { selection = GetObject("SodaMachine"), weight = 25 },
						new WeightedTransform { selection = GetObject("ZestyMachine"), weight = 85 },
			      		new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("cookieMachine").transform, weight = 90 },
			    		new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("sodaMachine").transform, weight = 50 },
				    	new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("genericSodaMachine").transform, weight = 43 },
				    	new WeightedTransform { selection = AssetsCreator.Get<SodaMachine>("iceCreamMachine").transform, weight = 20 },
						new WeightedTransform { selection = GetObject("WaterFountain"), weight = 50 },
					}
				}
				});
			}

			return roomAsset;
		}
	}
}

public class CustomDebugRoom : Room
{
	protected string _file;

	public CustomDebugRoom(string file) : base(file)
	{
		_file = file;
	}


	public override RoomAsset Build()
	{
		RoomAsset roomAsset = ScriptableObject.CreateInstance<RoomAsset>();
		Setup(roomAsset, _file);

		roomAsset.name = _file;
		roomAsset.category = EnumExtensions.ExtendEnum<RoomCategory>("Debug");
		roomAsset.type = RoomType.Room;
		roomAsset.color = Color.black;
		roomAsset.doorMats = ObjectCreators.CreateDoorDataObject("DebugDoor", AssetsCreator.CreateTexture("Door_Open", "Doors"), AssetsCreator.CreateTexture("Door_Closed", "Doors"));
		roomAsset.windowObject = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.windowObject;
		roomAsset.hasActivity = false;
		roomAsset.offLimits = true;
		roomAsset.keepTextures = true;
		roomAsset.wallTex = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.wallTex;
		roomAsset.florTex = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.florTex;
		roomAsset.ceilTex = RoomAssetMetaStorage.Instance.Get(RoomCategory.Faculty, "Room_Faculty_School_0").value.ceilTex;
		roomAsset.posterChance = 0;
		roomAsset.windowChance = 0.5f;
		return roomAsset;
	}

	public static void CreateRoomFunctionAndContainer<F>(RoomAsset room) where F : RoomFunction
	{
		if (room.roomFunctionContainer.gameObject.GetComponent<F>() == null)
		{
			var comp = room.roomFunctionContainer.gameObject.AddComponent<F>();
			room.roomFunctionContainer.AddFunction(comp);
		}
	}
}
