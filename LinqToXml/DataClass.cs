using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXml
{
    class DataClass
    {
        public static XDocument CreateData()
        {
            XDocument cdData = new XDocument(
                new XDeclaration("1.0", "utf8", "yes"),
                new XComment("Our CD Data"),
                new XElement("CDStoreDate",
                    new XAttribute("storeName", "Leaside"),
                    new XAttribute("Location", "Toronto, ON"),

                    new XElement("CD",
                        new XElement("Title","Love Gun"),
                        new XElement("Artist","Kiss"),
                        new XElement("Genre", "Rock"),
                        new XElement("SalesInfo", 
                            new XElement("Price","11.99"),
                            new XElement("Qty","5"))),

                    new XElement("CD",
                        new XElement("Title", "Let there Be Rock"),
                        new XElement("Artist", "AC/DC"),
                        new XElement("Genre", "Rock"),
                        new XElement("SalesInfo",
                            new XElement("Price", "16.99"),
                            new XElement("Qty", "10"))),

                    new XElement("CD",
                        new XElement("Title", "Live At Legends"),
                        new XElement("Artist", "Buddy Guy"),
                        new XElement("Genre", "Blues"),
                        new XElement("SalesInfo",
                            new XElement("Price", "12.99"),
                            new XElement("Qty", "8"))),

                    new XElement("CD",
                        new XElement("Title", "Black And Blu"),
                        new XElement("Artist", "Gary Clark Jr"),
                        new XElement("Genre", "Blues"),
                        new XElement("SalesInfo",
                            new XElement("Price", "12.99"),
                            new XElement("Qty", "7")))
               )
            );

            return cdData;
        }

        public static void SaveData(string pFileName)
        {
            XDocument document = CreateData();
            document.Save(pFileName);
        }

        public static void QueryData(XDocument pDoc)
        {
            var data = from item in pDoc.Descendants("CD")
                       where (item.Element("Genre").Value == "Blues")
                       where ( ((string)( item.Element("Artist").Value )).Contains("Jr"))
                       select new
                       {
                           Titre = item.Element("Title").Value,
                           Artist = item.Element("Artist").Value,
                           Price = item.Element("SalesInfo").Element("Price").Value,
                           Genre = item.Element("Genre").Value
                       };

            foreach (var info in data)
            {
                Console.WriteLine(info.ToString() );
            }
        }
    }
}
