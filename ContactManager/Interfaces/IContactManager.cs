using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Interfaces
{
    /// <summary>
    /// Represents a contact with information such as first name, last name, phone number, email, and address.
    /// </summary>
    public class Contact
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }

    /// <summary>
    /// ContactManager supports add, get, display, update and delete contacts.
    /// </summary>
    public interface IContactManager
    {
        /// <summary>
        /// Adds a new contact to the list of contacts, checking for duplicate emails.
        /// </summary>
        /// 
        public bool AddContact(Contact contact);

        /// <summary>
        /// Returns list of contacts sorted by name.
        /// </summary>
        public List<Contact> GetContactsSorted();

        /// <summary>
        /// Displays the list of contacts and allows the user to view details for a selected contact.
        /// </summary>
        public void DisplayContacts();

        /// <summary>
        /// Retuens details for a specific contact based on the email.
        /// </summary>
        /// <param name="email">The email of the contact to display details for.</param>
        public Contact? GetContactByIndex(int index);

        /// <summary>
        /// Gets a contact by email.
        /// </summary>
        /// <param name="email">The email of the contact to retrieve.</param>
        /// <returns>The contact with the specified email, or null if not found.</returns>
        Contact? GetContactByEmail(string email);

        /// <summary>
        /// Updates a contact with the specified email.
        /// </summary>
        /// <param name="email">The email of the contact to update.</param>
        /// <param name="updatedContact">The updated contact information.</param>
        /// <returns>True if the contact is successfully updated, false otherwise.</returns>
        bool UpdateContactByEmail(string email, Contact updatedContact);

        /// <summary>
        /// Displays details for a specific contact based on the email.
        /// </summary>
        /// <param name="index">The email of the contact to display details for.</param>
        public void DisplayContactByIndex(int index);

        /// <summary>
        /// Deletes the contact with the specified email from the list of contacts.
        /// </summary>
        /// <param name="email">The email of the contact to be deleted.</param>
        public bool DeleteContactByEmail(string email);

        /// <summary>
        /// Validates the provided email and phone number according to specific criteria.
        /// </summary>
        /// <param name="Email">The email to be validated.</param>
        /// <param name="Phone">The phone number to be validated.</param>
        /// <returns>True if the email and phone number meet the specified criteria; otherwise, false.</returns>
        public bool ValidateEmailPhone(string Email, string Phone);
    }
}
