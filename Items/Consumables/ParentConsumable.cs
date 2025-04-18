using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentConsumable : ParentItem
    {
        public bool IsUseableInBattle { get; protected set; }
        public bool IsUseableOnPlayer { get; protected set; }
        public bool IsUseableOnEnemy { get; protected set; }

        protected string _effectDescription;
        public ParentConsumable() { }

        public override void DisplayDescription()
        {
            Console.WriteLine($"{Name}\n" +
                $"Effect: {_effectDescription}\n" +
                $"{_flavourText}\n");
        }
    }
}
