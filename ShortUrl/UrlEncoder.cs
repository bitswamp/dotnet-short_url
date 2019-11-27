using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortUrl
{
    public class UrlEncoder
    {
        private const string DEFAULT_ALPHABET = "mn6j2c4rv8bpygw95z7hsdaetxuk3fq";
        private const int DEFAULT_BLOCK_SIZE = 24;
        private const int MIN_LENGTH = 5;

        private readonly string Alphabet;
        private readonly int Mask;
        private readonly IEnumerable<int> Mapping;

        public UrlEncoder(string alphabet=DEFAULT_ALPHABET, int blockSize=DEFAULT_BLOCK_SIZE)
        {
            Alphabet = alphabet;

            Mask = (1 << blockSize) - 1;
            Mapping = Enumerable.Range(0, blockSize);
        }

        public string EncodeUrl(int n, int minLength=MIN_LENGTH)
        {
            return Enbase(Encode(n), minLength);
        }

        public int Encode(int n)
        {
            return (n & ~Mask) | InternalEncode(n & Mask);
        }

        private int InternalEncode(int n)
        {
            int result = 0;
            List<int> reversed = Mapping.Reverse().ToList();
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
            char paddingChar = Alphabet[0];
            return result.PadLeft(minLength, paddingChar);
        }

        private string InternalEnbase(int x)
        {
            int n = Alphabet.Length;
            if (x < n)
            {
                return Alphabet[x].ToString();
            }
            return InternalEnbase(x / n) + Alphabet[x % n];
        }

        public int DecodeUrl(string n)
        {
            return Decode(Debase(n));
        }

        public int Decode(int n)
        {
            return (n & ~Mask) | InternalDecode(n & Mask);
        }

        private int InternalDecode(int n)
        {
            int result = 0;
            List<int> reversed = Mapping.Reverse().ToList();
            for (int i = 0; i < Mapping.Count(); i++)
            {
                if ((n & (1 << reversed[i])) != 0)
                {
                    result |= (1 << i);
                }
            }
            return result;
        }

        public int Debase(string x)
        {
            int n = Alphabet.Length;
            int result = 0;
            string reversed = string.Concat(x.Reverse());
            for (int i = 0; i < x.Length; i++) {
                result += Alphabet.IndexOf(reversed[i]) * (int)Math.Pow(n, i);
            }
            return result;
        }
    }
}
