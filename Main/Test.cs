using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            level.ChangeCurrentRoom(new DefaultRoom(difficulty));
        }

        public static void SetupTestInventory(Player player)
        {
            player.PickUpItem(new TesterSword());
            player.PickUpItem(new TesterHelm());
            player.PickUpItem(new TesterPotion());
            player.PickUpItem(new TesterPotion());
        }

        public static void TestItemGenerationAndContainers()
        {
            List<ParentItem> itemList = new List<ParentItem>();

            for (int i = 0; i < 10; i++)
            {
                ParentItem newItem = LookUp.GenerateRandomItem(-1);
                Debug.Assert(newItem != null, "WARNING!! some error has occured when trying to get a random item");
                itemList.Add(LookUp.GenerateRandomItem(-1));
            }
            Console.WriteLine("random item generation test complete");

            Console.WriteLine("now testing container item generation");
            ParentContainer container = new TestContainer();

            Debug.Assert(container.Items.Length != 0, "WARNING!! container items not populated correctly");
            Console.WriteLine($"container should contain {container.Items.Length} items...");
            foreach (ParentItem item in container.Items)
            {
                Debug.Assert(item != null, "WARNING!! some error has occured when trying to get a random item");
                Console.WriteLine(item.Name);
            }
        }
    }
}
