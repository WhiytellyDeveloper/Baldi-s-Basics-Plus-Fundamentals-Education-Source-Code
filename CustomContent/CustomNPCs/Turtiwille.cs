using bbpfer.Extessions;
using bbpfer.CustomLoaders;
using UnityEngine;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;
using System.Linq;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class Turtiwille : NPC, IClickable<int>, CustomDataNPC
    {
        public void Setup()
        {
            Sprite[] sprites = new Sprite[8];

            for (int i = 0; i <= 7; i++)
                sprites[i] = AssetsCreator.Get<Sprite>($"Tortuguito_{i}");

            renderer = this.CreateAnimatedSpriteRotator(new SpriteRotationMap[] {
                GenericExtensions.CreateRotationMap(8, sprites.ToArray()),
            });

            renderer.targetSprite = sprites[0];
            gameObject.layer = LayerStorage.iClickableLayer;
        }

        public void InGameSetup() => 
            eatCooldown = new Cooldown(4, 0, false, null, null, 1, false, true);
        

        //-----------------------------------------------------------------------------------

        private bool clickable = true;
        public AnimatedSpriteRotator renderer;
        private int sizeNum = 0;
        private float[] size = new float[] { 1, 0.8f, 0.6f, 0.4f, 0.2f, 0 };
        public Cooldown eatCooldown;
        public DijkstraMap map;

        public override void Initialize()
        {
            base.Initialize();

            map = new DijkstraMap(ec, PathType.Const, players[0].transform);
            behaviorStateMachine.ChangeState(new Turtiwille_Wandering(this));
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();

            eatCooldown.UpdateCooldown();
            eatCooldown.timeScale = ec.NpcTimeScale;
            navigator.Entity.SetVerticalScale(size[sizeNum]);
            CheckSizeAndDespawn();
        }

        private void CheckSizeAndDespawn()
        {
            if (sizeNum == size.Length - 1)
                Despawn();
        }

        public bool FacingPlayer()
        {
            if (renderer.targetSprite == spriteRenderer[0].sprite)
                return true;
            return false;
        }

        public void Clicked(int player)
        {
            if (eatCooldown.cooldownIsEnd)
            {
                Singleton<CoreGameManager>.Instance.GetPlayer(player).plm.stamina = 175;
                DecreaseSize();
                eatCooldown.Restart();
            }
        }

        public void ClickableUnsighted(int player) { }

        public void ClickableSighted(int player) { }

        public bool ClickableRequiresNormalHeight() => true;

        public bool ClickableHidden() => !clickable;

        private void DecreaseSize()
        {
            if (sizeNum < size.Length - 1)
            {
                sizeNum++;
                behaviorStateMachine.ChangeState(new Turtiwille_Flee(this));
            }
        }
    }

    public class Turtiwille_StateBase : NpcState
    {
        protected Turtiwille turtiwille;

        public Turtiwille_StateBase(Turtiwille npc) : base(npc) { turtiwille = npc; }
    }

    public class Turtiwille_Wandering : Turtiwille_StateBase
    {
        public Turtiwille_Wandering(Turtiwille npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            ChangeNavigationState(new NavigationState_WanderRandom(turtiwille, 0));
            turtiwille.Navigator.SetSpeed(16, 16);
        }

        public override void InPlayerSight(PlayerManager player)
        {
            base.InPlayerSight(player);

            if (turtiwille.FacingPlayer())
                turtiwille.behaviorStateMachine.ChangeState(new Turtiwille_Flee(turtiwille));
        }
    }

    public class Turtiwille_Flee : Turtiwille_StateBase
    {
        public Turtiwille_Flee(Turtiwille npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            turtiwille.map.Activate();
            turtiwille.map.QueueUpdate();
            ChangeNavigationState(new NavigationState_WanderFlee(turtiwille, 0, turtiwille.map));
            turtiwille.Navigator.SetSpeed(150, 150);
        }

        public override void PlayerLost(PlayerManager player)
        {
            base.PlayerLost(player);
            turtiwille.behaviorStateMachine.ChangeState(new Turtiwille_Wandering(turtiwille));
        }

        public override void Exit()
        {
            base.Exit();
            turtiwille.map.Deactivate();
        }
    }
}
