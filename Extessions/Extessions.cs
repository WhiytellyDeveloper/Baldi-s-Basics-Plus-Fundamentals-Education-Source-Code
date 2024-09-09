using System;
using MTM101BaldAPI.ObjectCreation;
using System.Collections.Generic;
using bbpfer.CustomRooms;
using bbpfer.FundamentalManagers;
using bbpfer.CustomLoaders;
using bbpfer.CustomContent.CustomItems;
using UnityEngine;
using BaldiLevelEditor;
using PlusLevelLoader;
using PlusLevelFormat;
using MTM101BaldAPI;
using MTM101BaldAPI.Components;

namespace bbpfer.Extessions
{
    public static class Extessions
    {
        public static RandomEvent CreateEvent(this RandomEvent _event, string[] floors, int[] weights)
        {
            int weight = 0;
            foreach (string data in floors)
            {
                FundamentalManager.GetFloorByName(data).randomEvents.Add(new WeightedRandomEvent { selection = _event, weight = weights[weight] });
                weight++;
            }

                return _event;
        }

        public static ItemObject CreateItem(this ItemObject _item, string[] floors, int[] weights)
        {
            int weight = 0;
            foreach (string data in floors)
            {
                FundamentalManager.GetFloorByName(data).items.Add(new WeightedItemObject { selection = _item, weight = weights[weight] });
                weight++;
            }

            return _item;
        }


        public static ItemObject CreateItem(this ItemObject _item, string[] floors)
        {
            foreach (string data in floors)  
                FundamentalManager.GetFloorByName(data).forcedItems.Add(_item);
           
            return _item;
        }

        public static ItemObject AddIntoShop(this ItemObject _item, string[] floors, int[] weights)
        {
            int weight = 0;
            foreach (string data in floors)
            {
                FundamentalManager.GetFloorByName(data).shopItems.Add(new WeightedItemObject { selection = _item, weight = weights[weight] });
                weight++;
            }

            return _item;
        }


        public static ItemObject InitializeItem(this ItemObject _item)
        {
            if (_item.item.gameObject.GetComponent<CustomDataItem>() == null)
            {
                Debug.LogError(_item);
                return _item;
            }

            var item = _item.item.gameObject.GetComponent<CustomDataItem>();
            item.Setup();
            return _item;
        }

        public static ItemBuilder SetSprites(this ItemBuilder builder, string itemPath)
        {
            builder.SetSprites(AssetsCreator.CreateSprite(itemPath + "_Small", "Items", 100), AssetsCreator.CreateSprite(itemPath + "_Large", "Items", 50));
            return builder;
        }

        public static void Unreveal(this MapTile map)
        {
            FundamentalCodingHelper.SetValue<bool>(map, "found", false);
            map.gameObject.SetActive(false);
        }

        public static void AddStructure(this ObjectBuilder structure, string[] floors, int[] weights)
        {
            if (weights != null)
            {
                int weight = 0;
                foreach (string data in floors)
                {
                   // FundamentalManager.GetFloorByName(data).shopItems.Add(new WeightedItemObject { selection = _item, weight = weights[weight] });
                    weight++;
                }
            }
            else
            {
                foreach (string data in floors)
                {
                    FundamentalManager.GetFloorByName(data).forcedObjectBuilders.Add(structure);
                }
            }
        }

        public static PosterObject CreatePoster(this PosterObject poster, string[] floors, int[] weights)
        {
            int weight = 0;
            foreach (string data in floors)
            {
                FundamentalManager.GetFloorByName(data).posters.Add(new WeightedPosterObject { selection = poster, weight = weights[weight] });
                weight++;
            }

            return poster;
        }

        public static BoxCollider AddBoxCollider(this GameObject g, Vector3 center, Vector3 size, bool isTrigger)
        {
            var c = g.AddComponent<BoxCollider>();
            c.center = center;
            c.size = size;
            c.isTrigger = isTrigger;
            return c;
        }

        public static void ResizeCollider(this SpriteRenderer spriteRenderer, Collider colliderComponent)
        {
            Vector3 spriteSize = spriteRenderer.bounds.size;
            if (colliderComponent is BoxCollider)
            {
                ((BoxCollider)colliderComponent).size = spriteSize;
            }
            else if (colliderComponent is CapsuleCollider)
            {
                CapsuleCollider capsuleCollider = (CapsuleCollider)colliderComponent;
                float radius = Mathf.Max(spriteSize.x, spriteSize.y) / 2f;
                float height = spriteSize.z;
                capsuleCollider.radius = radius;
                capsuleCollider.height = height;
            }
            else if (colliderComponent is SphereCollider)
            {
                float radius = Mathf.Max(spriteSize.x, Mathf.Max(spriteSize.y, spriteSize.z)) / 2f;
                ((SphereCollider)colliderComponent).radius = radius;
            }

            else
            {
                Debug.LogError("O tipo de Collider não é suportado por este script!");
            }
        }

        public static void SetSpeed(this Navigator nav, int speed, int maxSpeed)
        {
            nav.SetSpeed(speed);
            nav.maxSpeed = maxSpeed;
        }

        public static void RotateSmoothlyToNextPoint(this Transform transform, Vector3 nextPoint, float speed)
        {
            Vector3 vector = Vector3.RotateTowards(transform.forward, (nextPoint - transform.position).normalized, Time.deltaTime * 2f * Mathf.PI * speed, 0f);
            if (vector != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(vector, Vector3.up);
        }

        public static void RotateContinuously(this Transform transform, float speed) =>
            transform.Rotate(Vector3.up, Time.deltaTime * 2f * Mathf.PI * speed);

        
        public static ObjectBuilder InitializeBuilder(this ObjectBuilder _structure)
        {
            _structure.GetComponent<CustomDataStucture>().Setup();
            return _structure;
        }
        

        public static Material ReCreateMaterial(Material mat, Texture2D tex)
        {
            Material _mat = new Material(mat);
            mat.mainTexture = tex;
            return _mat;
        }

        public static void AddToEditor(this GameObject obj, string name, Vector3 offset)
        {
            PlusLevelLoaderPlugin.Instance.prefabAliases.Add(name, obj);
            BaldiLevelEditor.BaldiLevelEditorPlugin.editorObjects.Add(EditorObjectType.CreateFromGameObject<EditorPrefab, PrefabLocation>(name, obj, offset));
            obj.ConvertToPrefab(true);
        }

        public static T AddRoomFunctionToContainer<T>(this RoomFunctionContainer asset) where T : RoomFunction
        {
            var fun = asset.GetComponent<T>();
            if (!fun)
            {
                fun = asset.gameObject.AddComponent<T>();
                asset.AddFunction(fun);
            }
            return fun;
        }
        public static Sprite MergeWithOverlay(this Sprite baseSprite, Sprite overlaySprite)
        {
            if (baseSprite == null || overlaySprite == null)
            {
                Debug.LogError("Base or Overlay Sprite is not assigned.");
                return null;
            }

            if (baseSprite.rect.size != overlaySprite.rect.size)
            {
                Debug.LogError("Base and Overlay sprites must have the same size and rectangle.");
                return null;
            }

            Texture2D baseTexture = baseSprite.texture;
            Texture2D overlayTexture = overlaySprite.texture;

            Rect spriteRect = baseSprite.rect;
            int width = (int)spriteRect.width;
            int height = (int)spriteRect.height;

            Texture2D mergedTexture = new Texture2D(width, height) {
                filterMode = FilterMode.Point
            };

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int baseX = x + (int)spriteRect.x;
                    int baseY = y + (int)spriteRect.y;

                    Color baseColor = baseTexture.GetPixel(baseX, baseY);
                    Color overlayColor = overlayTexture.GetPixel(baseX, baseY);
                    Color mergedColor = Color.Lerp(baseColor, overlayColor, overlayColor.a);

                    mergedTexture.SetPixel(x, y, mergedColor);
                }
            }

            mergedTexture.Apply();

            Sprite mergedSprite = Sprite.Create(
                mergedTexture,
                new Rect(0, 0, width, height),
                new Vector2(0.5f, 0.5f),
                baseSprite.pixelsPerUnit
            );

            return mergedSprite;
        }

        public static Sprite ChangeColorToDominant(this Sprite originalSprite, Color referenceColor, Color colorToReplace)
        {
            Texture2D texture = GameObject.Instantiate(originalSprite.texture) as Texture2D;
            texture.filterMode = FilterMode.Point;

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color pixelColor = texture.GetPixel(x, y);
                    if (pixelColor.Equals(referenceColor))       
                        texture.SetPixel(x, y, colorToReplace);
                 
                }
            }

            texture.Apply();

            return Sprite.Create(texture, originalSprite.rect, new Vector2(0.5f, 0.5f));
        }

        public static Color GetMiddlePixelColor(this Sprite sprite)
        {
            Texture2D texture = sprite.texture;

            int midX = texture.width / 2;
            int midY = texture.height / 2;

            return texture.GetPixel(midX, midY);
        }

        public static AudioManager getAudMan(this NPC npc, string _color)
        {
            if (npc.GetComponent<AudioManager>() != null)
            {
                Color color = Color.white;
                ColorUtility.TryParseHtmlString(_color, out color);
                FundamentalCodingHelper.SetValue<bool>(npc.GetComponent<AudioManager>(), "overrideSubtitleColor", true);
                FundamentalCodingHelper.SetValue<Color>(npc.GetComponent<AudioManager>(), "subtitleColor", color);
                return npc.GetComponent<AudioManager>();
            }
            return null;
        }

        public static CustomSpriteAnimator AddAnimatorSprite(this SpriteRenderer renderer)
        {
            CustomSpriteAnimator animator = renderer.gameObject.AddComponent<CustomSpriteAnimator>();
            renderer.gameObject.GetComponent<CustomSpriteAnimator>().spriteRenderer = renderer;
            return animator;
        }
    }
}
