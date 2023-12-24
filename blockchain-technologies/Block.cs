using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockchain_technologies
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Vote> Votes { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public string MerkleRoot { get; set; }
        public int Nonce { get; set; }
    }

}
