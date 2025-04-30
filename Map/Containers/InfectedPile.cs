using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class InfectedPile : ParentContainer
    {
        public InfectedPile()
        {
            Name = "Infected Pile";
            // set up the number of items in the container
            Items = new List<ParentItem>();
            PopulateItems(Rnd.Next(1, 4), 6);
        }
    }
}
