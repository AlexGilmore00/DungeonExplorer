using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public abstract class ParentEnemy : LivingEntity, IDamageable
    // a class for all enemy types to inherit from
    // all public functions and fields of an enemy should be implemented in here
    {
        public List<Attack> Attacks { get; protected set; }

        public ParentEnemy()
        {

        }

        public void DealDamageTo(Player player, ParentEnemy enemy)
        {
            int attack = enemy.CurrentAtkDmg;
            int defence = player.CurrentDefence;

            int damage = attack - defence;
            // dont call to deal damage if no damage is dealt
            if (damage <= 0) { damage = 0; }
            else
            {
                player.TakeDamage(damage);
            }
            Console.WriteLine($"{player.Name} took {damage} damage");
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
    }
}
