using System;
using UnityEngine;
using System.Collections.Generic;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetTools;
using System.IO;
using bbpfer.Enums;
using System.Linq;


namespace bbpfer.FundamentalManagers
{
    public static class AssetsCreator
    {
        public static Texture2D CreateTexture(string textureName, string folder)
        {
            Texture2D texture = AssetLoader.TextureFromMod(BasePlugin.instance, Path.Combine("Textures", folder, textureName + ".png"));
            assetMan.Add<Texture2D>(textureName, texture);
            return texture;
        }

        public static Texture2D[] CreateTextures(string folder)
        {
            Texture2D[] textures = AssetLoader.TexturesFromMod(BasePlugin.instance, folder, Path.Combine("Textures"));

            foreach (Texture2D tex in textures)
                assetMan.Add<Texture2D>(Path.GetFileNameWithoutExtension(tex.name), tex);
            return textures;
        }

        public static Sprite CreateSprite(string spriteName, string folder, int pixelPerUnit)
        {
            Sprite sprite = AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromMod(BasePlugin.instance, Path.Combine("Textures", folder, spriteName + ".png")), pixelPerUnit);
            assetMan.Add<Sprite>(spriteName, sprite);
            return sprite;
        }

        public static Sprite[] CreateSprites(string folder, int pixelPerUnit)
        {
            Sprite[] sprites = AssetLoader.ToSprites(AssetLoader.TexturesFromMod(BasePlugin.instance, Path.Combine("Textures", folder)), pixelPerUnit);

            foreach (Sprite sprite in sprites)
            {
                assetMan.Add<Sprite>(Path.GetFileNameWithoutExtension(sprite.name), sprite);
                Debug.Log(Path.GetFileNameWithoutExtension(sprite.name));
            }
            return sprites;
        }

        public static List<Sprite> CreateSprites(float pixelsPerUnit, string folder, bool addToAssetMan = true)
        {
            string[] files = Directory.GetFiles(Path.Combine(AssetLoader.GetModPath(BasePlugin.instance), "Textures", folder));
            List<Sprite> sprites = new List<Sprite>();
            for (int i = 0; i < files.Length; i++)
            {
                if (addToAssetMan)
                    Debug.Log(Path.GetFileNameWithoutExtension(files[i]));
                sprites.Add(AssetLoader.SpriteFromTexture2D(AssetLoader.TextureFromFile(files[i]), pixelsPerUnit));

                for (int j = 0; j < sprites.Count; j++)
                    sprites[i].name = Path.GetFileNameWithoutExtension(files[i]);

                if (addToAssetMan)
                    AssetsCreator.assetMan.AddRange<Sprite>(sprites);
            }
            return sprites;
        }

        public static T Get<T>(string name)
        {
            return assetMan.Get<T>(name);
        }

        public static SoundObject CreateSound(string soundName, string folder, string subtitleKey, SoundType type, string color, int vauleMultiplier, params SubtitleTimedKey[] stk)
        {
            Color _color = Color.white;
            ColorUtility.TryParseHtmlString(color, out _color);
            SoundObject sound = ObjectCreators.CreateSoundObject(AssetLoader.AudioClipFromMod(BasePlugin.instance, Path.Combine("Audio", folder, soundName + ".wav")), subtitleKey, type, _color);
            sound.additionalKeys = stk;
            if (subtitleKey == "")
                sound.subtitle = false;

            sound.volumeMultiplier = vauleMultiplier;
            assetMan.Add<SoundObject>(soundName, sound);
            return sound;
        }

        public static void CreateTileTexture(string[] floors, string textureName, RoomGroupTextures[] groups, TileTextures type, int weight)
        {
            Texture2D texture = AssetsCreator.CreateTexture(textureName, "CellTextures");

            for (int j = 0; j < groups.Length; j++)
            {
                foreach (string data in floors)
                {
                    switch (type)
                    {
                        case TileTextures.Wall:
                            if (groups[j] == RoomGroupTextures.Hall) FundamentalManager.GetFloorByName(data).wallHallTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Class) FundamentalManager.GetFloorByName(data).wallClassTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Faculty) FundamentalManager.GetFloorByName(data).wallFacultyTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Storage) FundamentalManager.GetFloorByName(data).wallStorageTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            break;
                        case TileTextures.Floor:
                            if (groups[j] == RoomGroupTextures.Hall) FundamentalManager.GetFloorByName(data).floorHallTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Class) FundamentalManager.GetFloorByName(data).floorClassTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Faculty) FundamentalManager.GetFloorByName(data).floorFacultyTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Storage) FundamentalManager.GetFloorByName(data).floorStorageTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            break;
                        case TileTextures.Ceiling:
                            if (groups[j] == RoomGroupTextures.Hall) FundamentalManager.GetFloorByName(data).ceilingHallTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Class) FundamentalManager.GetFloorByName(data).ceilingClassTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Faculty) FundamentalManager.GetFloorByName(data).ceilingFacultyTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            if (groups[j] == RoomGroupTextures.Storage) FundamentalManager.GetFloorByName(data).ceilingStorageTextures.Add(new WeightedTexture2D { selection = texture, weight = weight });
                            break;
                    }
                }
            }
        }

        public static string LoadMidi(string midiName)
        {
            string midi = null;
            midi = AssetLoader.MidiFromMod(midiName, BasePlugin.instance, Path.Combine("Midi", midiName + ".midi"));
            assetMan.Add<string>(midiName, midi);
            return midi;
        }

        public static AssetManager assetMan = new AssetManager();
    }
}
