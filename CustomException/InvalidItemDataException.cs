namespace LibraryManagementExtended.CustomException
{
    /// <summary>
    /// Exception thrown when provided item data does not satisfy validation
    /// rules (for example: empty Title, invalid PublicationYear, etc.).
    /// </summary>
    public class InvalidItemDataException : Exception
    {
        public InvalidItemDataException(string message) : base(message) { }
    }
}