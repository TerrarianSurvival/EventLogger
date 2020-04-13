using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerrariaApi.Server;
using TShockAPI;

namespace EventLogger.GlobalEvents
{
    public class AnnouncementBoxEvent
    {
        public static void OnTriggerAnnouncementBox(TriggerAnnouncementBoxEventArgs args)
        {
            if (!EventLogger.Setting.AnnouncementBox)
            {
                return;
            }

            string logText = string.Format(CultureInfo.InvariantCulture, "ANNOUNCEMENTBOX:{0}",
                string.Join(",", new object[] { args.Who, args.TileX, args.TileY, args.Text }));
            TShock.Log.Info(logText);
        }
    }
}
