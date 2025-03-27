using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class ParentConsumable : ParentItem
    {
        public bool IsUseableInBattle { get; protected set; }
        public bool IsUseableOnPlayer { get; protected set; }
        public bool IsUseableOnEnemy { get; protected set; }
        public ParentConsumable() { }
    }
}
