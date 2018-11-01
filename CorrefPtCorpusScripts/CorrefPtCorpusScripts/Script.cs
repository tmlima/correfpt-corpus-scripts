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
        public int AnalizeText(XmlDocument document)
        {
            int distinctRelationsFound = 0;
            XmlNodeList cadeias = document.SelectNodes("//Cadeias/*");
            foreach (XmlNode chain in cadeias)
            {
                distinctRelationsFound += AnalizeChain(chain);
            }

            return distinctRelationsFound;
        }

        private int AnalizeChain(XmlNode chain)
        {
            XmlNodeList snNodes = chain.SelectNodes("sn");
            List<string> heads = new List<string>();
            foreach (XmlNode sn in snNodes)
            {
                heads.Add(sn.Attributes["nucleo"].Value);
            }

            int distinctHeads = heads.Distinct().Count();
            if (distinctHeads > 0)
                return distinctHeads - 1;
            else
                throw new Exception();
        }
    }
}
