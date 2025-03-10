﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Test
    {
        // class used for static function calls to test implementation

        public static void TestRoomGeneration(int difficulty)
        {
            ParentRoom room = new DefaultRoom(difficulty);
            // see if enemies are added to the Enemies array correctly
            Console.WriteLine($"there should be {room.Enemies.Length} enemies...");
            foreach (ParentEnemy enemy in room.Enemies)
            {
                Console.WriteLine(enemy.Name);
            }

            // see if containers are added to the Containers array correctly
            Console.WriteLine($"there should be {room.Containers.Length} containers...");
            foreach (ParentContainer container in room.Containers)
            {
                Console.WriteLine(container.Name);
            }

            // test display description method
            Console.WriteLine("\n testing Room.DisplayDescription \n");
            room.DisplayDescription();
        }

        public static void GenerateNewRoom(Level level, int difficulty)
        {
            level.ChanceCurrentRoom(new DefaultRoom(difficulty));
        }

        public static void SetupTestInventory(Player player)
        {
            player.PickUpItem(new TesterSword());
            player.PickUpItem(new TesterHelm());
            player.PickUpItem(new TesterPotion());
            player.PickUpItem(new TesterPotion());
        }
    }
}
