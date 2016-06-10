using System;
using System.Collections.Generic;
using System.IO;

namespace MarkovChain
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixCount = 2;
            var input = ReadInputFromFile("input.txt");
            var inputPairs = ParseInputPairs(input, prefixCount);
            var output = GenerateOutput(inputPairs, prefixCount);

            using (var sw = new StreamWriter("output.txt"))
            {
                sw.WriteLine(output);
            }
        }

        static string GenerateOutput(Dictionary<PrefixGroup, List<string>> input, int prefixCount, int wordLimit = 0)
        {
            var rnd = new Random();
            var result = new List<string>();
            var index = -prefixCount;
            string suffix = null;

            do
            {
                var prefixes = new List<string>();
                for (int i = 0; i < prefixCount; i++)
                {
                    if (index + i < 0)
                    {
                        prefixes.Add(null);
                    }
                    else
                    {
                        prefixes.Add(result[index + i]);
                    }
                }
                var pGroup = new PrefixGroup(prefixes.ToArray());
                List<string> value;

                if (input.TryGetValue(pGroup, out value))
                {
                    suffix = value[rnd.Next(value.Count)];
                    result.Add(suffix);
                }
                index++;
            } while (suffix != null && !(result.Count > wordLimit && wordLimit > 0));

            return string.Join(" ", result.ToArray());
        }

        static Dictionary<PrefixGroup, List<string>> ParseInputPairs(string[] input, int prefixCount)
        {
            var result = new Dictionary<PrefixGroup, List<string>>();
            var index = -prefixCount;
            string suffix = null;

            do
            {
                suffix = null;
                var prefixes = new List<string>();

                for (int i = 0; i < prefixCount; i++)
                {
                    if (index + i < 0)
                    {
                        prefixes.Add(null);
                    }
                    else
                    {
                        prefixes.Add(input[index + i]);
                    }
                }
                var pGroup = new PrefixGroup(prefixes.ToArray());
                List<string> value;

                if (prefixCount + index < input.Length)
                    suffix = input[prefixCount + index++];

                if(result.TryGetValue(pGroup, out value))
                {
                    value.Add(suffix);
                }
                else
                {
                    var suffixList = new List<string>();
                    suffixList.Add(suffix);
                    result.Add(new PrefixGroup(prefixes.ToArray()),suffixList);
                }

            } while (suffix != null);

            return result;
        }

        static string[] ReadInputFromFile(string path)
        {
            return File.ReadAllText(path).Replace("\r\n", " ").Split(' ');
        }
    }
}
