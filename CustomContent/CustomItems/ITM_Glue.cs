using System.Collections;
using UnityEngine;
using bbpfer.FundamentalManagers;
using PixelInternalAPI.Extensions;
using PixelInternalAPI.Classes;
using bbpfer.CustomLoaders;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Glue : Item, IEntityTrigger, CustomDataItem
	{
		public void Setup()
		{
			sprite = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.Get<Sprite>("Glue_Large"));
			sprite.gameObject.layer = LayerStorage.billboardLayer;
			sprite.transform.SetParent(transform);
			gameObject.layer = LayerStorage.standardEntities;
			entity = gameObject.CreateEntity(2, 2, sprite.transform);

			glueRenderer = ObjectCreationExtensions.CreateSpriteBillboard(AssetsCreator.CreateSprite("Glue", "Items", 30), false);
			glueRenderer.gameObject.layer = LayerStorage.billboardLayer;
			glueRenderer.transform.SetParent(transform);
			glueRenderer.transform.localPosition = new Vector3(0, -4.99f, 0);
			glueRenderer.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
			glueRenderer.gameObject.SetActive(false);

			audMan = gameObject.CreatePropagatedAudioManager(100, 210);

			stickySound = AssetsCreator.CreateSound("StickyGlue", "Items", "Sfx_Glue", SoundType.Effect, "#FFFFFF", 1);
			throwSound = AssetsCreator.Get<SoundObject>("TrowPretzel");
		}

		public override bool Use(PlayerManager pm)
        {
			this.pm = pm;
			cooldown = new Cooldown(30, 0, true);
			moveMod = new MovementModifier(Vector3.zero, 0.15f);
			cooldown.Initialize();
			transform.position = pm.transform.position;
			entity.Initialize(pm.ec, transform.position);
			StartCoroutine(Fall());
			audMan.PlaySingle(throwSound);
			return true;
        }

		private void Update()
        {
			if (move)
				entity.UpdateInternalMovement(Singleton<CoreGameManager>.Instance.GetCamera(0).transform.forward * 18 * pm.ec.EnvironmentTimeScale);
			else
				entity.SetFrozen(true);
        }

		private IEnumerator Fall()
		{
			move = true;
			yield return new WaitForSeconds(0.3f);
			move = false;
			audMan.PlaySingle(throwSound);
			sprite.flipY = true;
			sprite.transform.localPosition = Vector3.zero;
			float fallSpeed = 10f;
			while (true)
			{
				fallSpeed -= pm.ec.EnvironmentTimeScale * Time.deltaTime * 36f;
				sprite.transform.localPosition += Vector3.up * fallSpeed * Time.deltaTime;
				if (sprite.transform.localPosition.y <= fallLimit)
				{
					sprite.transform.localPosition = Vector3.up * fallLimit;
					break;
				}
				yield return null;
			}
			glueRenderer.gameObject.SetActive(true);
			sprite.gameObject.SetActive(false);
			StartCoroutine(Spawn());
			yield break;
		}

		private IEnumerator Spawn()
		{
			audMan.PlaySingle(stickySound);
			placed = true;
			float sizeMult = 0f;
			glueRenderer.transform.position = new Vector3(pm.ec.CellFromPosition(transform.position).TileTransform.position.x, glueRenderer.transform.position.y, pm.ec.CellFromPosition(transform.position).TileTransform.position.z);
			Vector3 ogSize = glueRenderer.transform.localScale;
			while (true)
			{
				sizeMult += pm.ec.EnvironmentTimeScale * Time.deltaTime * 1.9f;
				if (sizeMult >= 1f)
					break;
				glueRenderer.transform.localScale = ogSize * sizeMult;
				yield return null;
			}
			glueRenderer.transform.localScale = ogSize;
			yield break;
		}


		public void EntityTriggerEnter(Collider other)
		{
			if ((other.CompareTag("NPC") || other.CompareTag("Player")))
			{
				if (placed && other.GetComponent<ActivityModifier>())
				{
					audMan.PlaySingle(stickySound);
					other.GetComponent<ActivityModifier>().moveMods.Add(moveMod);
				}
			}
		}

		public void EntityTriggerStay(Collider other)
		{
		}

		public void EntityTriggerExit(Collider other)
		{
			if ((other.CompareTag("NPC") || other.CompareTag("Player")))
			{
				if (placed && other.GetComponent<ActivityModifier>())
					other.GetComponent<ActivityModifier>().moveMods.Remove(moveMod);
			}
		}

		public SpriteRenderer sprite;
		public SpriteRenderer glueRenderer;
		public Entity entity;
		public float fallLimit = -4f;
		private bool move;
		public bool placed;
		public MovementModifier moveMod;
		public AudioManager audMan;
		public SoundObject stickySound;
		public SoundObject throwSound;
		public Cooldown cooldown;
	}
}
