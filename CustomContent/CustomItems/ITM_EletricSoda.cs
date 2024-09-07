using UnityEngine;
using System.Collections.Generic;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_EletricSoda : Item
    {
        public override bool Use(PlayerManager pm)
        {
            /*
           this.pm = pm;
           transform.position = pm.transform.position;
           transform.forward = Singleton<CoreGameManager>.Instance.GetCamera(0).transform.forward;
           entity.Initialize(pm.ec, transform.position);
           sprite.SetSpriteRotation(Random.Range(0f, 360f));
           entity.SetGrounded(false);
           cooldown.Initialize();
           initialPosition = new Vector3(pm.ec.CellFromPosition(transform.position).TileTransform.position.x, pm.transform.position.y, pm.ec.CellFromPosition(transform.position).TileTransform.position.z);
           entity.OnEntityMoveInitialCollision += (hit) =>      
               Destroy();
                        */
            return true;
        }

        private void Update()
        {
            /*
            entity.UpdateInternalMovement(transform.forward * 40 * pm.ec.EnvironmentTimeScale);

            Vector3 cellPosition = new Vector3(pm.ec.CellFromPosition(transform.position).TileTransform.position.x, pm.transform.position.y, pm.ec.CellFromPosition(transform.position).TileTransform.position.z);
                            
            if (!groundedEletricitys.ContainsKey(cellPosition) && cellPosition != initialPosition && eletricityInGround != limit)
            {
                GroundEletricity grel =  Instantiate<GroundEletricity>(eletricity);
                grel.transform.position = cellPosition;
                grel.entity = grel.GetComponent<Entity>();
                grel.entity.Initialize(pm.ec, transform.position);
                grel.transform.SetParent(pm.ec.transform);
                groundedEletricitys.Add(cellPosition, grel);
                eletricityInGround++;
                
            }
            */
        }
        
        public void Destroy() =>
            Destroy(base.gameObject);

        public SpriteRenderer sprite;
        public Entity entity;
         /*
        public GroundEletricity eletricity;
        */
        public Cooldown cooldown;
        /*
        public Dictionary<Vector3, GroundEletricity> groundedEletricitys = new Dictionary<Vector3, GroundEletricity>();
        */
        public Vector3 initialPosition;
        public int limit = 5;
        public int eletricityInGround;
    }
}
