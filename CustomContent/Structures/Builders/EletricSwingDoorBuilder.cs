using System.Collections.Generic;
using UnityEngine;
using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;
using MTM101BaldAPI;

namespace bbpfer.CustomContent.Structures.Builders
{
    public class EletricSwingDoorBuilder : ObjectBuilder, CustomDataStucture
    {
        public void Setup()
        {
            SwingDoor swingDoor = UnityEngine.Object.Instantiate<SwingDoor>(FundamentalCodingHelper.FindResourceObjectWithName<SwingDoor>("Door_Swinging"));
            var eletricDoor = swingDoor.gameObject.AddComponent<EletricSwingDoor>();
            eletricDoor.swingDoor = swingDoor.GetComponent<SwingDoor>();
            eletricDoor.gameObject.ConvertToPrefab(true);

            _doorPrefab = eletricDoor;

            eletricDoor.offMat = bbpfer.Extessions.Extessions.ReCreateMaterial(eletricDoor.swingDoor.overlayLocked[0], AssetsCreator.CreateTexture("SwingDoorOFF", "Structures"));
        }

        public void InGameSetup() { }

        public override void Build(EnvironmentController ec, LevelBuilder builder, RoomController room, System.Random cRng)
        {
            base.Build(ec, builder, room, cRng);

            _doorsCount = cRng.Next(_minDoors, _maxDoors);

            while (_doorsAdded < _doorsCount)
            {
                int attempts = 0;
                List<Cell> availableCells = room.GetTilesOfShape(_tileShapes, true);
                Cell selectedCell = null;

                while (selectedCell == null && availableCells.Count > 0 && attempts < 20)
                {
                    int randomIndex = cRng.Next(0, availableCells.Count);
                    attempts++;

                    Cell potentialCell = availableCells[randomIndex];
                    bool isCellInvalid = ec.TrapCheck(potentialCell) || potentialCell.open;

                    if (isCellInvalid)                  
                        availableCells.RemoveAt(randomIndex);                  
                    else                
                        selectedCell = potentialCell;
                    
                }

                if (selectedCell != null)
                {
                    Direction doorDirection = selectedCell.AllOpenNavDirections[cRng.Next(0, selectedCell.AllOpenNavDirections.Count)];
                    EletricSwingDoor eletricDoorInstance = Instantiate(_doorPrefab, room.transform);

                    eletricDoorInstance.name = "EletricDoor(Clone)";
                    ec.SetupDoor(eletricDoorInstance.swingDoor, selectedCell, doorDirection);

                    GameButton.BuildInArea(ec, selectedCell.position, selectedCell.position, _buttonRange, eletricDoorInstance.gameObject, _buttonPrefab, cRng);

                    _doorsAdded++;
                }
            }
        }

        [SerializeField]
        private List<TileShape> _tileShapes = new List<TileShape>
        {
            TileShape.Straight,
            TileShape.Corner,
            TileShape.End
        };

        [SerializeField]
        private GameButton _buttonPrefab = FundamentalManagers.FundamentalCodingHelper.FindResourceObjectContainingName<GameButton>("GameButton");

        [SerializeField]
        private int _buttonRange = 2;

        [SerializeField]
        private int _minDoors = 1;

        [SerializeField]
        private int _maxDoors = 3;

        private int _doorsCount;
        private int _doorsAdded;

        public EletricSwingDoor _doorPrefab = new EletricSwingDoor();
    }
}
