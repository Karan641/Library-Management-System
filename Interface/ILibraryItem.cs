namespace LibraryManagementExtended.Interface
{
    /// <summary>
    /// Represents a generic library item. Implementations must provide
    /// properties common to all library items and a way to display them.
    /// </summary>
    public interface ILibraryItem
    {
        /// <summary>
        /// Item title (required).
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Publisher name (required).
        /// </summary>
        string Publisher { get; set; }

        /// <summary>
        /// Year the item was published. Implementations should validate this value.
        /// </summary>
        int PublicationYear { get; set; }

        /// <summary>
        /// Write a human-readable representation of the item to the console.
        /// Implementations control the exact formatting.
        /// </summary>
        void Display();
    }
}