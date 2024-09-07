using UnityEngine;

namespace bbpfer.CustomContent.CustomItems
{
    public class ITM_Traumatized : Item, IEntityTrigger
    {
        public override bool Use(PlayerManager pm)
        {
            cell = pm.ec.CellFromPosition(pm.transform.position);
            transform.position = new Vector3(cell.TileTransform.position.x, 0, cell.TileTransform.position.z);
            entity.Initialize(pm.ec, transform.position);
            foreach (Direction dir in Directions.All())
                cell.Block(dir, true);
            return true;
        }

        public void EntityTriggerEnter(Collider other)
        {
            if (other.CompareTag("NPC"))
            {
                other.GetComponent<NPC>().DestinationEmpty();
                other.GetComponent<Entity>().AddForce(new Force((other.GetComponent<Entity>().transform.position - transform.position).normalized, 7, -7));
            }
        }
        public void EntityTriggerStay(Collider other)
        {
        }
        public void EntityTriggerExit(Collider other)
        {
        }

        public Cell cell;
        public Entity entity;
    }
}
