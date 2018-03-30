using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class ContactCreationTests :TestBase
    {
      
        [Test]
        public void ContactCreationTest()
        {

            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));

            ContactData contact = new ContactData();
            contact.Firstame = "Ivan";
            contact.Lastname = "Pupkin";
            app.Navigator.OpenAddNewPage(); 
            app.Contacts.AddNewContact(contact);
            app.Contacts.SubmitContactCreation(); 

        }

    }
}

