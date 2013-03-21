using System.Collections.Generic;

namespace JUtil
{
    public class CharMap
    {
        public static IList<char> GetMap()
        {
            return charMapList.AsReadOnly();
        }

        private static List<char> charMapList;

        static CharMap()
        {
            charMapList = new List<char>();

            charMapList.Add('A');
            charMapList.Add('B');
            charMapList.Add('C');
            charMapList.Add('D');
            charMapList.Add('E');
            charMapList.Add('F');
            charMapList.Add('G');
            charMapList.Add('H');
            charMapList.Add('I');
            charMapList.Add('J');

            charMapList.Add('K');
            charMapList.Add('L');
            charMapList.Add('M');
            charMapList.Add('N');
            charMapList.Add('O');
            charMapList.Add('P');
            charMapList.Add('Q');
            charMapList.Add('R');
            charMapList.Add('S');
            charMapList.Add('T');

            charMapList.Add('U');
            charMapList.Add('V');
            charMapList.Add('W');
            charMapList.Add('X');
            charMapList.Add('Y');
            charMapList.Add('Z');

            charMapList.Add('0');
            charMapList.Add('1');
            charMapList.Add('2');
            charMapList.Add('3');
            charMapList.Add('4');
            charMapList.Add('5');
            charMapList.Add('6');
            charMapList.Add('7');
            charMapList.Add('8');
            charMapList.Add('9');
        }
    }
}
