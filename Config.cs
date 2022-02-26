using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Terraria.ID;


namespace Plugin
{
    public class Config
    {
        public List<BuffData> buff = new List<BuffData>();

        public static Config Load(string path)
        {
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
            }
            else
            {
                var c = new Config();
                c.InitDefault();
                File.WriteAllText(path, JsonConvert.SerializeObject(c, Formatting.Indented));
                return c;
            }
        }

        public void InitDefault()
        {
            buff.Add( new BuffData() );
        }

    }

    public class BuffData
    {
        // buff id
        public int id = BuffID.Lucky;

        // 持续时长（秒数）
        public int seconds = 52*60;
    }

}