using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using bbpfer.Extessions;

namespace bbpfer.FundamentalManagers.ExternalLoaders
{
    public partial class PosterCreator
    {
        public static void Load()
        {
            Posters.exitPoster = CreatePoster(
                "ExitPoster", "Posters",
                new PosterTextData[]
                {
                    new PosterTextData
                    {
                        textKey = "Did you know?",
                        position = new IntVector2(5, 130),
                        size = new IntVector2(300, 100),
                        alignment = TMPro.TextAlignmentOptions.Center,
                        fontSize = 25,
                        color = Color.white,
                        style = TMPro.FontStyles.Bold,
                        font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_36_Pro")
                    },
                    new PosterTextData
                    {
                        textKey = "If you enter a door with a sign nearby that says exit. \nyou exit the game with the \ngame saved!",
                        position = new IntVector2(55, 70),
                        size = new IntVector2(200, 100),
                        alignment = TMPro.TextAlignmentOptions.Center,
                        fontSize = 15,
                        color = Color.white,
                        style = TMPro.FontStyles.Normal,
                        font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_36_Pro")
                    }
                }
            );

            Posters.bisexualityPoster = CreatePoster(
                "BiPoster", "Posters",
                new PosterTextData[]
                {
                    new PosterTextData
                    {
                        textKey = "Therapy!",
                        position = new IntVector2(30, 180),
                        size = new IntVector2(200, 50),
                        fontSize = 26,
                        color = Color.white,
                        style = TMPro.FontStyles.Bold,
                        alignment = TMPro.TextAlignmentOptions.Center,
                        font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_36_Pro")
                    },
                    new PosterTextData
                    {
                        textKey = "Calm down, \nI'm Making a joke",
                        position = new IntVector2(55, 130),
                        size = new IntVector2(150, 75),
                        fontSize = 14,
                        color = Color.white,
                        style = TMPro.FontStyles.SmallCaps,
                        alignment = TMPro.TextAlignmentOptions.Center,
                        font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_18_Pro")
                    }
                }
            ).CreatePoster(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 80, 50, 120, 50, 75 });

            Posters.reflexPoster = CreatePoster(
                "NewDrReflexPoster", "Posters",
                new PosterTextData
                {
                    textKey = "(99) REFLEX-3045",
                    position = new IntVector2(5, 170),
                    size = new IntVector2(250, 38),
                    fontSize = 20,
                    color = Color.black,
                    style = FontStyles.Normal,
                    alignment = TextAlignmentOptions.Center,
                    font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_36_Pro")
                },
                new PosterTextData
                {
                    textKey = "If you want to know if your reflexes aren't great, come to a room with the door *H* written on it!",
                    position = new IntVector2(40, 110),
                    size = new IntVector2(175, 75),
                    fontSize = 12,
                    color = Color.black,
                    style = FontStyles.Normal,
                    alignment = TextAlignmentOptions.Center,
                    font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_18_Pro")
                }
            ).CreatePoster(new string[] { "F2", "F3", "F4", "END" }, new int[] { 120, 100 ,80, 95});

            Posters.zumiaPoster = CreatePoster(
                "hnt_zumia", "Posters",
                new PosterTextData
                {
                    textKey = "Just accept fate, \ndon't try to change it. It would be the worst choice!",
                    position = new IntVector2(55, 50),
                    size = new IntVector2(145, 190),
                    fontSize = 22,
                    color = Color.white,
                    style = FontStyles.Normal,
                    alignment = TextAlignmentOptions.Center,
                    font = FundamentalCodingHelper.FindResourceObjectContainingName<TMP_FontAsset>("COMIC_24_Pro")
                }
            ).CreatePoster(new string[] { "F2", "F3", "F4", "END" }, new int[] { 50, 105, 80, 105 });

            Posters.noobPoster = CreatePoster("NoobPoster", "Posters").CreatePoster(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 95, 100, 120, 130, 105 });
            Posters.mystma12SunglassesPoster = CreatePoster("Mystman12SunglassesPoster", "Posters").CreatePoster(new string[] { "F1", "F2", "F3", "F4", "END" }, new int[] { 65, 80, 75, 100, 80 });

            Posters.secretAdmirer10LikesPoster = CreatePoster("tyfor10likes", "Posters");
            Posters.secretAdimirerLovePlayer = CreatePoster("AdmirerSecretPoster1", "Posters");

            Posters.proibithedGottaSweep = CreatePoster("NoGottaSweep", "Posters");

            Posters.clockPoster = CreatePoster("Clock", "Structures");
        }

        private static PosterObject CreatePoster(string textureName, string textureFolder, params PosterTextData[] textData)
        {
            return new PosterObject
            {
                baseTexture = AssetsCreator.CreateTexture(textureName, textureFolder),
                textData = textData
            };
        }
    }
}

public static class Posters
{
    public static PosterObject exitPoster;
    public static PosterObject bisexualityPoster;
    public static PosterObject reflexPoster;
    public static PosterObject zumiaPoster;
    public static PosterObject tipsPoster;
    public static PosterObject ditheringPoster;
    public static PosterObject noobPoster;
    public static PosterObject altBrunitoPoster;
    public static PosterObject mystma12SunglassesPoster;
    public static PosterObject secretAdmirer10LikesPoster;
    public static PosterObject secretAdimirerLovePlayer;
    public static PosterObject proibithedGottaSweep;

    public static PosterObject teacherChalkboard;
    public static PosterObject spellChalkboard;
    public static PosterObject planeChalkboard;
    public static PosterObject educationChalkboard;

    public static PosterObject clockPoster;
}
