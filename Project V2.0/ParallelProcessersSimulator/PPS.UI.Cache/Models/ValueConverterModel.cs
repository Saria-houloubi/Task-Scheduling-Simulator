using System;
using System.Collections.Generic;
using PPS.UI.Shared.Models.Base;
using PPS.UI.Cache.Enums;

namespace PPS.UI.Cache.Models
{
  public class ValueConverterModel: BaseNotifyModel
    {
        #region properties
        private Dictionary<int, int> _MemorySizesDictionary;        
        private Dictionary<int, string> _StringMemorySizesDictionary;
        private Dictionary<string, int> _FlipStringMemorySizesDictionary;
        #region map between hexadecimal and binary values
       private static Dictionary<string, string> _BinaryHexadecimalValue = new Dictionary<string, string>
            {
                {"0000","0" },
                {"0001","1" },
                {"0010","2" },
                {"0011","3" },
                {"0100","4" },
                {"0101","5" },
                {"0110","6" },
                {"0111","7" },
                {"1000","8" },
                {"1001","9" },
                {"1010","a" },
                {"1011","b" },
                {"1100","c" },
                {"1101","d" },
                {"1110","e" },
                {"1111","f" },
                {"0","0000" },
                {"1","0001" },
                {"2","0010" },
                {"3","0011"},
                {"4","0100"},
                {"5","0101"},
                {"6","0110"},
                {"7","0111"},
                {"8","1000"},
                {"9","1001"},
                {"a","1010"},
                {"b","1011"},
                {"c","1100"},
                {"d","1101"},
                {"e","1110"},
                {"f","1111"},
            };
        public static string GetHexadecimalValue(string binaryValue)
        {
            string result;
            if (_BinaryHexadecimalValue.TryGetValue(binaryValue, out result)) return result;
            return null;
        }
        #endregion

#endregion

        #region constructor
        public ValueConverterModel()
        {
            Initialization();
        }
        public void Initialization()
        {
            FillMemorySizes();
            FillStringMemorySizes();
            FillFlipStringMemorySizes();
        }
        #endregion

        #region Helper
        /// <summary>
        /// maps between the value of the enum MemorySizes and its corresponding memory size values and fill it in the <see cref="_MemorySizesDictionary"/>
        /// </summary>
        public void FillMemorySizes()
        {
            _MemorySizesDictionary = new Dictionary<int, int>();
            foreach (var item in Enum.GetValues(typeof(MemorySizes)))
            {
                int power = (int)Math.Pow(2, (10 * (int)item));//for instance if item= 1 then power= 2^10 wich is one KiloByte
                _MemorySizesDictionary.Add((int)item, power);
            }
        }
        /// <summary>
        /// fills the <see cref="_StringMemorySizesDictionary"/> with the MemorySizes values 
        /// </summary>
        public void FillStringMemorySizes()
        {
            _StringMemorySizesDictionary = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(typeof(MemorySizes)))
            {
                _StringMemorySizesDictionary.Add((int)item, Enum.GetName(typeof(MemorySizes), (int)item));
            }
        }
        /// <summary>
        /// maps between every MemorySizes value with its corresponding binary power value and fill it in the <see cref="_FlipStringMemorySizesDictionary"/>
        /// </summary>
        public void FillFlipStringMemorySizes()
        {
            _FlipStringMemorySizesDictionary = new Dictionary<string, int>();
            foreach (var item in Enum.GetValues(typeof(MemorySizes)))
            {
                int power = (int)Math.Pow(2, (10 * (int)item));
                _FlipStringMemorySizesDictionary.Add(Enum.GetName(typeof(MemorySizes), (int)item), power);
            }
        }

        /// <summary>
        /// converts the int value to the correct memory size
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string ConvertToString(int val)
        {
            int logVal = ((int)Math.Log(val, 2)) / 10;
            return (val / _MemorySizesDictionary[logVal] + _StringMemorySizesDictionary[logVal]);
        }
        /// <summary>
        /// convert the memory size to the correct int value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public int ConvertToInt(int val)
        {
            int logVal = ((int)Math.Log(val, 2)) / 10;
            return (val * _MemorySizesDictionary[logVal]);
        }
        /// <summary>
        /// related to associativity type in the cache view model
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string ConvertToWays(int val)
        {
            return (val + "ways");
        }
        public int ConvertMemorySizeTypeToByte(string memorySizeType)
        {
            return _FlipStringMemorySizesDictionary[memorySizeType];
        }
        #endregion
    }
}
