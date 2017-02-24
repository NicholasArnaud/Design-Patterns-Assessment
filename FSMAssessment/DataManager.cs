using System.IO;
using System.Xml.Serialization;

namespace FSMAssessment
{
    /// <summary>
    /// Information goes to this class to be saved or retrieved in a XML document format
    /// </summary>
    /// <typeparam name="T">Type of information to be serialized</typeparam>
    class DataManager<T>
    {
        //Folder that will be created when data will be saved
        public static string folderName = @"Save\";

        /// <summary>
        /// Saves called data into an Xml file
        /// </summary>
        /// <param name="filename">Saves a file with this name</param>
        /// <param name="data">Saves information associated with this type</param>
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

        /// <summary>
        /// Retrieves saved data by reading an already present 
        /// Xml file
        /// </summary>
        /// <param name="fileName">Reads a file with this name</param>
        /// <returns></returns>
        public static T Deserialize(string fileName)
        {
            T data;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextReader reader = new StreamReader(folderName + fileName  + ".xml");
            data = (T)serializer.Deserialize(reader);
            reader.Close();
            return data;
        }
    }
}