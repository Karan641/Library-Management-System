using System;
using System.IO;
using LibraryManagementExtended.Model;
using LibraryManagementExtended.Service;
using LibraryManagementExtended.CustomException;

// Lightweight console test runner used for quick verification in this workspace.
// Keeps the same assertions as the original xUnit examples but avoids
// test-framework dependencies so the project builds and runs here.
internal static class Program
{
    private const string DataFile = "library.json";

    private static int Main()
    {
        try
        {
            Clean();
            Test_InvalidPublicationYear_Throws();
            Clean();
            Test_DuplicateBook_Throws();
            Clean();

            Console.WriteLine("All tests passed.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"TEST FAILED: {ex.GetType().Name} - {ex.Message}");
            return 2;
        }
    }

    private static void Clean()
    {
        if (File.Exists(DataFile)) File.Delete(DataFile);
    }

    private static void Test_InvalidPublicationYear_Throws()
    {
        // PublicationYear validation is performed by the property setter on the base class — assert that directly.
        var book = new Book
        {
            Title = "T",
            Author = "A",
            Publisher = "P"
        };

        try
        {
            book.PublicationYear = DateTime.Now.Year + 1; // should throw from property setter
            throw new Exception("Expected InvalidItemDataException was not thrown when setting PublicationYear.");
        }
        catch (InvalidItemDataException)
        {
            // expected
        }
    }

    private static void Test_DuplicateBook_Throws()
    {
        var svc = new LibraryService();
        var book = new Book
        {
            Title = "Dup",
            Author = "Author",
            Publisher = "Pub",
            PublicationYear = 2000
        };

        svc.AddItem(book);
        try
        {
            svc.AddItem(book);
            throw new Exception("Expected DuplicateItemException was not thrown.");
        }
        catch (DuplicateItemException)
        {
            // expected
        }
    }
}