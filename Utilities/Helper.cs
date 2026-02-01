using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LibraryManagementExtended.Interface;
using LibraryManagementExtended.Model;

namespace LibraryManagementExtended.Utilities
{
    /// <summary>
    /// Small helper utilities used by the application. Currently provides
    /// simple JSON persistence for <see cref="ILibraryItem"/> instances.
    /// </summary>
    public static class Helper
    {
        // Path where library data is stored (simple, relative path for this exercise)
        private static readonly string filePath = "library.json";

        /// <summary>
        /// Loads items from disk. The persisted format is an array of objects
        /// with a <c>Type</c> discriminator and a <c>Data</c> payload, e.g.:
        /// [{ "Type": "Book", "Data": { ... } }, ...]
        /// </summary>
        public static List<ILibraryItem> LoadItems()
        {
            try
            {
                if (!File.Exists(filePath))
                    return new List<ILibraryItem>();

                var json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<ILibraryItem>();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                var result = new List<ILibraryItem>();

                foreach (var element in root.EnumerateArray())
                {
                    // Expect the 'Type' discriminator and the 'Data' payload
                    if (!element.TryGetProperty("Type", out var tProp) ||
                        !element.TryGetProperty("Data", out var dProp))
                        continue;

                    var typeName = tProp.GetString();
                    var dataJson = dProp.GetRawText();

                    // Deserialize according to the discriminator
                    switch (typeName)
                    {
                        case "Book":
                            var b = JsonSerializer.Deserialize<Book>(dataJson);
                            if (b != null) result.Add(b);
                            break;
                        case "Magazine":
                            var m = JsonSerializer.Deserialize<Magazine>(dataJson);
                            if (m != null) result.Add(m);
                            break;
                        case "Newspaper":
                            var n = JsonSerializer.Deserialize<Newspaper>(dataJson);
                            if (n != null) result.Add(n);
                            break;
                    }
                }

                return result;
            }
            catch
            {
                // If reading fails, return empty list so app still runs.
                // In a production app log the error before swallowing.
                return new List<ILibraryItem>();
            }
        }

        /// <summary>
        /// Persists the provided items to disk using the discriminator+payload format.
        /// </summary>
        public static void SaveItems(List<ILibraryItem> items)
        {
            try
            {
                var wrapper = new List<object>();
                foreach (var item in items)
                {
                    // Store a lightweight type discriminator to allow polymorphic restore
                    wrapper.Add(new
                    {
                        Type = item.GetType().Name,
                        Data = item
                    });
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(wrapper, options);
                File.WriteAllText(filePath, json);
            }
            catch
            {
                // swallow errors for now (app should continue running)
                // In a real app surface/log the error so the user knows persistence failed.
            }
        }
    }
}