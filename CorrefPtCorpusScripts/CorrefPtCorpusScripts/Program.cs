using System;
using System.IO;
using System.Xml;

namespace CorrefPtCorpusScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            string corpusFolder = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Corpus\";
            string[] texts = Directory.GetFiles(corpusFolder);

            foreach (string text in texts)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(text);
                int mentionsWithDifferentHead = new Script().AnalizeText(doc);
                Console.WriteLine(mentionsWithDifferentHead + "|" + text);
            }

            Console.ReadKey();
        }
    }
}
