## What is Symmetric Encryption?



**Symmetric encryption** is a type of encryption where the same key is used for both the encryption and decryption of the data. In C#, the .NET Framework and .NET Core provide classes in the `System.Security.Cryptography` namespace to perform symmetric encryption. The most commonly used symmetric encryption algorithms are DES (Data Encryption Standard), 3DES (Triple DES), AES (Advanced Encryption Standard), and RC4.

#### **They key size is commonly used for AES encryption is 128-bit, what does that mean?**

The "key size" in the context of **AES (Advanced Encryption Standard)** refers to the length of the cryptographic key used for encryption and decryption. In AES, the key size determines the number of bits in the key, and it directly affects the strength of the encryption.

AES supports three key sizes: 128-bit, 192-bit, and 256-bit. The number in each key size (128, 192, or 256) represents the length of the key in bits. Here's a breakdown of what each of these key sizes means:

- 128-bit Key:
  - The key is 128 bits long, which means it has 2^128 possible combinations.
  - This is considered strong encryption and is widely used for many secure applications.
- 192-bit Key:
  - The key is 192 bits long, providing a higher level of security than 128-bit keys.
  - While it offers increased security, 192-bit keys are less commonly used than 128-bit and 256-bit keys.
- 256-bit Key:
  - The key is 256 bits long, providing the highest level of security among the three key sizes.
  - AES with a 256-bit key is often used in situations where maximum security is required.

The key <u>size directly impacts the difficulty of a brute-force attack</u>, where an attacker tries all possible combinations of the key to decrypt the data. As the key size increases, the number of possible combinations grows exponentially, making it significantly more difficult and time-consuming for an attacker to break the encryption through brute force.

In practice, a 128-bit key is considered secure for most applications, and it is widely used in various cryptographic protocols and systems. However, for scenarios where an extra layer of security is desired or required, one might opt for a 192-bit or 256-bit key. It's important to note that the increased security comes with a trade-off in terms of computational overhead, as encryption and decryption with longer keys can be more computationally intensive.

<mark>**You should not lose the key!**</mark>

Remember that in real-world scenarios, you would typically use a more secure method for key management, and for sensitive information, it's important to handle encryption keys securely. Additionally, consider using authenticated encryption modes for added security.
