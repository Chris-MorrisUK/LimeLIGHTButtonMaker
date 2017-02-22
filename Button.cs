using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApplication1
{
    [XmlType(TypeName = "Button")]
    public class LLButton
    {
        [XmlElement]
        public string Name;
        public OutputRange ORng;
        [XmlAttribute]
        public bool Rowstart;

        public LLButton():this(string.Empty,0,0,false,false)
        {

        }


        public LLButton(string name, uint min , uint max,bool rowStart,bool useShortName)
        {
            Name = useShortName ? shorten(name) : name;
            ORng = new OutputRange(min, max);
            Rowstart = rowStart;
        }

        static readonly string[] splitOn = {" ", "-->", "-"};
        private static string shorten(string name)
        {            
            name = name.Replace("\"",string.Empty);
            string[] words = name.Split(splitOn,StringSplitOptions.RemoveEmptyEntries);
            StringBuilder result = new StringBuilder();
            int wordCount = words.Count();
            for (int idx = 1; idx < wordCount; idx++)//deliberately lose the first word, which is actually a number
            {
                switch (words[idx].ToLower())
                {
                    case "black":
                        words[idx] = "Blk";
                        break;
                    case "white":
                        words[idx] = "W";
                        break;
                    case "red":
                        words[idx] = "R";
                        break;
                    case "blue":
                        words[idx] = "B";
                        break;
                    case "green":
                        words[idx] = "G";
                        break;
                    case "cyan":
                        words[idx] = "C";
                        break;
                    case "magenta":
                        words[idx] = "M";
                        break;
                    case "yellow":
                        words[idx] = "Y";
                        break;
                    case "fast":
                        words[idx] = "fst";
                        break;
                    case "slowly":
                    case "slow":
                        words[idx] = "slw";
                        break;
                    case "\'":
                    case "\"":
                        words[idx] = string.Empty;
                        break;
                    case "horizontal":
                        words[idx] = "Horz.";
                        break;
                    case "vertical":
                        words[idx] = "Vert.";
                        break;
                    case "diagonal":
                        words[idx] = "Diag.";
                        break;
                    case "colour":
                    case "color":
                        words[idx] = "Col.";
                        break;
                }
                result.Append(words[idx] + " ");
            }
            return result.ToString().Trim();
        }
    }
}
