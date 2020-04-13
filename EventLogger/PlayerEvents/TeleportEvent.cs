using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace EventLogger.PlayerEvents
{
    public class TeleportEvent
    {
        public static void OnSendData(SendDataEventArgs args)
        {
            if (!EventLogger.Setting.PlayerTeleport)
            {
                return;
            }

            switch (args.MsgId)
            {
                case PacketTypes.Teleport:
                    {
                        if (args.number != 0)
                        {
                            break;
                        }
                        int playerIndex = (int)args.number2;
                        float x = args.number3;
                        float y = args.number4;
                        string logText = "TELEPORT:" + string.Join(",", Main.player[playerIndex].name, x, y);
                        TShock.Log.Info(logText);
                        break;
                    }
            }
        }
    }
}
