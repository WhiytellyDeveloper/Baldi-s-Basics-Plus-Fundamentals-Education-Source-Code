using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomContent.CustomEvents
{
    public class RaveEvent : RandomEvent
    {
        public List<Color> oldColors = new List<Color>();
        public List<Cell> positions = new List<Cell>();
        public bool updateLight;
        public bool oldLights;

        public override void Begin()
        {
            base.Begin();
            updateLight = true;
            Singleton<MusicManager>.Instance.PlayMidi(AssetsCreator.LoadMidi("RaveEvent"), true);
            StartCoroutine(RaveStart());

            if (Singleton<CoreGameManager>.Instance.currentMode == Mode.Main)
            {
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().GetExtraAnger(5);
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().looker.enabled = false;
            }
        }

        public override void End()
        {
            Singleton<MusicManager>.Instance.StopMidi();
            updateLight = false;
            StopAllCoroutines();
            StartCoroutine(RaveEnd());

            if (Singleton<CoreGameManager>.Instance.currentMode == Mode.Main)
            {
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().GetExtraAnger(-5);
                Singleton<BaseGameManager>.Instance.Ec.GetBaldi().looker.enabled = true;
            }
        }

        public IEnumerator RaveStart()
        {
            positions = Singleton<BaseGameManager>.Instance.Ec.AllCells();

            List<Cell> cellsToUpdate = new List<Cell>(positions);

            while (cellsToUpdate.Count > 0)
            {
                if (updateLight)
                {
                    int j = Random.Range(0, cellsToUpdate.Count);
                    if (!oldLights)
                        oldColors.Add(cellsToUpdate[j].CurrentColor);
                    Singleton<CoreGameManager>.Instance.UpdateLighting(Random.ColorHSV(), cellsToUpdate[j].position);
                    cellsToUpdate.RemoveAt(j);
                }
            }

            yield return new WaitForSeconds(0.2f);
            oldLights = true;
            StartCoroutine(RaveStart());
        }

        public IEnumerator RaveEnd()
        {
            positions = Singleton<BaseGameManager>.Instance.Ec.AllCells();

            for (int i = 0; i < oldColors.Count; i++)
            {
                if (oldColors[i] != Color.black)
                    Singleton<CoreGameManager>.Instance.UpdateLighting(oldColors[i], positions[i].position);
                else
                    Singleton<CoreGameManager>.Instance.UpdateLighting(Color.white, positions[i].position);
                yield return new WaitForSeconds(0.01f);
            }
            oldLights = true;
            oldColors.Clear();
        }
    }
}
