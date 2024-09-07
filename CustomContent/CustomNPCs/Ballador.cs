using bbpfer.Extessions;
using System.Collections.Generic;
using UnityEngine;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomNPCs
{
    public class Ballador : NPC, CustomDataNPC
    {
        public void Setup()
        {
            idleSprite = AssetsCreator.Get<Sprite>("Ballador");
            readingSprite = AssetsCreator.Get<Sprite>("Ballador_Reading_Notebook");

        }

        public void InGameSetup() 
        { 
            cooldown = new Cooldown(10, 0, false, FindNotebook);
            readingCooldown = new Cooldown(30, 0, false, EndReading, null, 1, true);
        }

        //-------------------------------------------------------------------------------------------

        public override void Initialize()
        {
            base.Initialize();
            behaviorStateMachine.ChangeState(new Ballador_Wandering(this));

            if (notebooks.Count == 0)
                Despawn();
        }

        protected override void VirtualUpdate()
        {
            base.VirtualUpdate();

            GetNotebooks();


            if (reading)
            {
                notebooks[index].icon.spriteRenderer.enabled = false;
                notebooks[index].gameObject.SetActive(false);
            }
        }

        public void GetNotebooks()
        {
            notebooks.Clear();
            for (int i = 0; i < ec.notebooks.Count; i++)
            {
                notebooks.Add(ec.notebooks[i]);

                if (!reading && ec.notebooks[i].icon.spriteRenderer.enabled == false)
                    notebooks.Remove(ec.notebooks[i]);
            }
        }

        public void FindNotebook() =>
            behaviorStateMachine.ChangeState(new Ballador_GoToClass(this));

        public void EndReading()
        {
            behaviorStateMachine.ChangeState(new Ballador_Wandering(this));
            notebooks[index].gameObject.SetActive(true);
            notebooks[index].icon.spriteRenderer.enabled = true;
            reading = false;
            cooldown.Restart();
            cooldown.Pause(false);
            readingCooldown.Pause(true);
            readingCooldown.Restart();
            Navigator.Entity.SetFrozen(false);
            spriteRenderer[0].sprite = idleSprite;
        }

        public List<Notebook> notebooks;
        public bool reading;
        public int index;
        public Sprite idleSprite;
        public Sprite readingSprite;
        public Cooldown cooldown;
        public Cooldown readingCooldown;
    }

    public class Ballador_BaseState : NpcState
    {
        protected Ballador bal;
        public Ballador_BaseState(Ballador npc) : base(npc) { this.npc = npc; bal = npc; }
    }

    public class Ballador_Wandering : Ballador_BaseState
    {
        public Ballador_Wandering(Ballador npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(15, 15);
            ChangeNavigationState(new NavigationState_WanderRandom(bal, 0));
        }
    }

    public class Ballador_GoToClass : Ballador_BaseState
    {
        public Ballador_GoToClass(Ballador npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            npc.Navigator.SetSpeed(50, 75);
        }

        public override void Update()
        {
            base.Update();

            if (bal.notebooks[bal.index] == null)
                bal.index = Random.Range(0, bal.notebooks.Count);

            if (((int)Vector3.Distance(bal.transform.position, bal.notebooks[bal.index].transform.position)) <= 7)
                bal.behaviorStateMachine.ChangeState(new Ballador_Reading(bal));

            ChangeNavigationState(new NavigationState_TargetPosition(bal, 0, bal.notebooks[bal.index].transform.position));
        }
    }

    public class Ballador_Reading : Ballador_BaseState
    {
        public Ballador_Reading(Ballador npc) : base(npc) { }

        public override void Initialize()
        {
            base.Initialize();
            bal.readingCooldown.Restart();
            bal.readingCooldown.Pause(false);
            bal.cooldown.Pause(true);
            npc.Navigator.Entity.SetFrozen(true);
            /*
            Color color = new Color();
            ColorUtility.TryParseHtmlString("#FF0519", out color);
            npc.spriteRenderer[0].sprite = bal.readingSprite.ChangeColorToDominant(color, FundamentalCodingHelper.GetVariable<SpriteRenderer>(bal.notebooks[bal.index], "sprite").sprite.GetMiddlePixelColor());
            */
            npc.spriteRenderer[0].sprite = bal.readingSprite;
            bal.reading = true;
        }

        public override void Update()
        {
            base.Update();
            bal.readingCooldown.UpdateCooldown();
        }
    }
}
