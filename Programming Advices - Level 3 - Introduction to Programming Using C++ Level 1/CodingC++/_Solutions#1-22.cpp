#include<iostream>
#include<cmath>
using namespace std;



//======================================//
//======================================//



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

void printTheCircleAreaBystIsoscelesTriangle(stIsoscelesTriangle triangle) {

    cout << "\n\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;
    cout << "--> The area is: [ " << calculateTheInscribedTriangleCircleArea(triangle) << " ]" << endl;
    cout << "\n|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|\n" << endl;

}

//======================================//
//======================================//



//======================================//
//======================================//



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
        printTheCircleAreaBystIsoscelesTriangle( readIsoscelesTriangleDimensions() );
        cout << "\n|+++++++++++++++++++++++++++++++++++++++++|\n" << endl;

    //-----------------------------------------------------------------------------//
    */

    return 0;
}

    