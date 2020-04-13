using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace EventLogger
{
    public static class Utils
    {
        public static int CalcDamage(Player player, int damage, bool pvp = false, bool crit = false)
        {
            int defence = player.statDefense;

            double damageCalculated = Main.CalculatePlayerDamage(damage, defence);
            if (crit)
            {
                damageCalculated *= 2;
            }
            if (damageCalculated >= 1.0)
            {
                damageCalculated = (int)((1d - player.endurance) * damageCalculated);
                if (damageCalculated < 1.0)
                {
                    damageCalculated = 1.0;
                }

                // SolarFlare damage reduction here

                // Beetle armor damage reduction here

                // Paladin shield defence here
            }
            return (int)damageCalculated;
        }
    }
}
