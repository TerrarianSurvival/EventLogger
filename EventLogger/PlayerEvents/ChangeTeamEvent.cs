using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShockAPI;

namespace EventLogger.PlayerEvents
{
    public class ChangeTeamEvent
    {
        public static void OnChangeTeam(object sender, GetDataHandlers.PlayerTeamEventArgs args)
        {
            if (!EventLogger.Setting.ChangeTeam)
            {
                return;
            }

            string logText = string.Format(CultureInfo.InvariantCulture, "CHANGETEAM:{0}",
                string.Join(",", new object[] { args.Player.Name, args.Player.Team, args.Team, args.Player.TPlayer.position.X, args.Player.TPlayer.position.Y }));
            TShock.Log.Info(logText);
        }
    }
}
