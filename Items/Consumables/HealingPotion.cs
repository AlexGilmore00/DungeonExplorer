using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class HealingPotion : ParentConsumable
    {
        public HealingPotion()
        {
            Name = "Healing Potion";
            Category = "Consumable";
            _effectDescription = "heals 15 hp";
            _flavourText = "a red liquid in a small glass vial";
        }

        public override void UseItem(LivingEntity entity)
        {
            if (entity is Player)
            {
                Player player = (Player)entity;
                player.TakeDamage(-15);
            }
            else if (entity is ParentEnemy)
            {
                ParentEnemy enemy = (ParentEnemy)entity;
                enemy.TakeDamage(-15);
            }
            Console.WriteLine($"the item was used on {entity.Name} and they gained 15 hp\n" +
                $"{entity.Name} is at {entity.Health}/{entity.MaxHealth}\n");
        }
    }
}
