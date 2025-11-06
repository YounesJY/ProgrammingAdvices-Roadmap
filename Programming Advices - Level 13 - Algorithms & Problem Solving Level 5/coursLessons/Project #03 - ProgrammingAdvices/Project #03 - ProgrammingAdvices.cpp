//ProgrammingAdvices.com
//Mohammed Abu-Hadhoud

#include <iostream>
#include "../../headerFiles/Stack.h"

using namespace std;

int main()
{

    Stack <int> stack;

    stack.push(10);
    stack.push(20);
    stack.push(30);
    stack.push(40);
    stack.push(50);


    cout << "\nStack: \n";
    stack.print();

    cout << "\nStack Size: " << stack.size();
    cout << "\nStack Top: " << stack.top();
    cout << "\nStack Bottom: " << stack.bottom();

    stack.pop();

    cout << "\n\nStack after pop() : \n";
    stack.print();

    //Extension #1
    cout << "\n\n Item(2) : " << stack.getItem(2);

    //Extension #2
    stack.reverse();
    cout << "\n\nStack after reverse() : \n";
    stack.print();

    //Extension #3
    stack.updateItem(2, 600);
    cout << "\n\nStack after updating Item(2) to 600 : \n";
    stack.print();

    //Extension #4
    stack.insertAfter(2, 800);
    cout << "\n\nStack after Inserting 800 after Item(2) : \n";
    stack.print();


    //Extension #5
    stack.insertAtFront(1000);
    cout << "\n\nStack after Inserting 1000 at top: \n";
    stack.print();

    //Extension #6
    stack.insertAtBack(2000);
    cout << "\n\nStack after Inserting 2000 at bottom: \n";
    stack.print();

    //Extension #7
    stack.clear();
    cout << "\n\nStack after Clear(): \n";
    stack.print();

    system("pause>0");

}