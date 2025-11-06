#pragma once
#include <iostream>
#include <string>
#include "MyInputLibrary.h";
#include "MyMathLibrary.h";

using namespace std;
using namespace inputsFunctions;
using namespace math;

enum randomPattern {
	smallLetter,
	capitalLetter,
	specialCharacter,
	digit
};

namespace others {

    void invertedNumberPattern() {
        int number = readPositiveInteger(1, 9);

        for (int pattern = number; pattern >= 1; --pattern) {
            for (int counter = 1; counter <= pattern; ++counter)
                cout << pattern << " ";
            cout << endl;
        }

    }

    void invertedLettersPattern() {
        int number = readPositiveInteger(1, 26);

        for (char letter(number + 'A' - 1); letter >= 'A'; --letter) {
            for (int counter = 1; counter <= (letter - 'A' + 1); ++counter)
                cout << letter << " ";
            cout << endl;
        }

    }

    void numberPattern() {
        int number = readPositiveInteger(1, 9);

        for (int pattern = 1; pattern <= number; ++pattern) {
            for (int counter = 1; counter <= pattern; ++counter)
                cout << pattern << " ";
            cout << endl;
        }

    }

    void lettersPattern() {
        int number = readPositiveInteger(1, 26);

        for (char letter = 'A'; letter <= (number + 'A' - 1); ++letter) {
            for (int counter = 1; counter <= (letter - 'A' + 1); ++counter)
                cout << letter << " ";
            cout << endl;
        }

    }

    void allWordsFromAAAtoZZZ() {
        short stop;
        for (char firstLetter = 'A'; firstLetter <= 'Z'; ++firstLetter) {
            cout << "------------------[ " << firstLetter << " ]------------------" << endl;
            cin >> stop;
            for (char secondLetter = 'A'; secondLetter <= 'Z'; ++secondLetter) {
                for (char thirdLetter = 'A'; thirdLetter <= 'Z'; ++thirdLetter)
                    cout << firstLetter << secondLetter << thirdLetter << endl;
            }
            cout << endl;
        }

    }

    string readA3LettersPassword(string message) {
        string password;

        do {
            cout << message;
            cin >> password;
            if (password.length() != 3) {
                cout << " !! Invalid password [" << password << "], length is (" << password.length() << ") !" << endl;
                cout << "  - Try again: \n" << endl;
            }
        } while (password.length() != 3);
        return password;
    }

    void guessA3LettersPassword() {
        int trials = 0;
        string password;
        string guessedPassword = "";

        password = readA3LettersPassword("--> Please enter a 3 letters password: ");

        for (char firstLetter = 'A'; firstLetter <= 'Z'; ++firstLetter) {
            for (char secondLetter = 'A'; secondLetter <= 'Z'; ++secondLetter) {
                for (char thirdLetter = 'A'; thirdLetter <= 'Z'; ++thirdLetter) {
                    ++trials;

                    guessedPassword += firstLetter;
                    guessedPassword += secondLetter;
                    guessedPassword += thirdLetter;

                    cout << "  - Trial[" << trials << "] : [ " << guessedPassword << " ]" << endl;

                    if (guessedPassword.compare(password) == 0) {
                        cout << "\n-------------------------------------" << endl;
                        cout << " -> The password you entered is: [ " << guessedPassword << " ]" << endl;
                        cout << "  ? Password found after [" << trials << "] trial(s) !" << endl;
                        cout << "-------------------------------------" << endl;
                        return;
                    }
                    guessedPassword = "";
                }
            }
        }
        cout << "  X Password not found after [" << trials << "] trial(s) !" << endl;

    }

    string readPlainText(string message) {
        string plainText;

        cout << message;
        getline(cin, plainText);

        return plainText;
    }

    string encryptText(string plainText, const short encryptionKey) {

        string cipherText = "";

        for (int i = 0; i < plainText.length(); ++i)
            cipherText += (plainText[i] + encryptionKey);

        return cipherText;
    }

    string decryptText(string cipherText, const short encryptionKey) {

        string originalPlainText = "";

        for (int i = 0; i < cipherText.length(); ++i)
            originalPlainText += (cipherText[i] - encryptionKey);

        return originalPlainText;
    }

    void encryptAndDecryptText() {
        const short encryptionKey = 2;
        string plainText, cipherText, originalPlainText;

        plainText = readPlainText("--> Please enter your a text: ");
        cipherText = encryptText(plainText, encryptionKey);
        originalPlainText = decryptText(cipherText, encryptionKey);

        cout << "--> Your text: " << endl;
        cout << "  - Before Encryption: " << plainText << endl;
        cout << "  - After Encryption : " << cipherText << endl;
        cout << "  - After Decryption : " << originalPlainText << endl;

    }

    short menu() {

        cout << "--> Please enter your choice: " << endl;
        cout << " [1] Small Letter" << endl;
        cout << " [2] Capital Letter" << endl;
        cout << " [3] Special Character" << endl;
        cout << " [4] Digit" << endl;

        return readPositiveInteger(1, 4);
    }

    char randomPatternResult(randomPattern pattern) {

        switch ((randomPattern)pattern) {
        case randomPattern::smallLetter:
            return (char)randomNumbers('a', 'z');
        case randomPattern::capitalLetter:
            return (char)randomNumbers('A', 'Z');
        case randomPattern::specialCharacter:
            return (char)randomNumbers(33, 47);
        case randomPattern::digit:
            return (char)randomNumbers('0', '9');
        }

    }

    string generateAWord(short length, randomPattern charactersType) {
        string word = "";

        for (int letter = 1; letter <= length; ++letter)
            word += randomPatternResult(charactersType);
        return word;
    }

    string generateAKey(short numberOfSlots, short sizeOfWords, randomPattern typeOfCharacters) {
        string key = "";

        for (int group = 1; group <= numberOfSlots; ++group) {
            key += generateAWord(sizeOfWords, typeOfCharacters);
            if (group != numberOfSlots)
                key += "-";
        }
        return key;
    }


    string generateAKey(short numberOfSlots) {
        string key = "";

        for (int group = 1; group <= numberOfSlots; ++group) {
            key += generateAWord(5, randomPattern::capitalLetter);
            if (group != numberOfSlots)
                key += "-";
        }
        return key;
    }

    void generateKeys(int numberOfkeys, short numberOfSlots) {
        for (int key = 1; key <= numberOfkeys; ++key)
            cout << " -- Key [" << key << "] : " << generateAKey(numberOfSlots) << endl;
    }

    string upperCaseAllString(string text) {
        for (int i = 0; i < text.size(); i++)
            text[i] = toupper(text[i]);
        return text;
    }

    string lowerCaseAllString(string text) {
        for (int i = 0; i < text.size(); i++)
            text[i] = tolower(text[i]);
        return text;
    }

}