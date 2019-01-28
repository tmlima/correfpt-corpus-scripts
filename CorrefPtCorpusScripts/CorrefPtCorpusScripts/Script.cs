using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CorrefPtCorpusScripts
{
    public class Script
    {
        public Text AnalizeText(XmlDocument document, string textName)
        {
            Text text = new Text(textName);
            text.NounPhraseQuantity = document.SelectNodes( "//Cadeias//sn" ).Count;
            XmlNodeList cadeias = document.SelectNodes("//Cadeias/*");
            foreach (XmlNode chain in cadeias)
            {
                text.Chains.Add(AnalizeChain(chain));
            }

            return text;
        }

        private Chain AnalizeChain(XmlNode chainXml)
        {
            XmlNodeList snNodes = chainXml.SelectNodes("sn");
            List<string> heads = new List<string>();
            foreach (XmlNode sn in snNodes)
            {
                heads.Add(sn.Attributes["nucleo"].Value);
            }

            Chain chains = new Chain(chainXml.Name);
            chains.Heads.AddRange(heads.Distinct());
            return chains;
        }
    }
}
