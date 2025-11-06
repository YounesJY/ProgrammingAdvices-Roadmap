#include<iostream>
#include<cmath>

using namespace std;

void PrintMyName() {

    cout << "\n===========================================\n" << endl;
    cout << "------------------+ JY +-------------------" << endl;
    cout << "\n===========================================\n" << endl;

}

void PrintUserName() {

    string userName;

    cout << "-----------+ Print User Name +-------------" << endl;
    cout << "\n===========================================\n" << endl;
    cout << "--> Please enter your name: ";
    getline(cin, userName);
    cout << " => Hi [ " << userName << " ] :-)" << endl;
    cout << "\n===========================================\n" << endl;

}

void SwapNumbers() {

    int num1, num2, tempSwap;

    cout << "-------------+ Swap Numbers +--------------" << endl;
    cout << "\n===========================================\n" << endl;
    cout << "--> Please enter the first number: ";
    cin >> num1;
    cout << "--> Please enter the first number: ";
    cin >> num2;

    cout << "\n                + Before + \n" << endl;
    cout << " => The first number : [ " << num1 << " ]" << endl;
    cout << " => The second number: [ " << num2 << " ]" << endl;

    tempSwap = num1;
    num1 = num2;
    num2 = tempSwap;

    cout << "\n                + After + \n" << endl;
    cout << " => The first number : [ " << num1 << " ]" << endl;
    cout << " => The second number: [ " << num2 << " ]" << endl;
    cout << "\n===========================================\n" << endl;


}

int GetRectangleArea(unsigned int Height, unsigned int Width) {
    return (Height * Width);
}

float RectangleAreaByDiagonal(unsigned int Height, unsigned int Diagonal) {
    return ( Height * sqrt(pow(Diagonal, 2) - pow(Height, 2)) );
}

float GetCircleArea(unsigned int rValue) {
    const float PI = 3.14;
    return (PI * pow(rValue, 2));
}

float CircleAreaByDiameter(unsigned int Diameter) {
    const float PI = 3.14;
    return ( (PI * pow(Diameter, 2)) / 4 );
}

float CircleAroundedBySquare(unsigned int aValue) {
    const float PI = 3.14;
    return (PI * pow((aValue / 2), 2));
    //return ( (PI * pow(aValue, 2)) / 4 );

}

float CircleAreaByCircumference(unsigned int circumferenceValue) {
    const float PI = 3.14;
    return ( pow(circumferenceValue, 2) / (4 * PI) );
}

float AreaOfCircleIntoTriangle(unsigned int aValue, unsigned int bValue) {
    const float PI = 3.14;
    float longExpression =   ( (float) (2 * aValue - bValue) / (2 * aValue + bValue));
    return  (PI * ( pow(bValue, 2) / 4 ) * longExpression) ;

}

float AreaOfCircleIntoTriangle2(unsigned int aValue,  unsigned int bValue, unsigned int cValue) {
    const float PI = 3.14;
    float pValue = (aValue + bValue+ cValue) / 2;
    float SquaretRt = sqrt( pValue * (pValue - aValue) * (pValue - bValue) * (pValue - cValue) );
    float longExpression =   ( (aValue * bValue * cValue) / ( 4 * SquaretRt) );
    return  (PI * pow(longExpression, 2)) ;
}

float Power(float number, int power) {
    return pow(number, power);
}

int ConvertToSeconds(unsigned int Days, unsigned int Hours,unsigned int Minutes, unsigned int Seconds) {
    return ( Seconds + (Minutes * 60) + (Hours * 60 * 60) + (Days * 24 * 3600) );
}

void SecondsConversion(unsigned int Seconds) {
    unsigned int Days, Hours, Minutes;

    Days = Seconds / (24 * 3600);
    Seconds -= (Days * 24 * 3600);

    Hours = Seconds / (60 * 60);
    Seconds -= (Hours * 60 * 60);

    Minutes = Seconds / 60;
    Seconds -= (Minutes * 60);

    cout << "\n => [ " << Days << "D:" << Hours << "H:" << Minutes << "M:" << Seconds << "S ]" << endl;
}


int main()
{
    //// Problems Lesson #35
    /*
        // Problem #1 Print your name on screen
        PrintMyName();

	//-----------------------------------------------------------------------------//

        // Problem #2 Print user name on screen
        PrintUserName();

		//-----------------------------------------------------------------------------//
                
    	// Problem #14 Swap numbers
        SwapNumbers();

	//-----------------------------------------------------------------------------//

        // Problem #15 Rectangle  area
        unsigned int Height, Width;

        cout << "------------+ Rectangle Area +-------------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the height: ";
        cin >> Height;
        cout << "--> Please enter the width: ";
        cin >> Width;
        cout << "\n => The rectangle area is : [ " << GetRectangleArea(Height, Width)<< " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

        // Problem #16 Rectangle area through diagonal
        unsigned int Height, Diagonal;

        cout << "----+ Rectangle Area  With Diagonal +----" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the height: ";
        cin >> Height;
        cout << "--> Please enter the diagonal: ";
        cin >> Diagonal;
        cout << "\n => The rectangle area is : [ " << RectangleAreaByDiagonal(Height, Diagonal) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #18 Circle area
        unsigned int rValue;

    	cout << "--------------+ Circle Area +--------------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the [r] Value: ";
        cin >> rValue;
        cout << "\n => The Circle area is : [ " << GetCircleArea(rValue) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #19 Circle area through diameter
        unsigned int Diameter;

    	cout << "-------+ Circle Area With Diameter +-------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the [diameter] Value: ";
        cin >> Diameter;
        cout << "\n => The Circle area is : [ " << CircleAreaByDiameter(Diameter) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #20 Circle area inscribed in a square
        unsigned int aValue;

    	cout << "-+ Area Of Circle Inscribed In A Square +-" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the [A] Value: ";
        cin >> aValue;
        cout << "\n => The Circle area is : [ " << CircleAroundedBySquare(aValue) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #21 Circle area through circumference
        unsigned int circumferenceValue;

    	cout << "-+ Area Of Circle Inscribed In A Square +-" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the [Circumference] Value: ";
        cin >> circumferenceValue;
        cout << "\n => The Circle area is : [ " << CircleAreaByCircumference(circumferenceValue) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #22 Area of circle inscribed in a isosceles triangle
        unsigned int aValue, bValue;

        cout << "+ Area Of Circle Inscribed In A Triangle +" << endl;
        cout << "\n===========================================\n" << endl;
    	cout << "--> Please enter [a] Value: ";
        cin >> aValue;
        cout << "--> Please enter [b] Value: ";
        cin >> bValue;
        cout << "\n => The Circle area is : [ " << AreaOfCircleIntoTriangle(aValue, bValue) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #23 Area of circle inscribed in an arbitrary triangle
        unsigned int aValue, bValue, cValue;

        cout << "+ Area Of Circle Inscribed In A Triangle +" << endl;
        cout << "\n===========================================\n" << endl;
    	cout << "--> Please enter [a] Value: ";
        cin >> aValue;
        cout << "--> Please enter [b] Value: ";
        cin >> bValue;
        cout << "--> Please enter [c] Value: ";
        cin >> cValue;
        cout << "\n => The Circle area is : [ " << AreaOfCircleIntoTriangle2(aValue, bValue, cValue) << " ]" << endl;
        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #31 Get power of 2 3 4
    	float number;
        int power;

        cout << "---------+ Power Function +----------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter a number: ";
        cin >> number;

        cout << " +> [ " << number << " ^ 2 ] = " << Power(number, 2) << endl;
        cout << " +> [ " << number << " ^ 2 ] = " << Power(number, 3) << endl;
        cout << " +> [ " << number << " ^ 2 ] = " << Power(number, 4) << endl;

        cout << "\n===========================================\n" << endl;

	//-----------------------------------------------------------------------------//

    	// Problem #32 Get power of M
    	float number;
        int power;

        cout << "---------+ Power Function +----------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter a number: ";
        cin >> number;
        cout << "--> Please enter the [power] value: ";
        cin >> power;

        cout << " +> [ " << number << " ^ " << power << " ] = " << Power(number, power) << endl;
    
	//-----------------------------------------------------------------------------//

    	// Problem #43 Convert seconds to [ D, H , M , S]
    	unsigned int numOfDays, numOfHours, numOfMinutes, numOfSeconds;

        cout << "-----------+ Convert to Seconds +----------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the total number of: " << endl;
        cout << "  - Days: ";
        cin >> numOfDays;
        cout << "  - Hours: ";
        cin >> numOfHours;
        cout << "  - Minutes: ";
        cin >> numOfMinutes;
        cout << "  - Seconds: ";
        cin >> numOfSeconds;

        cout << "\n => The total of seconds is: [ " <<  ConvertToSeconds(numOfDays, numOfHours, numOfMinutes, numOfSeconds) << " ]" << endl;
        cout << "\n===========================================\n" << endl;


	//-----------------------------------------------------------------------------//

    	// Problem #43 Convert seconds to [ D, H , M , S]
    	unsigned int totalSeconds;

        cout << "---------+ Convert Seconds +---------" << endl;
        cout << "\n===========================================\n" << endl;
        cout << "--> Please enter the total number of seconds: ";
        cin >> totalSeconds;
        SecondsConversion(totalSeconds);
        cout << "\n===========================================\n" << endl;

    */
    return 0;
}




