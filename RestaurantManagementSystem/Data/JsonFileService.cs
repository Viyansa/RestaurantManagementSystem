
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RestaurantManagementSystem.Data
{
    public class JsonFileService
    {
        private readonly string folderPath =
            Path.Combine(AppContext.BaseDirectory, "Data");

        private readonly JsonSerializerOptions options =
            new JsonSerializerOptions
            {
                WriteIndented = true
            };

        public List<T> Load<T>(string fileName)
        {
            try
            {
                Directory.CreateDirectory(folderPath);

                string path = Path.Combine(folderPath, fileName);

                if (!File.Exists(path))
                {
                    return new List<T>();
                }

                string json = File.ReadAllText(path);

                return JsonSerializer.Deserialize<List<T>>(json, options)
                    ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loading error: " + ex.Message);
                return new List<T>();
            }
        }

        public void Save<T>(string fileName, List<T> data)
        {
            try
            {
                Directory.CreateDirectory(folderPath);

                string path = Path.Combine(folderPath, fileName);
                string json = JsonSerializer.Serialize(data, options);

                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Saving error: " + ex.Message);
            }
        }
    }
}
