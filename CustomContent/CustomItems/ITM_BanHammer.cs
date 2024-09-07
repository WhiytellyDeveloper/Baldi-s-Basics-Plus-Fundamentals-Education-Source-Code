using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_BanHammer : NpcItem, CustomDataItem
    {
        public void Setup()
        {
            banSound = AssetsCreator.CreateSound("BanHammer_Use", "Items", "", SoundType.Effect, "", 1);
            UnBanSound = AssetsCreator.CreateSound("BanHammer_End", "Items", "", SoundType.Effect, "", 1);
        }

        public override void PostUseItem()
        {
            base.PostUseItem();
            destroyOnUse = false;
        }

        public override void OnUse(PlayerManager pm, NPC npc)
        {
            cooldown = new Cooldown(30, 0, true, Unban);
            cooldown.Initialize();

            Cell cell = new Cell();

            foreach (Cell _cell in pm.ec.cells)
                if (_cell.Null)
                    cell = _cell;
            npc.transform.position = lastPosition;
            npc.Navigator.Entity.Teleport(cell.FloorWorldPosition);
            npc.Navigator.Entity.SetFrozen(true);
            lastNPC = npc;
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(banSound);
        }


        public void Unban()
        {
            lastNPC.Navigator.Entity.Teleport(lastPosition);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(UnBanSound);
            lastNPC.Navigator.Entity.SetFrozen(false);
        }

        protected NPC lastNPC;
        protected UnityEngine.Vector3 lastPosition;
        public Cooldown cooldown;
        public SoundObject banSound;
        public SoundObject UnBanSound;
    }
}
