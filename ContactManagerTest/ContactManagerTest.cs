using ContactManager.Interfaces;
using System.Text.Json;

namespace ContactManagerTest
{
    public class ContactManagerTest
    {
        private IContactManager contactManager = new ContactManager.Models.ContactManager(false);

        [Fact]
        public void TestAddContact()
        {
            //Arrange
            deleteContacts();
            Contact testContact = new Contact();
            testContact.FirstName = "TestFirstName";
            testContact.LastName = "TestLastName";
            testContact.PhoneNumber = "1234567890";
            testContact.Email = "example@example.com";
            testContact.Address = "TestHome";

            // Act
            var Result = contactManager.AddContact(testContact);

            // Assert
            Assert.True(Result);

            // Act
            var Result2 = contactManager.AddContact(testContact);

            // Assert
            Assert.False(Result2);
        }

        [Fact] 
        public void TestGetContactsSorted()
        {
            // Arrange
            deleteContacts();

            Contact testContact = new Contact();
            testContact.FirstName = "TestFirstName1";
            testContact.LastName = "TestLastName1";
            testContact.PhoneNumber = "1234567890";
            testContact.Email = "example1@example.com";
            testContact.Address = "TestHome";

            Contact testContact2 = new Contact();
            testContact2.FirstName = "TestFirstName2";
            testContact2.LastName = "TestLastName2";
            testContact2.PhoneNumber = "1234567890";
            testContact2.Email = "example2@example.com";
            testContact2.Address = "TestHome";

            contactManager.AddContact(testContact2);
            contactManager.AddContact(testContact);

            // Act
            var result = contactManager.GetContactsSorted();

            // Assert
            Assert.NotNull(result);

            // Check the sorting order (assuming sorting is based on LastName)
            Assert.Equal(testContact.LastName, result[0].LastName);
            Assert.Equal(testContact2.LastName, result[1].LastName);
        }

        [Fact]
        public void TestGetContactByIndex()
        {
            // Arrange
            deleteContacts();

            Contact testContact2 = new Contact
            {
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                PhoneNumber = "123456789",
                Email = "example@example.com2",
                Address = "TestHome2"
            };

            Contact testContact1 = new Contact
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                PhoneNumber = "123456789",
                Email = "example@example.com1",
                Address = "TestHome1"
            };

            contactManager.AddContact(testContact2);
            contactManager.AddContact(testContact1);

            // Act
            var result = contactManager.GetContactByIndex(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testContact2.FirstName, result.FirstName);
            Assert.Equal(testContact2.LastName, result.LastName);
            Assert.Equal(testContact2.PhoneNumber, result.PhoneNumber);
            Assert.Equal(testContact2.Email, result.Email);
            Assert.Equal(testContact2.Address, result.Address);
        }

        [Fact]
        public void TestDeleteContactByEmail()
        {
            //Arrange
            deleteContacts();

            Contact testContact2 = new Contact();
            testContact2.FirstName = "TestFirstName2";
            testContact2.LastName = "TestLastName2";
            testContact2.PhoneNumber = "1234567890";
            testContact2.Email = "example@example.com2";
            testContact2.Address = "TestLastName2";

            Contact testContact1 = new Contact();
            testContact1.FirstName = "TestFirstName1";
            testContact1.LastName = "TestLastName1";
            testContact1.PhoneNumber = "12345678901";
            testContact1.Email = "example@example.com1";
            testContact1.Address = "TestLastName1";

            contactManager.AddContact(testContact2);
            contactManager.AddContact(testContact1);

            // Act
            var Result = contactManager.DeleteContactByEmail(testContact2.Email);

            // Assert
            Assert.True(Result);
        }

        [Fact] 
        public void TestDisplayContactByIndex()
        {
            //Arrange
            deleteContacts();

            Contact testContact2 = new Contact
            {
                FirstName = "TestFirst",
                LastName = "TestLast",
                PhoneNumber = "1234567890",
                Email = "example@example.com",
                Address = "TestAddress"
            };

            contactManager.AddContact(testContact2);

            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw); // Redirect console output to StringWriter
                contactManager.DisplayContactByIndex(0); // Change 0 to the index of the contact you added
                string consoleOutput = sw.ToString(); // Get the console output

                // Assert
                Assert.Contains("1234567890", consoleOutput);
                Assert.Contains("example@example.com", consoleOutput);
                Assert.Contains("TestAddress", consoleOutput);
            }
        }

        [Fact]
        public void TestGetContactByEmail()
        {
            // Arrange
            deleteContacts();

            Contact testContact = new Contact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "0000-0001",
                Email = "Test1@Test.com",
                Address = "TestHome1"
            };

            contactManager.AddContact(testContact);

            // Act
            Contact resultExist = contactManager.GetContactByEmail("Test1@Test.com");
            Contact resultNotExist = contactManager.GetContactByEmail("NonExistent@Test.com");

            // Assert
            Assert.NotNull(resultExist);
            Assert.Equal("TestFirstName", resultExist?.FirstName);
            Assert.Equal("TestLastName", resultExist?.LastName);

            Assert.Null(resultNotExist);
        }

        [Fact]
        public void TestUpdateContactByEmail()
        {
            // Arrange
            deleteContacts();

            Contact testContact = new Contact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "0000-0001",
                Email = "Test@Test.com",
                Address = "TestHome"
            };

            contactManager.AddContact(testContact);

            // Act
            Contact updatedContact = new Contact
            {
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                PhoneNumber = "0000-0002",
                Email = "Test@Test.com",
                Address = "UpdatedHome"
            };

            bool updateResult = contactManager.UpdateContactByEmail("Test@Test.com", updatedContact);
            Contact updatedContactResult = contactManager.GetContactByEmail("Test@Test.com");

            // Assert
            Assert.True(updateResult, "Update should return true for success");
            Assert.NotNull(updatedContact);
            Assert.Equal("UpdatedFirstName", updatedContact?.FirstName);
            Assert.Equal("UpdatedLastName", updatedContact?.LastName);
            Assert.Equal("0000-0002", updatedContact?.PhoneNumber);
            Assert.Equal("UpdatedHome", updatedContact?.Address);
        }


        [Fact] //nour
        public void TestValidateEmailPhone()
        {
            // Arrange
            deleteContacts();

            Contact validContact = new Contact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "0000-0001",
                Email = "Test@Test.com",
                Address = "TestHome"
            };

            Contact invalidContactPhone = new Contact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "1",
                Email = "Test@Test.com",
                Address = "TestHome"
            };

            Contact invalidContactEmail = new Contact
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                PhoneNumber = "0000-0001",
                Email = "TestTest.com",
                Address = "TestHome"
            };

            //Act
            bool result1 = contactManager.ValidateEmailPhone(validContact.Email, validContact.PhoneNumber);
            bool result2 = contactManager.ValidateEmailPhone(invalidContactEmail.Email, invalidContactEmail.PhoneNumber);
            bool result3 =  contactManager.ValidateEmailPhone(invalidContactPhone.Email, invalidContactPhone.PhoneNumber);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

        private void deleteContacts()
        {
            List<Contact> contacts = contactManager.GetContactsSorted();
            Contact[] clonedContacts = new Contact[contacts.Count];
            contacts.CopyTo(clonedContacts, 0);
            foreach (var contact in clonedContacts)
            {
                contactManager.DeleteContactByEmail(contact.Email);
            }
        }
    }
}
