using System.IO;
using System.Xml.Serialization;

namespace FSMAssessment
{
    /// <summary>
    /// Information goes to this class to be saved or retrieved in a XML document format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class DataManager<T>
    {
        public static string folderName = @"Save\";
        
        //saves called data into an xml file
        public static void Serialize(string filename, T data)
        {
            //The class "XmlSerializer" comes from the namespace of "System.Xml.Serialization" to change data into save data in an xml format
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            Directory.CreateDirectory(folderName);
            //The class "TextWriter" comes from the namespace of "System.IO" to create an xml document for information to be placed
            TextWriter writer = new StreamWriter(folderName + filename + ".xml");
            serializer.Serialize(writer, data);
            writer.Close();
        }

        //retrieves saved data
        public static T Deserialize(string filename)
        {
            T data;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextReader reader = new StreamReader(folderName + filename  + ".xml");
            data = (T)serializer.Deserialize(reader);
            reader.Close();
            return data;
        }
    }
}
