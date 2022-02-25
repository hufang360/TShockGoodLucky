using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Terraria.ID;


namespace Plugin
{
    public class Config
    {
        public List<int> buff = new List<int>() {BuffID.Lucky};

        public int seconds = 52 * 60;

        public static Config Load(string path)
        {
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
            }
            else
            {
                var c = new Config();
                File.WriteAllText(path, JsonConvert.SerializeObject(c, Formatting.Indented));
                return c;
            }
        }

    }

}