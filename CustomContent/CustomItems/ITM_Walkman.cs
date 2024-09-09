using bbpfer.CustomLoaders;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Walkman : Item, CustomDataItem
    {
        public void Setup()
        {
            antiHearingSound = FundamentalCodingHelper.GetVariable<SoundObject>(FundamentalCodingHelper.FindResourceObject<TapePlayer>(), "beep");
            inseretSound = FundamentalCodingHelper.GetVariable<SoundObject>(FundamentalCodingHelper.FindResourceObject<TapePlayer>(), "audInsert");
        }

        protected Cooldown coolown;
        protected EnvironmentController ec;
        public SoundObject antiHearingSound, inseretSound;

        public override bool Use(PlayerManager pm)
        {
            coolown = new Cooldown(15, 0, true, Destroy);
            coolown.Initialize();
            ec = pm.ec;
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(inseretSound);
            Singleton<CoreGameManager>.Instance.audMan.PlaySingle(antiHearingSound);
            return true;
        }

        private void Update()
        {
            if (ec.GetBaldi() != null)
                ec.GetBaldi().Distract();
        }

        public void Destroy() =>
            Destroy(base.gameObject);
    }
}
