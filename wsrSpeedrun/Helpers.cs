using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsrSpeedrun
{
    static class Helpers
    {
        static public bool IsOkeyWithLevenstneinsLength(string original, string reference, int length) {

            if (string.IsNullOrEmpty(original))
                return true;

            original = original.Trim();
            if (string.IsNullOrEmpty(reference))
                return true;

            int min = Math.Min(original.Length, reference.Length);

            length -= Math.Max(original.Length, reference.Length) - min;

            for(int i = 0; i < min; i++)
                if (original[i] != reference[i])
                    length--;
            
            return length >= 0;
        }
    }
}
