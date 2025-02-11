# **🔑 Password API Assessment**

A **C# Console Application** to perform **brute-force authentication** and submit a **Base64-encoded ZIP file** using a **REST API**.

---

## **🚀 Features**
✅ Automates password brute-force testing for an API 🔓  
✅ Retrieves the **correct password** and extracts the **upload URL** 🔑  
✅ Converts a ZIP file to **Base64 encoding** 📂  
✅ Submits the Base64 file to the API for verification 📝  
✅ Well-structured and modular code with **separate classes** 📌  

---

## **🛠 Technologies Used**
| Tech Stack   | Description |
|-------------|------------|
| **C# (.NET 6.0)** | Core programming language |
| **REST API** | Interacting with HTTP endpoints |
| **Base64 Encoding** | Encoding ZIP files for API submission |
| **Git & GitHub** | Version control and code management |

---

## **📌 Setup Instructions**

### **1️⃣ Clone the Project**
```sh
git clone https://github.com/your-username/PasswordAPIChallenge.git
cd PasswordAPIChallenge
2️⃣ Project Structure
python
Copy
Edit
PasswordAPIChallenge/
│── src/
│   ├── Program.cs
│   ├── PasswordBruteForce.cs
│   ├── ZipEncoder.cs
│   ├── FileUploader.cs
│── dict.txt
│── README.md
│── submission.zip
│── upload_url.txt
│── .gitignore
3️⃣ Install .NET SDK
Ensure .NET 6.0 or later is installed.
Check your version:

sh
Copy
Edit
dotnet --version
📜 Steps to Run the Project
1️⃣ Step 1: Generate Password Dictionary (dict.txt)
Run this C# script to create the password variations file:

sh
Copy
Edit
dotnet run --project PasswordDictionaryGenerator.csproj
📌 This will generate a dict.txt file with multiple password variations.

2️⃣ Step 2: Run Brute-Force Attack (Program.cs)
sh
Copy
Edit
dotnet run --project PasswordBruteForce.csproj
📌 This script will:

Attempt to authenticate with each password in dict.txt.
If successful, it will extract and save the upload URL to upload_url.txt.
❗ Common Issue: Unauthorized (401)

If all passwords fail, the API may have changed or is blocking requests.
Try testing manually using:
sh
Copy
Edit
curl -u John:password http://recruitment.warpdevelopment.co.za/api/authenticate
3️⃣ Step 3: Convert ZIP File to Base64 (ZipEncoder.cs)
After authentication, convert the submission.zip file into Base64:

sh
Copy
Edit
dotnet run --project ZipEncoder.csproj
📌 Output: Prints a Base64-encoded string of submission.zip.

4️⃣ Step 4: Submit ZIP File (FileUploader.cs)
sh
Copy
Edit
dotnet run --project FileUploader.csproj
📌 This script:

Reads upload_url.txt (from Step 2).
Encodes submission.zip (from Step 3).
Submits it via POST request.
🔍 Troubleshooting Issues
Issue	Possible Fix
401 Unauthorized	Check if the username and passwords are correct.
Upload URL not found	Ensure upload_url.txt contains a valid API URL.
ZIP file not found	Ensure submission.zip exists in the project directory.
API not responding	Wait a few minutes and retry (API might have rate limits).
📌 API Endpoints
Method	Endpoint	Description
GET	/api/authenticate	Attempts to log in with Basic Auth.
POST	/api/upload/{id}	Submits the Base64 ZIP file.
👨‍💻 Example API Requests
1️⃣ Authenticate (Manual Test)
sh
Copy
Edit
curl -u John:password http://recruitment.warpdevelopment.co.za/api/authenticate
📌 Expected Response (if successful):

json
Copy
Edit
{"upload_url": "http://recruitment.warpdevelopment.co.za/api/upload/xyz"}
2️⃣ Submit ZIP File
sh
Copy
Edit
curl -X POST "http://recruitment.warpdevelopment.co.za/api/upload/xyz" \
-H "Content-Type: application/json" \
-d '{
  "Data": "Base64EncodedZipFile",
  "Name": "John Doe",
  "Surname": "Doe",
  "Email": "johndoe@email.com"
}'
