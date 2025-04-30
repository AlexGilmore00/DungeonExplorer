using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentConsumable : ParentItem, INeedsDescription
    {
        public bool IsUseableInBattle { get; protected set; }
        public bool IsUseableOnPlayer { get; protected set; }
        public bool IsUseableOnEnemy { get; protected set; }

        protected string _effectDescription;
        public ParentConsumable() { }

        public void DisplayDescription()
        {
            Console.WriteLine($"{Name}\n" +
                $"Effect: {_effectDescription}\n" +
                $"{_flavourText}\n");
        }

        public abstract void UseItem(LivingEntity entity);
    }
}
