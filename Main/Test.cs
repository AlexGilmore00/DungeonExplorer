using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
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

        public static void ExecMainTests()
        {
            Game game = new Game(testing: true);

            // map generation, inluding level, room setup, and container generation
            Console.WriteLine("TESTING MAP GENERATION::");
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            for (int i = 0; i < 3; i++)
            {
                TestRoomGeneration(i);
            }
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            TestLevelGeneration();
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            TestItemGenerationAndContainers();

            // living entity functionality, including player pick up
            // player equip and unequip, and status effects
            Console.WriteLine("TESTING LIVING ENTITY FUNCTIONALITY::");
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            SetupTestInventory(game.Player);
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            TestEquipAndUnequip(game.Player);
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            TestStatusEffects(game.Player);
            Console.WriteLine("press any key to continu0e...");
            Console.ReadKey(true);
            Console.WriteLine();
        }

        public static void TestRoomGeneration(int difficulty)
        {
            Console.WriteLine($"TEST: room generation difficulty: {difficulty}\n");
            ParentRoom room = new DefaultRoom(difficulty);
            // see if enemies are added to the Enemies array correctly
            Console.WriteLine($"there should be {room.Enemies.Length} enemies...");
            foreach (ParentEnemy enemy in room.Enemies)
            {
                Debug.Assert(enemy != null, "WARNING!! null enemy was added when generating room");
                Console.WriteLine(enemy.Name);
            }

            // see if containers are added to the Containers array correctly
            Console.WriteLine($"there should be {room.Containers.Length} containers...");
            foreach (ParentContainer container in room.Containers)
            {
                Debug.Assert(container != null, "WARNING!! null container was added when generating room");
                Console.WriteLine(container.Name);
            }

            // test display description method
            Console.WriteLine("\n testing Room.DisplayDescription \n");
            room.DisplayDescription();
            Console.WriteLine("TEST: test complete, test passed if no 'WARNING!!' messages\n");
        }

        public static void SetupTestInventory(Player player)
        {
            Console.WriteLine("TEST: setting up player test inventory...\n");
            player.PickUpItem(new TesterHelm());
            player.PickUpItem(new TesterPotion());
            player.PickUpItem(new TesterPotion());
            player.PickUpItem(new TesterShield());
            player.PickUpItem(new TesterZwei());

            for (int i = 0; i < 25; i++)
            {
                player.PickUpItem(new TesterSword());
            }
            Debug.Assert(player.InvWeapons.Count == 27
                && player.InvArmour.Count == 1
                && player.InvConsumables.Count == 2,
                "WARNING!! not all items were picked up properly");
            Debug.Assert(!player.InvWeapons.Any(w => w == null)
                && !player.InvArmour.Any(a => a == null)
                && !player.InvConsumables.Any(c => c == null),
                "WARNING!! null items were added to the players inventory");
            Console.WriteLine("TEST: test complete, test passed if no 'WARNING!!' messages\n");
        }

        public static void TestItemGenerationAndContainers()
        {
            Console.WriteLine("TEST: testing item and container generation\n");
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
            Console.WriteLine("TEST: test complete, test passed if no 'WARNING!!' messages\n");
        }

        public static void TestLevelGeneration()
        {
            Console.WriteLine("TEST: testing level generation for all difficulties\n");
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
                    Debug.Assert(roomInfo[0] == 8 + difficulty * 4, "WARNING!! desired room count not achieved");
                    Debug.Assert(roomInfo[1] == 0, "WARNING!! all excess room not filled in");
                }
            }
            Console.WriteLine("TEST: test complete, test passed if no 'WARNING!!' messages\n");
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
                    else if (levelLayout[i, j] is BossRoom)
                        room = "[ B ]";
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

        public static void TestEquipAndUnequip(Player player)
        {
            Console.WriteLine("TEST: testing player equip and unequip\n");

            // player base stats
            int baseDmg = 10;
            int baseDef = 5;

            // set up objects
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
                eqWeps = player.EqWeapon;
                if (wep.IsTwoHanded)
                    Debug.Assert(eqWeps["Rhand"] == wep && eqWeps["Lhand"] == wep, $"WARNING!! error when equipping" +
                        $"{wep.Name} to players equipped dict");
                else
                    Debug.Assert(eqWeps[wep.Slot] == wep, $"WARNING!! error when equipping" +
                        $"{wep.Name} to players equipped dict");
                // uniquip item
                player.UnequipItem(wep.Slot);
                eqWeps = player.EqWeapon;
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
            eqArms = player.EqArmour;
            Debug.Assert(player.CurrentDefence == baseDef + helm.Defence, $"WARNING!! error when trying to add" +
                $"stats of {helm.Name} to player. def:{player.CurrentDefence}");
            Debug.Assert(eqArms[helm.Slot] == helm, $"WARNING!! error when equipping {helm.Name} to players" +
                $"equipped dict.");
            // unequip item
            player.UnequipItem(helm.Slot);
            eqArms = player.EqArmour;
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
            eqWeps = player.EqWeapon;
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
            eqWeps = player.EqWeapon;
            Debug.Assert(player.CurrentAtkDmg == baseDmg + zwei.Attack && player.CurrentDefence == baseDef + zwei.Defence,
                $"WARNING!! error when changing stats when ovveriding a lef handed one hander with a two hander");
            Debug.Assert(eqWeps["Rhand"] == zwei && eqWeps["Lhand"] == zwei, $"WARNING!! error when handling " +
                $"players equipped dict when overriding a left handed one hander with a two hander");

            Console.WriteLine("TEST: test complete, test passed if no 'WARNING!!' messages\n");
        }

        public static void TestStatusEffects(Player player)
        {
            Console.WriteLine("TEST: testing status effects\n");
            // make sure player is at full health
            player.TakeDamage(-99999);
            int initialHealth = player.Health;

            // test bleed damage
            Console.WriteLine("testing bleed application, effect, and removal\n");
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, 5);
            Debug.Assert(player.StatusEffects.Count == 1
                && player.StatusEffects[0].ID == (int)StatusIds.Bleed,
                "WARNING!! bleed status not applied properly");
            for (int i = 1; i <= 5; i++)
            {
                StatusInteractions.UpdateStatuses(player);
                Debug.Assert(player.Health == initialHealth - 4 * i,
                    "WARNING!! bleed damage is not being applied properly");
            }
            StatusInteractions.UpdateStatuses(player);
            Debug.Assert(player.StatusEffects.Count == 0,
                "WARNING!! status effect has not been removed properly");

            // test strenght application
            Console.WriteLine("testing strength application, effect, and removal\n");
            int initialStrength = player.CurrentAtkDmg;
            StatusInteractions.ApplyStatusTo(player, StatusIds.Strength, 3);
            Debug.Assert(player.StatusEffects.Count == 1
                && player.StatusEffects[0].ID == (int)StatusIds.Strength,
                "WARNING!! strength status not applied properly");
            for (int i = 1; i <= 3; i++)
            {
                StatusInteractions.UpdateStatuses(player);
                Debug.Assert(player.CurrentAtkDmg == initialStrength + 5,
                    $"WARNING!! strength has not been applied properly, the " +
                    $"players CurrentAtkDmg is {player.CurrentAtkDmg} when it " +
                    $"should be {initialStrength + 5}");
            }
            StatusInteractions.UpdateStatuses(player);
            Debug.Assert(player.StatusEffects.Count == 0,
                "WARNING!! status effect has not been removed properly");

            // make sure player is at full health
            player.TakeDamage(-99999);

            // test stacking the same buff with different durations

            // new effect with lower duration
            Console.WriteLine("testing adding already applied effect with lower duration\n");
            int initialDuration = 6;
            int newDuration = 4;
            // apply initial effect
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, initialDuration);
            // apply new effect
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, newDuration);
            Debug.Assert(player.StatusEffects.Count == 1
                && player.StatusEffects[0].Duration == initialDuration,
                "WARNING!! error when adding new similar status with lower duration");
            // clear statuses
            for (int i = 0; i < 10; i++)
            {
                StatusInteractions.UpdateStatuses(player);
            }

            // new effect with greater duration
            Console.WriteLine("testing adding already applied effect with greater duration\n");
            initialDuration = 4;
            newDuration = 6;
            // apply initial effect
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, initialDuration);
            // apply new effect
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, newDuration);
            Debug.Assert(player.StatusEffects.Count == 1
                && player.StatusEffects[0].Duration == newDuration,
                "WARNING!! error when adding new similar status with lower duration");
            // clear statuses
            for (int i = 0; i < 10; i++)
            {
                StatusInteractions.UpdateStatuses(player);
            }

            // new effect with same duration
            Console.WriteLine("testing adding already applied effect with same duration\n");
            initialDuration = 5;
            newDuration = 5;
            // apply initial effect
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, initialDuration);
            // apply new effect
            StatusInteractions.ApplyStatusTo(player, StatusIds.Bleed, newDuration);
            Debug.Assert(player.StatusEffects.Count == 1
                && player.StatusEffects[0].Duration == newDuration,
                "WARNING!! error when adding new similar status with lower duration");
            // clear statuses
            for (int i = 0; i < 10; i++)
            {
                StatusInteractions.UpdateStatuses(player);
            }

            Console.WriteLine("TEST: test complete, test passed if no 'WARNING!!' messages\n");
        }
    }
}
