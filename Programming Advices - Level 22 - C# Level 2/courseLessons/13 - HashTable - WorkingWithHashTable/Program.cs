using System;
using System.Collections;

class Program
{
    static void Main()
    {
        Hashtable myHashtable = new Hashtable();
        myHashtable.Add("key1", "value1");
        myHashtable.Add("key2", "value2");
        myHashtable.Add("key3", "value3");


        Console.WriteLine($"Accessing key1: {myHashtable["key1"]}");
        myHashtable["key1"] = "newValue1";
        myHashtable.Remove("key2");

        Console.WriteLine("Current Hashtable contents:");
        foreach (DictionaryEntry entry in myHashtable)
            Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");

        Console.ReadKey();
    }
}
