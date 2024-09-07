using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Bag : Item
    {
        public override bool Use(PlayerManager pm)
        {
            if (open)
                Singleton<InGameManager>.Instance.OpenBag(pm.playerNumber);
            else
                Singleton<InGameManager>.Instance.CloseBag(pm.playerNumber);
            Destroy(base.gameObject);
            return false;
        }

        public bool open;
    }
}
