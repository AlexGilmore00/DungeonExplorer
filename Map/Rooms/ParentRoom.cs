using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class ParentRoom
    // a class for all room types to inherit from
    // all public functions and fields of an room should be implemented in here
    {
        public ParentEnemy[] Enemies { get; protected set; }
        public ParentContainer[] Containers { get; protected set; }

        protected string _flavourText;

        public void DisplayDescription()
        // print the flavour text of the current room as well as and information reguarding
        // enemies, containers, or items
        // should eventually also print connections to other rooms once levels are properly implemented
        {
            Console.WriteLine(_flavourText);
            //display enemy list if necesary
            if (Enemies.Length > 0)
            {
                Console.WriteLine($"this room contains {Enemies.Count()} enemies:");
                foreach (var enemy in Enemies)
                {
                    Console.WriteLine(enemy.Name);
                }
            }
            //display container list if necesarry
            if (Containers.Length > 0)
            {
                Console.WriteLine($"this room contains {Containers.Count()} containers:");
                foreach (var container in Containers)
                {
                    Console.WriteLine(container.Name);
                }
            }
            Console.WriteLine();
        }
    }
}
