using System;
using UnityEngine;
using bbpfer.FundamentalManagers;

namespace bbpfer
{
    public class Cooldown
    {
        public float startCooldown = 60;
        public float cooldown = 60;
        public float endCooldown = 0;
        public Action endAction;
        public Action restartAction;
        private bool end;
        public float timeScale;
        public bool updatedInEc;

        public bool cooldownIsEnd
        {
            get 
            {
                return end;
            }
        }

        private bool paused;

        public bool isPaused
        {
            get
            {
                return paused;
            }
        }

        public Cooldown(float startTime, float endTime, bool updatedInEc, Action action = null, Action restartAct = null, float timeScale = 1, bool startPaused = false, bool startIn0 = false)
        {
            if (startPaused)
                Pause(true);

            cooldown = startTime;
            if(!startIn0)
            startCooldown = startTime;
            else
                cooldown = 0;
            endCooldown = endTime;
            endAction = action;
            restartAction = restartAct;
            this.timeScale = timeScale;
            this.updatedInEc = updatedInEc;
        }

        public void Initialize()
        {
            if (updatedInEc)
                Singleton<InGameManager>.Instance.cooldowns.Add(this);
        }

        public void UpdateCooldown()
        {
            if (!paused && !end)
            {           
                cooldown -= Time.deltaTime * timeScale;

                if (cooldown <= endCooldown)
                {
                    cooldown = endCooldown;
                    if (!end)
                    {
                        end = true;
                        endAction?.Invoke();
                    }
                }
            }
        }

        public void Restart()
        {
            cooldown = startCooldown;
            end = false;
            restartAction?.Invoke();
        }

        public void Pause(bool value) =>
            paused = value;
    }
}
