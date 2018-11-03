using System.Collections.Generic;

namespace CorrefPtCorpusScripts
{
    public class Chain
    {
        public string Id { get; set; }
        public List<string> Heads { get; set; }

        public Chain(string id)
        {
            this.Id = id;
            Heads = new List<string>();
        }

        public int DistinctHeads()
        {
            if (Heads.Count > 0)
                return this.Heads.Count - 1;
            else
                throw new System.Exception();
        }
    }
}
