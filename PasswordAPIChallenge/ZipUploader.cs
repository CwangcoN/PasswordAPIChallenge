using System;
using System.IO;
using System.Net;
using System.Text;

public class ZipUploader
{
    private string zipFilePath;
    private string uploadUrl;

    // Constructor to initialize the file path and API URL
    public ZipUploader(string zipPath, string apiUrl)
    {
        zipFilePath = zipPath;
        uploadUrl = apiUrl;
    }

    // Converts the ZIP file to Base64 format
    public string ConvertFileToBase64()
    {
        try
        {
            if (!File.Exists(zipFilePath))
            {
                Console.WriteLine("Error: ZIP file not found!");
                return "";
            }

            Console.WriteLine("Converting ZIP file to Base64...");
            byte[] fileBytes = File.ReadAllBytes(zipFilePath);
            return Convert.ToBase64String(fileBytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error converting file: {ex.Message}");
            return "";
        }
    }

    // Uploads the Base64 ZIP file to the API
    public void UploadZipFile()
    {
        string base64Zip = ConvertFileToBase64();

        if (string.IsNullOrEmpty(base64Zip))
        {
            Console.WriteLine("Error: Base64 encoding failed.");
            return;
        }

        Console.WriteLine("Preparing JSON payload...");
        string jsonPayload = $"{{ \"Data\": \"{base64Zip}\", \"Name\": \"John Doe\", \"Surname\": \"Doe\", \"Email\": \"johndoe@email.com\" }}";
        byte[] byteArray = Encoding.UTF8.GetBytes(jsonPayload);

        if (uploadUrl.Contains("your_actual_upload_path_here"))
        {
            Console.WriteLine("Error: Replace the upload URL with the actual one from authentication.");
            return;
        }

        try
        {
            Console.WriteLine("Sending ZIP file to API...");
            HttpWebRequest postRequest = (HttpWebRequest)WebRequest.Create(uploadUrl);
            postRequest.Method = "POST";
            postRequest.ContentType = "application/json";
            postRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = postRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            using (HttpWebResponse postResponse = (HttpWebResponse)postRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(postResponse.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine($"Upload Response: {responseText}");
                }
            }

            Console.WriteLine("Submission completed successfully.");
        }
        catch (WebException ex)
        {
            Console.WriteLine($"API Request Failed: {ex.Message}");

            if (ex.Response != null)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    string errorResponse = reader.ReadToEnd();
                    Console.WriteLine($"Error Details: {errorResponse}");
                }
            }
        }
    }
}
