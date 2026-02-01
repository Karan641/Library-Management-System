using System;
using LibraryManagementExtended.Abstraction;
using LibraryManagementExtended.CustomException;

namespace LibraryManagementExtended.Model
{
    /// <summary>
    /// Represents a book. Inherits common validation from <see cref="LibraryItemBase"/>.
    /// </summary>
    public class Book : LibraryItemBase
    {
        private string _author = string.Empty;

        /// <summary>
        /// Author name (required).
        /// Throws <see cref="InvalidItemDataException"/> when set to an invalid value.
        /// </summary>
        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Author cannot be empty");
                _author = value;
            }
        }

        /// <summary>
        /// Writes a single-line, human-readable representation of the book to the console.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Book | {Title} | {Author} | {Publisher} | {PublicationYear}");
        }
    }
}