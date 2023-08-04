// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

class PasswordGenerator
{
    private const string AllowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{}|;:,.<>?";

    public static string GeneratePassword(int length)
    {
        Random random = new Random();
        return new string(Enumerable.Repeat(AllowedChars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

class PasswordManager
{
    private Dictionary<string, string> passwords;

    public PasswordManager()
    {
        passwords = new Dictionary<string, string>();
    }

    public void AddPassword(string website, string username, string password)
    {
        passwords[website] = $"{username}:{password}";
    }

    public string GetPassword(string website)
    {
        if (passwords.TryGetValue(website, out string credentials))
        {
            string[] parts = credentials.Split(':');
            return parts[1];
        }

        return null;
    }

    public void PrintAllPasswords()
    {
        foreach (var kvp in passwords)
        {
            string[] parts = kvp.Value.Split(':');
            Console.WriteLine($"Website: {kvp.Key}, Username: {parts[0]}, Password: {parts[1]}");
        }
    }
}

class Program
{
    static void Main()
    {
        PasswordGenerator generator = new PasswordGenerator();
        PasswordManager manager = new PasswordManager();

        // Generate a password and add it to the manager
        string newPassword = PasswordGenerator.GeneratePassword(12);
        manager.AddPassword("example.com", "john_doe", newPassword);

        // Get the password for a website
        string password = manager.GetPassword("example.com");
        Console.WriteLine($"Password for example.com: {password}");

        // Print all saved passwords
        manager.PrintAllPasswords();
    }
}

