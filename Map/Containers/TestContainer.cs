using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class TestContainer : ParentContainer
    {
        public TestContainer()
        {
            Name = "Test Conatainer";
            // set up the number of items in the container
            Items = new ParentItem[Rnd.Next(1, 4)];
            PopulateItems(-1);
        }
    }
}
