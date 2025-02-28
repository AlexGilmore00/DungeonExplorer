using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class LookUp
    {
        // this class will be used for static function calls when looking up things
        // such as an items type/damage/effects or looking up a random enemy type 
        // or room description

        private static Random rnd = new Random();
        public LookUp() { }

        public static LivingEntity GenerateRandomEnemy()
        {
            return new TestEnemy();
        }

        public static ParentContainer GenerateRandomContainer()
        {
            return new TestContainer();
        }
    }
}
