using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

            // Test03();

            //Test04();

            //Test05();

            //Test06();

            //Test07();

            //Test08(); // KO

            //Test09(); // OK

            //Test10();

            Test11(); // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-retrieve-the-value-of-an-element-linq-to-xml



            Console.ReadKey();

        }

        private static void Test11()
        {
            XElement e = new XElement("StringElement", "abcde");
            Console.WriteLine($"e: {e}"); //e: < StringElement > abcde </ StringElement >
            Console.WriteLine($"e.Value: {e.Value}"); // e.Value: abcde

            Console.WriteLine("-------------------------------------------------------------");

            XElement root = new XElement( "Root",
                new XElement("Child1","child 1 content"),
                new XElement("Child2","child 2 content")
                );

            // The following assignments show why it is easier to use  
            // casting when the element might or might not exist.
            string c1 = (string)root.Element("Child1");
            Console.WriteLine("c1:{0}" , c1 ?? "element does not exist"); //c1 == null ? "element does not exist" : c1) // c1:child 1 content

            string c3 = (string)root.Element("Child3");
            Console.WriteLine("c3:{0}" , c3 ?? "element does not exist"); //c3:element does not exist


        }

        /// <summary>
        /// https://stackoverflow.com/questions/12087014/how-can-i-get-all-text-nodes-from-xml-file
        /// </summary>
        private static void Test10()
        {
            string input = @"
            <root>
                <slide>
                    <Image>hi</Image>
                    <ImageContent>this</ImageContent>
                    <Thumbnail>is</Thumbnail>
                    <ThumbnailContent>A</ThumbnailContent>
                </slide>
            </root>";

            XDocument doc = XDocument.Parse(input);
            //You can also load data from file by passing file path to Load method
            //XDocument doc = XDocument.Load("Data.xml");
            foreach (var slide in doc.Root.Elements("slide"))
            {
                var words = slide.Elements().Select(el => el.Value);
                string s = String.Join(" ", words.ToArray());
            }
        }

        /// <summary>
        /// https://support.microsoft.com/fr-fr/help/307548/how-to-read-xml-from-a-file-by-using-visual-c
        /// </summary>
        private static void Test09()
        {
            XmlTextReader reader = new XmlTextReader(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\products.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }

        }

        private static void Test08()
        {
            XDocument xdoc = XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\products.xml");

            foreach (var infoWebService in xdoc.Root.Elements("products"))
                foreach (var str in infoWebService.Element("product").Elements("name").Elements().Select(x => x.Value).ToArray())
                    Console.WriteLine(str);
        }

        private static void Test07()
        {
            XDocument xdoc = XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\products.xml");

            // Selection
            Console.WriteLine("\n-Affichage de tout ------------------------------------------------------------");
            xdoc.Elements("things").Select(p => new

            //xdoc.Descendants("product").Select(p => new
            {
                id = p.Attribute("id").Value,
                name = p.Element("name").Value,
                price = p.Element("price").Value,
                currency = p.Element("price").Attribute("currency").Value
            }).ToList().ForEach(p =>
            {
                Console.WriteLine("Id: " + p.id);
                Console.WriteLine("Name: " + p.name);
                Console.WriteLine("Price: " + p.price);
                Console.WriteLine("Currency: " + p.currency);
                Console.WriteLine("=================================");
            }
            );
        }

        /// <summary>
        /// Reading XML using LINQ in C#
        /// https://www.youtube.com/watch?v=sfDPdflXbiM&t=173s
        /// </summary>
        private static void Test06()
        {
            XDocument xdoc = XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\products.xml");

            // Selection
            Console.WriteLine("\n-Affichage de tout ------------------------------------------------------------");

            xdoc.Descendants("product").Select( p => new
            {
                id = p.Attribute("id").Value,
                name = p.Element("name").Value,
                price = p.Element("price").Value,
                currency = p.Element("price").Attribute("currency").Value
            }).ToList().ForEach( p =>
            {
                Console.WriteLine("Id: " + p.id);
                Console.WriteLine("Name: " + p.name);
                Console.WriteLine("Price: " + p.price);
                Console.WriteLine("Currency: " + p.currency);
                Console.WriteLine("=================================");
            }
            );

            // using conditions
            Console.WriteLine("\n-Prix supérieurs à 1000 ------------------------------------------------------------");
            xdoc.Descendants("product")
                .Where(p => Convert.ToInt32(p.Element("price").Value) > 1000)
                .Select(p => new
                {
                    id = p.Attribute("id").Value,
                    name = p.Element("name").Value,
                    price = p.Element("price").Value,
                    currency = p.Element("price").Attribute("currency").Value
                }).ToList().ForEach(p =>
                {
                    Console.WriteLine("Id: " + p.id);
                    Console.WriteLine("Name: " + p.name);
                    Console.WriteLine("Price: " + p.price);
                    Console.WriteLine("Currency: " + p.currency);
                    Console.WriteLine("=================================");
                });

            Console.WriteLine("\n-Nom qui contien Nokia------------------------------------------------------------");
            xdoc.Descendants("product")
                .Where(p => p.Element("name").Value.Contains("Nokia") )
                .Select(p => new
                {
                    id = p.Attribute("id").Value,
                    name = p.Element("name").Value,
                    price = p.Element("price").Value,
                    currency = p.Element("price").Attribute("currency").Value
                }).ToList().ForEach(p =>
                {
                    Console.WriteLine("Id: " + p.id);
                    Console.WriteLine("Name: " + p.name);
                    Console.WriteLine("Price: " + p.price);
                    Console.WriteLine("Currency: " + p.currency);
                    Console.WriteLine("=================================");
                });

            Console.WriteLine("\n-Tri par prix ------------------------------------------------------------");
            xdoc.Descendants("product")
                //.Where(p => p.Element("name").Value.Contains("Nokia"))
                .Select(p => new
                {
                    id = p.Attribute("id").Value,
                    name = p.Element("name").Value,
                    price = p.Element("price").Value,
                    currency = p.Element("price").Attribute("currency").Value
                }).ToList()
                .OrderByDescending(p => p.price).ToList()
                .ForEach(p =>
                {
                    Console.WriteLine("Id: " + p.id);
                    Console.WriteLine("Name: " + p.name);
                    Console.WriteLine("Price: " + p.price);
                    Console.WriteLine("Currency: " + p.currency);
                    Console.WriteLine("=================================");
                });


        }

        //

        /// <summary>
        /// Part 7 Transform one XML format to another XML format using linq to xml
        /// </summary>
        private static void Test05()
        {
            XDocument xmlDocument = XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\XMLFileExample.xml");

            XDocument result = new XDocument(
                new XElement("Students",
                    new XElement("USA",
                    from s in xmlDocument.Descendants("Student")
                    where s.Attribute("Country").Value == "USA"
                    select new XElement("Student",
                        new XElement("Name", s.Element("Name").Value),
                        new XElement("Gender", s.Element("Gender").Value),
                        new XElement("TotalMarks", s.Element("TotalMarks").Value)
                        )),
                    new XElement("India",
                    from s in xmlDocument.Descendants("Student")
                    where s.Attribute("Country").Value == "India"
                    select new XElement("Student",
                        new XElement("Name", s.Element("Name").Value),
                        new XElement("Gender", s.Element("Gender").Value),
                        new XElement("TotalMarks", s.Element("TotalMarks").Value)
                        ))
                ));

            result.Save(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\Result.xml");
        }

        /// <summary>
        /// Part 6 Transforming XML to HTML table using LINQ to XML
        /// https://www.youtube.com/watch?v=nNMiyILom3s&list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk&index=7&t=0s
        /// </summary>
        private static void Test04()
        {
            XDocument result = new XDocument(
                new XElement("table",new XAttribute("border",1),
                    new XElement("thead",
                        new XElement("tr",
                            new XElement("th","Country"),
                            new XElement("th","Country"),
                            new XElement("th","Gender"),
                            new XElement("th", "TotalMarks")
                            )
                        ),
                    new XElement("tbody",
                    from student in XDocument.Load(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\XMLFileExample.xml")
                            .Descendants("Student")
                            select new XElement("tr",
                                new XElement("td", student.Attribute("Country").Value),
                                new XElement("td", student.Element("Name").Value),
                                new XElement("td", student.Element("Gender").Value),
                                new XElement("td", student.Element("TotalMarks").Value)
                        )
                )
                ));

            result.Save(@"D:\BITBUCKET\c-sharp\LinqToXML\LinqToXml\LinqToXml\Ressources\Result.htm");
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
