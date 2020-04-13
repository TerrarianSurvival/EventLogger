using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace EventLogger.PlayerEvents
{
    public class SpawnEvent
    {
        public static void OnSpawn(object sender, GetDataHandlers.SpawnEventArgs args)
        {
            if (!EventLogger.Setting.PlayerSpawn)
            {
                return;
            }

            TShock.Log.Info("SPAWN:" + string.Join(",", new object[] { args.Player.Name, args.SpawnX, args.SpawnY }));
        }
    }
}
