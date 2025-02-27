using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LivingEntity
    {
        public string Name { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Health { get; protected set; }
        public LivingEntity()
        {
        }
    }
}
