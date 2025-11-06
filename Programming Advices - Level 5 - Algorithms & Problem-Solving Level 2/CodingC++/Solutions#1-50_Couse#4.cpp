#include<iostream>
#include<cmath>
#include<string.h>

using namespace std;

//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@//
//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@//

void readPositiveNumber(string messageToDisplay,float &number) {

    do {

        cout << messageToDisplay;
        cin >> number;

        if (number <= 0) {

            cout << " !! Invalid negative value: [ " << number << " ]" << endl;
            cout << "  - Try again: \n\n";
        }

    } while (number <= 0);

}

void readPositiveIntegerNumber(string messageToDisplay,int &number) {

    do {

        cout << messageToDisplay;
        cin >> number;

        if (number <= 0) {

            cout << " !! Invalid negative value: [ " << number << " ]" << endl;
            cout << "  - Try again: \n\n";
        }

    } while (number <= 0);

}


void readPositiveIntegerNumberInRange(string messageToDisplay,int &number, int from, int to) {

    do {

        cout << messageToDisplay;
        cin >> number;

        if ( (number < from) || (number > to) ) {

            cout << " !! Invalid value [ " << number << " ] in range (" << from << " - " << to << ")" << endl;
            cout << "  - Try again: \n\n";
        }

    } while ( (number < from) || (number > to) );

}


void printAndShowAvalue(string messageToDisplay) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << messageToDisplay << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
}



//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@//
//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@//




string readUserName() {

    string userName;

    cout << "--> Please enter you full name: ";
    getline(cin, userName);

    return userName;
}

void printUserName(string Name) {
    cout << "--> Your name is: [ " << Name << " ]" << endl;
}


//======================================//
//======================================//


enum enCheckNumberType {
    even,
    odd
};


int readTheNumber() {

    int number;

    cout << "--> Please enter a number: ";
    cin >> number;

    return number;
}

enCheckNumberType CheckNumberType(int number) {

    if(number % 2 == 0) {
        return enCheckNumberType::even;
    }

    else {
        return enCheckNumberType::odd;
    }

}

void PrintNumberType(enCheckNumberType numberType) {

    if(numberType == enCheckNumberType::even) {
        cout << "  - The number you entered is an [even] number." << endl;
    }

    else {
        cout << "  - The number you entered is an [odd] number." << endl;
    }

}


//======================================//
//======================================//


enum enHasDriverLicense {
    no = 1,
    yes = 2
};

struct stCandidateInfos {
    short int Age;
    bool hasDriverLicense;
};

void readCandidateInformations(stCandidateInfos &candidateInfos) {

    short int userAnswer;

    cout << "--> Please enter your age: ";
    cin >> candidateInfos.Age;

    cout << "--> Do you have a driver license ?" << endl;
    cout << " [1] No " << endl;
    cout << " [2] Yes" << endl;

    do {

        cout << "  -- your answer ? ";
        cin >> userAnswer;

        if( (userAnswer != 1) && (userAnswer != 2) )
            cout << "  ! Invalid choice [" << userAnswer << "], try again: \n" << endl;

    } while ( (userAnswer != 1) && (userAnswer != 2) );

    if ( (enHasDriverLicense) userAnswer == enHasDriverLicense::yes)
        candidateInfos.hasDriverLicense = true;
    else
        candidateInfos.hasDriverLicense = false;

}

bool checkInformations(stCandidateInfos Candidate) {
    return ( Candidate.Age >= 21 && Candidate.hasDriverLicense );
}

void printAnswer(bool Accepted) {

    cout << "\n|-----------------------|\n" << endl;
    if (Accepted) {
        cout << ">> ✓ You are [ Hired ] " << endl;
    }
    else {
        cout << ">> × You are [ Rejected ]" << endl;
    }
    cout << "\n|-----------------------|\n" << endl;

}


//======================================//
//======================================//


struct stFullName {
    string firstName;
    string lastName;
};


stFullName readFullName() {

    stFullName userName;

    cout << "--> Please enter your first name: ";
    getline(cin, userName.firstName);

    cout << "--> Please enter your last name : ";
    getline(cin, userName.lastName);

    return userName;
}

string getFullName(stFullName userName) {

    int versionChoice;

    do {

        cout << "\n -> Get a normal or a reversed Name:" << endl;
        cout << "  - [1] Normal version" << endl;
        cout << "  - [0] Reversed version" << endl;
        cout << "  > Your choice ? ";
        cin >>  versionChoice;

        if (versionChoice != 1 && versionChoice != 0) {
            cout << " !! Invalid Choice [" << versionChoice << "]," << endl;
            cout << " -- Try again: \n" << endl;
        }

    } while (versionChoice != 1 && versionChoice != 0);

    if(versionChoice == 1 )
        return ( userName.firstName + " " + userName.lastName );
    else
        return ( userName.lastName + " " + userName.firstName );

}

void printFullName(string fullName) {

    cout << "\n|----------------------------------|\n" << endl;
    cout << " -> Your full name is: [ " << fullName << " ]" << endl;
    cout << "\n|----------------------------------|\n" << endl;

}


//======================================//
//======================================//


float readNumber() {

    float number;

    cout << "--> Please enter a number: ";
    cin >> number;

    return number;
}

float GetTheHalf(float number) {
    return ( number / 2.0 );
}

void printTheHalf(float halfOfTheNumber) {

    cout << "\n|----------------------------|\n" << endl;
    cout << " -> The half is: [ " << halfOfTheNumber << " ]" << endl;
    cout << "\n|----------------------------|\n" << endl;

}


//======================================//
//======================================//


unsigned short readMark() {

    unsigned short Mark;

    do {

        cout << "--> Please enter your mark (0 - 100): ";
        cin >> Mark;

        if( (Mark < 0) || (Mark > 100) ) {
            cout << " !! Invalid Mark [" << Mark << "]" << endl;
            cout << " -- Try again: \n" << endl;
        }

    }  while ( (Mark < 0) || (Mark > 100) );

    return Mark;
}

bool IsPassed(unsigned short Mark) {
    return (Mark >= 50);
}

void printPassOrFail(unsigned short Mark) {

    cout << "\n|----------------------------|\n" << endl;
    if(IsPassed(Mark))
        cout << "  √ Passed [ Your Mark: " << Mark << " ]" << endl;
    else
        cout << "  × Failed [ Your Mark: " << Mark << " ]" << endl;
    cout << "\n|----------------------------|\n" << endl;

}


//======================================//
//======================================//


void readNumbers(int arrayOfNumbers[]) {

    for(int i = 0; i < 3; ++i) {
        cout << " -> please enter number N.[" << i + 1 << "]: ";
        cin >> arrayOfNumbers[i];
    }

}

int calculateTheSumOfNumbers(int arrayOfNumbers[]) {

    int sumOfArrayNumbers = 0;

    for(int i = 0; i < 3 ; ++i) {
        sumOfArrayNumbers += arrayOfNumbers[i];
    }

    return sumOfArrayNumbers;
}

void printTheSumOfNumbers(int sumOfArrayNumbers) {

    cout << "\n|----------------------------|\n" << endl;
    cout << " -> The sum is: [ " << sumOfArrayNumbers << " ]" << endl;
    cout << "\n|----------------------------|\n" << endl;

}


//======================================//
//======================================//


void readMarks(unsigned short int Marks[]) {

    for(int i = 0; i < 3; ++i) {

        do {

            cout << " -> please enter mark N.[" << i + 1 << "] (0 - 100): ";
            cin >> Marks[i];
            if (Marks[i] < 0 || Marks[i] > 100) {
                cout << " !! Invalid Mark [" << Marks[i] << "]" << endl;
                cout << " -- Try again: \n" << endl;
            }

        } while (Marks[i] < 0 || Marks[i] > 100);

    }

}

unsigned short int calculateTheSumOfMarks(unsigned short int Marks[]) {

    unsigned short int sumOfMarks = 0;

    for(int i = 0; i < 3 ; ++i) {
        sumOfMarks += Marks[i];
    }

    return sumOfMarks;
}

float calculateThAverageOfMarks(unsigned short int Marks[]) {

    return ( (float)calculateTheSumOfMarks(Marks) / 3 );
}

bool checkAverage(float average) {
    return (average >= 50);
}

void printThAverageOfMarks(float averageOfMarks) {

    cout << "\n|----------------------------|\n" << endl;
    cout << " -> The average is: [ " << averageOfMarks << " ]" << endl;
    cout << "\n|----------------------------|\n" << endl;

}

void printPassOrFailByAverage(float average) {

    cout << "\n|------------------------------------|\n" << endl;
    if( checkAverage( average ) )
        cout << "  √ Passed [ Your Mark is : " << average << " ]" << endl;
    else
        cout << "  × Failed [ Your Mark is : " << average << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}


//======================================//
//======================================//


void read2Numbers(float &num1, float &num2) {

    cout << "--> please enter the first number: ";
    cin >> num1;
    cout << "--> please enter the second number: ";
    cin >> num2;

}

float maxOf2Numbers(float num1, float num2) {

    if ( num1 > num2 )
        return num1;
    else
        return num2;

}

void printMaxOf2Number(float num1, float num2) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << "--> The maximum number is: [ " << maxOf2Numbers(num1, num2) << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;


}


//======================================//
//======================================//


void read3Numbers(float &num1, float &num2, float &num3) {

    cout << "--> please enter the first number: ";
    cin >> num1;
    cout << "--> please enter the second number: ";
    cin >> num2;
    cout << "--> please enter the third number: ";
    cin >> num3;

}

float maxOf3Numbers(float num1, float num2, float num3) {

    if ( (num1 > num2) && (num1 > num3) )
        return num1;

    else if (num2 > num3)
        return num2;

    else
        return num3;

}

void printMaxOf3Numbers(float maxNumber) {

    cout << "\n|------------------------------------|\n" << endl;
    cout << "--> The maximum number is: [ " << maxNumber << " ]" << endl;
    cout << "\n|------------------------------------|\n" << endl;

}


//======================================//
//======================================//


void printNumbers(float num1, float num2) {

    cout << " -> Number 1 : [ " << num1 <<" ]" << endl;
    cout << " -> Number 2 : [ " << num2 <<" ]" << endl;

}

void swapNumbers(float &num1, float &num2) {

    float tempSwap = num1;
    num1 = num2;
    num2 = tempSwap;

}

void printNumbersBeforeAndAfterSwap(float num1, float num2) {

    cout << "\n|===================================|\n" << endl;
    cout << "\n                + Before + \n" << endl;
    printNumbers(num1, num2);

    swapNumbers(num1, num2);

    cout << "\n                + After +\n" << endl;
    printNumbers(num1, num2);
    cout << "\n|===================================|\n" << endl;

}


//======================================//
//======================================//


struct stRectangle {
    float height;
    float width;
};


stRectangle readRectangleDimensions() {

    stRectangle rectangle;

    cout << "--> Please enter the height value: ";
    cin >> rectangle.height;
    cout << "--> Please enter the width value: ";
    cin >> rectangle.width;

    return rectangle;
}

float  calculateTheRectangleArea(stRectangle rectangle) {
    return (rectangle.height * rectangle.width);
}

void printTheRectangleArea(stRectangle rectangle) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ " << rectangle.height << " * " << rectangle.width << " = " << calculateTheRectangleArea(rectangle) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


struct stRectangleWithDiagonal {
    float height;
    float diagonal;
};

stRectangleWithDiagonal readRectangleDimensionsWithDiagonal() {

    stRectangleWithDiagonal rectangle;

    cout << "--> Please enter the height value: ";
    cin >> rectangle.height;
    cout << "--> Please enter the diagonal value: ";
    cin >> rectangle.diagonal;

    return rectangle;
}

float  calculateTheRectangleAreaByDiagonal(stRectangleWithDiagonal rectangle) {

    float powerExpression = pow(rectangle.diagonal, 2) - pow(rectangle.height, 2);

    return ( rectangle.height * sqrt(powerExpression) );
}

void printTheRectangleAreaByDiagonal(stRectangleWithDiagonal rectangle) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ " << calculateTheRectangleAreaByDiagonal(rectangle) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


struct stTriangle {
    float height;
    float base;
};


stTriangle readTriangleDimensions() {

    stTriangle triangle;

    cout << "--> Please enter the height value: ";
    cin >> triangle.height;
    cout << "--> Please enter the base value: ";
    cin >> triangle.base;

    return triangle;
}

float  calculateTheTriangleArea(stTriangle triangle) {
    return ( (triangle.height * triangle.base) / 2);
}

void printTheTriangleArea(stTriangle triangle) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ (" << triangle.height << " * " << triangle.base << ") / 2 = " << calculateTheTriangleArea(triangle) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


float readCircleDimensions() {

    float radius;

    cout << "--> Please enter the radius value: ";
    cin >> radius;

    return radius;
}

float calculateTheCircleArea(float radius) {

    const float PI = 3.14;

    return ( PI * pow(radius, 2) );
}

void printTheCircleArea(float radius) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ π * " << radius << "² = " << calculateTheCircleArea(radius) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


float readCircleDimensionsByDiameter() {

    float diameter;

    cout << "--> Please enter the diameter value: ";
    cin >> diameter;

    return diameter;
}

float calculateTheCircleAreaByDiameter(float diameter) {

    const float PI = 3.14;

    return ( (PI * pow(diameter, 2)) / 4 );
}

void printTheCircleAreaByDiameter(float diameter) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ (π * " << diameter << "²) / 4 = " << calculateTheCircleAreaByDiameter(diameter) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


float readCircleDimensionsBySquareSide() {

    float squareSide;

    cout << " -> Please enter the square side value: ";
    cin >> squareSide;

    return squareSide;
}

float calculateTheCircleAreaBySquareSide(float squareSide) {

    const float PI = 3.14;

    return ( (PI * pow(squareSide, 2)) / 4 );
}

void printTheCircleAreaBySquareSide(float squareSide) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ (π * " << squareSide << "²) / 4 = " << calculateTheCircleAreaBySquareSide(squareSide) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


float readCircleDimensionsByCircumference() {

    float circleCircumference;

    cout << "--> Please enter the circle circumference: ";
    cin >> circleCircumference;

    return circleCircumference;
}

float calculateTheCircleAreaByCircumference(float circleCircumference) {

    const float PI = 3.14;
    float powerExpression = pow(circleCircumference, 2);

    return ( powerExpression / (4 * PI) );
}

void printTheCircleAreaByCircumference(float circleCircumference) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ " << circleCircumference << "² / (4•π) = " << calculateTheCircleAreaByCircumference(circleCircumference) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


struct stIsoscelesTriangle {
    float side;
    float base;
};

stIsoscelesTriangle readIsoscelesTriangleDimensions() {

    stIsoscelesTriangle triangle;

    cout << "--> Please enter the side value: ";
    cin >> triangle.side;
    cout << "--> Please enter the base value: ";
    cin >> triangle.base;

    return triangle;
}

float calculateTheInscribedTriangleCircleArea(stIsoscelesTriangle triangle) {

    const float PI = 3.14;
    float longExpression = (2 * triangle.side - triangle.base) / (2 * triangle.side + triangle.base);
    float powerExpression = pow(triangle.base, 2) / 4;

    return (PI * powerExpression * longExpression);
}

void printTheCircleAreaByIsoscelesTriangle(stIsoscelesTriangle triangle) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ " << calculateTheInscribedTriangleCircleArea(triangle) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


struct stArbitraryTriangle {
    float sideA;
    float sideB;
    float sideC;
};

stArbitraryTriangle readArbitraryTriangleDimensions() {

    stArbitraryTriangle triangle;

    cout << "--> Please enter the [a] side value: ";
    cin >> triangle.sideA;
    cout << "--> Please enter the [b] side value: ";
    cin >> triangle.sideB;
    cout << "--> Please enter the [c] side value: ";
    cin >> triangle.sideC;

    return triangle;
}

float calculateCircleAreaWithArbitraryTriangle(stArbitraryTriangle triangle) {

    const float PI = 3.14;
    float pValue = (triangle.sideA + triangle.sideB + triangle.sideC) / 2;
    float squareRoot = sqrt( pValue * (pValue - triangle.sideA) * (pValue - triangle.sideB) * (pValue - triangle.sideC) );
    float longExpression = (triangle.sideA * triangle.sideB * triangle.sideC) / (4 * squareRoot);
    float powerExpression = pow(longExpression, 2);

    return (PI * powerExpression);
}

void printTheCircleAreaByArbitraryTriangle(float areaValue) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ " << areaValue << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


unsigned short int readAgeValue() {

    unsigned short int age;

    cout << "--> Please enter your age (18 - 45): ";
    cin >> age;

    return age;
}


unsigned short int validteAgeValue() {

    unsigned short int age;

    do {

        age = readAgeValue();

        if ( (age < 18) || (age > 45) ) {
            cout << " !! Invalid Age [ " << age << " ]" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while ( (age < 18) || (age > 45) ) ;

    return age;
}

void printValidAge(unsigned short int age) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> Congratulations, your age is valid: [ " << age << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


void printHeader() {

    for(int i = 1 ; i <= 10; ++i) {
        cout << " " << i << "  ";
    }

}

void generateTheTable() {

    printHeader();
    cout << "\n|---------------------------------------|\n" << endl;
    for(int i = 1; i <= 10; ++i) {

        cout << " ";
        for(int j = 1; j <= 10; ++j) {

            if ( (j * i) < 10 )
                cout << j * i << "  |";
            else if ( (j * i) < 100 )
                cout << j * i << " |";
            else
                cout << j * i << "|";

        }
        cout << "\n" << endl;

    }
    cout << "\n|---------------------------------------|\n" << endl;

}

void printMultiplicationTable() {
    generateTheTable();
}


//======================================//
//======================================//


int validNumberValueInRange() {

    int number;

    do {

        number = readTheNumber();

        if (number < 1) {
            cout << " !! Wrong number value [ " << number << " ]" << endl;
            cout << "  - Try again with value greater than 1:\n" << endl;
        }

    } while (number < 1) ;
    cout << endl;

    return number;
}

void printNumbersWithFor(int number) {

    cout << "\n|================== For ==================|\n" << endl;
    for(int i = 1; i <= number; ++i) {
        cout << "  - [ " << i << " ]" << endl;
    }

}

void printNumbersWithWhile(int number) {

    int i = 1;

    cout << "\n|================= While =================|\n" << endl;
    while (i <= number) {

        cout << "  - [ " << i << " ]" << endl;
        ++i;

    }

}

void printNumbersWithDoWhile(int number) {

    int i = 1;

    cout << "\n|=============== Do While ================|\n" << endl;

    do {

        cout << "  - [ " << i << " ]" << endl;
        ++i;

    } while (i <= number);

}

void printNumbersFrom1ToN(int number) {

    printNumbersWithFor(number);
    printNumbersWithWhile(number);
    printNumbersWithDoWhile(number);

}


//======================================//
//======================================//


void printFromNto1WithFor(int number) {

    cout << "\n|================== For ==================|\n" << endl;
    for(int i = number; i >= 1; --i) {
        cout << "  - [ " << i << " ]" << endl;
    }

}

void printFromNto1WithWhile(int number) {

    int i = number;

    cout << "\n|================= While =================|\n" << endl;
    while (i >= 1) {

        cout << "  - [ " << i << " ]" << endl;
        --i;

    }

}

void printFromNto1WithDoWhile(int number) {

    int i = number;

    cout << "\n|=============== Do While ================|\n" << endl;

    do {

        cout << "  - [ " << i << " ]" << endl;
        --i;

    } while (i >= 1);

}

void printNumbersFromNto1(int number) {

    printFromNto1WithFor(number);
    printFromNto1WithWhile(number);
    printFromNto1WithDoWhile(number);

}


//======================================//
//======================================//


int printSumOfOddsUsingFor(int number) {

    int sum = 0;

    cout << "\n|================== For \n" << endl;
    for(int i = 1; i <= number; i += 2) {
        sum += i;
    }

    return sum;
}

int printSumOfOddsUsingWhile(int number) {

    int sum = 0;
    int i = 1;

    cout << "\n|================= While \n" << endl;
    while(i <= number) {

        sum += i;
        i += 2;

    }

    return sum;
}

int printSumOfOddsUsingDoWhile(int number) {

    int sum = 0;
    int i = 1;

    cout << "\n|=============== Do While \n" << endl;
    do {

        sum += i;
        i += 2;

    } while(i <= number);

    return sum;
}

void printSumResult(int sumResult) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The sum is: [ " << sumResult << " ]"	<< endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}

void printSumOfOddNumbersFrom1toN(int number) {

    printSumResult( printSumOfOddsUsingFor(number) );
    printSumResult( printSumOfOddsUsingWhile(number) );
    printSumResult( printSumOfOddsUsingDoWhile(number) );

}


//======================================//
//======================================//


int printSumOfEvensUsingFor(int number) {

    int sum = 0;

    cout << "\n|================== For \n" << endl;
    for(int i = 2; i <= number; i += 2) {
        sum += i;
    }

    return sum;
}

int printSumOfEvensUsingWhile(int number) {

    int sum = 0;
    int i = 2;

    cout << "\n|================= While \n" << endl;
    while(i <= number) {

        sum += i;
        i += 2;

    }

    return sum;
}

int printSumOfEvensUsingDoWhile(int number) {

    int sum = 0;
    int i = 2;

    cout << "\n|=============== Do While \n" << endl;
    do {

        sum += i;
        i += 2;

    } while(i <= number);

    return sum;
}

void printSumOfEvenNumbersFrom1toN(int number) {

    if (number == 1) {

        cout << "\n|================= All loops \n" << endl;
        printSumResult(0);

    }

    else {

        printSumResult( printSumOfEvensUsingFor(number) );
        printSumResult( printSumOfEvensUsingWhile(number) );
        printSumResult( printSumOfEvensUsingDoWhile(number) );

    }

}


//======================================//
//======================================//


int readFactorialNumber() {

    int number;
    do {

        cout << "--> please enter a positive number: ";
        cin >> number;

        if (number < 0) {
            cout << " !! Invalid number for factorial [ " << number << " ]" << endl;
            cout << "  - Try again: \n\n" << endl;
        }

    } while (number < 0);

    return number;
}

int calculateTheFactorial(int number) {

    int factorial = 1;

    for(int i = 2; i <= number; ++i) {
        factorial *= i;
    }

    return factorial;
}

void printTheFactorial(int number) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The factorial is: [ " << number << "! = " << calculateTheFactorial(number) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


void printThePower(int number, int power, int result) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> : [ " << number << " ^ " << power << " = " << result << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}

void printPowerOf2_3_4(int number) {

    int powerOf2 = number * number;
    int powerOf3 = powerOf2 * number;
    int powerOf4 = powerOf3 * number;

    printThePower(number, 2, powerOf2);
    printThePower(number, 3, powerOf3);
    printThePower(number, 4, powerOf4);

}


//======================================//
//======================================//


void readTheBase(float &base) {

    cout << "--> please enter the base: ";
    cin >> base;

}

void readThepower(int &exponent) {

    cout << "--> please enter the exponent: ";
    cin >> exponent;

}

float calculateThePower(float base, int exponent) {

    float result = 1;

    if ( (base == 1) || (exponent == 0) )
        return result;

    else if (base == 0) {

        if (exponent > 0)
            return 0;
        else
            return (result / 0);
    }

    else if (exponent < 0) {

        exponent *= (-1);
        result = base;

        for( int i = 2; i <= exponent; ++i) {
            result *= base;
        }

        return (1 / result);
    }

    else {

        result = base;
        for( int i = 2; i <= exponent; ++i) {
            result *= base;
        }
    }

    return result;
}

void printThePowerResult(float base, int exponent) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The result is: [ " << base << " ^ " << exponent << " = " << calculateThePower(base, exponent) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


int readStudentGrade() {

    int studentGrade;

    do {

        cout << "--> Please enter your grade (0 - 100): ";
        cin >> studentGrade;

        if ( (studentGrade < 0) || (studentGrade > 100) ) {

            cout << " !! Invalid grade value [ " << studentGrade << " ]" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while ( (studentGrade < 0) || (studentGrade > 100) );

    return studentGrade;
}

char getGradeValue(int studentGrade) {

    if (studentGrade >= 90)
        return 'A';

    else if (studentGrade >= 80)
        return 'B';

    else if (studentGrade >= 70)
        return 'C';

    else if (studentGrade >= 60)
        return 'D';

    else if (studentGrade >= 50)
        return 'E';

    else
        return 'F';

}

void printStudentGrade(int studentGrade) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> Your grade is: [ " << studentGrade << " --> " << getGradeValue(studentGrade) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


int readTheTotalOfSales() {

    int totalSales;

    do {

        cout << "--> Please enter your total seals: ";
        cin >> totalSales;

        if (totalSales < 0) {

            cout << " !! Invalid negative value for sales [ " << totalSales << " ]" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while (totalSales < 0);

    return totalSales;
}

float getcommissionPercentage(int totalSales) {

    cout << "\n" << endl;
    if(totalSales >= 1000000) {

        cout << "  - The commission percentage is: 0.01" << endl;
        return 0.01;
    }

    else if (totalSales >= 500000) {

        cout << "  - The commission percentage is: 0.02" << endl;
        return 0.02;
    }

    else if (totalSales >= 100000) {

        cout << "  - The commission percentage is: 0.03" << endl;
        return 0.03;
    }

    else if (totalSales >= 50000) {

        cout << "  - The commission percentage is: 0.05" << endl;
        return 0.05;
    }

    else {

        cout << "  - The commission percentage is: 0" << endl;
        return 0;
    }

}

float calculateTheTotalCommission(int totalSales, float commissionPercentage) {

    return (totalSales * commissionPercentage);
}

void printTotalCommission(int totalSales) {

    float commissionPercentage = getcommissionPercentage(totalSales);

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> Your total commission: [ " << calculateTheTotalCommission(totalSales, commissionPercentage) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


struct stPiggyBank {

    int pennies, nickels, dimes, quarters, dollars;
};

void validtPenniesValue(int &pennies) {

    do {

        cin >> pennies;

        if (pennies < 0) {

            cout << " !! Invalid negative value: [ " << pennies << " ] \n" << endl;
            cout << "  - Try again: " ;
        }

    } while (pennies < 0);

}

stPiggyBank readPiggyPennies() {

    stPiggyBank piggyBank;

    cout << "--> Please enter the number of: \n";

    cout << "  - Pennies : ";
    validtPenniesValue(piggyBank.pennies);

    cout << "  - Nickels : ";
    validtPenniesValue(piggyBank.nickels);

    cout << "  - Dimes   : ";
    validtPenniesValue(piggyBank.dimes);

    cout << "  - Quarters: ";
    validtPenniesValue(piggyBank.quarters);

    cout << "  - Dollars : ";
    validtPenniesValue(piggyBank.dollars);

    return piggyBank;
}

int calculateTheTotalNumberOfPennies(stPiggyBank piggyBank) {

    return ( piggyBank.pennies + (piggyBank.nickels * 5) + (piggyBank.dimes * 10) + (piggyBank.quarters * 25) + (piggyBank.dollars * 100) );
}

void printTheTotalNumberOfPennies(int totalOfPennies) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The total of pennies: [ " << totalOfPennies << " ]" << endl;
    cout << "--> The total of dollars: [ " << ( (float) totalOfPennies / 100 ) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


enum operations { Sum = 1, Subtraction, Multiplication, Division };

struct stoperationMembers {

    float number1, number2;
    short int operation;
};


short int showOperationMenu() {

    short int operation;

    cout << "--> Please choose the operation to perform:" << endl;
    cout << "\n  --------------------\n" << endl;
    cout << "  - [1] Sum"		    << endl;
    cout << "  - [2] Subtraction"    << endl;
    cout << "  - [3] Multiplication" << endl;
    cout << "  - [4] Division" 	  << endl;
    cout << "\n  --------------------\n" << endl;

    do {

        cout << " -- Your choice ? ";
        cin >> operation;

        if ( (operation < 1) || (operation > 4) ) {

            cout << " !! Invalid operation number: [ " << operation << " ]" << endl;
            cout << "  - Try again: \n" << endl;
        }

    } while ( (operation < 1) || (operation > 4) );

    return operation;
}

stoperationMembers readOperationMembers() {

    stoperationMembers operationMembers;

    cout << "--> please enter the first number: ";
    cin >> operationMembers.number1;
    cout << "--> please enter the second number: ";
    cin >> operationMembers.number2;

    operationMembers.operation = showOperationMenu();

    return operationMembers;
}

float performTheOperation(stoperationMembers operationMembers) {

    switch ( (operations) operationMembers.operation ) {

    case operations::Sum :
        return ( operationMembers.number1 + operationMembers.number2 );

    case operations::Subtraction :
        return ( operationMembers.number1 - operationMembers.number2 );

    case operations::Multiplication :
        return ( operationMembers.number1 * operationMembers.number2 );

    case operations::Division :
        return ( operationMembers.number1 / operationMembers.number2 );

    }

}

void printOperationResult(float operationResult) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The result is: [ " << operationResult << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


float performSumUntilaNumber() {

    float number, counter = 1, sumOfEnteredNumbers = 0;

    do {

        cout << " -> Please enter number [" << counter << "]: ";
        cin >> number;

        if (number == -99) {

            cout << "\n !! You entered: [ " << number << " ]" << endl;
            cout << "  - The number will not be added to the sum, and the reading process willstop: \n" << endl;
            break;
        }

        sumOfEnteredNumbers += number;
        ++counter;

    } while ( number != -99);

    return sumOfEnteredNumbers;
}

void printTheSumUntilaNumber(float sumOfEnteredNumbers) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The sum is: [ " << sumOfEnteredNumbers << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


bool isPrimeNumber(int number) {

    int halfOfNumber = (number / 2);



    // The largest and slowest method
    /*  int divisible = 0;
        for(int divisor = 1; divisor <= number; ++divisor) {

            if(number % divisor == 0)
                ++divisible;

        }

        return (divisible >= 2);
    */

    // The shortest and fastest method
    for(int divisor = 2; divisor <= halfOfNumber; ++divisor) {

        if(number % divisor == 0)
            return false;

    }

    return true;
}

void printPrimeNumber(int number) {

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

    if( isPrimeNumber(number) )
        cout << "  √ [ " << number << " ] is a prime number." << endl;
    else
        cout << "  x [ " << number << " ] isn't a prime number." << endl;

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


void readTheTotalPaid(float &totalBill, float &cashPaid) {

    readPositiveNumber("--> Please enter the total bill: ", totalBill);
    readPositiveNumber("--> Please enter the cash paid : ", cashPaid);

}

float calculateTheRemainder(float totalBill, float cashPaid) {

    return (cashPaid - totalBill);
}

void printTheRemainder(float totalBill, float cashPaid) {

    float remainder = calculateTheRemainder(totalBill, cashPaid);

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The remainder is: [ " << cashPaid << " - " << totalBill << " = " << remainder << " $ ]" << endl;

    if(remainder > 0)
        cout << "  √ The client paid the total bill,\n  Back him the remainder [ " << remainder << " $ ]" << endl;

    else if (remainder == 0)
        cout << "  √ The client paid the total bill" << endl;

    else
        cout << "  x The client needs to pay [ " << -remainder << " $ ]" << endl;

    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}


//======================================//
//======================================//


float calculateServiceFeeAndSalesTax(float theBillValue) {

    float theServiceFee = theBillValue * 1.1;
    float theSalesTax = theServiceFee * 1.16;

    return theSalesTax;
}


//======================================//
//======================================//


void convertHoursToDaysAndWeeks(float numberOfHours, float &numberOfDays, float &numberOfWeeks ) {

    numberOfDays = (float) numberOfHours / 24;
    numberOfWeeks = (float) numberOfDays / 7;

}


//======================================//
//======================================//


struct stTime {
    int days, hours, minutes, seconds;
};

stTime readTheTaskDuration() {

    stTime time;

    cout << "--> Please enter the number of:" << endl;
    readPositiveIntegerNumber(" -- Days    : ", time.days);
    readPositiveIntegerNumber(" -- Hours   : ", time.hours);
    readPositiveIntegerNumber(" -- Minutes : ", time.minutes);
    readPositiveIntegerNumber(" -- Seconds : ", time.seconds);

    return time;
}

int ConvertAllToSeconds(stTime time) {

    int minutesToSeconds = 60;
    int hoursToSeconds = 60 * minutesToSeconds;
    int daysToSeconds = 24 * hoursToSeconds;

    return ( (time.days * daysToSeconds) + (time.hours * hoursToSeconds) + (time.minutes * minutesToSeconds) + time.seconds );
}


//======================================//
//======================================//


stTime convertTaskDurationToDHMS(int taskDurationInSeconds) {

    stTime time;
    const int secondsPerDay = 24 * 60 * 60;
    const int secondsPerHour = 60 * 60;
    const int secondsPerMinute = 60;

    time.days = taskDurationInSeconds / secondsPerDay;
    taskDurationInSeconds %= secondsPerDay;

    time.hours = taskDurationInSeconds / secondsPerHour;
    taskDurationInSeconds %= secondsPerHour;

    time.minutes = taskDurationInSeconds / secondsPerMinute;
    taskDurationInSeconds %= secondsPerMinute;

    time.seconds = taskDurationInSeconds;

    return time;
}


//======================================//
//======================================//


enum enDays { Monday = 1, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };

string getTheDayOfWeek(int theDayNumber) {

    switch ( (enDays) theDayNumber ) {

    case enDays::Monday:
        return "Monday";
    case enDays::Tuesday:
        return "Tuesday";
    case enDays::Wednesday:
        return "Wednesday";
    case enDays::Thursday:
        return "Thursday";
    case enDays::Friday:
        return "Friday";
    case enDays::Saturday:
        return "Saturday";
    case enDays::Sunday:
        return "Sunday";

    }

}


enum enMonths { January = 1, February, March, April, Mai, June, July, August, September, October, November, December };

string getTheMonthName(int theMonthNumber) {

    switch ( (enMonths) theMonthNumber ) {

    case enMonths::January:
        return "January";
    case enMonths::February:
        return "February";
    case enMonths::March:
        return "March";
    case enMonths::April:
        return "April";
    case enMonths::Mai:
        return "Mai";
    case enMonths::June:
        return "June";
    case enMonths::July:
        return "July";
    case enMonths::August:
        return "August";
    case enMonths::September:
        return "September";
    case enMonths::October:
        return "October";
    case enMonths::November:
        return "November";
    case enMonths::December:
        return "December";

    }

}


//======================================//
//======================================//


void printEnglishLettersFromAtoZ() {

    cout << "\n  ===========================>\n" << endl;

    for(char character = 'A' ; character <= 'Z'; ++character) {

        cout << "  - Character N.[" << (int) character << "] --> [ " << character << " ]" << endl;
    }

    cout << "\n  ===========================>\n" << endl;

}


//======================================//
//======================================//


struct stLoanComponents {

    float loanAmount, monthlyPayment;
    int monthsForPayment;

};

void readloanComponents(stLoanComponents &loanComponents) {

    readPositiveNumber( "--> Please enter the loan amount: ", loanComponents.loanAmount);
    readPositiveNumber( "--> Please value of monthly payment you want: ", loanComponents.monthlyPayment);

}

void calculateMonthsNeededForPayment(stLoanComponents &loanComponents) {
    loanComponents.monthsForPayment = (int) (loanComponents.loanAmount / loanComponents.monthlyPayment);
}

void calculateTheMonthlyPayment(stLoanComponents &loanComponents) {
    loanComponents.monthlyPayment = (int) (loanComponents.loanAmount / loanComponents.monthsForPayment);
}


//======================================//
//======================================//


void readUserATM_PIN(int userATM_Code, int inputATM_PIN) {

    do {

        readPositiveIntegerNumber( "--> Please enter you ATM PIN: ", inputATM_PIN);

        if(inputATM_PIN != userATM_Code)
            cout << " !! Wrong PIN, try again: \n" << endl;

    } while (inputATM_PIN != userATM_Code);

}


void readATM_PIN_WithChances(int inputATM_PIN, int userATM_Code, bool &isSuccessfullyAccessed) {
    /*
        int wrongPIN = 0;

        do {

            readPositiveIntegerNumber( "--> Please enter you ATM PIN: ", inputATM_PIN);

            if(inputATM_PIN != userATM_Code) {

                ++wrongPIN;
                cout << " !! Wrong PIN !!" << endl;

                if (wrongPIN < 3)
                    cout << " -- Try again [ " << 3 - wrongPIN << " chances left ] :\n" << endl;

                else {
                    cout << " ⛔ Card locked, no more changes left" << endl;
                    isSuccessfullyAccessed = false;
                    break;
                }

            }

            else {
                isSuccessfullyAccessed = true;
                break;
            }

        } while (inputATM_PIN != userATM_Code);
    */

    int leftChances = 3;

    do {

        readPositiveIntegerNumber( "--> Please enter you ATM PIN: ", inputATM_PIN);

        if(inputATM_PIN != userATM_Code) {

            --leftChances;
            cout << " !! Wrong PIN !!" << endl;

            if (leftChances > 0)
                cout << " -- Try again [ " << leftChances << " chances left ] :\n" << endl;

            else {
                cout << " ⛔ Card locked, no more changes left" << endl;
                isSuccessfullyAccessed = false;
                break;
            }

        }

        else {
            isSuccessfullyAccessed = true;
            break;
        }

    } while (inputATM_PIN != userATM_Code);


}




int main() {
    /*

    //-----------------------------------------------------------------------------//

        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printUserName("Haydra Arex");
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n--------| Get And Print User Name |--------\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printUserName(readUserName());
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n ----| Check if Number Is Odd Or Even |----\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        PrintNumberType(CheckNumberType(readTheNumber()));
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        stCandidateInfos candidateInfos;

        cout << "\n --------| Check if Hired Or Not |--------\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readCandidateInformations(candidateInfos);
        printAnswer( checkInformations(candidateInfos) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n+---------| Get Your Full Name |---------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printFullName( getFullName( readFullName() ) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n+--------| Get Half Of A Number |--------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheHalf( GetTheHalf( readNumber() ) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n +-----------| Pass Or Fail  |-----------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printPassOrFail(readMark());
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        int arrayOfNumbers[3];

        cout << "\n+----------| Sum Of 3 Numbers  |----------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readNumbers(arrayOfNumbers);
        printTheSumOfNumbers( calculateTheSumOfNumbers(arrayOfNumbers) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        unsigned short int Marks[3];

        cout << "\n+-------| The Average Of 3 Marks  |-------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readMarks(Marks);
        printThAverageOfMarks( calculateThAverageOfMarks(Marks) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        unsigned short int Marks[3];

        cout << "\n+ Pass Or Fail Accordingly To The Average +\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readMarks(Marks);
        printPassOrFailByAverage( calculateThAverageOfMarks(Marks) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n +---------- Max Of Two Numbers ---------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        float num1, num2;

        read2Numbers(num1, num2);
        printMaxOf2Number(num1, num2);
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n+---------- Max Of Three Numbers ---------+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        float num1, num2, num3;

        read3Numbers(num1, num2, num3);
        printMaxOf3Numbers( maxOf3Numbers(num1, num2, num3) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+------------- Swap Numbers ------------+|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        float num1, num2;

        read2Numbers(num1, num2);
        printNumbersBeforeAndAfterSwap(num1, num2);
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+---------- Get Rectangle Area ---------+|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheRectangleArea( readRectangleDimensions() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+--- Get Rectangle Area By Diagonal  ---+|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheRectangleAreaByDiagonal( readRectangleDimensionsWithDiagonal() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+---------- Get Triangle Area ----------+|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheTriangleArea( readTriangleDimensions() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+----------- Get Circle Area -----------+|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheCircleArea( readCircleDimensions() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+----- Get Circle Area By Diameter -----+|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheCircleAreaByDiameter( readCircleDimensionsByDiameter() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+ Area Of Circle Inscribed In A Square  +|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheCircleAreaBySquareSide( readCircleDimensionsBySquareSide() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|-+ Get Circle Area Using Circumference +-|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheCircleAreaByCircumference( readCircleDimensionsByCircumference() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n+Circle Inscribed In An Isosceles Triangle+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheCircleAreaByIsoscelesTriangle( readIsoscelesTriangleDimensions() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n+Circle Inscribed In An Arbitrary Triangle+\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheCircleAreaByArbitraryTriangle( calculateCircleAreaWithArbitraryTriangle( readArbitraryTriangleDimensions() ) );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n------+ Read Age Between 18 And 45 +------\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printValidAge( validteAgeValue() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|+ Get Multiplication Table From 1 to 10 +|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        generateTheTable();
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|--+ Get Numbers Sequence From 1 To N  +--|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printNumbersFrom1ToN( validNumberValueInRange() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|--+ Get Numbers Sequence From N To 1  +--|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printNumbersFromNto1( validNumberValueInRange() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|-+ Get Sum Of Even Numbers From 1 To N +-|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printSumOfEvenNumbersFrom1toN( validNumberValueInRange() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|----+ Get The Factorial Of A Number +----|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheFactorial( readFactorialNumber() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|-+ Get Power Of 2 3 And 4 Of A Number  +-|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printPowerOf2_3_4( readTheNumber() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|------+ Get The Power Of A Number +------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        float base;
        int exponent;

        readTheBase(base);
        readThepower(exponent);
        printThePowerResult(base, exponent);
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|----+ Get Your Grade [ A B C D E F] +----|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printStudentGrade( readStudentGrade() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|--+ Get The Total Commission Of Sales +--|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTotalCommission( readTheTotalOfSales() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;


        cout << "\n|---+ Get The Total Number Of Pennies +---|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheTotalNumberOfPennies(calculateTheTotalNumberOfPennies( readPiggyPennies() ));
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|----------+ Simple Calculator +----------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printOperationResult(performTheOperation( readOperationMembers() ));
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|------+ Perform The Sum Until -99 +------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printTheSumUntilaNumber( performSumUntilaNumber() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|------------+ Prime Numbers +------------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printPrimeNumber( readTheNumber() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|-----+ The Total Bill & Cash Paid +-----|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        float totalBill, cashPaid;

        readTheTotalPaid(totalBill, cashPaid);
        printTheRemainder(totalBill, cashPaid);
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|------+ Service Fee And Sales Tax +------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        float theBillValue, theTotalBill;

        readPositiveNumber("--> Please enter the bill value: ", theBillValue);
        theTotalBill = calculateServiceFeeAndSalesTax(theBillValue);

        printAndShowAvalue("--> The total bill is: [ " + to_string(theTotalBill) + " ]");
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|-------+ Hours To Days And Weeks +-------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        int numberOfHours;
        float numberOfDays, numberOfWeeks;

        readPositiveIntegerNumber( "--> Please enter the number of hours: ", numberOfHours);
        convertHoursToDaysAndWeeks(numberOfHours, numberOfDays, numberOfWeeks);

        printAndShowAvalue( " -> Hours to days [ " + to_string(numberOfHours) + "  --> " + to_string(numberOfDays) + " ]" );
        printAndShowAvalue( " -> Hours to weeks [ " + to_string(numberOfHours) + " --> " + to_string(numberOfWeeks) + " ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        int totalOfSeconds;

        cout << "\n|---+ Get The Task Duration In seconds +--|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        totalOfSeconds = ConvertAllToSeconds( readTheTaskDuration() );

        printAndShowAvalue( "--> The task duration in seconds: [ " + to_string(totalOfSeconds) + " ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        stTime time;
        int taskDurationInSeconds;

        cout << "\n|---+ The Task Duration In [ D H M S ] +--|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readPositiveIntegerNumber( " -> Please enter the total task duration in seconds: ", taskDurationInSeconds );
        time = convertTaskDurationToDHMS(taskDurationInSeconds);

        printAndShowAvalue( "--> The task duration is: [ " + to_string(time.days) + "D: " + to_string(time.hours) + "H: " + to_string(time.minutes) + "M: " + to_string(time.seconds) +  "S ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        int theDayNumber;
        string theDayName;

        cout << "\n|------------+ Days Of Week +-------------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readPositiveIntegerNumberInRange( "--> Please enter the number of day (1 - 7): ",theDayNumber, 1, 7);

        theDayName = getTheDayOfWeek(theDayNumber);

        printAndShowAvalue( " -> Day N.[" + to_string(theDayNumber) + "] is: [ " + theDayName + " ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        int theMonthNumber;
        string theMonthName;


        cout << "\n|------------+ Days Of Week +-------------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readPositiveIntegerNumberInRange( "--> Please enter the number of month (1 - 12): ",theMonthNumber, 1, 12);

        theMonthName = getTheMonthName(theMonthNumber);
        printAndShowAvalue( " -> Month N.[" + to_string(theMonthNumber) + "] is: [ " + theMonthName + " ]" );

        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        cout << "\n|---+ Get English Letters From A To Z +---|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        printEnglishLettersFromAtoZ();
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        stLoanComponents loanComponents;

        cout << "\n|-----+ Loan Installment Months V.1 +-----|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readloanComponents(loanComponents);
        calculateMonthsNeededForPayment(loanComponents);
        printAndShowAvalue( "--> The number of months you need to pay the loan amount is [ " + to_string(loanComponents.monthsForPayment) + " ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        stLoanComponents loanComponents;

        cout << "\n|-----+ Loan Installment Months V.2 +-----|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readPositiveNumber( "--> Please enter the loan amount: ", loanComponents.loanAmount);
        readPositiveIntegerNumber( "--> Please enter the number of months you need the loan amount: ", loanComponents.monthsForPayment);
        calculateTheMonthlyPayment(loanComponents);
        printAndShowAvalue( "--> The monthly payment will be: [ " + to_string(loanComponents.monthlyPayment) + " $ ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//

        int userATM_Code = 1234, inputATM_PIN;
        float userBalance = 7282.27;

        cout << "\n|-------------+ ATM PIN Code +------------|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readUserATM_PIN(userATM_Code, inputATM_PIN);
        printAndShowAvalue( " -> √ Correct PIN, your balance is: [ " + to_string(userBalance) + " $ ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
    //-----------------------------------------------------------------------------//

        int userATM_Code = 1234, inputATM_PIN;
        bool isSuccessfullyAccessed;
        float userBalance = 7282.27;

        cout << "\n|-----+ ATM PIN Code With 3 Chances +-----|\n" << endl;
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;
        readATM_PIN_WithChances(inputATM_PIN, userATM_Code, isSuccessfullyAccessed);

        if (isSuccessfullyAccessed)
            printAndShowAvalue( " -> √ Correct PIN, your balance is: [ " + to_string(userBalance) + " $ ]" );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//
    */

    return 0;
}

