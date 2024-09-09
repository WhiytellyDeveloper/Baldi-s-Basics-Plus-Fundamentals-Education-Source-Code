using bbpfer.CustomLoaders;
using UnityEngine;
using PixelInternalAPI.Extensions;
using bbpfer.FundamentalManagers;
using System.Collections.Generic;
using MTM101BaldAPI;
using bbpfer.Extessions;

namespace bbpfer.CustomContent.Structures.Builders
{
    public class SpeedBumpsBuilder : ObjectBuilder, CustomDataStucture
    {
        public void Setup()
        {
            SpriteRenderer spr = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("Speedbump", "Structures", 13), false).AddSpriteHolder(0.01f);
            spr.transform.localRotation = Quaternion.Euler(90, 0, 0);
            SpeedBumps speedBump = new GameObject("SpeedBump").AddComponent<SpeedBumps>();
            spr.transform.SetParent(speedBump.transform);
            speedBump.gameObject.AddBoxCollider(Vector3.zero, new Vector3(9.8f, 4, 10), true);
            speedBump.gameObject.ConvertToPrefab(true);
            bumpPref = speedBump;
        }

        //------------------------------------------------------

        public override void Build(EnvironmentController ec, LevelBuilder builder, RoomController room, System.Random cRng)
        {
            base.Build(ec, builder, room, cRng);
            List<Cell> availableCells = room.GetTilesOfShape(shapes, false);

            int vauleCount = cRng.Next(vauleMin, vauleMax);

            while (vaule != vauleCount)
            {
                Cell selectedCell = null;

                while (selectedCell == null && availableCells.Count > 0)
                {
                    int index = cRng.Next(0, availableCells.Count);
                    Cell potentialCell = availableCells[index];
                    bool isCellInvalid = ec.TrapCheck(potentialCell);

                    if (isCellInvalid)
                        availableCells.RemoveAt(index);
                    else
                        selectedCell = potentialCell;
                }

                vaule++;

                if (selectedCell != null)
                {
                    selectedCell.HardCover(CellCoverage.Down);
                    Direction dir = selectedCell.AllOpenNavDirections[cRng.Next(0, selectedCell.AllOpenNavDirections.Count)];
                    SpeedBumps bump = Instantiate<SpeedBumps>(bumpPref, transform);
                    bump.transform.position = new Vector3(selectedCell.FloorWorldPosition.x, 0, selectedCell.FloorWorldPosition.z);
                    bump.transform.rotation = dir.ToRotation();
                    Debug.Log("Speed Bump Created!");
                }
            }
        }

        public SpeedBumps bumpPref;
        public List<TileShape> shapes = new List<TileShape> { TileShape.Straight, TileShape.Single };
        public int vaule = 0, vauleMin = 3, vauleMax = 7;
    }
}
