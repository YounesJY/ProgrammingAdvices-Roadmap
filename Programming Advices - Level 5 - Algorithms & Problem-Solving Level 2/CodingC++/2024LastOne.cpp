#include<iostream>
#include<string.h>
#include<math.h>
using namespace std;

enum choice {yes = 1, no = 2};
enum randomPattern {smallLetter,
                    capitalLetter,
                    specialCharacter,
                    digit
                   };

void readAnInteger(int &number, string messageToDisplay) {
    cout << messageToDisplay;
    cin >> number;
}

int readAPositiveInteger(string message) {
    int number;
    do {
        cout << message;
        cin >> number;
        if(number < 1) {
            cout << " !! Invalid value [" << number << "] !" << endl;
            cout << "  - Try again: \n" <<  endl;
        }
    } while(number < 1 );
    return number;
}

int readAPositiveIntegerInRange(int from, int to) {
    int number;
    do {
        cout << "--> Please enter a number in range [" << from << " .. " << to << "]: ";
        cin >> number;
        if(number < from || number > to) {
            cout << " !! Invalid value [" << number << "] !" << endl;
            cout << "  - Try again: \n" <<  endl;
        }
    } while(number < from || number > to);
    return number;
}

/*
//------------------------------------------------------------------------------------//
void printHeader() {
    cout << "                          * Multiplication Table *        \n " << endl;
    for(int i = 1; i <= 10; ++i)
        cout << "      " << i;
    cout << "\n _______________________________________________________________________" << endl;
}


void printaside(int row) {
    if(row <= 9)
        cout << " " << row << "  | ";
    else
        cout << " " << row << " | ";
}

void printResult(int row, int column) {
    if(row * column <= 9)
        cout << row * column << "      ";
    else if(row * column <= 99)
        cout << row * column << "     ";
    else
        cout << row * column << "   ";
}

void printOperations() {
    for(int row = 1; row <= 10; ++row) {
        printaside(row);
        for(int column = 1; column <= 10; ++column)
            printResult(row, column);
        cout << endl;
    }
}

void printMultiplicationTable() {
    printHeader();
    printOperations();
}

void printPrimeNumbersFrom1ToN() {

    int number;

    cout << "--> Please enter a positive number: ";
    cin >> number;

    for(int i = 1; i <= number; ++i) {
        if(isPrime(i))
            cout << "- [ " << i << " ]" << endl;
    }

}
//------------------------------------------------------------------------------------//
*/
bool isAPrimeNumber(int number) {

    for(int i = 2; i <= (number/2); ++i) {
        if(number % i == 0)
            return false;
    }
    return true;
}

void printPrimeNumbersFrom1ToN() {
    int number = readAPositiveInteger("--> Please enter a positive number: ");

    for(int i = 1; i <= number; ++i) {
        if(isAPrimeNumber(i))
            cout << "- [ " << i << " ]" << endl;
    }

}

bool isAPerfectNumber(int number) {
    int sum = 0;
    int halfOfTheNumber = (number / 2);
    for(int i = 1; i <= halfOfTheNumber; ++i) {
        if(number % i == 0)
            sum += i;
    }
    return (sum == number);
}

void perfectNumber(int number) {
    if(isAPerfectNumber(number))
        cout << "--> √ [ " << number << " ] is a perfect number!" << endl;
    else
        cout << "--> x [ " << number << " ] isn't a perfect number!" << endl;
}

void printPerfectNumbersFrom1ToN() {
    int number = readAPositiveInteger("--> Please enter a positive number: ");

    cout << "--> Perfect numbers from 1 to " << number << ": " << endl;
    for(int i = 1; i <= number; ++i) {
        if(isAPerfectNumber(i))
            cout << "--> [ " << i << " ]" << endl;
    }
}

void printNumerDigitsInReversedOrder(int number) {
    //int number = readAPositiveInteger("--> Please enter a positive number: ");
    int remainder = 0;

    do {
        remainder = (number % 10);
        cout << remainder << endl;
        number /= 10;

    } while (number > 0);

}

void printTheSumOfANumberDigits() {
    int number = readAPositiveInteger("--> Please enter a positive number: ");
    int sum = 0;

    do {
        sum += (number % 10);
        number /= 10;
    } while (number > 0);

    cout << "--> The sum of digits is : [ " << sum << " ]" << endl;
}

int getNumberOfDigitsOfANumber(int number) {
    int remainder, counter = 0;

    do {
        remainder = (number % 10);
        number /= 10;
        ++counter;
    } while (number > 0);

    return counter;
}

int getANumerInReversedOrder(int number) {
    //int number = readAPositiveInteger("--> Please enter a positive number: ");
    int reveredNumber = 0, remainder = 0, numberOfDigits = 0;
    /*
        numberOfDigits = getNumberOfDigitsOfANumber(number);

        do {
            remainder = (number % 10);
            reveredNumber += remainder * pow(10, --numberOfDigits);
            number /= 10;
        } while (number > 0);
    */
    do {
        remainder = (number % 10);
        reveredNumber = reveredNumber * 10 + remainder;
        number /= 10;
    } while (number > 0);

    //cout << "--> The reversed version is : [ " << reveredNumber << " ]" << endl;
    return reveredNumber;
}

short printADigitFrequencyInANumber(int number, short digit) {
    short remainder = 0, counter = 0;

    do {
        remainder = (number % 10);
        if(remainder == digit)
            ++counter;
        number /= 10;
    } while (number > 0);

    return counter;
}

void printNumberDigitsFrequency() {
    int number = readAPositiveInteger("--> Please enter a positive number: ");
    short digitFrequency = 0;

    for(short digit = 0; digit <= 9; ++digit) {
        digitFrequency = printADigitFrequencyInANumber(number, digit);
        if(digitFrequency > 0)
            cout << "[" <<  digit << "] frequency is [" <<  digitFrequency << "] times." << endl;
    }
}

void printNumberDigitsInOrder() {
    int number = readAPositiveInteger("--> Please enter a positive number: ");
    int reversedVersion = getANumerInReversedOrder(number);
    printNumerDigitsInReversedOrder(reversedVersion);
}

bool isAPalindromeNumber(int number) {
    return (number == getANumerInReversedOrder(number));
}

void palindromeNumber() {
    int number = readAPositiveInteger("--> Please enter a positive number: ");

    if(isAPalindromeNumber(number))
        cout << "--> √ [ " << number << " ] is a palindrome number!" << endl;
    else
        cout << "--> x [ " << number << " ] isn't a palindrome number!" << endl;
}

void invertedNumberPattern() {
    int number = readAPositiveIntegerInRange(1, 9);

    for(int pattern = number; pattern >= 1; --pattern) {
        for(int counter = 1; counter <= pattern; ++counter)
            cout << pattern << " ";
        cout << endl;
    }

}

void invertedLettersPattern() {
    int number = readAPositiveIntegerInRange(1, 26);

    for(char letter = (number + 'A' - 1); letter >= 'A'; --letter) {
        for(int counter = 1; counter <= (letter - 'A' + 1); ++counter)
            cout << letter << " ";
        cout << endl;
    }

}



void numberPattern() {
    int number = readAPositiveIntegerInRange(1, 9);

    for(int pattern = 1; pattern <= number; ++pattern) {
        for(int counter = 1; counter <= pattern; ++counter)
            cout << pattern << " ";
        cout << endl;
    }

}

void lettersPattern() {
    int number = readAPositiveIntegerInRange(1, 26);

    for(char letter = 'A'; letter <= (number + 'A' - 1); ++letter) {
        for(int counter = 1; counter <= (letter - 'A' + 1); ++counter)
            cout << letter << " ";
        cout << endl;
    }

}

void allWordsFromAAAtoZZZ() {
    short stop;
    for(char firstLetter = 'A'; firstLetter <= 'Z'; ++firstLetter) {
        cout << "------------------[ " << firstLetter << " ]------------------" << endl;
        cin >> stop;
        for(char secondLetter = 'A'; secondLetter <= 'Z'; ++secondLetter) {
            for(char thirdLetter = 'A'; thirdLetter <= 'Z'; ++thirdLetter)
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
        if(password.length() != 3) {
            cout << " !! Invalid password [" << password << "], length is (" <<  password.length() << ") !" << endl;
            cout << "  - Try again: \n" <<  endl;
        }
    } while(password.length() != 3);
    return password;
}

void guessA3LettersPassword() {
    int trials = 0;
    string password;
    string guessedPassword = "";

    password = readA3LettersPassword("--> Please enter a 3 letters password: ");

    for(char firstLetter = 'A'; firstLetter <= 'Z'; ++firstLetter) {
        for(char secondLetter = 'A'; secondLetter <= 'Z'; ++secondLetter) {
            for(char thirdLetter = 'A'; thirdLetter <= 'Z'; ++thirdLetter) {
                ++trials;

                guessedPassword += firstLetter;
                guessedPassword += secondLetter;
                guessedPassword += thirdLetter;

                cout << "  - Trial[" << trials << "] : [ " << guessedPassword << " ]" << endl;

                if(guessedPassword.compare(password) == 0) {
                    cout << "\n-------------------------------------" << endl;
                    cout << " -> The password you entered is: [ " << guessedPassword << " ]" << endl;
                    cout << "  √ Password found after [" << trials << "] trial(s) !" << endl;
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

    for(int i = 0; i < plainText.length(); ++i)
        cipherText += (plainText[i] + encryptionKey);

    return cipherText;
}

string decryptText(string cipherText, const short encryptionKey) {

    string originalPlainText = "";

    for(int i = 0; i < cipherText.length(); ++i)
        originalPlainText += (cipherText[i] - encryptionKey);

    return originalPlainText;
}

void encryptAndDecryptText() {
    const short encryptionKey = 2;
    string plainText, cipherText, originalPlainText;

    plainText = readPlainText("--> Please enter your a text: ");
    cipherText =  encryptText(plainText, encryptionKey);
    originalPlainText = decryptText(cipherText, encryptionKey);

    cout << "--> Your text: " << endl;
    cout << "  - Before Encryption: " << plainText << endl;
    cout << "  - After Encryption : " << cipherText << endl;
    cout << "  - After Decryption : " << originalPlainText << endl;

}

int randomNumbers(int from, int to) {
    //(rand() % (5 - 1 + 1) + 1) ==> [1, 2 ,3 ,4 ,5]
    //(rand() % (5) + 1) ==> [1, 2 ,3 ,4 ,5]
    //([0, 1, 2 ,3 ,4] + 1) ===> [1, 2 ,3 ,4 ,5]
    return (rand() % (to - from + 1) + from);
}

short menu() {

    cout << "--> Please enter your choice: " << endl;
    cout << " [1] Small Letter" << endl;
    cout << " [2] Capital Letter" << endl;
    cout << " [3] Special Character" << endl;
    cout << " [4] Digit" << endl;

    return readAPositiveIntegerInRange(1, 4);
}

char randomPatternResult(randomPattern pattern) {

    switch((randomPattern)pattern) {
    case randomPattern::smallLetter:
        return (char) randomNumbers('a', 'z');
    case randomPattern::capitalLetter:
        return (char) randomNumbers('A', 'Z');
    case randomPattern::specialCharacter:
        return (char) randomNumbers(33, 47);
    case randomPattern::digit:
        return (char) randomNumbers('0', '9');
    }

}

string generateAWord(short length, randomPattern charactersType) {
    string word = "";

    for(int letter = 1; letter <= length; ++letter)
        word += randomPatternResult(charactersType);
    return word;
}

string generateAKey(short numberOfSlots, short sizeOfWords, randomPattern typeOfCharacters) {
    string key = "";

    for(int group = 1; group <= numberOfSlots; ++group) {
        key += generateAWord(sizeOfWords, typeOfCharacters);
        if(group != numberOfSlots)
            key += "-";
    }
    return key;
}


string generateAKey(short numberOfSlots) {
    string key = "";

    for(int group = 1; group <= numberOfSlots; ++group) {
        key += generateAWord(5, randomPattern::capitalLetter);
        if(group != numberOfSlots)
            key += "-";
    }
    return key;
}

void generateKeys(int numberOfkeys, short numberOfSlots) {
    for(int key = 1; key <= numberOfkeys; ++key)
        cout << " -- Key [" << key << "] : " << generateAKey(numberOfSlots) << endl;
}


void readElements(int array[],  int &sizeToRead) {

    sizeToRead = readAPositiveInteger("--> Please enter the number of elements to read: ");

    cout << "--> Enter array elements:" << endl;;
    for(int i = 0; i < sizeToRead; ++i) {
        cout << "  - Enter element [" << (i + 1) << "]: ";
        cin >> array[i];
    }

}

int countRepeatedTimes(int array[], int size, int elementToCheck) {

    int counter = 0;

    for(int i = 0; i <size; ++i) {
        if(array[i] == elementToCheck)
            ++counter;
    }

    return counter;
}

void printArray(int array[], int size) {

    for(int i = 0; i <size; ++i)
        cout << "  - Element N.[" << (i + 1) << "] : " << array[i] << endl;
    cout << endl;
}

void printStringArray(string array[], int size) {

    for(int i = 0; i <size; ++i)
        cout << "  - Element N.[" << (i + 1) << "] : " << array[i] << endl;
    cout << endl;
}

void countRepeatedElement(int array[], int size) {
    int elementToCheck;

    cout << "--> Please enter the element to check: ";
    cin >> elementToCheck;
    cout << " -> The original array: ";
    printArray(array, size);
    cout << " -- Number [" << elementToCheck << "] is repeated {" << countRepeatedTimes(array, size, elementToCheck) << "} time(s)" << endl;

}

void fillArrayWithRandomNumbers(int array[], int &sizeToFill, int from, int to) {

    cout << " >> To fill the array: " << endl;
    sizeToFill = readAPositiveIntegerInRange(1, 100);
    for(int i = 0; i < sizeToFill; ++i)
        array[i] = randomNumbers(from, to);

}

int maximumValueOfArray(int array[], int size) {
    int maxValue = array[0];

    for(int i = 0; i < size ; ++i) {
        if(maxValue < array[i])
            maxValue = array[i];
    }

    return maxValue;
}

int minimumValueOfArray(int array[], int size) {
    int minValue = array[0];

    for(int i = 0; i < size ; ++i) {
        if(minValue > array[i])
            minValue = array[i];
    }

    return minValue;
}

int sumOfArrayValues(int array[], int size) {
    int sum = 0;

    for(int i = 0; i < size ; ++i)
        sum += array[i];

    return sum;
}

float averageOfArrayValues(int array[], int size) {
    return ((float)sumOfArrayValues(array, size)/ size);
}

void copyArrayToAnother(int sourceArray[], int destinationArray[], int size) {
    for(int i = 0; i < size; ++i)
        destinationArray[i] = sourceArray[i];
}

void copyReversedArrayToAnother(int sourceArray[], int destinationArray[], int size) {
    int reversedIndex = size;

    for(int i = 0; i < size; ++i)
        destinationArray[i] = sourceArray[--reversedIndex];
}

void copyPrimeNumbersOfArray(int sourceArray[], int destinationArray[], int size, int &sizeOfPrimes) {
    int counter = 0;

    for(int i = 0; i < size; ++i) {
        if(isAPrimeNumber(sourceArray[i])) {
            destinationArray[counter] = sourceArray[i];
            ++counter;
        }
    }
    sizeOfPrimes = counter;

}

void fillArrayWithRandomNumbers2(int array[], int sizeToFill, int from, int to) {
    for(int i = 0; i < sizeToFill; ++i) {
        array[i] = randomNumbers(from, to);
    }
}

void sumOf2Arrays(int sourceArray1[], int sourceArray2[], int destinationArray[], int size) {
    for(int i = 0; i < size; ++i) {
        destinationArray[i] = sourceArray1[i] + sourceArray2[i];
    }
}

void fillArrayWithOrderedNumbers(int array[], int sizeToFill, int from, int to) {
    for(int i = 0; i < sizeToFill; ++i) {
        if(from > to)
            break;
        array[i] = from++;
    }
}

void swap2Numbers(int &number1, int &number2) {
    int tempSwap;

    tempSwap = number1;
    number1 = number2;
    number2 = tempSwap;

}

void shuffleArray(int array[], int sizeToFill) {
    int sourceIndex, destinationIndex;

    for(int i = 0; i < sizeToFill; ++i) {
        sourceIndex = randomNumbers(0, sizeToFill - 1);
        destinationIndex = randomNumbers(0, sizeToFill - 1);
        swap2Numbers(array[sourceIndex], array[destinationIndex]);
    }
}

void fillArrayWithRandomKeys(string array[], int sizeToFill) {
    for(int i = 0; i < sizeToFill; ++i)
        array[i] = generateAKey(3, 5, randomPattern::capitalLetter);
}

int findNumberPositionInArray(int number, int array[], int size) {
    for(int i = 0; i < size; ++i) {
        if(array[i] == number)
            return i;
    }
    return (-1);
}

bool isItFoundInArray(int number, int array[], int size) {
    // if the return position is -1, that's means the number is not found(not exist)
    return (findNumberPositionInArray(number, array, size) != (-1));
}

void addAnElementToArray(int element, int array[], int atIndex, int size) {
    if(atIndex < size)
        array[atIndex] = element;
    else
        cout << " !! Index (" << atIndex << ") is out of range (0 - " << (size - 1) << ") !" << endl;
}

void askForChoice(enum choice &userChoice, string messageToDisplay) {

    cout << messageToDisplay;
    cout << "  > [1] Yes" << endl;
    cout << "  > [2] No" << endl;
    cout << " => Your choice ?" << endl;
    userChoice = (enum choice) readAPositiveIntegerInRange(1, 2);

}

void fillArrayDynamically(int array[], int size, int &filledSize) {
    int numberToAdd;
    enum choice userChoice;

    while(true) {
        cout << "\n-------------------------------------\n";
        askForChoice(userChoice, "==> Do you want to add more numbers: \n");
        if(userChoice == choice::yes) {
            readAnInteger(numberToAdd, " => Please enter the number to add: ");
            addAnElementToArray(numberToAdd, array, filledSize, size);
            ++filledSize;
        }
        else
            break;
    }

}

int main()
{
    srand((unsigned)time(NULL));
    // Cours 5: Algorithm Level 2
    /*
        //---------------------------------------------------------------------------
        //--> Exercise 1: Print Multiplication Table
        printMultiplicationTable();

        //---------------------------------------------------------------------------
        //--> Exercise 2: Print Prime Numbers From 1 To N
        printPrimeNumbersFrom1ToN();

        //---------------------------------------------------------------------------
        //--> Exercise 3: Check If A Number Is Perfect Or Not
        perfectNumber();

        //---------------------------------------------------------------------------
        //--> Exercise 4: Print Perfect Numbers From 1 To N
        //printPerfectNumbersFrom1ToN();

        //---------------------------------------------------------------------------
        //--> Exercise 5: Print A Number Digits In Reversed Order
        printNumerDigitsInReversedOrder();

        //---------------------------------------------------------------------------
        //--> Exercise 6: Print The Sum Of A Number Digits
        printTheSumOfANumberDigits();

        //---------------------------------------------------------------------------
        //--> Exercise 7: Reverse A Number
        printNumerInReversedOrder();

        //---------------------------------------------------------------------------
        //--> Exercise 8: Print A Digit Frequency In A Number
        while(true) {
            int number = readAPositiveInteger("--> Please enter a positive number: ");
            short digit = readAPositiveIntegerInRange(0, 9);
            cout << "[" <<  digit << "] frequency is [" <<  printADigitFrequencyInANumber(number, digit) << "] times." << endl;
        }

        //---------------------------------------------------------------------------
        //--> Exercise 9: Print A Number Digits Frequency
        printNumberDigitsFrequency();

        //---------------------------------------------------------------------------
        //--> Exercise 10: Print A Number Digits In Order
        printNumberDigitsInOrder();

        //---------------------------------------------------------------------------
        //--> Exercise 11: Palindrome Number
        while(true)
            palindromeNumber();

        //---------------------------------------------------------------------------
        //--> Exercise 12: Inverted Number Pattern
        while(true)
            invertedNumberPattern();

        //---------------------------------------------------------------------------
        //--> Exercise 13:  Number Pattern
        while(true)
            numberPattern();

        //--> Exercise 14:  Invested Letters Pattern

    while(true)
        invertedLettersPattern();
            //--> Exercise 15:  Letters Pattern
         while(true)
        lettersPattern();
    }


    //--> Exercise 16:  all from AAA to ZZZ
    allWordsFromAAAtoZZZ();

    //--> Exercise 17:  all from AAA to ZZZ
    guessA3LettersPassword();

    //  cout << ((string)"Aaa").length() << endl;
    //--> Exercise 18:  Encrypt and Decrypt Name
    encryptAndDecryptText();

    //--> Exercise 19:  Encrypt and Decrypt Name
    srand((unsigned)time(NULL));
    int from, to;
    /*
    while(true) {
        from = readAPositiveInteger("Enter the first bound (from): ");
        to = readAPositiveInteger("Enter the second bound (to): ");
        cout << randomNumbers(from, to) << endl << endl;
    }
    //--> Exercise 20:  Generate A Random Pattern
    while(true) {
        cout << "\n-------------------------------------" << endl;
        from = menu();
        cout << " -- Result is: " << randomPatternResult((randomPattern)from) << endl;
        cout << "\n-------------------------------------" << endl;
    }


    //--> Exercise 21:  Generate Keys
    while(true) {
        cout << "-------------------------------------\n";
        generateKeys(readAPositiveInteger("--> Please enter the number of keys: "), readAPositiveInteger("--> Please enter the number of key's slots: "));
        cout << "-------------------------------------\n";
    }
    //--> Exercise 22:  Count Repeated Element In An Array

        int numbers[100], sizeToRead;

        readElements(numbers, sizeToRead);
        countRepeatedElement(numbers, sizeToRead);

    //--> Exercise 23-24-25-26-27-28:  Fill Array With Random Numbers - Print The Maximum Value - Print The Minimum Value - Print The Sum - Print The Average - Copy Array

    int numbers[100], copyNumners[100], size, sizeOfPrimes;
    printPrimeNumbersFrom1ToN();
    while(true) {
        cout << "-------------------------------------\n";
        fillArrayWithRandomNumbers(numbers, size, 1, 100);
        cout << " -- The original (source) array: " << endl;
        printArray(numbers, size);
        copyPrimeNumbersOfArray(numbers, copyNumners, size, sizeOfPrimes);
        cout << " -- The new (destination) array: " << endl;
        printArray(copyNumners, sizeOfPrimes);
        cout << " -> The maximum value is [ " << maximumValueOfArray(copyNumners, sizeOfPrimes) << " ] " << endl;
        cout << " -> The minimum value is [ " << minimumValueOfArray(copyNumners, sizeOfPrimes) << " ] " << endl;
        cout << " -> The sum of values is [ " << sumOfArrayValues(copyNumners, sizeOfPrimes) << " ] " << endl;
        cout << " -> The average of values is [ " << averageOfArrayValues(copyNumners, sizeOfPrimes) << " ] " << endl;
        cout << "-------------------------------------\n";
    }


    //--> Exercise 30:  Sum Of Two Arrays
    int numbers1[100], numbers2[100], sumOfNumbers[100], size;

    cout << "==> Please choose a size for arrays: " << endl;
    size = readAPositiveIntegerInRange(1, 100);

    fillArrayWithRandomNumbers2(numbers1, size, 1, 100);
    fillArrayWithRandomNumbers2(numbers2, size, 1, 100);

    sumOf2Arrays(numbers1, numbers2, sumOfNumbers, size);

    cout << "-------------------------------------\n";
    cout << "==> First array elements :" << endl;
    printArray(numbers1, size);
    cout << "-------------------------------------\n";
    cout << "==> Second array elements:" << endl;
    printArray(numbers2, size);
    cout << "-------------------------------------\n";
    cout << "==> The sum of two array elements:" << endl;
    printArray(sumOfNumbers, size);
    cout << "-------------------------------------\n";

    //--> Exercise 31: Shuffling An Array

    int numbers[100], size;

    cout << "==> Please choose the array size: " << endl;
    size = readAPositiveIntegerInRange(1, 100);

    fillArrayWithOrderedNumbers(numbers, size, 1, size);

    cout << "==> Array before shuffling: " << endl;
    printArray(numbers, size);

    shuffleArray(numbers, size);

    cout << "==> Array After shuffling: " << endl;
    printArray(numbers, size);


    //--> Exercise 32: Copy Array In Reversed Order
    int numbers[100], reversedNumners[100], size;

    while(true) {
        cout << "-------------------------------------\n";
        cout << "==> Please the array size: " << endl;
        size = readAPositiveIntegerInRange(1, 100);
        fillArrayWithRandomNumbers2(numbers, size, 1, 100);

        cout << " -- The original (source) array: " << endl;
        printArray(numbers, size);
        copyReversedArrayToAnother(numbers, reversedNumners, size);
        cout << " -- The reversed (destination) array: " << endl;
        printArray(reversedNumners, size);
        cout << "-------------------------------------\n";
    }


    //--> Exercise 33: Generate Random Keys
    string keys[100];
    int size;

    cout << "==> How many keys 🗝️🔐 ? " << endl;
    size = readAPositiveIntegerInRange(1, 100);

    fillArrayWithRandomKeys(keys, size);

    cout << "-------------------------------------\n";
    cout << " => Generated keys are 🗝️🔐: " << endl;
    printStringArray(keys, size);
    cout << "-------------------------------------\n";

    //--> Exercise 34 - 35: search for array element index - Element Found or Not

    int numbers[100], size, numberToSearch, index;

    cout << "==> Please choose a size for arrays: " << endl;
    size = readAPositiveIntegerInRange(1, 100);

    fillArrayWithRandomNumbers2(numbers, size, 1, 100);
    printArray(numbers, size);

    cout << "==> Please enter a number to search for: ";
    cin >> numberToSearch;
    cout << " => The number you are looking for is [ " << numberToSearch << " ]" << endl;

    //   index = findNumberPositionInArray(numberToSearch, numbers, size);

        if(index == (-1))
            cout << " => The number is not found :(" << endl;
        else {
            cout << " => The number found at position: [ " << index<< " ]" << endl;
            cout << " => The number order in array is: [ " << (index + 1) << " ]" << endl;

        }

    if(!isItFoundInArray(numberToSearch, numbers, size))
        cout << " => x No,The number is not found -_-" << endl;
    else
        cout << " => √ yes,The number is found >_<" << endl;

    */
    //--> Exercise 36: Add element to the array semi-dynamic


    int numbers[100], size = 100, filledSize = 0;

    fillArrayDynamically(numbers, size, filledSize);

    cout << "==> Array length is: [ " << filledSize << " ]" << endl;
    cout << "==> Array elements: " << endl;
    printArray(numbers, filledSize);


    return 0;

}
/*
string generateAWord(short length, randomPattern charactersType) {
    string word = "";

    for(int letter = 1; letter <= length; ++letter)
        word += randomPatternResult(charactersType);
    return word;
}

string generateAKey(short numberOfSlots, short sizeOfWords, randomPattern typeOfCharacters) {
    string key = ""

    for(int group = 1; group <= numberOfSlots; ++group) {
        key += generateAWord(sizeOfWords, typeOfCharacters);
        if(group != numberOfSlots)
            key += "-";
    }
    return key;
}

void generateKeys(int numberOfkeys, short numberOfSlots) {
    for(int key = 1; key <= numberOfkeys; ++key)
        cout << " -- Key [" << key << "] : " << generateAKey(numberOfSlots) << endl;
}

string GenerateAKey() {
    string key = "";

    for(short group = 1; group <= 4; ++group) {
        for(short letter = 1; letter <= 4; ++letter)
            key += randomPatternResult(randomPattern::capitalLetter);
        if(group != 4)
            key += "-";
    }
    return key;
}

void generateKeys(int numberOfkeys) {
    for(int key = 1; key <= numberOfkeys; ++key)
        cout << " -- Key [" << key << "] : " << getAKey() << endl;
}

void generateKeys() {

    cout << "-------------------------------------\n";
    generateKeys(numberOfkeys);
    cout << "-------------------------------------\n";
}

*/

    