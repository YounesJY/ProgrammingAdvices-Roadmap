#include <iostream>
#include "Period.h";

int main()

{

    Period Period1(DateFullEdition(1, 1, 2022), DateFullEdition(10, 1, 2022));
    Period1.Print();

    cout << "\n";

    Period Period2(DateFullEdition(3, 1, 2022), DateFullEdition(5, 1, 2022));
    Period2.Print();


    //You can check like this
    cout << Period1.IsOverLapWith(Period2) <<endl;


    //Also you can call the static method and send period 1 and period 2
    cout << Period::IsOverlapPeriods(Period1, Period2) << endl;


    return 0;
}