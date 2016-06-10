﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    class PrefixGroup
    {
        private string[] _prefixes;

        public PrefixGroup(params string[] prefixes)
        {
            _prefixes = prefixes;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < _prefixes.Length; i++)
            {
                sb.Append(_prefixes[i]);
                if (i < _prefixes.Length)
                    sb.Append(", ");
            }
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
