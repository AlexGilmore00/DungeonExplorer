using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{

    public class ParentContainer
    // a class for all container types to inherit from
    // all public functions and fields of an container should be implemented in here
    {
        public static Random Rnd = new Random();
        public string Name { get; protected set; }
        public ParentItem[] Items { get; protected set; }

        public ParentContainer() { }

        protected void PopulateItems(int itemPool)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = LookUp.GenerateRandomItem(itemPool);
            }
        }
    }
}
