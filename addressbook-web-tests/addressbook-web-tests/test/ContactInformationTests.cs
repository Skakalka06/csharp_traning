using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {

        [Test]
        public void ContactInformationFromHomePageTest()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //проверки

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void ContactInformationFromDetailsPageTest()
        {
            ContactData fromPage = app.Contacts.GetContactInformationFromDetailsPage(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            //проверки

            Assert.AreEqual(fromPage, fromForm);
            Assert.AreEqual(fromPage.FullInformation, fromForm.FullInformation);
            //Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            //Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

    }
}
