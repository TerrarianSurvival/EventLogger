using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using TShockAPI;

namespace EventLogger.PlayerEvents
{
    public class KillMeEvent
    {
        public static void OnKillMe(object sender, GetDataHandlers.KillMeEventArgs args)
        {
            if (!EventLogger.Setting.KillMe)
            {
                return;
            }

            // almost same as OnPlayerDamage, diff: logText, DAMAGED -> DEATH
            PlayerDeathReason reason = args.PlayerDeathReason;
            TSPlayer enemyPlayer = reason.SourcePlayerIndex >= 0 && reason.SourcePlayerIndex < 255
                ? TShock.Players[reason.SourcePlayerIndex] : null;

            // Format: DeadPlayer, KillerPlayer, Damage, DeadPlayerX, DeadPlayerY, KillerPlayerX, KillerPlayerY, KillerItem, KillerProj, KillerNPC, KillerOther
            string deadPlayerName = args.Player.Name;
            string killerPlayerName;
            string killerPlayerX;
            string killerPlayerY;

            string projName = reason.SourceProjectileIndex >= 0 ? Lang.GetProjectileName(reason.SourceProjectileType).Value : string.Empty;
            string itemName = reason.SourceItemType != 0 ? Lang.GetItemName(reason.SourceItemType).Value : string.Empty;
            string npcName = reason.SourceNPCIndex >= 0 ? Main.npc[reason.SourceNPCIndex].GetGivenOrTypeNetName().ToString() : string.Empty;
            string otherText = string.Empty;

            if (enemyPlayer == null)
            {
                killerPlayerName = string.Empty;
                killerPlayerX = string.Empty;
                killerPlayerY = string.Empty;
            }
            else
            {
                killerPlayerName = enemyPlayer.Name;
                killerPlayerX = enemyPlayer.X.ToString(CultureInfo.InvariantCulture);
                killerPlayerY = enemyPlayer.Y.ToString(CultureInfo.InvariantCulture);
            }

            switch (reason.SourceOtherIndex)
            {
                case 0:
                    otherText = "FELL";
                    break;
                case 1:
                    otherText = "DROWNED";
                    break;
                case 2:
                    otherText = "LAVA";
                    break;
                case 3:
                    otherText = "DEFAULT";
                    break;
                case 4:
                    otherText = "SLAIN";
                    break;
                case 5:
                    otherText = "PETRIFIED";
                    break;
                case 6:
                    otherText = "STABBED";
                    break;
                case 7:
                    otherText = "SUFFOCATED";
                    break;
                case 8:
                    otherText = "BURNED";
                    break;
                case 9:
                    otherText = "POISONED";
                    break;
                case 10:
                    otherText = "ELECTROCUTED";
                    break;
                case 11:
                    otherText = "TRIED_TO_ESCAPE";
                    break;
                case 12:
                    otherText = "WAS_LICKED";
                    break;
                case 13:
                    otherText = "TELEPORT_1";
                    break;
                case 14:
                    otherText = "TELEPORT_2_MALE";
                    break;
                case 15:
                    otherText = "TELEPORT_2_FEMALE";
                    break;
                case 254:
                    otherText = "NONE";
                    break;
                case 255:
                    otherText = "SLAIN";
                    break;
            }

            string logText = string.Format(CultureInfo.InvariantCulture, "DEATH:{0}",
                string.Join(",", new object[] {
                    deadPlayerName,
                    killerPlayerName,
                    args.Damage,
                    args.Player.X,
                    args.Player.Y,
                    killerPlayerX,
                    killerPlayerY,
                    itemName,
                    projName,
                    npcName,
                    otherText,
                }));
            TShock.Log.Info(logText);
        }
    }
}
