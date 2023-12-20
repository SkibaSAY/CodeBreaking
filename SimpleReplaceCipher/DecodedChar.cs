using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBreaking
{
    public class DecodedChar
    {
        public CharItem InputChar { get; set; }
        public CharItem OutputChar { get; set; }

        public bool IsReady { get; private set; }
        public DecodedChar()
        {

        }
        public DecodedChar(CharItem item)
        {
            InputChar = item;
        }

        public void Ready()
        {
            IsReady = true;
        }
        public void UnReady()
        {
            IsReady = false;
        }
        public override string ToString()
        {
            return $"{InputChar} -> {OutputChar}";
        }
    }

    public class CharItem
    {
        public char Value { get; set; }
        public int Count { get; set; }
        public override string ToString()
        {
            return $"<{Value}:{Count}>";
        }
    }
}
