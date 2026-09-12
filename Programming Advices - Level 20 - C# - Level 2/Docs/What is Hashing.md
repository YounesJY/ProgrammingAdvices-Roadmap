## What is Hashing?



### Hashing:

Hash functions are commonly used to create a fixed-size string of characters, which is typically a hash value, from variable-size input data.

Hash functions, by design, are one-way functions. This means that you cannot directly reverse a hash to retrieve the original data. Hashing is primarily used for integrity verification and not for data retrieval.

If you need to check whether a given input matches a previously computed hash, you can hash the new input and compare the resulting hash with the stored hash. If the hashes match, it suggests that the input data is the same.

**SHA-256** (Secure Hash Algorithm 256-bit) is a cryptographic hash function that belongs to the SHA-2 family of hash functions. It's one of the widely used hash functions for various security applications and protocols. The "256" in SHA-256 refers to the bit length of the hash output, which is 256 bits.

Common use cases for SHA-256 include:

- Password Hashing: Storing hashed passwords rather than plain text in databases for security.
- Digital Signatures: Creating digital signatures for documents and messages to ensure authenticity and integrity.
- Blockchain Technology: SHA-256 is used in many blockchain protocols (e.g., Bitcoin) to secure transactions and blocks.
- Data Integrity: Verifying the integrity of transmitted or stored data by comparing hash values.

To use SHA-256 in programming, you typically leverage libraries or classes provided by the programming language. In C#, for example, you can use the `System.Security.Cryptography` namespace, as demonstrated in the code examples provided in previous responses.
