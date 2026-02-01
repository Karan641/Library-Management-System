using System;
using LibraryManagementExtended.Abstraction;
using LibraryManagementExtended.CustomException;

namespace LibraryManagementExtended.Model
{
    /// <summary>
    /// Represents a magazine (periodical) with an issue number.
    /// </summary>
    public class Magazine : LibraryItemBase
    {
        private int _issueNumber;

        /// <summary>
        /// Issue number for the magazine; must be > 0.
        /// </summary>
        public int IssueNumber
        {
            get => _issueNumber;
            set
            {
                if (value <= 0)
                    throw new InvalidItemDataException("IssueNumber must be greater than 0");
                _issueNumber = value;
            }
        }

        /// <summary>
        /// Writes a single-line, human-readable representation of the magazine to the console.
        /// </summary>
        public override void Display()
        {
            Console.WriteLine($"Magazine | {Title} | Issue {IssueNumber} | {Publisher} | {PublicationYear}");
        }
    }
}