using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Horn : Item, CustomDataItem
    {
        public void Setup() =>
            hornSound = AssetsCreator.CreateSound("Horn", "Items", "", SoundType.Effect, "#FFFFFF", 1);

        public override bool Use(PlayerManager pm)
        {
            pm.ec.MakeNoise(pm.transform.position, 127);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(hornSound);
            Destroy(base.gameObject);
            return true;
        }

        public SoundObject hornSound;
    }
}
