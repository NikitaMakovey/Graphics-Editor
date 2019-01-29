using NewPaint.Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace NewPaint
{
    public static class Serializator
    {
        public static void Serialize(string fileName)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Figure>));
            var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, GlobalVars.figures);
            File.WriteAllText(fileName, stringWriter.ToString());
        }

        public static void Deserialize(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            var xmlSerializer = new XmlSerializer(typeof(List<Figure>));
            var stringReader = new StringReader(File.ReadAllText(fileName));
            GlobalVars.figures = (List<Figure>)xmlSerializer.Deserialize(stringReader);
        }
    }
}
