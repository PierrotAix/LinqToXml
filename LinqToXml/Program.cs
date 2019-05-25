using System;
using System.Collections.Generic;
using System.IO;
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
           // Test01(); //

           // Test02();

            Test03();

            Console.ReadKey();

        }

        private static void Test03()
        {
            // Part 5 Transforming XML to CSV using LINQ to XML https://www.youtube.com/watch?v=b93ZT4ecij8&list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk&index=5
            XDocument myDoc = XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\XMLFileExample.xml");
            myDoc.Save(Console.Out);

            // 
            StringBuilder sb = new StringBuilder();
            string delimiter = ",";
            XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\XMLFileExample.xml")
                .Descendants("Student").ToList().ForEach(
                    element => sb.Append(element.Attribute("Country").Value + delimiter +
                                         element.Element("Name").Value + delimiter +
                                         element.Element("Gender").Value + delimiter +
                                         element.Element("TotalMarks").Value + "\r\n")
                                         );
            StreamWriter sw = new StreamWriter(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\Result.csv");
            sw.WriteLine(sb.ToString());
            sw.Close();
            /*
                USA,Mark,Male,800
                USA,Rosy,Female,900
                India,Pam,Female,850
                India,John,Male,950
             * */

        }

        private static void Test02()
        {
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

            Console.WriteLine("----------------------------------------------------------------------------------------");
            // https://www.youtube.com/watch?v=OsfVJ485RY4&list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk&index=4 Part 4 Modifying xml document using linq to xml
            IEnumerable<string> artists01 = from CD in XDocument.Load("myCDdata.xml")
                                    .Element("CDStoreData").Elements("CD")
                                    .Where(x => Int32.Parse(x.Element("SalesInfo").Element("Price").Value) > 12)
                                            orderby Int32.Parse(CD.Element("SalesInfo").Element("Price").Value) descending
                                            select CD.Element("Artist").Value;

            foreach (var artist in artists01)
            {
                Console.WriteLine($"Nom de l'artiste: {artist}");
            }
        }

        private static void Test01()
        {
            //XDocument myDocument = DataClass.CreateData();
            //Console.WriteLine(myDocument.ToString());

            DataClass.SaveData("myCDdata.xml");

            XDocument myDoc = XDocument.Load("myCDdata.xml");

            DataClass.QueryData(myDoc);
        }
    }
}
