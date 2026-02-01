namespace LibraryManagementExtended.CustomException
{
    /// <summary>
    /// Exception thrown when an attempt is made to add a library item that
    /// already exists according to the duplicate-detection rules.
    /// </summary>
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }
}