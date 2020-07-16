using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public class XMLDeserializator<TSource> : IDeserialize<TSource>
    {
        public TSource Load(string path)
        {
            TSource item;
            XmlSerializer serializer = new XmlSerializer(typeof(TSource));
            using (var reader = XmlReader.Create(path))
            {
               item = (TSource)serializer.Deserialize(reader);
            }
            return item;
        }
    }
}
