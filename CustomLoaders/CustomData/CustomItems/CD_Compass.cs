/*
using System;
using System.Collections.Generic;
using bbpfer.FundamentalManagers;
using bbpfer.CustomContent.CustomItems;
using UnityEngine;
using PixelInternalAPI.Extensions;

namespace bbpfer.CustomLoaders.CustomData.CustomItems
{

    public class CD_Compass : CustomDataItem
    {
        public override void LoadAllSounds()
        {
            base.LoadAllSounds();
            ITM_Compass compass = (ITM_Compass)item;
            compass.audMan = compass.gameObject.CreateAudioManager(300, 400);
            compass.audMan.AddStartingAudiosToAudioManager<AudioManager>(true, AssetsCreator.CreateSound("CompassLocal", "Items", "*THE COMPASS IS POINTING HERE*", SoundType.Effect, "#FFFFFF", 1));
        }

        public override void PostLoad()
        {
            base.PostLoad();
            ITM_Compass compass = (ITM_Compass)item;
            compass.cooldown = new Cooldown(25, 0, true);
        }
    }
}
*/