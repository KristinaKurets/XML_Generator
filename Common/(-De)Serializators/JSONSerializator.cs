using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    class JSONSerializator<TSource> : AbstractSerializator<TSource>
    {
        public JSONSerializator(string path) : base(path)
        {

        }
        public override void Write(TSource source)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(source));
        }
    }
}
