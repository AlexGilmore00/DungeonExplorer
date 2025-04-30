using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentEnemy : LivingEntity, IDamageable, IEntityNeedsDescription
    // a class for all enemy types to inherit from
    // all public functions and fields of an enemy should be implemented in here
    {
        private static Random _rnd = new Random();
        public List<Attack> Attacks { get; protected set; }
        public Attack NextAttack { get; protected set; }
        public int ChargeTurnCounter { get; protected set; }

        public ParentEnemy()
        {

        }

        public void DealDamageTo(Player player, ParentEnemy enemy)
        {
            // only deal damage if the desired attack isnt currently charging
            if (!(enemy.NextAttack.IsCharge && enemy.ChargeTurnCounter > 0))
            {
                double attackDecimal = enemy.CurrentAtkDmg * enemy.NextAttack.DmgMod;
                // attack rounded towards zero
                int attack = (int)attackDecimal;
                int defence = player.CurrentDefence;

                double damageReduction = (double)defence / ((double)defence + 50);
                double damageDouble = attack * (1 - damageReduction);
                int damage = (int)damageDouble;

                // deal a minimum of 1 damage
                if (damage <= 0) { damage = 1; }

                // apply damage
                player.TakeDamage(damage);
                Console.WriteLine($"{enemy.Name} used {enemy.NextAttack.Name}");
                Console.WriteLine($"{player.Name} took {damage} damage");

                // apply any relevant status effects
                if (enemy.NextAttack.AppliesStatus && _rnd.NextDouble() < enemy.NextAttack.StatusChance)
                    StatusInteractions.ApplyStatusTo(player, enemy.NextAttack.Status, enemy.NextAttack.StatusDuration);
            }
            else
            // otherwise display charge message
            {
                Console.WriteLine($"{enemy.Name} {enemy.NextAttack.ChargeMessage}");
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            // kill the entity if their health is 0 or below
            if (Health <= 0)
            {
                IsDead = true;
                Name += " (Dead)";
            }
        }

        public void DisplayEntityDescription()
        // display a brief overview of the creatures name and its damage range
        {
            // get the damage values of all the enemy's attacks
            List<int> damages = new List<int>();
            foreach (Attack attack in Attacks)
            {
                double damageDecimal = (double)CurrentAtkDmg * attack.DmgMod;
                int damage = (int)damageDecimal;
                damages.Add(damage);
            }
            // order the list from lowest damage to highest
            damages = damages.OrderBy(x => x).ToList();

            Console.WriteLine($"{Name}\n" +
                $"damage range: {damages[0]} - {damages[damages.Count - 1]}");

            Console.WriteLine();
        }

        public void ChooseNextAttack()
        // takes a random attack from the enemy's Attacks list
        // and sets NextAttack as the chosen value
        // if an attack needs to be charged, no new attack will be
        // chosen until the charge attack comes out
        {
            if (ChargeTurnCounter <= 0)
            {
                // choose a random attack is an attack is not currently
                // being charged
                NextAttack = Attacks[_rnd.Next(0, Attacks.Count)];

                // check if the chosen move needs to be charged
                if (NextAttack.IsCharge)
                {
                    ChargeTurnCounter = NextAttack.ChargeTurns;
                }
            }
            else
            {
                // reduce the charge counter if a charge move has been selected
                // before and has not yet com out
                ChargeTurnCounter--;
            }
        }
    }
}
