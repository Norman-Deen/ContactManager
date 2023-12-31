using ContactManager.Interfaces;
using ContactManager.Models;

namespace ContactManager
{
    internal class Programf
    {
        static void Main()
        {
            Console.Title = "Contacts Manager V1.0";
            IContactManager contactManager = new Models.ContactManager(true);

            while (true)  //loop to keep the application running continuously
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Copyright ©  by NourAb");   //title
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("1 Display Contacts");   //main menu
                Console.WriteLine("2 Add Contact");
                Console.WriteLine("3 Update Contact");
                Console.WriteLine("4 Delete Contact");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("5 Exit");
                Console.ResetColor();
                Console.WriteLine();
                   ConsoleKeyInfo key = Console.ReadKey(intercept: true);

                switch (key.KeyChar)        //read and handle user input
                {
                    case '1': //Display Contacts
                             Console.Clear();
                             Console.ForegroundColor = ConsoleColor.DarkGray;
                             Console.WriteLine("----All Contacts----");
                             Console.ResetColor();
                             Console.WriteLine();
                             contactManager.DisplayContacts();
                             break;

                    case '2': //Add Contact"
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Add New Contact\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("FirstName: ");
                        string firstName = Console.ReadLine();
                        Console.Write("LastName: ");
                        string lastName = Console.ReadLine();
                        Console.Write("PhoneNumber: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Address: ");
                        string address = Console.ReadLine();
                        Console.ResetColor();

                        Contact newContact = new Contact
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            PhoneNumber = phoneNumber,
                            Email = email,
                            Address = address
                        };

                        contactManager.AddContact(newContact);
                        break;

                    case '3': //Update Contact
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("Enter the contact email you need to update: ");
                        string emailToUpdate = Console.ReadLine();
                        
                        if (contactManager.GetContactByEmail(emailToUpdate) != null) // Check if the email exists
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write($"Email: \"{emailToUpdate}\" is exists  \n\n");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("Enter new FirstName: ");
                            string newFirstName = Console.ReadLine();
                            Console.Write("Enter new LastName: ");
                            string newLastName = Console.ReadLine();
                            Console.Write("Enter new PhoneNumber: ");
                            string newPhoneNumber = Console.ReadLine();
                            Console.Write("Enter new Email: ");
                            string newEmail = Console.ReadLine();
                            Console.Write("Enter new Address: ");
                            string newAddress = Console.ReadLine();

                            Contact updatedContact = new Contact // Create an updated contact
                            {
                                FirstName = newFirstName,
                                LastName = newLastName,
                                PhoneNumber = newPhoneNumber,
                                Email = newEmail,
                                Address = newAddress
                            };
                            if (contactManager.UpdateContactByEmail(emailToUpdate, updatedContact)) // Update the contact
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Contact with email '{emailToUpdate}' updated successfully.\n");
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine($"Failed to update contact with email '{emailToUpdate}'.");
                            }

                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Press any key to return to the main menu");
                            Console.ResetColor();
                           // Console.ReadKey(); //I have error here, must Check
                        }
                        else
                        {
                            Console.Clear(); // Email does not exist, show error message
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Error: Contact with email '{emailToUpdate}' not found.\n");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Press any key to return to the main menu");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        break;

                    case '4': //Delete Contact
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Enter email contact to delete:");
                        Console.ResetColor();
                        string emailToDelete = Console.ReadLine();
                        contactManager.DeleteContactByEmail(emailToDelete);
                        break;

                    case '5': //Exit app
                        Environment.Exit(0);
                        break;
                }

            }
        }
    }
}
