using System.Collections.Generic;
using bbpfer.Extessions;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_GPS : Item, CustomDataItem
    {
        public void Setup()
        {
            beepSound = AssetsCreator.CreateSound("gps_beep", "Items", "", SoundType.Effect, "", 1);
            lowSound = AssetsCreator.CreateSound("gps_end", "Items", "", SoundType.Effect, "", 1);
        }

        private Dictionary<(int, int), bool> originalCellStates = new Dictionary<(int, int), bool>();

        public override bool Use(PlayerManager pm)
        {
            this.pm = pm;
            Cooldown = new Cooldown(34, 0, true, End);
            if (Singleton<InGameManager>.Instance.gpsInUse)
            {
                Destroy(base.gameObject);
                return false;
            }

            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(beepSound);
            Singleton<InGameManager>.Instance.gpsInUse = true;
            Cooldown.Initialize();
            for (int i = 0; i < pm.ec.map.size.x; i++)
            {
                for (int j = 0; j < pm.ec.map.size.z; j++)
                    originalCellStates[(i, j)] = pm.ec.map.tiles[i, j].Found;         
            }

            for (int i = 0; i < pm.ec.map.size.x; i++)
            {
                for (int j = 0; j < pm.ec.map.size.z; j++)
                {
                    if (!pm.ec.cells[i, j].Null)
                        pm.ec.map.Find(i, j, pm.ec.cells[i, j].ConstBin, pm.ec.cells[i, j].room);
                }
            }
            return true;
        }

        public void End()
        {
            for (int i = 0; i < pm.ec.map.size.x; i++)
            {
                for (int j = 0; j < pm.ec.map.size.z; j++)
                {
                    if (originalCellStates.TryGetValue((i, j), out bool wasFound) && !wasFound)
                        UnFind(i, j, pm.ec.cells[i, j].room, pm.ec.map);
                }
            }
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(lowSound);
            Singleton<InGameManager>.Instance.gpsInUse = false;
        }

        private void UnFind(int posX, int posZ, RoomController room, Map map)
        {
            if (map.tiles[posX, posZ] != null)
            {
                map.tiles[posX, posZ].SpriteRenderer.color = room.color;
                map.tiles[posX, posZ].Unreveal();
                map.foundTiles[posX, posZ] = false;
            }
        }

        public Cooldown Cooldown;
        public SoundObject beepSound;
        public SoundObject lowSound;
    }
}
