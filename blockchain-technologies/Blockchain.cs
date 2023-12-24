using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blockchain_technologies
{
    public class Blockchain
    {
        public List<Block> Chain { get; set; } = new List<Block>();
        private RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

        public Blockchain()
        {
            
            CreateNewBlock(new List<Vote>(), "0");
        }

        public void AddVote(string voter, string candidate)
        {
            var vote = CreateVote(voter, candidate);
            
               CreateNewBlock(new List<Vote> { vote }, Chain.Last().Hash);
               Console.WriteLine("Vote added successfully!");
           
        }

        private Vote CreateVote(string voter, string candidate)
        {
            var vote = new Vote
            {
                Voter = voter,
                Candidate = candidate,
                Signature = SignData($"{voter}{candidate}")
            };

            return vote;
        }

        private string CalculateMerkleRoot(List<Vote> votes)
        {
            if (votes.Count == 1)
            {

                return votes[0].GetHash();
            }
            return "0";
        }

            private void CreateNewBlock(List<Vote> votes, string previousHash)
        {
            int index = Chain.Count;
            DateTime timestamp = DateTime.Now;
            int nonce = 0;

            string hash = MineBlock(index, timestamp, votes, previousHash, nonce);
            var merkleRoot = CalculateMerkleRoot(votes);
            var newBlock = new Block
            {
                Index = index,
                Timestamp = timestamp,
                Votes = votes,
                PreviousHash = previousHash,
                MerkleRoot = merkleRoot,
                Hash = hash,
                Nonce = nonce
            };

            Chain.Add(newBlock);
        }

        private string MineBlock(int index, DateTime timestamp, List<Vote> votes, string previousHash, int nonce)
        {
            string data = $"{index}{timestamp}{votes}{previousHash}{nonce}";

            while (true)
            {
                string hash = CalculateHash(data);
                if (IsHashValid(hash))
                {
                    return hash;
                }

                nonce++;
                data = $"{index}{timestamp}{votes}{previousHash}{nonce}";
            }
        }

        private string CalculateHash(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private bool IsHashValid(string hash)
        {
          
            return hash.StartsWith("000");
        }

        private byte[] SignData(string data)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                byte[] signatureBytes = rsa.SignData(dataBytes, new SHA256CryptoServiceProvider());
                return signatureBytes;
            }
        }

  
    }
}