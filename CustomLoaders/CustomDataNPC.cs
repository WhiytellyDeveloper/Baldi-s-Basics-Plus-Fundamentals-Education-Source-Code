using System;
using System.Collections.Generic;
using bbpfer.Extessions;
using MTM101BaldAPI.Components;
using UnityEngine;
using bbpfer.FundamentalManagers;

namespace bbpfer.CustomLoaders
{
    public interface CustomDataNPC : BaseCustomData
    {
        void InGameSetup();
    }
}
