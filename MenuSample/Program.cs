using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MenuSample
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length < 2)
            {
                Console.WriteLine("Please include the file path to the menu xml and the active path when running this program.");
                return;
            }

            //string menuLoc = @"c:\Users\vampg\Documents\Avinode Homework\schedAeroMenu.xml"; //args[0]; 
            //string activePath = "/Requests/OpenQuotes.aspx"; //args[1];

            string menuLoc = @args[0];
            string activePath = args[1];

            XDocument menu = XDocument.Load(menuLoc);
            XElement m = menu.Element("menu");
            var xItems = m.Elements("item");

            foreach(var item in xItems)
            {
                string name = item.Element("displayName").Value;
                string path = item.Element("path").Attribute("value").Value;
                bool isActive = activePath.Equals(path);

                List<string> itemList = new List<string>();
                itemList.Add(string.Format("{0}, {1}", name, path));

                if (item.Elements("subMenu").Any())
                {
                    var subMenu = item.Element("subMenu");
                    var subItems = subMenu.Elements("item");

                    foreach (var subItem in subItems)
                    {
                        string subName = subItem.Element("displayName").Value;
                        string subPath = subItem.Element("path").Attribute("value").Value;

                        bool subActive = activePath.Equals(subPath);
                        isActive = subActive ? subActive : isActive;

                        string active = subActive ? "ACTIVE" : string.Empty;

                        itemList.Add(string.Format("\t {0}, {1} {2}", subName, subPath, active));
                    }
                }

                itemList[0] = isActive ? itemList[0] + " ACTIVE" : itemList[0];

                foreach (var line in itemList)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
