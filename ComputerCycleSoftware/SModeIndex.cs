using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCycleSoftware
{
    public class
         SModeIndex
    {
        public string indexing(string mode)
        {
            string pos = mode;
            string sub = pos.Substring(7, 1);
            return sub;
        }
    }
}

//SMode in HRM file designates units. It is the 8th character along. 0 is kilometers. 
//1 is miles. This class finds the 8th character 