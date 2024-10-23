using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WinFormsApp1;

public static class PersonFileHandler
{
    public static void SaveToFile(List<IPerson> people, string fileName)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(people, options);
        File.WriteAllText(fileName, jsonString);
    }

    public static List<Person> LoadFromFile(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        var tempPersons = JsonSerializer.Deserialize<List<TempPerson>>(jsonString);
        var persons = new List<Person>();

        foreach (var temp in tempPersons)
        {
            var person = new Person
            {
                CardNumber = temp.CardNumber,
                Name = temp.Name,
                HashPassword = temp.HashPassword, // Просто присваиваем значение
                Birthday = temp.Birthday
            };
            persons.Add(person);
        }

        return persons;
    }
}
public class TempPerson
{
    public int CardNumber { get; set; }
    public string Name { get; set; }
    public string HashPassword { get; set; }
    public DateTime Birthday { get; set; }
}