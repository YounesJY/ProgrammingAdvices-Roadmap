// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud

#include <iostream>
#include "../../HeaderFiles/Utility.h"

int main() {
    Utility::seedRandomNumberGenerator(); 
    
    cout << Utility::randomNumber(1, 10) << endl; 
    cout << Utility::getRandomCharacter(Utility::enCharType::CapitalLetter) << endl; 
    cout << Utility::generateWord(Utility::enCharType::MixChars, 8) << endl; 
    cout << Utility::generateKey(Utility::enCharType::MixChars) << endl; 
    Utility::generateKeys(10, Utility::enCharType::MixChars); 

    cout << "\n";

    // Swap Int
    int x = 10, y = 20;
    cout << x << " " << y << endl;
    Utility::swap(x, y); 
    cout << x << " " << y << endl << endl;

    // Swap double
    double a = 10.5, b = 20.5;
    cout << a << " " << b << endl;
    Utility::swap(a, b); 
    cout << a << " " << b << endl << endl;

    // Swap String
    string s1 = "Ali", s2 = "Ahmed";
    cout << s1 << " " << s2 << endl;
    Utility::swap(s1, s2); 
    cout << s1 << " " << s2 << endl << endl;

    // Swap Dates
    Date d1(1, 10, 2022), d2(1, 1, 2022);
    cout << d1.dateToString() << " " << d2.dateToString() << endl;
    Utility::swap(d1, d2); 
    cout << d1.dateToString() << " " << d2.dateToString() << endl;

    // Shuffle Array

    // int array
    int Arr1[5] = { 1, 2, 3, 4, 5 };
    Utility::shuffleArray(Arr1, 5); 
    cout << "\nArray after shuffle:\n";
    for (int i = 0; i < 5; i++) {
        cout << Arr1[i] << endl;
    }

    // string array
    string Arr2[5] = { "Ali", "Fadi", "Ahmed", "Qasem", "Khalid" };
    Utility::shuffleArray(Arr2, 5); 
    cout << "\nArray after shuffle:\n";
    for (int i = 0; i < 5; i++) {
        cout << Arr2[i] << endl;
    }

    int Arr3[5];
    Utility::fillArrayWithRandomNumbers(Arr3, 5, 20, 50); 
    cout << "\nArray after fill:\n";
    for (int i = 0; i < 5; i++) {
        cout << Arr3[i] << endl;
    }

    string Arr4[5];
    Utility::fillArrayWithRandomWords(Arr4, 5, Utility::MixChars, 8); 
    cout << "\nArray after fill:\n";
    for (int i = 0; i < 5; i++) {
        cout << Arr4[i] << endl;
    }

    string Arr5[5];
    Utility::fillArrayWithRandomKeys(Arr5, 5, Utility::MixChars); 
    cout << "\nArray after filling keys:\n";
    for (int i = 0; i < 5; i++) {
        cout << Arr5[i] << endl;
    }

    cout << "\nText1 " << Utility::tabs(5) << "Text2\n"; 

    const short EncryptionKey = 2; // This is the key.

    string TextAfterEncryption, TextAfterDecryption;
    string Text = "Mohammed Abu-Hadhoud";
    TextAfterEncryption = Utility::encryptText(Text, EncryptionKey); 
    TextAfterDecryption = Utility::decryptText(TextAfterEncryption, EncryptionKey); 

    cout << "\nText Before Encryption : ";
    cout << Text << endl;
    cout << "Text After Encryption  : ";
    cout << TextAfterEncryption << endl;
    cout << "Text After Decryption  : ";
    cout << TextAfterDecryption << endl;

    system("pause>0");

    return 0;
}