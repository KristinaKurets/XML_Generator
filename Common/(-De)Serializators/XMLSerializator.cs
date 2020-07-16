using SharpCompress.Writers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    public class XMLSerializator<TSource> : AbstractSerializator<TSource>

    {
        public XMLSerializator(string path) : base(path)
        {

        }
        public override void Write(TSource item)
        {
            var serializer = new XmlSerializer(typeof(TSource));
            var settings = new XmlWriterSettings { Indent = true };
            using (var writer = XmlWriter.Create(path, settings))
            {
                serializer.Serialize(writer, item);
            }
            //using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            //{
            //    serializer.Serialize(fs, item);
            //}
        }
    }
}
