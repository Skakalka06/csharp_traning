using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string type = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];
            if (type == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                        Header = TestBase.GenerateRandomString(10),
                        Footer = TestBase.GenerateRandomString(10)
                    });
                }

                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, filename);
                }
                else
                {

                    StreamWriter writer = new StreamWriter(filename);
                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }

                    else
                    {
                        System.Console.Out.Write("Unrecognized format" + " " + format);
                    }
                    writer.Close();
                }
            }

            else if (type == "contacts")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData()
                    {
                        Firstname = TestBase.GenerateRandomString(10),
                        Lastname = TestBase.GenerateRandomString(10)
                    });
                }
                StreamWriter writer = new StreamWriter(filename);
                if (format == "csv")
                {
                    WriteContactsToCsvFile(contacts, writer);
                }
                else if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }

                else
                {
                    System.Console.Out.Write("Unrecognized format" + " " + format);
                }
                writer.Close();
            }
            else
            {
                System.Console.Out.Write("Unrecognized type" + " " + type);
            }
        }

        public static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        public static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        public static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1}",
                    contact.Firstname, contact.Lastname));
            }
        }

        public static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.Name;
                sheet.Cells[row, 2] = group.Header;
                sheet.Cells[row, 3] = group.Footer;
                row++;
            }

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(fullPath);
            wb.Close();
            app.Visible = false;
            app.Quit();
        }

        public static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }

        }

        public static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }


        public static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
