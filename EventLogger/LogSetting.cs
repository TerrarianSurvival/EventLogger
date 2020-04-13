using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventLogger
{
    public class LogSetting
    {
        [JsonProperty]
        public bool PlayerSpawn { get; private set; } = true;

        [JsonProperty]
        public bool AnnouncementBox { get; private set; } = true;

        [JsonProperty]
        public bool ChangeTeam { get; private set; } = true;

        [JsonProperty]
        public bool GetBuff { get; private set; } = true;

        [JsonProperty]
        public bool KillMe { get; private set; } = true;

        [JsonProperty]
        public bool PlayerTeleport { get; private set; } = true;

        [JsonProperty]
        public bool Damaged { get; private set; } = true;

        public static LogSetting Read(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("LogSetting: Using default configs.");
                LogSetting setting = new LogSetting();
                setting.Write(path);
                return setting;
            }
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Read(fs);
            }
        }

        public static LogSetting Read(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<LogSetting>(sr.ReadToEnd());
            }
        }

        public void Write(string path)
        {
            string dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                Write(fs);
            }
        }

        public void Write(Stream stream)
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            using (var sw = new StreamWriter(stream))
            {
                sw.Write(str);
            }
        }
    }
}
