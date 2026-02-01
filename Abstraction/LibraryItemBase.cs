using System;
using LibraryManagementExtended.Interface;
using LibraryManagementExtended.CustomException;

namespace LibraryManagementExtended.Abstraction
{
    /// <summary>
    /// Base implementation for library items. Provides common properties and
    /// enforces basic validation for public properties.
    /// </summary>
    public abstract class LibraryItemBase : ILibraryItem
    {
        // Backing fields (protected so derived types may access if needed)
        protected string _title = string.Empty;
        protected string _publisher = string.Empty;
        private int _publicationYear;

        /// <summary>
        /// Title of the item. Cannot be null/empty or whitespace.
        /// Throws <see cref="InvalidItemDataException"/> on invalid input.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Title cannot be empty");
                _title = value;
            }
        }

        /// <summary>
        /// Publisher of the item. Cannot be null/empty or whitespace.
        /// Throws <see cref="InvalidItemDataException"/> on invalid input.
        /// </summary>
        public string Publisher
        {
            get => _publisher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Publisher cannot be empty");
                _publisher = value;
            }
        }

        /// <summary>
        /// Publication year. Must be a positive value and not in the future.
        /// Validation is performed by the setter and will throw
        /// <see cref="InvalidItemDataException"/> for invalid values.
        /// </summary>
        public int PublicationYear
        {
            get => _publicationYear;
            set
            {
                var current = DateTime.Now.Year;
                if (value <= 0 || value > current)
                    throw new InvalidItemDataException($"PublicationYear must be between 1 and {current}.");
                _publicationYear = value;
            }
        }

        /// <summary>
        /// Derived types must implement a textual display of the item.
        /// </summary>
        public abstract void Display();
    }
}