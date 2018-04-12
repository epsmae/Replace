using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Replace.DataModel;

namespace Replace
{
    public class ConfigPersistence
    {
        private static XmlSerializer _serializer;

        public void Save(string fullFilePath, Config config)
        {
            WriteConfig(fullFilePath, config);
        }

        public Config Load(string fullFilePath)
        {
            return ReadConfig(fullFilePath);
        }

        private static XmlSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new XmlSerializer(typeof(Config));
                }

                return _serializer;
            }
        }

        private static XmlWriterSettings WriterSettings
        {
            get
            {
                return new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    NewLineOnAttributes = true,
                    Indent = true
                };
            }
        }
        
        private void WriteConfig(string filePath, Config config)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                XmlWriter writer = XmlWriter.Create(fileStream, WriterSettings);

                XmlSerializerNamespaces emptyNamespace = new XmlSerializerNamespaces();
                emptyNamespace.Add(string.Empty, string.Empty);

                Serializer.Serialize(writer, config, emptyNamespace);

                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        private static Config ReadConfig(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (Config)Serializer.Deserialize(fileStream);
            }
        }
    }
}
