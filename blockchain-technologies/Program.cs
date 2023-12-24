using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace blockchain_technologies;

public class Program
{
    static void Main()
    {
        var blockchain = new Blockchain();

        while (true)
        {
            Console.WriteLine("1. Add Vote");
            Console.WriteLine("2. Display Blockchain");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddVote(blockchain);
                    break;
                case "2":
                    DisplayBlockchain(blockchain);
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void AddVote(Blockchain blockchain)
    {
        Console.Write("Voter: ");
        string voter = Console.ReadLine();

        Console.Write("Candidate: ");
        string candidate = Console.ReadLine();

        blockchain.AddVote(voter, candidate);
    }

    static void DisplayBlockchain(Blockchain blockchain)
    {
        foreach (var block in blockchain.Chain)
        {
            Console.WriteLine($"Index: {block.Index}");
            Console.WriteLine($"Timestamp: {block.Timestamp}");
            Console.WriteLine($"Previous Hash: {block.PreviousHash}");
            Console.WriteLine($"Hash: {block.Hash}");
            Console.WriteLine($"Nonce: {block.Nonce}");
            Console.WriteLine("Votes:");

            foreach (var vote in block.Votes)
            {
                Console.WriteLine($"  Voter: {vote.Voter}");
                Console.WriteLine($"  Candidate: {vote.Candidate}");
                Console.WriteLine($"  Signature: {BitConverter.ToString(vote.Signature)}");
                Console.WriteLine();
            }

            Console.WriteLine("---------------------");
        }
    }
}
