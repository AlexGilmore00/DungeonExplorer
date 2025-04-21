using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal interface IDamageable
    {
        void DealDamageTo(Player player, ParentEnemy enemy);
        void TakeDamage(int damage);
    }
}
