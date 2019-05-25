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


            // Autre test inspiré de https://www.youtube.com/watch?v=Ii3QSkTiRA8&list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk&index=3
            IEnumerable<string> artists = from CD in XDocument.Load("myCDdata.xml")
                                                .Element("CDStoreData").Elements("CD")
                                              //where (int)Int32.Parse((CD.Element("SalesInfo").Element("Price").Value)) > 12
                                          //where CD.Element("SalesInfo").Element("Price").Value == "12"
                                          where Int32.Parse(CD.Element("SalesInfo").Element("Price").Value) > 12
                                          orderby Int32.Parse(CD.Element("SalesInfo").Element("Price").Value) descending
                                          select CD.Element("Artist").Value;

            foreach (var artist in artists)
            {
                Console.WriteLine($"Nom de l'artiste: {artist}");
            }


            Console.ReadKey();

        }
    }
}
