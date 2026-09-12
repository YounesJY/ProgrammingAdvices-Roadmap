## Encrypt and Decrypt Image Example

### **You can encrypt and decrypt image using AES,**

Before you run the program,

1. create a folder in c:\ drive and name it Image ==> c:\Image
2. Copy the attached image below and put it in the folder.
3. Run the program.

### **Sample Code to encrypt image using AES.**

using System;
using System.IO;
using System.Security.Cryptography;
class Program
{
    static void Main()
    {
        string inputFile = "c:\\Image\\MyImage.jpg";
        string encryptedFile = "c:\\Image\\encrypted.jpg";
        string decryptedFile = "c:\\Image\\decrypted.jpg";
        // Generate a random IV for each encryption operation
        byte[] iv;
        using (Aes aesAlg = Aes.Create())
        {
            iv = aesAlg.IV;
        }
        string key = "1234567890123456";
        EncryptFile(inputFile, encryptedFile, key, iv);
        DecryptFile(encryptedFile, decryptedFile, key, iv);
        Console.WriteLine("Encryption and decryption completed successfully.");
        Console.WriteLine("go to c:\\Image folder to see the results");
        Console.ReadKey();  
    }
    static void EncryptFile(string inputFile, string outputFile, string key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aesAlg.IV = iv;
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
            using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
            using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
            {
                // Write the IV to the beginning of the file
                fsOutput.Write(iv, 0, iv.Length);
                fsInput.CopyTo(cryptoStream);
            }
        }
    }
    static void DecryptFile(string inputFile, string outputFile, string key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
            aesAlg.IV = iv;
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
            using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
            using (CryptoStream cryptoStream = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
            {
                // Skip the IV at the beginning of the file
                fsInput.Seek(iv.Length, SeekOrigin.Begin);
                fsInput.CopyTo(cryptoStream);
            }
        }
    }
}

### 

### **Code Explanation:**

### Main Method

static void Main()
{
    string inputFile = "c:\\Image\\MyImage.jpg";
    string encryptedFile = "c:\\Image\\encrypted.jpg";
    string decryptedFile = "c:\\Image\\decrypted.jpg";
    // Generate a random IV for each encryption operation
    byte[] iv;
    using (Aes aesAlg = Aes.Create())
    {
        iv = aesAlg.IV;
    }
    string key = "1234567890123456";
    EncryptFile(inputFile, encryptedFile, key, iv);
    DecryptFile(encryptedFile, decryptedFile, key, iv);
    Console.WriteLine("Encryption and decryption completed successfully.");
    Console.WriteLine("go to c:\\Image folder to see the results");
    Console.ReadKey();  
}

This is the entry point of the program. Here's what each part does:

1. File Paths and Key: Defines file paths for the input image (`MyImage.jpg`), the encrypted image (`encrypted.jpg`), and the decrypted image (`decrypted.jpg`). It also defines a key (`"1234567890123456"`). Note that in real-world scenarios, using a hardcoded key is not secure, and key management should be handled more securely.
2. Initialization Vector (IV) Generation: Creates an instance of `Aes` to generate a random IV for each encryption operation. The IV is stored in the `iv` variable.
3. Encryption and Decryption Calls: Calls the `EncryptFile` method to encrypt the input image and the `DecryptFile` method to decrypt the encrypted image. Both methods receive the input file, output file, key, and IV as parameters.
4. Console Output and User Interaction: Prints messages indicating the success of encryption and decryption. It prompts the user to press any key before exiting, allowing them to view the results.

### EncryptFile Method

static void EncryptFile(string inputFile, string outputFile, string key, byte[] iv)
{
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
        aesAlg.IV = iv;
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
        using (ICryptoTransform encryptor = aesAlg.CreateEncryptor())
        using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
        {
            // Write the IV to the beginning of the file
            fsOutput.Write(iv, 0, iv.Length);
            fsInput.CopyTo(cryptoStream);
        }
    }
}

This method performs file encryption using the AES algorithm:

1. AES Initialization: Creates an instance of `Aes` and sets its key and IV based on the provided parameters.
2. File Streams and CryptoStream: Creates file streams for input and output files. Also, creates a `CryptoStream` to perform the encryption. The encrypted data is written to `fsOutput` through the `CryptoStream`.
3. Write IV to Output File: Writes the IV to the beginning of the output file before writing the actual encrypted content.

### DecryptFile Method

static void DecryptFile(string inputFile, string outputFile, string key, byte[] iv)
{
    using (Aes aesAlg = Aes.Create())
    {
        aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(key);
        aesAlg.IV = iv;
        using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
        using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
        using (ICryptoTransform decryptor = aesAlg.CreateDecryptor())
        using (CryptoStream cryptoStream = new CryptoStream(fsOutput, decryptor, CryptoStreamMode.Write))
        {
            // Skip the IV at the beginning of the file
            fsInput.Seek(iv.Length, SeekOrigin.Begin);
            fsInput.CopyTo(cryptoStream);
        }
    }
}

This method performs file decryption using the AES algorithm:

1. AES Initialization: Creates an instance of `Aes` and sets its key and IV based on the provided parameters.
2. File Streams and CryptoStream: Creates file streams for input and output files. Also, creates a `CryptoStream` to perform the decryption. The decrypted data is written to `fsOutput` through the `CryptoStream`.
3. Skip IV during Decryption: Skips the IV bytes at the beginning of the input file before starting the decryption process.

Overall, this code demonstrates a simple file encryption and decryption process using the AES algorithm in C#. It includes the generation of a random IV for each encryption operation and writes the IV to the output file for later use during decryption.
