using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentRoom
    // a class for all room types to inherit from
    // all public functions and fields of an room should be implemented in here
    {
        public ParentEnemy[] Enemies { get; protected set; }
        public ParentContainer[] Containers { get; protected set; }
        public HashSet<char> Connections { get; protected set; }

        protected string _flavourText;
        protected HashSet<char> _validConnections;

        public ParentRoom()
        {
            Connections = new HashSet<char>();
            _validConnections = new HashSet<char> { 'N', 'E', 'S', 'W' };
        }

        public void DisplayDescription()
        // print the flavour text of the current room as well as and information reguarding
        // enemies, containers, or items
        // should eventually also print connections to other rooms once levels are properly implemented
        {
            Console.WriteLine(_flavourText);

            // display directions of connecting rooms
            Console.WriteLine("this room contains connections to:");
            foreach (char c in Connections)
            {
                switch (c)
                {
                    case 'N':
                        Console.WriteLine("N - a room to the North");
                        break;
                    case 'E':
                        Console.WriteLine("E - a room to the East");
                        break;
                    case 'S':
                        Console.WriteLine("S - a room to the South");
                        break;
                    case 'W':
                        Console.WriteLine("W - a room to the West");
                        break;
                    case 'D':
                        Console.WriteLine("D - a way down?");
                        break;
                }
            }

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
        protected void PopulateEnemies(int difficulty)
        // fill the rooms Enemies param with random enemy classes
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = LookUp.GenerateRandomEnemy(difficulty);
            }
        }

        protected void PopulateContainers()
        // fill the rooms Containers param with random containers
        {
            for (int i = 0; i < Containers.Length; i++)
            {
                Containers[i] = LookUp.GenerateRandomContainer();
            }
        }

        public void AddConnection(char connection)
        // add a room connection
        {
            if (!_validConnections.Contains(connection))
            {
                Console.WriteLine("WARNING!! an invalid character has been given when adding a " +
                    "connection the a room. no default connection has been added meaning this room" +
                    "may be missing a connection");
            }
            else
            {
                Connections.Add(connection);
            }
        }
    }
}
