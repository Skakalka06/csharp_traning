using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper :HelperBase
    {

        public ContactHelper(ApplicationManager manager) 
            : base(manager)
        {
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            string allEmails = cells[4].Text;
            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails,

            };
        }



        public ContactData GetContactInformationFromDetailsPage(int index)
        {

            manager.Navigator.OpenHomePage();
            SelectDetailsContact(0);
            string[] firstandlastName = driver.FindElement(By.Id("content"))
                .FindElement(By.TagName("b")).Text.Split(' ');
            
            string firstName = firstandlastName[0];
            string lastName = firstandlastName[1];

            string fullInformation = driver.FindElement(By.Id("content")).Text;



            return new ContactData(firstName, lastName)
            {
                FullInformation = fullInformation

            }; 

        }

        public void SelectDetailsContact(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);

        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectEditContact(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(v);
            RemoveContact();
            SubmitDeleted();
            manager.Navigator.OpenHomePage();

            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(contact.Id);
            RemoveContact();
            SubmitDeleted();
            manager.Navigator.OpenHomePage();

            return this;
        }

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitEdingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count() > 0);


            return this;

        }

        public ContactHelper DeleteContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter(group.Name);
            SelectContact(contact.Id);
            DeleteContactFromGroup();

            return this;
        }

        public void DeleteContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void CommitEdingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void ClearGroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count; 
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> items = element.FindElements(By.CssSelector("td"));
                    contactCache.Add(new ContactData(items[1].Text, items[2].Text));
                }
            }
            return contactCache;
        }
        public ContactHelper Modify(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

                SelectEditContact(index);
                FillContactForm(newData);
                SubmitContactModification();
                ReturnToHomePage();

            return this;
        }

        public ContactHelper Modify(ContactData oldContact, ContactData newContact)
        {
            manager.Navigator.OpenHomePage();

            SelectEditContact(oldContact.Id);
            FillContactForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();

            return this;
        }



        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenAddNewPage();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public void CreateContactIfNotExist()
        {
            manager.Navigator.OpenHomePage();
            if (!IsContactExist())
            {
                ContactData contact = new ContactData("Pupkina", "Maria");
    
                Create(contact);
            }
        }

        public bool IsContactExist()
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])"));
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }



        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }


        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(String id)
        {
            //driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            driver.FindElement(By.Id(id)).Click();
            return this;
        }


        public ContactHelper SubmitDeleted()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectEditContact(int index)
        {
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();

            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper SelectEditContact(String id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])"))
                .FindElement(By.XPath("(//img[@alt='Edit'])")).Click();
            return this;
        }




        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}
