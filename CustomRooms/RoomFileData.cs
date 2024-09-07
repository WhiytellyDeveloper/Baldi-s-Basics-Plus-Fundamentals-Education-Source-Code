using System.Collections.Generic;
using UnityEngine;

namespace bbpfer.CustomRooms
{
    public class RoomFileData
    {
		public List<CellData> cells = new List<CellData>();
		public List<BasicObjectData> basicObjects = new List<BasicObjectData>();
		public List<string> basicObjectsNames = new List<string>(); 
		public List<BasicObjectSwapData> basicSwaps = new List<BasicObjectSwapData>();
		public List<Vector2> itemSpawnPoints = new List<Vector2>();
		public List<IntVector2> potentialDoorPositions = new List<IntVector2>();
		public List<IntVector2> forcedDoorPositions = new List<IntVector2>();
		public List<IntVector2> requiredDoorPositions = new List<IntVector2>();
		public List<IntVector2> entitySafeCells = new List<IntVector2>();
		public List<IntVector2> eventSafeCells = new List<IntVector2>();
		public List<IntVector2> blockedWallCells = new List<IntVector2>();
		public List<IntVector2> secretCells = new List<IntVector2>();
		public List<IntVector2> standardLightCells = new List<IntVector2>();
		public ActivityData activity;
	}
}
