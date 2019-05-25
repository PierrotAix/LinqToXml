using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        /// <summary>
        /// Tutorial https://www.youtube.com/watch?v=20PK4fOzEZw
        /// de Aaron Lehaney 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //XDocument myDocument = DataClass.CreateData();
            //Console.WriteLine(myDocument.ToString());

            DataClass.SaveData("myCDdata.xml");

            XDocument myDoc = XDocument.Load("myCDdata.xml");

            DataClass.QueryData(myDoc);

            Console.ReadKey();

        }
    }
}
