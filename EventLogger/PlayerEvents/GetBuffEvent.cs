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
    public class GetBuffEvent
    {
        public static void OnSendData(SendDataEventArgs args)
        {
            if (!EventLogger.Setting.GetBuff)
            {
                return;
            }

            switch (args.MsgId)
            {
                case PacketTypes.PlayerAddBuff:
                    {
                        int playerIndex = (int)args.number;
                        int buffType = (int)args.number2;
                        int buffTime = (int)args.number3;
                        string logText = "TELEPORT:" + string.Join(",", Main.player[playerIndex].name, buffType, buffTime);
                        TShock.Log.Info(logText);
                        break;
                    }
            }
        }
    }
}
