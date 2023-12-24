using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace blockchain_technologies
{
    public class Vote
    {
        public string Voter { get; set; }
        public string Candidate { get; set; }
        public byte[] Signature { get; set; }

        public string GetHash()
        {
            string voteString = $"{Voter}{Candidate}{Signature}";
            return CalculateHash(voteString);
        }

        private string CalculateHash(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }


}
