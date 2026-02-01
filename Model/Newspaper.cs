using System;
using LibraryManagementExtended.Abstraction;
using LibraryManagementExtended.CustomException;

namespace LibraryManagementExtended.Model
{
    /// <summary>
    /// Represents a newspaper item; contains an editor field.
    /// </summary>
    public class Newspaper : LibraryItemBase
    {
        private string _editor = string.Empty;

        /// <summary>
        /// Editor name (required).
        /// </summary>
        public string Editor
        {
            get => _editor;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidItemDataException("Editor cannot be empty");
                _editor = value;
            }
        }

        /// <summary>
        /// Writes a single-line, human-readable representation of the newspaper to the console.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Newspaper | {Title} | Editor {Editor} | {Publisher} | {PublicationYear}");
        }
    }
}