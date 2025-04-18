using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class TesterPotion : ParentConsumable
    {
        public TesterPotion()
        {
            Name = "TesterPotion";
            Category = "Consumable";
            _effectDescription = "heals 15 hp";
            _flavourText = "a potion used for testing";
        }

        public override void UseItem(LivingEntity entity)
        {
            entity.TakeDamage(-15);
            Console.WriteLine($"the item was used on {entity.Name} and they gained 15 hp\n" +
                $"{entity.Name} is at {entity.Health}/{entity.MaxHealth}\n");
        }
    }
}
