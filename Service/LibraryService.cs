using LibraryManagementExtended.Interface;
using LibraryManagementExtended.CustomException;
using LibraryManagementExtended.Utilities;

namespace LibraryManagementExtended.Service
{
    /// <summary>
    /// Responsible for application business logic: adding, persisting and
    /// displaying library items. Duplicate detection runs against persisted data.
    /// </summary>
    public class LibraryService
    {
        // In-memory cache of items (loaded from disk on construction).
        private List<ILibraryItem> _items;

        /// <summary>
        /// Loads persisted items (if any) into memory on service construction.
        /// </summary>
        public LibraryService()
        {
            // Load existing data from file or initialize empty list
            _items = Helper.LoadItems() ?? new List<ILibraryItem>();
        }

        /// <summary>
        /// Add a new library item. Validates input, checks for duplicates and
        /// persists the updated list to disk.
        /// </summary>
        /// <param name="newItem">Item supplied by the caller (must be non-null).</param>
        /// <exception cref="InvalidItemDataException">When input is invalid.</exception>
        /// <exception cref="DuplicateItemException">When an equivalent item already exists.</exception>
        public void AddItem(ILibraryItem newItem)
        {
            // Validate item
            if (newItem == null)
                throw new InvalidItemDataException("Item cannot be null.");

            // Check duplicate from file-loaded list
            if (CheckForDuplicate(newItem))
                throw new DuplicateItemException("Duplicate item found.");

            // Add item and save to file
            _items.Add(newItem);
            Helper.SaveItems(_items);
        }

        /// <summary>
        /// Prints all items to the console using each item's <see cref="ILibraryItem.Display"/>.
        /// </summary>
        public void DisplayAll()
        {
            foreach (var item in _items)
            {
                item.Display();
            }
        }

        /// <summary>
        /// Iterate persisted items and determine whether <paramref name="newItem"/>
        /// already exists.
        /// </summary>
        private bool CheckForDuplicate(ILibraryItem newItem)
        {
            foreach (var existingItem in _items)
            {
                if (IsDuplicate(existingItem, newItem))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Determine whether two items should be considered duplicates. The check:
        /// 1) returns false when types differ;
        /// 2) compares base properties (Title, Publisher, PublicationYear);
        /// 3) compares type-specific properties (Author / IssueNumber / Editor).
        /// </summary>
        private bool IsDuplicate(ILibraryItem existingItem, ILibraryItem newItem)
        {
            // If item types differ, they cannot be duplicates
            if (existingItem.GetType() != newItem.GetType())
                return false;

            // Compare common properties
            if (existingItem.Title != newItem.Title ||
                existingItem.Publisher != newItem.Publisher ||
                existingItem.PublicationYear != newItem.PublicationYear)
                return false;

            // Type-specific comparison (concise and easy to extend)
            return existingItem switch
            {
                Model.Book b when newItem is Model.Book nb =>
                    b.Author == nb.Author,

                Model.Magazine m when newItem is Model.Magazine nm =>
                    m.IssueNumber == nm.IssueNumber,

                Model.Newspaper n when newItem is Model.Newspaper nn =>
                    n.Editor == nn.Editor,

                _ => false
            };
        }
    }
}
