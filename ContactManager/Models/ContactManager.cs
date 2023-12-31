using ContactManager.Interfaces;
using System.Text.Json;

namespace ContactManager.Models
{
   public class ContactManager : IContactManager
    {
        private readonly string JsonFilePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "JSON", "text.json");
        private List<Contact> contacts = new List<Contact>();
        private bool shouldUseConsole; 

        public ContactManager(bool useConcsole)
        {
            shouldUseConsole = useConcsole;
            LoadContacts();
        }

        public bool AddContact(Contact contact)
        {
            if (ContactExists(contact.Email))
            {
                DisplayErrorMessage($"Error: Contact with the same email '{contact.Email}' already exists.\n");
                return false;
            }
            // Validate phone number and email 
            if (!ValidateEmailPhone(contact.Email, contact.PhoneNumber))
            {
                DisplayErrorMessage("Error: Invalid phone number or email. Contact not added, Please check your input.\n     - The phone number must be between 7 and 10 digits.\n     - The email must contain the @ symbol.\n\n");
                return false;
            }
           
            contacts.Add(contact);
            contacts = contacts.OrderBy(c => c?.FirstName).ThenBy(c => c?.LastName).ToList();
            SaveAndDisplayContacts("Contact added successfully.\n");
            return true;
        }
        public bool ValidateEmailPhone(string Email, string Phone) //simple ValidateEmailPhone method
        {
            if (Phone.Length <= 10 && Phone.Length >= 7 && Email.Length > 7 && Email.Contains("@"))
            {
                return true; // Valid phone and email
            }

            return false; // Invalid phone or email
        }

        public List<Contact> GetContactsSorted()
        {
            return contacts;
        }

        public void DisplayContacts()
        {
            var sortedContacts = GetContactsSorted();

            int index = 1;
            var displayedContacts = new List<Contact>();
            Console.Clear();

            foreach (var contact in sortedContacts)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"[{index}]:");
                Console.ResetColor();
                Console.WriteLine($" {contact?.FirstName} {contact?.LastName}");
                displayedContacts.Add(contact);
                index++;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Insert [Index]+Enter  to display Contact Details");
            Console.WriteLine("Press  [0]    +Enter  to return to the main menu");
            Console.ResetColor();

            string input = Console.ReadLine();

            if (input == "0")
            {
                Console.Clear();
                return;
            }

            if (int.TryParse(input, out int selectedContactIndex) && selectedContactIndex >= 1 && selectedContactIndex <= displayedContacts.Count)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("----Contact Details----");
                Console.ResetColor();
                Console.WriteLine();
                    DisplayContactByIndex(selectedContactIndex - 1);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press any key to return to the main menu");
                Console.ResetColor();
                Console.ReadKey();
                DisplayContacts(); 
            }
            else
            {
                Console.Clear();
                DisplayErrorMessage("Error: Invalid choice. Please enter a valid option.\n     - You must input either the [Index] number corresponding to your choice or [0].\n\n");
                DisplayContacts(); 
            }
        }

        public Contact? GetContactByIndex(int index)
        {
            if(index <0 || index > contacts.Count -1)
            {
                return null;
            }

            return contacts[index];
        }

        public void DisplayContactByIndex(int index)
        {
            var contact = GetContactByIndex(index);

            if (contact != null)
            {
                DisplayProperty("Name", $"{contact.FirstName} {contact.LastName}");
                DisplayProperty("Phone", contact.PhoneNumber);
                DisplayProperty("Email", contact.Email);
                DisplayProperty("Address", contact.Address);
            }
            else
            {
                Console.WriteLine("Error: Contact not found.\n");
            }
        }

        public bool DeleteContactByEmail(string email)
        {
            Contact? contactToDelete = GetContactByEmail(email);

            if (contactToDelete != null)
            {
                contacts.Remove(contactToDelete);
                SaveAndDisplayContacts($"Contact with email '{email}' deleted successfully.\n");
                return true;
            }
            else
            {
                DisplayErrorMessage($"Error: Contact with email '{email}' not found.\n");
                return false;
            }
        }

        public Contact? GetContactByEmail(string email)
        {
            return contacts.FirstOrDefault(c => string.Equals(c?.Email, email, StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateContactByEmail(string email, Contact updatedContact)
        {
            if (!ValidateEmailPhone(updatedContact.Email, updatedContact.PhoneNumber))  // Validate the updated contact details
            {
                DisplayErrorMessage("Error: Invalid phone number or email. Contact not updated. Please check your input.\n");
                return false;
            } 
            Contact? contactToUpdate = GetContactByEmail(email);
            if (contactToUpdate != null)
            {
                // Replace the existing contact with the updated one
                int index = contacts.IndexOf(contactToUpdate);
                contacts[index] = updatedContact;

                SaveAndDisplayContacts($"Contact with email '{email}' updated successfully.\n");
                return true;
            }
            else
            {
                DisplayErrorMessage($"Error: Contact with email '{email}' not found.\n");
                return false;
            }
        }

        private bool ContactExists(string email) => contacts.Any(c => c?.Email?.Equals(email, StringComparison.OrdinalIgnoreCase) ?? false);

        /// <summary>
        /// Saves the list of contacts to the JSON file and displays a success message.
        /// </summary>
        /// <param name="successMessage">The message to be displayed upon successful saving of contacts.</param>
        private void SaveAndDisplayContacts(string successMessage)
        {
            SaveContacts();

            if (!shouldUseConsole) return;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(successMessage);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to return to the main menu");
            Console.ResetColor();
            Console.ReadKey();
        }

        /// <summary>
        /// Saves the list of contacts to the JSON file.
        /// </summary>
        private void SaveContacts()
        {
            try
            {
                File.WriteAllText(JsonFilePath, JsonSerializer.Serialize(contacts));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving contacts: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads contacts from the JSON file.
        /// </summary>
        private void LoadContacts()
        {
            if (File.Exists(JsonFilePath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(JsonFilePath);
                    contacts = JsonSerializer.Deserialize<List<Contact>>(jsonContent, new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                    });

                    if (contacts == null)
                    {
                        Console.WriteLine("Deserialization resulted in null contacts list.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during deserialization: {ex.Message}");
                }
            }
            else
            {
                DisplayErrorMessage($"Warning!\nJSON file not found at: {JsonFilePath}\n\n"); // Optionally, i can exit the program or handle it differently based on my requirements.
            }
        }

        /// <summary>
        /// Clears the console, displays an error message in dark red, and prompts the user to return to the main menu.
        /// </summary>
        /// <param name="errorMessage">The error message to be displayed.</param>
        private void DisplayErrorMessage(string errorMessage)
        {
            if (!shouldUseConsole) return;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to return to the main menu");
            Console.ResetColor();
            Console.ReadKey();
        }

        /// <summary>
        /// Displays a property name and its corresponding value in the console with proper formatting.
        /// </summary>
        /// <param name="propertyName">The name of the property to be displayed.</param>
        /// <param name="propertyValue">The value of the property to be displayed.</param>
        private static void DisplayProperty(string propertyName, string propertyValue)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"{propertyName}: ");
            Console.ResetColor();
            Console.WriteLine($"{propertyValue}");
        }
    }
}
