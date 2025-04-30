using DungeonExplorer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
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

        //
        //
        // for room generation:
        //
        //

        public static ParentEnemy GenerateRandomEnemy(int difficulty)
        // returns a random parent enemy corresponding to the difficulty type
        // a +10 modifier to difficulty is to be used if a boss is wanted to be returned
        {
            // a list of the valid enemies for this difficulty
            List<ParentEnemy> enemySet;

            switch (difficulty)
            {
                // regular enemies
                case 0:
                    enemySet = new List<ParentEnemy>()
                    {
                        new BowSkeleton(),
                        new ShieldSkeleton(),
                        new SwordSkeleton(),
                    };
                    break;
                case 1:
                    enemySet = new List<ParentEnemy>()
                    {
                        new GrassSlime(),
                        new LivingMushroom(),
                        new TreeEnt(),
                    };
                    break;
                case 2:
                    enemySet = new List<ParentEnemy>()
                    {
                        new Abomination(),
                        new HardenedInfected(),
                        new Mumbler(),
                    };
                    break;
                // bosses
                case 10:
                    enemySet = new List<ParentEnemy>()
                    {
                        new FusedSkeletonPhalanx(),
                    };
                    break;
                case 11:
                    enemySet = new List<ParentEnemy>()
                    {
                        new GiantGiardDog(),
                    };
                    break;
                case 12:
                    enemySet = new List<ParentEnemy>()
                    {
                        new TheHeart(),
                    };
                    break;
                default:
                    Console.WriteLine("WARNING!! an invalid difficulty was given when calling" +
                        "Test.GenerateRandomEnemy. a default value of TestEnemy was returned");
                    return new TestEnemy();
            }

            // return a random enemy from the list
            return enemySet[rnd.Next(0, enemySet.Count)];
        }

        public static ParentContainer GenerateRandomContainer(int difficulty)
        // returns a random container depending on the difficulty
        {
            // a list of valid containers fir this difficulty
            List<ParentContainer> containers;

            switch (difficulty)
            {
                case 0:
                    containers = new List<ParentContainer>()
                    {
                        new Cabinet(),
                        new Table(),
                    };
                break;
                case 1:
                    containers = new List<ParentContainer>()
                    {
                        new DeceasedBody(),
                        new HiddenStash(),
                    };
                    break;
                case 2:
                    containers = new List<ParentContainer>()
                    {
                        new InfectedPile(),
                        new LeftoverPouch(),
                    };
                    break;
                default:
                    Console.WriteLine("WARNING!! an invalid difficulty was given when " +
                        "calling TEST.GenerateRandomContainer. a default container " +
                        "TestContainer was returned");
                    return new TestContainer();
            }

            // return a random container from the list
            return containers[rnd.Next(0, containers.Count)];
        }

        public static string GetRandomFlavourText(int difficulty)
        // returns a random description of a room depending on the difficulty of said room
        // a room on level 1 will have difficulty 0 and so on
        {
            // the list descriptions contains lists with descriptions of a room of a given difficulty
            // the index of the nested list is the difficulty of the room descriptions within it
            // so access using descriptions[difficulty][x]
            List<List<string>> descriptions = new List<List<string>>();

            // level one
            descriptions.Add(new List<string> 
            {
                "the walls are cold and damp, the cracks in the stone brick let a light breeze through. " +
                "you cant tell whether the sounds you hear are feint screams or wind rushing through " +
                "distant halls."
            });

            // level two
            descriptions.Add(new List<string>
            {
                "placeholder lvl 2"
            });

            // level three
            descriptions.Add(new List<string>
            {
                "placeholder lvl 3"
            });

            return descriptions[difficulty][rnd.Next(0, descriptions[difficulty].Count)];
        }

        //
        //
        // for loot generation
        //
        //

        public static ParentItem GenerateRandomItem(int lootTable)
        // returns a random item of type ParentItem from a pool of items determined by the lootTable arg
        {
            // get a list of calid items for the loot table
            List<ParentItem> items;

            switch (lootTable)
            {
                // test container loot table
                case -1:
                    items = new List<ParentItem>()
                    {
                        new TesterHelm(),
                        new TesterPotion(),
                        new TesterShield(),
                        new TesterSword(),
                        new TesterZwei(),
                    };
                    break;
                // cabinet loot table
                case 1:
                    items = new List<ParentItem>()
                    {
                        new StrawHat(),
                        new ParingKnife(),
                        new LargeWoodenLid(),
                        new HealingPotion(),
                    };
                    break;
                // table loot table
                case 2:
                    items = new List<ParentItem>()
                    {
                        new LightGambeson(),
                        new LeatherShoes(),
                        new ClothTrousers(),
                        new RustedSheers(),
                        new HealingPotion()
                    };
                    break;
                // deceased body loot table
                case 3:
                    items = new List<ParentItem>()
                    {
                        new ChainmailVest(),
                        new SturdyBoots(),
                        new LeatherChausses(),
                        new Mace(),
                        new Buckler(),
                        new HealingPotion(),
                        new StrengthPotion(),
                    };
                    break;
                // hidden stash loot table
                case 4:
                    items = new List<ParentItem>()
                    {
                        new PaddedHood(),
                        new Longbow(),
                        new HealingPotion(),
                        new StrengthPotion(),
                    };
                    break;
                // leftover pouch loot table
                case 5:
                    items = new List<ParentItem>()
                    {
                        new GreaterHealingPotion(),
                        new StrengthPotion(),
                    };
                    break;
                // infected pile loopt table
                case 6:
                    items = new List<ParentItem>()
                    {
                        new PlateCuirass(),
                        new PlateBoots(),
                        new PlateHelm(),
                        new PlateLegs(),
                        new Warpick(),
                        new Claymore(),
                        new KiteShield(),
                    };
                    break;
                default:
                    Console.WriteLine("WARNING!! invalid loot table input has been given when" +
                        " calling LookUp.GetRandomItem(). a defualt item of TestSword has " +
                        "been returned");
                    return new TesterSword();
            }

            // return an item form the list
            return items[rnd.Next(0, items.Count)];
        }
    }
}
