using System;
using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI.ObjectCreation;
using bbpfer.CustomContent.CustomEvents;
using bbpfer.Extessions;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
    public partial class EventCreator
    {
        public static void Load()
        {
            SoundObject raveVoiceline = AssetsCreator.CreateSound("BAL_Event_Rave", "Characters", "Wait, did you activate my rave machine?", SoundType.Voice, "#0EE716", 1);
            raveVoiceline.additionalKeys = new SubtitleTimedKey[]
            {
                new SubtitleTimedKey
                {
                    key = "Now I can't see anything clearly.",
                    time = 3.7f
                }
            };

            SoundObject giftVoiceline = AssetsCreator.CreateSound("BAL_Event_SurpriseGift", "Characters", "Hey everyone, there's a gift in a random room!", SoundType.Voice, "#0EE716", 1);
            giftVoiceline.additionalKeys = new SubtitleTimedKey[]
            {
                new SubtitleTimedKey
                {
                    key = "Get it before I get you!",
                    time = 3.937f
                }
            };

            RandomEvent rave = new RandomEventBuilder<RaveEvent>(BasePlugin.instance.Info)
            .SetName("CustomEvent_Rave")
            .SetEnum("Rave")
            .SetMeta(MTM101BaldAPI.Registers.RandomEventFlags.None)
            .SetSound(raveVoiceline)
            .SetMinMaxTime(45, 62)
            .Build()
            .CreateEvent(new string[] { "F2", "F3", "F4", "END" }, new int[] { 62, 78, 80, 98 });

            RandomEvent supriseGift = new RandomEventBuilder<SupriseGiftEvent>(BasePlugin.instance.Info)
            .SetName("CustomEvent_SupriseGift")
            .SetEnum("SupriseGift")
            .SetMeta(MTM101BaldAPI.Registers.RandomEventFlags.None)
            .SetSound(giftVoiceline)
            .SetMinMaxTime(38, 50)
            .Build()
            .CreateEvent(new string[] {"F2", "F3", "F4", "END" }, new int[] { 90, 50, 23, 75 });

        }
    }
}
