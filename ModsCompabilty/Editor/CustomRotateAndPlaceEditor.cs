using System;
using System.Collections.Generic;
using BaldiLevelEditor;
using UnityEngine;
using bbpfer.FundamentalManagers;

namespace bbpfer.ModsCompabilty.Editor
{
    public class CustomRotateAndPlaceEditor : RotateAndPlacePrefab
    {
        public override Sprite editorSprite
        {
            get
            {
                return AssetsCreator.Get<Sprite>("Object_" + poster);
            }
        }

        public string poster;

        public CustomRotateAndPlaceEditor(string poster) : base(poster) 
        {
            this.poster = poster;
        }
    }
}
