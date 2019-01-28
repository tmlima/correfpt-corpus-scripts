using System.Collections.Generic;

namespace CorrefPtCorpusScripts
{
    public class Text
    {
        public string Name { get; set; }
        public List<Chain> Chains { get; set; }
        public int NounPhraseQuantity { get; set; }

        public Text(string name)
        {
            this.Name = name;
            this.Chains = new List<Chain>();
        }
    }
}
