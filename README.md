# dotnet-short_url

A C# port of https://github.com/Alir3z4/python-short_url

Excerpt from original project readme:

> A bit-shuffling approach is used to avoid generating consecutive, predictable URLs. However, the algorithm is deterministic and will guarantee that no collisions will occur.
>
> The URL alphabet is fully customizable and may contain any number of characters. By default, digits and lower-case letters are used, with some removed to avoid confusion between characters like o, O and 0. The default alphabet is shuffled and has a prime number of characters to further improve the results of the algorithm.
>
> The intended use is that incrementing, consecutive integers will be used as keys to generate the short URLs. For example, when creating a new URL, the unique integer ID assigned by a database could be used to generate the URL by using this module. Or a simple counter may be used. As long as the same integer is not used twice, the same short URL will not be generated twice.

### Installation

```
dotnet add package dotnet-short_url
```

### Usage

Minimal console app example:

```
using System;
using ShortUrl;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var encoder = new UrlEncoder();

            int id = 1;
            string key = encoder.EncodeUrl(id);
            Console.WriteLine(key);
            // "867nv"

            int decodedId = encoder.DecodeUrl("867nv");
            Console.WriteLine(decodedId);
            // 1
        }
    }
}
```
