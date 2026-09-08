## Serialization Attributes

In C#, the term "serialized attribute" refers to the concept of serialization attributes used with classes and objects when performing serialization or deserialization.

Serialization is the process of converting an object or data structure into a format that can be easily stored, transmitted, or reconstructed later. Deserialization is the reverse process, where the serialized data is converted back into an object.



Attributes in C# are used to provide metadata about the code elements like classes, methods, or properties. Serialization attributes are often used to control how objects are serialized or desterilized by indicating how certain members should be treated during the process.

Here are some commonly used serialization attributes in C#:

### `**[Serializable]**` **Attribute:**

- - The `[Serializable]` attribute is applied to a class to indicate that its instances can be serialized.
- Example:

```csharp
[Serializable]
public class MyClass
{
    // Class members
}
```

### `**[NonSerialized]**` **Attribute:**

- - Applied to a field to indicate that it should not be serialized.
- Example:

```csharp
[Serializable]
public class MyClass
{
    // Will be serialized
    public int SerializedField;
    // Will not be serialized
    [NonSerialized]
    public int NonSerializedField;
}
```

<mark>These attributes help customize the serialization and deserialization process to meet specific requirements</mark>, ***<u>such as excluding certain fields, renaming elements, or controlling the order of serialization</u>***. <mark>Depending on the serialization framework or library used</mark> (e.g., XML serialization, JSON serialization, binary serialization), different attributes might be employed.

**<mark><u>Note: there are more advance attributes for serialization, at your level now you dont need to know them</u>.</mark>**
