using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class StrengthPotion : ParentConsumable
    {
        public StrengthPotion()
        {
            Name = "Strength Potion";
            Category = "Consumable";
            _effectDescription = "adds +5 damage for 5 turns";
            _flavourText = "a weird orange liquid with sediment at the bottom";
        }

        public override void UseItem(LivingEntity entity)
        {
            StatusInteractions.ApplyStatusTo(entity, StatusIds.Strength, 5);
            Console.WriteLine($"the item was used on {entity.Name} and they gained +5 damage" +
                $"for 5 turns\n");
        }
    }
}
