using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    public class JSONDeserializator<TSource> : IDeserialize<TSource>
    {
        public TSource Load(string path)
        {
            string item;
            using (StreamReader sr = new StreamReader(path))
            {
                item = sr.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<TSource>(item); 
        }
    }
}
