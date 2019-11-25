using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortUrl
{
    public class ShortUrl
    {
        private const string DEFAULT_ALPHABET = "mn6j2c4rv8bpygw95z7hsdaetxuk3fq";
        private const int DEFAULT_BLOCK_SIZE = 24;
        private const int MIN_LENGTH = 5;

        private string userAlphabet;
        private int userBlockSize;
        private int mask;
        private IEnumerable<int> mapping;

        public ShortUrl(string alphabet=DEFAULT_ALPHABET, int blockSize= DEFAULT_BLOCK_SIZE)
        {
            userAlphabet = alphabet;
            userBlockSize = blockSize;

            mask = (1 << blockSize) - 1;
            mapping = Enumerable.Range(0, blockSize);
        }

        public string EncodeUrl(int n, int minLength=MIN_LENGTH)
        {
            return Enbase(Encode(n), minLength);
        }

        public int Encode(int n)
        {
            return (n & ~mask) | InternalEncode(n & mask);
        }

        private int InternalEncode(int n)
        {
            int result = 0;
            List<int> reversed = mapping.Reverse().ToList();
            for (int i = 0; i < reversed.Count(); i++)
            {
                if ((n & (1 << i)) != 0)
                {
                    result |= (1 << reversed[i]);
                }
            }
            return result;
        }

        public string Enbase(int x, int minLength=MIN_LENGTH)
        {
            string result = InternalEnbase(x);
            char paddingChar = userAlphabet[0];
            return result.PadLeft(minLength, paddingChar);
        }

        private string InternalEnbase(int x)
        {
            int n = userAlphabet.Length;
            if (x < n)
            {
                return userAlphabet[x].ToString();
            }
        }
    }
}
