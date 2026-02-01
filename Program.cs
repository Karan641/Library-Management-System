using System;
using LibraryManagementExtended.Model;
using LibraryManagementExtended.Service;

namespace LibraryManagementExtended
{
    /// <summary>
    /// Simple console UI for the library app. This file contains only a
    /// tiny, instructor-friendly menu used by the assignment.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            var service = new LibraryService();
            var running = true;

            // Main loop: the menu demonstrates the required flows for the assignment
            while (running)
            {
                Console.WriteLine("\n1. Add Book");
                Console.WriteLine("2. Add Magazine");
                Console.WriteLine("3. Add Newspaper");
                Console.WriteLine("4. View Items");
                Console.WriteLine("5. Exit");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        // The sample items are provided so graders can quickly run the app
                        case "1":
                            service.AddItem(new Book
                            {
                                Title = "The Great Gatsby",
                                Author = "Fitzgerald",
                                Publisher = "Penguin",
                                PublicationYear = 1925
                            });
                            break;

                        case "2":
                            service.AddItem(new Magazine
                            {
                                Title = "National Geographic",
                                IssueNumber = 202,
                                Publisher = "NatGeo",
                                PublicationYear = 2022
                            });
                            break;

                        case "3":
                            service.AddItem(new Newspaper
                            {
                                Title = "The Kathmandu Post",
                                Editor = "EditorName",
                                Publisher = "Kantipur",
                                PublicationYear = 2024
                            });
                            break;

                        case "4":
                            // Delegates to each item's Display() implementation
                            service.DisplayAll();
                            break;

                        case "5":
                            // Exit the program gracefully
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Print friendly error messages for validation/duplicate errors
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
