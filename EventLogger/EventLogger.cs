using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using EventLogger.GlobalEvents;
using EventLogger.PlayerEvents;
using System.IO;

namespace EventLogger
{
    [ApiVersion(2, 1)]
    public class EventLogger : TerrariaPlugin
    {
        public override string Author => "Miyabi";
        public override string Description => "Event Logger";
        public override string Name => "Event Logger";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        private const string SettingFileName = "EventLoggerSetting.json";

        public EventLogger(Main game)
            : base(game)
        {
            Setting = LogSetting.Read(Path.Combine(TShock.SavePath, SettingFileName));
        }

        public static LogSetting Setting { get; private set; }

        public override void Initialize()
        {
            ServerApi.Hooks.WireTriggerAnnouncementBox.Register(this, AnnouncementBoxEvent.OnTriggerAnnouncementBox);
            ServerApi.Hooks.NetSendData.Register(this, TeleportEvent.OnSendData);
            ServerApi.Hooks.NetSendData.Register(this, GetBuffEvent.OnSendData);

            GetDataHandlers.PlayerTeam += ChangeTeamEvent.OnChangeTeam;
            GetDataHandlers.PlayerDamage += DamagedEvent.OnPlayerDamage;
            GetDataHandlers.KillMe += KillMeEvent.OnKillMe;
            GetDataHandlers.PlayerSpawn += SpawnEvent.OnSpawn;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.WireTriggerAnnouncementBox.Deregister(this, AnnouncementBoxEvent.OnTriggerAnnouncementBox);
                ServerApi.Hooks.NetSendData.Deregister(this, TeleportEvent.OnSendData);
                ServerApi.Hooks.NetSendData.Deregister(this, GetBuffEvent.OnSendData);

                GetDataHandlers.PlayerTeam -= ChangeTeamEvent.OnChangeTeam;
                GetDataHandlers.PlayerDamage -= DamagedEvent.OnPlayerDamage;
                GetDataHandlers.KillMe -= KillMeEvent.OnKillMe;
                GetDataHandlers.PlayerSpawn -= SpawnEvent.OnSpawn;
            }

            base.Dispose(disposing);
        }
    }
}
