using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        

        [Test]
        public void ContactCreationTest()
        {

            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));

            //OpenAddNewPage(); // открываем страницу с контактами

            ContactData contact = new ContactData();
            // создаем экземпляр класса и заполняем поля имя и фамилия
            contact.Firstame = "Ivan";
            contact.Lastname = "Pupkin";
            //AddNewContact(contact); // добавляем новый контакт
            //SubmitContactCreation(); //сохраняем

        }
        
      /*  private void SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        private void AddNewContact(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstame);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);   
        }

        private void OpenAddNewPage()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }*/

       

        

        
    }
}

