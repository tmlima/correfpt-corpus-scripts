using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;

namespace CorrefPtCorpusScripts
{
    class Program
    {
        static void Main(string[] args)
        {
            string corpusFolder = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Corpus\";
            string[] textsXml = Directory.GetFiles(corpusFolder);

            List<Text> texts = new List<Text>();
            foreach (string textXml in textsXml)
            {
                Script script = new Script();
                XmlDocument doc = new XmlDocument();
                doc.Load(textXml);
                texts.Add(script.AnalizeText(doc, Path.GetFileName(textXml)));
            }

            Print(texts);
            TextsWithMoreDistinctHeads( texts );
            TextsWithMoreChainsWithAtLeastOneDistinctHead( texts );

            Console.WriteLine( "Done" );

            Console.ReadKey();
        }

        static void Print(List<Text> texts)
        {
            foreach (Text t in texts)
            {
                Console.WriteLine("Distinct heads: " + t.Chains.Sum(x => x.DistinctHeads()));
                Console.WriteLine(t.Name);
                foreach (Chain c in t.Chains)
                    Console.WriteLine(c.Id + " : " + "[" + string.Join("][", c.Heads) + "]");
            }
        }

        static void TextsWithMoreDistinctHeads( List<Text> texts )
        {
            Console.WriteLine( "Texts with more distinct heads" );
            texts = texts.OrderByDescending( x => x.Chains.Sum( y => y.DistinctHeads() ) ).Take( 3 ).ToList();
            foreach ( Text t in texts )
                Console.WriteLine( "Text [" + t.Name + "]: " + t.Chains.Sum( x => x.DistinctHeads() ) );
        }

        static void TextsWithMoreChainsWithAtLeastOneDistinctHead( List<Text> texts )
        {
            Console.WriteLine( "Texts with more chains containing at least one distinct head" );
            Func<Chain, bool> chainWithAtLeastOneDistinctHead = y => y.DistinctHeads() > 0;
            texts = texts.OrderByDescending( x => x.Chains.Where( chainWithAtLeastOneDistinctHead ).Count() ).Take( 3 ).ToList();
            foreach ( Text t in texts )
                Console.WriteLine( "Text [" + t.Name + "]: " + t.Chains.Where( chainWithAtLeastOneDistinctHead ).Count() );
        }
    }
}
