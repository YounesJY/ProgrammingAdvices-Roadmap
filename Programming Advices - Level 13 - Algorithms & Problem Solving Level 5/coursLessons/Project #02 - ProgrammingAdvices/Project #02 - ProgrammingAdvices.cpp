//ProgrammingAdvices.com
//Mohammed Abu-Hadhoud

#include <iostream>
#include "../../headerFiles/Queue.h"

using namespace std;

int main()
{

    Queue <int> queue;

    queue.push(10);
    queue.push(20);
    queue.push(30);
    queue.push(40);
    queue.push(50);


    cout << "\nQueue: \n";
    queue.print();

    cout << "\nQueue Size: " << queue.size();
    cout << "\nQueue Front: " << queue.front();
    cout << "\nQueue Back: " << queue.back();

    queue.pop();

    cout << "\n\nQueue after pop() : \n";
    queue.print();

    //Extension #1
    cout << "\n\n Item(2) : " << queue.getItem(2);

    //Extension #2
    queue.reverse();
    cout << "\n\nQueue after reverse() : \n";
    queue.print();

    //Extension #3
    queue.updateItem(2, 600);
    cout << "\n\nQueue after updating Item(2) to 600 : \n";
    queue.print();

    //Extension #4
    queue.insertAfter(2, 800);
    cout << "\n\nQueue after Inserting 800 after Item(2) : \n";
    queue.print();


    //Extension #5
    queue.insertAtFront(1000);
    cout << "\n\nQueue after Inserting 1000 at front: \n";
    queue.print();

    //Extension #6
    queue.insertAtBack(2000);
    cout << "\n\nQueue after Inserting 2000 at back: \n";
    queue.print();

    //Extension #7
    queue.clear();
    cout << "\n\nQueue after Clear(): \n";
    queue.print();




    system("pause>0");
}