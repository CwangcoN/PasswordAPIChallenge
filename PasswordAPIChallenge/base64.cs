using System;
using System.IO;

class base64
{
    static void Base64()
    {
        string zipFilePath = "C:\\Users\\Cyril\\Desktop\\submission.zip"; // ZIP file to encode

        // Convert ZIP file to Base64 string
        string base64Zip = ConvertFileToBase64(zipFilePath);

        Console.WriteLine("Base64 ZIP File Content:");
        Console.WriteLine(base64Zip);
    }

    static string ConvertFileToBase64(string filePath)
    {
        // Read the ZIP file bytes
        byte[] fileBytes = File.ReadAllBytes(filePath);

        // Convert bytes to Base64 string
        return Convert.ToBase64String(fileBytes);
    }
}
