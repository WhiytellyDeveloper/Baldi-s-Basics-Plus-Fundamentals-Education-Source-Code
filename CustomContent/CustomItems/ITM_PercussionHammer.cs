using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_PercussionHammer : NpcItem, CustomDataItem
    {
        public void Setup() =>
            smashSound = AssetsCreator.CreateSound("PlayerPercussionHammer", "Items", "", SoundType.Effect, "", 1);

        public override void OnUse(PlayerManager pm, NPC npc)
        {
            base.OnUse(pm, npc);
            if (npc.Navigator.Entity.Squished)
                returnTrue = false;
            else
            {
                Singleton<CoreGameManager>.Instance.audMan.PlaySingle(smashSound);
                npc.Navigator.Entity.Squish(squishTime);
            }
            pm.RuleBreak("Bullying", 3);
        }

        public override void PostUseItem()
        {
            base.PostUseItem();
            destroyOnUse = true;
        }

        public SoundObject smashSound;
        public int squishTime = 15;
    }
}
