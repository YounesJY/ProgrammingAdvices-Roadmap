#pragma once

#include <iostream>
#include "MyInputLibrary.h";
#include "MyMathLibrary.h";
#include "MyOthersStuffLibrary.h";

using namespace std;
using namespace input;
using namespace math;
using namespace others;

namespace array {
    void readElements(int array[], int& sizeToRead) {
        sizeToRead = readPositiveInteger("--> Please enter the number of elements to read: ");

        cout << "--> Enter array elements:" << endl;;
        for (int i = 0; i < sizeToRead; ++i) {
            cout << "  - Enter element [" << (i + 1) << "]: ";
            cin >> array[i];
        }

    }

    void printArray(int array[], int size) {

        for (int i = 0; i < size; ++i)
            cout << "  - Element N.[" << (i + 1) << "] : " << array[i] << endl;
        cout << endl;
    }

    int countRepeatedTimes(int elementToCheck, int array[], int size) {

        int counter = 0;

        for (int i = 0; i < size; ++i) {
            if (array[i] == elementToCheck)
                ++counter;
        }

        return counter;
    }

    void printStringArray(string array[], int size) {

        for (int i = 0; i < size; ++i)
            cout << "  - Element N.[" << (i + 1) << "] : " << array[i] << endl;
        cout << endl;
    }

    void countRepeatedElement(int array[], int size) {
        int elementToCheck;

        cout << "--> Please enter the element to check: ";
        cin >> elementToCheck;
        cout << " -> The original array: ";
        printArray(array, size);
        cout << " -- Number [" << elementToCheck << "] is repeated {" << countRepeatedTimes(elementToCheck, array, size) << "} time(s)" << endl;

    }

    void fillArrayWithRandomNumbers(int array[], int& sizeToFill, int from, int to) {

        cout << " >> Must determine the size to fill the array: " << endl;
        sizeToFill = readPositiveInteger(1, 100);
        for (int i = 0; i < sizeToFill; ++i)
            array[i] = randomNumbers(from, to);

    }

    int maximumValueOfArray(int array[], int size) {
        int maxValue = array[0];

        for (int i = 0; i < size; ++i) {
            if (maxValue < array[i])
                maxValue = array[i];
        }

        return maxValue;
    }

    int minimumValueOfArray(int array[], int size) {
        int minValue = array[0];

        for (int i = 0; i < size; ++i) {
            if (minValue > array[i])
                minValue = array[i];
        }

        return minValue;
    }

    int sumOfArrayValues(int array[], int size) {
        int sum = 0;

        for (int i = 0; i < size; ++i)
            sum += array[i];

        return sum;
    }

    float averageOfArrayValues(int array[], int size) {
        return ((float)sumOfArrayValues(array, size) / size);
    }

    void copyArrayToAnother(int sourceArray[], int destinationArray[], int size) {
        for (int i = 0; i < size; ++i)
            destinationArray[i] = sourceArray[i];
    }

    void copyReversedArrayToAnother(int sourceArray[], int destinationArray[], int size) {
        int reversedIndex = size;

        for (int i = 0; i < size; ++i)
            destinationArray[i] = sourceArray[--reversedIndex];
    }

    void copyPrimeNumbersOfArray(int sourceArray[], int destinationArray[], int size, int& sizeOfPrimes) {
        int counter = 0;

        for (int i = 0; i < size; ++i) {
            if (isAPrimeNumber(sourceArray[i])) {
                destinationArray[counter] = sourceArray[i];
                ++counter;
            }
        }
        sizeOfPrimes = counter;
    }

    void fillArrayWithRandomNumbers2(int array[], int sizeToFill, int from, int to) {
        for (int i = 0; i < sizeToFill; ++i) {
            array[i] = randomNumbers(from, to);
        }
    }

    void sumOf2Arrays(int sourceArray1[], int sourceArray2[], int destinationArray[], int size) {
        for (int i = 0; i < size; ++i) {
            destinationArray[i] = sourceArray1[i] + sourceArray2[i];
        }
    }

    void fillArrayWithOrderedNumbers(int array[], int sizeToFill, int from, int to) {
        for (int i = 0; i < sizeToFill; ++i) {
            if (from > to)
                break;
            array[i] = from++;
        }
    }

    void swap2Numbers(int& number1, int& number2) {
        int tempSwap;

        tempSwap = number1;
        number1 = number2;
        number2 = tempSwap;

    }

    void shuffleArray(int array[], int sizeToFill) {
        int sourceIndex, destinationIndex;

        for (int i = 0; i < sizeToFill; ++i) {
            sourceIndex = randomNumbers(0, sizeToFill - 1);
            destinationIndex = randomNumbers(0, sizeToFill - 1);
            swap2Numbers(array[sourceIndex], array[destinationIndex]);
        }
    }

    void fillArrayWithRandomKeys(string array[], int sizeToFill) {
        for (int i = 0; i < sizeToFill; ++i)
            array[i] = generateAKey(3, 5, randomPattern::capitalLetter);
    }

    int findNumberPositionInArray(int number, int array[], int size) {
        for (int i = 0; i < size; ++i) {
            if (array[i] == number)
                return i;
        }
        return (-1);
    }

    bool isElementInTheArray(int number, int array[], int size) {
        // if the return position is -1, that's means the number is not found(not exist)
        return (findNumberPositionInArray(number, array, size) != (-1));
    }

    void askForChoice(enum choice& userChoice, string messageToDisplay) {

        cout << messageToDisplay;
        cout << "  > [1] Yes" << endl;
        cout << "  > [2] No" << endl;
        cout << " => Your choice ?" << endl;
        userChoice = (enum choice)readPositiveInteger(1, 2);

    }

    void addAnElementToArrayByIndex(int element, int array[], int atIndex, int size) {
        if (atIndex < size && atIndex >= 0)
            array[atIndex] = element;
        else
            cout << " !! Index (" << atIndex << ") is out of range (0 - " << (size - 1) << ") !" << endl;
    }

    void addAnElementToArrayByFilledSize(int element, int array[], int& filledSize) {
        array[filledSize] = element;
        ++filledSize;
    }

    void fillArrayDynamically(int array[], int size, int& filledSize) {
        int numberToAdd;
        enum choice userChoice;

        while (true) {
            cout << "\n-------------------------------------\n";
            if (filledSize < size) {
                askForChoice(userChoice, "==> Do you want to add more numbers: \n");
                if (userChoice == choice::yes) {
                    numberToAdd = readInteger( " => Please enter the number to add: ");
                    addAnElementToArrayByIndex(numberToAdd, array, filledSize, size);
                    ++filledSize;
                }
                else
                    break;
            }
            else {
                cout << " !! Array is fill, you can't add more elements" << endl;
                return;
            }
        }
    }

    void copyArrayToAnotherByFunction(int sourceArray[], int destinationArray[], int sizeOfSource, int& sizeOfDestination) {

        for (int i = 0; i < sizeOfSource; ++i) {
            if (!(i < sizeOfDestination)) {
                cout << "  × Copying Process Not completed , source array size is bigger than the destination !!" << endl;
                return;
            }
            addAnElementToArrayByIndex(sourceArray[i], destinationArray, i, sizeOfDestination);
        }

        cout << "  √ Copying Process completed successfully" << endl;
        if (sizeOfSource < sizeOfDestination) {
            cout << " !! The destination array size is bigger that the source, so the size will  modified to match exactly the source size !" << endl;
            sizeOfDestination = sizeOfSource;
        }
    }

    void copyOddNumbersOfArray(int sourceArray[], int destinationArray[], int sizeOfSource, int& sizeOfDestination) {
        int filledSize = 0;

        for (int i = 0; i < sizeOfSource; ++i) {
            if (!(filledSize < sizeOfDestination)) {
                cout << "  × Copying Process Not completed , source array size (odd numbers size) is bigger than the destination !!" << endl;
                return;
            }
            if (isEvenOrOdd(sourceArray[i]) == evenOrOdd::odd) {
                addAnElementToArrayByFilledSize(sourceArray[i], destinationArray, filledSize);
                ++filledSize;
            }
        }

        cout << "  √ Copying Process completed successfully" << endl;
        if (filledSize < sizeOfDestination) {
            cout << " !! The destination array size is bigger the copied numbers, so the size will  modified to match the size of copied odd numbers !" << endl;
            sizeOfDestination = filledSize;
        }
    }

    void copyPrimeNumbersOfArrayByFunction(int sourceArray[], int destinationArray[], int sourceSize, int& destinationFilledSize) {

        for (int i = 0; i < sourceSize; ++i) {
            if (isAPrimeNumber(sourceArray[i]))
                addAnElementToArrayByFilledSize(sourceArray[i], destinationArray, destinationFilledSize);
        }

    }

    void copyDistinctNumbersOfArrayByFunction(int sourceArray[], int destinationArray[], int sourceSize, int& destinationFilledSize) {

        for (int i = 0; i < sourceSize; ++i) {

            //if(countRepeatedTimes(sourceArray[i], destinationArray, destinationFilledSize) == 0)
            if (!isElementInTheArray(sourceArray[i], destinationArray, destinationFilledSize))
                addAnElementToArrayByFilledSize(sourceArray[i], destinationArray, destinationFilledSize);
        }

    }

    bool isItAPalindromicArray(int array[], int size) {
        /*
        // My Solution
        int reversedArray[100];

        copyReversedArrayToAnother(sourceArray, reversedArray, size);

        for(int i = 0; i < size; ++i) {
            if(sourceArray[i] != reversedArray[i])
                return false;
        }
        */
        // Teacher Solution

        // Teacher Solution >>>>>>> My Solution
        for (int i = 0, j = (size - 1); i < size; ++i, --j) {
            if (array[i] != array[j])
                return false;
        }

        return true;
    }

    int countEvenNumbersInArray(int array[], int size) {
        int counter = 0;

        for (int i = 0; i < size; ++i) {
            if (isEvenOrOdd(array[i]) == evenOrOdd::even)
                ++counter;
        }

        return counter;
    }

    int countOddNumbersInArray(int array[], int size) {
        int counter = 0;

        for (int i = 0; i < size; ++i) {
            if (isEvenOrOdd(array[i]) == evenOrOdd::odd)
                ++counter;
        }

        return counter;
    }

    int countPositiveNumbersInArray(int array[], int size) {
        int counter = 0;

        for (int i = 0; i < size; ++i) {
            if (isPositiveOrNegative(array[i]) == positiveNegativeOrNull::positive)
                ++counter;;
        }

        return counter;
    }
    
    int countNegativeNumbersInArray(int array[], int size) {
        int counter = 0;

        for (int i = 0; i < size; ++i) {
            if (isPositiveOrNegative(array[i]) == positiveNegativeOrNull::negative)
                ++counter;;
        }

        return counter;
    }

}