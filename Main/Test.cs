using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Test
    {
        // class used for static function calls to test implementation

        // !!add a RunMainTests function that instanciates a game object and from there
        // runs main tests on all functions to see if inputs will be ok and all errors
        // handled. output the reports of these tests into a text file!!

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

        public static void SetupTestInventory(Player player)
        {
            player.PickUpItem(new TesterHelm());
            player.PickUpItem(new TesterPotion());
            player.PickUpItem(new TesterPotion());

            for (int i = 0; i < 25; i++)
            {
                player.PickUpItem(new TesterSword());
            }
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

            Debug.Assert(container.Items.Count != 0, "WARNING!! container items not populated correctly");
            Console.WriteLine($"container should contain {container.Items.Count} items...");
            foreach (ParentItem item in container.Items)
            {
                Debug.Assert(item != null, "WARNING!! some error has occured when trying to get a random item");
                Console.WriteLine(item.Name);
            }
        }

        public static void TestLevelGeneration()
        {
            for (int difficulty = 0; difficulty < 3; difficulty++)
            {
                for (int k = 1; k <= 10; k++)
                {
                    Console.WriteLine($"GENERATING level num {k} at difficulty {difficulty}");
                    Level level = new Level(difficulty);
                    ShowCurrentLevelLayout(level);

                    int[] roomInfo = level.GetRoomCountInfo();
                    Console.WriteLine($"\nroom count = {roomInfo[0]}\n" +
                        $"excess rooms = {roomInfo[1]}");
                }
            }
        }

        public static void ShowCurrentLevelLayout(Level level, ParentRoom currentRoom = null)
        {
            ParentRoom[,] levelLayout = level.LevelLayout;

            // display level layout as well as number of rooms
            for (int i = 0; i < levelLayout.GetLength(0); i++)
            {
                Console.Write($"{i}    ");
                for (int j = 0; j < levelLayout.GetLength(0); j++)
                {
                    string room;
                    if (currentRoom != null && levelLayout[i, j] == currentRoom)
                        room = "[ x ]";
                    else if (levelLayout[i, j] != null)
                        room = "[   ]";
                    else
                        room = "Null ";
                    Console.Write($"{room}");
                }
                Console.WriteLine();
            }

            Console.Write("     ");
            for (int i = 0; i < levelLayout.GetLength(0); i++)
            {
                Console.Write($"{i}    ");
            }
        }

        public static void TestEquipAndUnequip()
        {
            Console.WriteLine("TESTING EQUIPPING AND UNEQUIPPING ITEMS");

            // set up objects
            int baseDmg = 10;
            int baseDef = 5;
            Player player = new Player("TesterMan", 100, baseDmg, baseDef);

            ParentWeapon sword = new TesterSword();
            ParentWeapon shield = new TesterShield();
            ParentWeapon zwei = new TesterZwei();
            ParentArmour helm = new TesterHelm();

            //
            // try equip individual pieces
            //

            List<ParentWeapon> weps = new List<ParentWeapon> { sword, zwei, shield, };
            Dictionary<string, ParentWeapon> eqWeps = new Dictionary<string, ParentWeapon>();
            Dictionary<string, ParentArmour> eqArms = new Dictionary<string, ParentArmour>();

            // weapons
            foreach (ParentWeapon wep in weps)
            {
                Console.WriteLine($"trying to equip {wep.Name} in {wep.Slot} where Two Handed = {wep.IsTwoHanded}");
                // equip item
                player.TryEquipItem(wep);
                Debug.Assert(player.CurrentAtkDmg == baseDmg + wep.Attack && player.CurrentDefence == baseDef + wep.Defence,
                    $"WARNING!! error when adding stats of {wep.Name} to player. atk:{player.CurrentAtkDmg}," +
                    $" def:{player.CurrentDefence}");
                eqWeps = player.GetEquippedWeapons();
                if (wep.IsTwoHanded)
                    Debug.Assert(eqWeps["Rhand"] == wep && eqWeps["Lhand"] == wep, $"WARNING!! error when equipping" +
                        $"{wep.Name} to players equipped dict");
                else
                    Debug.Assert(eqWeps[wep.Slot] == wep, $"WARNING!! error when equipping" +
                        $"{wep.Name} to players equipped dict");
                // uniquip item
                player.UnequipItem(wep.Slot);
                eqWeps = player.GetEquippedWeapons();
                Debug.Assert(player.CurrentAtkDmg == baseDmg && player.CurrentDefence == baseDef,
                    $"WARNING!! error when removing stats of {wep.Name} when unequipping it." +
                    $"atk:{player.CurrentAtkDmg}, def:{player.CurrentDefence}");
                if (wep.IsTwoHanded)
                    Debug.Assert(eqWeps["Rhand"] == null && eqWeps["Lhand"] == null, $"WARNING!! error when" +
                        $"uniquipping item {wep.Name} from players equipped dict");
                else
                    Debug.Assert(eqWeps[wep.Slot] == null, $"WARNING!! error when unequipping {wep.Name}" +
                        $" from players equipped dict");
            }

            // armour
            Console.WriteLine($"trying to equip {helm.Name} to {helm.Slot}");
            // equip item
            player.TryEquipItem(helm);
            eqArms = player.GetEquippedArmour();
            Debug.Assert(player.CurrentDefence == baseDef + helm.Defence, $"WARNING!! error when trying to add" +
                $"stats of {helm.Name} to player. def:{player.CurrentDefence}");
            Debug.Assert(eqArms[helm.Slot] == helm, $"WARNING!! error when equipping {helm.Name} to players" +
                $"equipped dict.");
            // unequip item
            player.UnequipItem(helm.Slot);
            eqArms = player.GetEquippedArmour();
            Debug.Assert(player.CurrentDefence == baseDef, $"WARNING!! error when removing stats of {helm.Name}" +
                $"from player");
            Debug.Assert(eqArms[helm.Slot] == null, $"WARNING!! error when removing {helm.Name} from players" +
                "equipped dict");

            //
            // try overriding two handed with one handed
            //

            Console.WriteLine($"trying to equip {shield.Name} over {zwei.Name}");
            player.TryEquipItem(zwei);
            player.TryEquipItem(shield);
            eqWeps = player.GetEquippedWeapons();
            Debug.Assert(player.CurrentAtkDmg == baseDmg + shield.Attack && player.CurrentDefence == baseDef + shield.Defence,
                "WARNING!! error when changing stats after overriding the two handed weapon");
            Debug.Assert(eqWeps["Rhand"] == null && eqWeps["Lhand"] == shield, "WARNING!! error when handling " +
                "players equipped dict when overriding a two hander with a one hander");

            player.UnequipItem(shield.Slot);

            //
            // try overriding lhanded one hander with two hander
            //


            Console.WriteLine($"tring to equip {zwei.Name} over {shield.Name}");
            player.TryEquipItem(shield);
            player.TryEquipItem(zwei);
            eqWeps = player.GetEquippedWeapons();
            Debug.Assert(player.CurrentAtkDmg == baseDmg + zwei.Attack && player.CurrentDefence == baseDef + zwei.Defence,
                $"WARNING!! error when changing stats when ovveriding a lef handed one hander with a two hander");
            Debug.Assert(eqWeps["Rhand"] == zwei && eqWeps["Lhand"] == zwei, $"WARNING!! error when handling " +
                $"players equipped dict when overriding a left handed one hander with a two hander");

            Console.WriteLine("TESTING FINISHED. if no warning pop up then all tests were successful\n");
        }
    }
}
