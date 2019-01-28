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

            Console.WriteLine( "Noun phrase quantity [" + texts.Sum( x => x.NounPhraseQuantity ) + "]" );
            Console.WriteLine( "Indirect relationships [" + texts.Sum( x => x.Chains.Sum( y => y.IndirectHeads() ) ) + "]" );
            TextsWithMoreDistinctHeads( texts );
            TextsWithMoreChainsWithAtLeastOneDistinctHead( texts );

            Console.WriteLine( "Done" );

            Console.ReadKey();
        }

        static void Print(List<Text> texts)
        {
            foreach (Text t in texts)
            {
                Console.WriteLine("Distinct heads: " + t.Chains.Sum(x => x.IndirectHeads()));
                Console.WriteLine(t.Name);
                foreach (Chain c in t.Chains)
                    Console.WriteLine(c.Id + " : " + "[" + string.Join("][", c.Heads) + "]");
            }
        }

        static void TextsWithMoreDistinctHeads( List<Text> texts )
        {
            Console.WriteLine( "Texts with more distinct heads" );
            texts = texts.OrderByDescending( x => x.Chains.Sum( y => y.IndirectHeads() ) ).Take( 3 ).ToList();
            foreach ( Text t in texts )
                Console.WriteLine( "Text [" + t.Name + "]: " + t.Chains.Sum( x => x.IndirectHeads() ) );
        }

        static void TextsWithMoreChainsWithAtLeastOneDistinctHead( List<Text> texts )
        {
            Console.WriteLine( "Texts with more chains containing at least one distinct head" );
            Func<Chain, bool> chainWithAtLeastOneDistinctHead = y => y.IndirectHeads() > 0;
            texts = texts.OrderByDescending( x => x.Chains.Where( chainWithAtLeastOneDistinctHead ).Count() ).Take( 3 ).ToList();
            foreach ( Text t in texts )
                Console.WriteLine( "Text [" + t.Name + "]: " + t.Chains.Where( chainWithAtLeastOneDistinctHead ).Count() );
        }
    }
}
