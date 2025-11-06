#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    //// --> Level 3 Lesson #26
    /*
        // --> Problem #16 Rectangle area through diagonal
        unsigned int Height, Diagonal;

        cout << "--+ Get Rectangle Area Through Diagonal +--" << endl;

        cout << "\n$=========================================$\n" << endl;
        cout << "--> Please enter the height value: ";
        cin >> Height;
        cout << "--> Please enter the diagonal value: ";
    	cin >> Diagonal;
        cout << "\n -> The rectangle area is: " << Height * sqrt(pow(Diagonal, 2) - pow(Height, 2)) << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #18 Circle area
        const float PI = 3.14;
        unsigned int rValue;

    	cout << "------------+ Get Circle Area +------------" << endl;

        cout << "\n$=========================================$\n" << endl;
        cout << "--> Please enter the value of r: ";
        cin >> rValue;
        cout << "\n -> The circle area is: " << ceil(PI * pow(rValue, 2)) << " (exactly [ "  << PI * pow(rValue, 2) << " ])" << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #19 Circle area through diameter
        const float PI = 3.14;
        unsigned int diameterValue;

        cout << "---+ Get Circle Area through Diameter +---" << endl;


        cout << "\n$=========================================$\n" << endl;
    	cout << "--> Please enter the diameter value: ";
    	cin >> diameterValue;
        cout << "\n -> The circle area is: " << ceil((PI * pow(diameterValue, 2)) / 4 ) << " (exactly [ "  << (PI * pow(diameterValue, 2)) / 4 << " ])" << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #20 Circle inscribed in a square
        const float PI = 3.14;
        unsigned int ValueOfA;

        cout << "-+ Area Of Circle Inscribed In A Square +-" << endl;


        cout << "\n$=========================================$\n" << endl;
    	cout << "--> Please enter the value of A: ";
    	cin >> ValueOfA;
        cout << "\n => The circle area is: " << ceil((PI * pow(ValueOfA, 2)) / 4 ) << " (exactly [ "  << (PI * pow(ValueOfA, 2)) / 4 << " ])" << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #21 Circle area through circumference
    	const float PI = 3.14;
        unsigned int circumferenceValue;

        cout << "-+ Get Circle Area through Circumference +-" << endl;

        cout << "\n$=========================================$\n" << endl;
    	cout << "--> Please enter the value of circumference: ";
    	cin >> circumferenceValue;
        cout << "\n => The circle area is: " << floor(pow(circumferenceValue, 2) / (4 * PI) ) << " (exactly [ "  << pow(circumferenceValue, 2) / (4 * PI) << " ])" << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #22 Circle inscribed in an isosceles triangle
    	const float PI = 3.14;
        float aValue, bValue, Area;

        cout << "+Circle Inscribed In An Isosceles Triangle+" << endl;

        cout << "\n$=========================================$\n" << endl;
    	cout << "--> Please enter the value of a: ";
    	cin >> aValue;
        cout << "--> Please enter the value of b: ";
    	cin >> bValue;

        Area = PI * (pow(bValue, 2) / 4) * ((2 * aValue - bValue) / (2 * aValue + bValue));
        Area = floor(Area);

        cout << "\n => The circle area is: " << Area << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #23 Circle with an arbitrary triangle into

    	const float PI = 3.14;
        float aValue, bValue, cValue, pValue, longExpression, Area;

        cout << "+ Circle With An Arbitrary Triangle Into +" << endl;

        cout << "\n$=========================================$\n" << endl;
    	cout << "--> Please enter the value of a: ";
    	cin >> aValue;
        cout << "--> Please enter the value of b: ";
    	cin >> bValue;
        cout << "--> Please enter the value of c: ";
    	cin >> cValue;


        pValue = (aValue + bValue + cValue) / 2;
        longExpression = (aValue * bValue * cValue) / (4 * sqrt( pValue * (pValue - aValue) * (pValue - bValue) * (pValue - cValue)));

        Area = round(PI * pow(longExpression, 2));

        cout << "\n => The circle area is: " << Area << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #31 Power of 2 3 4
        int num, aValue, bValue, cValue;

        cout << "+ Get Power Of 2 3 4 +" << endl;

        cout << "\n$=========================================$\n" << endl;
        cout << "--> Please enter a number: ";
    	cin >> num;

        aValue = pow(num, 2);
        bValue = pow(num, 3);
        cValue = pow(num, 4);

        cout << "\n => [ " << num << " ^ 2 ] = " << aValue << endl;
        cout << "\n => [ " << num << " ^ 3 ] = " << bValue << endl;
        cout << "\n => [ " << num << " ^ 4 ] = " << cValue << endl;
        cout << "\n$=========================================$\n" << endl;

    	/----------------------------------------------------------------------/

    	// --> Problem #31 Power of 2 3 4

        cout << "-------------+ Get Power Of M +------------" << endl;
        int number, power;

        cout << "\n$=========================================$\n" << endl;
        cout << "--> Please enter a number: ";
    	cin >> number;
        cout << "--> Please enter the power: ";
    	cin >> power;
        cout << "\n => [ " << number << " ^ " << power << " ] = " << pow(number, power) << endl;
        cout << "\n$=========================================$\n" << endl;
    */



    return 0;
}