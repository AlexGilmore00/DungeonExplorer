using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class AttackTypes
    {
        //
        // using melee weapon
        //

        public static readonly Attack Slash = new Attack("Slash", 1);
        public static readonly Attack HeavyThrust = new Attack("Heavy Thrust", 1.8,
            isCharge: true, chargeTurns: 1, chargeMessage: "winds back their sword arm");
        public static readonly Attack HeavySlash = new Attack("Heavy Slash", 1.2,
            isCharge: true, chargeTurns: 1, chargeMessage: "raises their sword arm",
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 0.6, statusDuration: 2);

        //
        // using shield
        //

        public static readonly Attack ShieldBash = new Attack("Shield Bash", 0.7,
            appliesStatus: true, status: StatusIds.Stun, statusChance: 0.3, statusDuration: 1);

        //
        // using bow
        //

        public static readonly Attack BowShot = new Attack("Bow Shot", 1);
        public static readonly Attack PoisonShot = new Attack("Poison Shot", 1,
            isCharge: true, chargeTurns: 1, chargeMessage: "coates their arrow with poison",
            appliesStatus: true, status: StatusIds.Poison, statusChance: 1, statusDuration: 3);

        //
        // without weapon
        //

        // head
        public static readonly Attack WailingCry = new Attack("Wailing cry", 0.4,
            appliesStatus: true, status: StatusIds.Stun, statusChance: 0.8, statusDuration: 1);
        public static readonly Attack Bite = new Attack("Bite", 0.9,
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 0.8, statusDuration: 3);
        // arms
        public static readonly Attack DesperateScratch = new Attack("Desperate Scratch", 0.8,
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 0.8, statusDuration: 3);
        public static readonly Attack Scratch = new Attack("Scratch", 1,
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 0.6, statusDuration: 2);
        public static readonly Attack InfectedScratch = new Attack("Infected Scratch", 1.2,
            appliesStatus: true, status: StatusIds.Poison, statusChance: 0.8, statusDuration: 3);
        public static readonly Attack GroundSlam = new Attack("Ground Slam", 2,
            isCharge: true, chargeTurns: 1, chargeMessage: "raises their arms in the air");
        public static readonly Attack LabouredPunch = new Attack("Laboured Punch", 3.2,
            isCharge: true, chargeTurns: 1, chargeMessage: "slowly tries to stand up");

        //
        // slime type
        //

        public static readonly Attack SlimeShot = new Attack("Slime Shot", 0.8,
            appliesStatus: true, status: StatusIds.Poison, statusChance: 0.7, statusDuration: 3);
        public static readonly Attack SlimeWhip = new Attack("Slime Whip", 1.2);
        public static readonly Attack Engulf = new Attack("Engulf", 0.4,
            isCharge: true, chargeTurns: 1, chargeMessage: "enlarges themselves",
            appliesStatus: true, status: StatusIds.Stun, statusChance: 1, statusDuration: 1);

        //
        // plant type
        //

        public static readonly Attack Spores = new Attack("Spores", 0.2,
            isCharge: true, chargeTurns: 1, chargeMessage: "starts emitting an odd mist",
            appliesStatus: true, status: StatusIds.Poison, statusChance: 1, statusDuration: 5);
        public static readonly Attack CapSmack = new Attack("Cap Smack", 1.3);
        public static readonly Attack Root = new Attack("Root", 0.2,
            appliesStatus: true, status: StatusIds.Stun, statusChance: 0.7, statusDuration: 1);
        public static readonly Attack BranchSlap = new Attack("Branch Slap", 1.3);
        public static readonly Attack BrambleWhip = new Attack("Bramble Whip", 0.8,
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 0.7, statusDuration: 2);

        //
        // animal type
        //

        public static readonly Attack TailWhip = new Attack("Tail Whip", 1.2);

        //
        // the heart
        //

        public static readonly Attack ThunderousBeat = new Attack("Thunderous Beat", 0.6,
            appliesStatus: true, status: StatusIds.Stun, statusChance: 0.8, statusDuration: 1);
        public static readonly Attack BloodShower = new Attack("Blood Shower", 2,
            isCharge: true, chargeTurns: 2, chargeMessage: "muscles twitch, ready to contract",
            appliesStatus: true, status: StatusIds.Bleed, statusChance: 1, statusDuration: 6);
        public static readonly Attack ArteryWhip = new Attack("Artery Whip", 1);
        public static readonly Attack VeinEntangle = new Attack("Vein Entangle", 2.3,
            isCharge: true, chargeTurns: 1, chargeMessage: "veins entering it move towards you");


        public AttackTypes()
        {
        }
    }
}
