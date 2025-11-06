// Lesson _02 - Requirements - ProgrammingAdvices.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "../../headerFiles/DoubleLinkedList.h"

int main() {
    // Test 1: Create an empty list
    DoubleLinkedList<int> list;
    std::cout << "Empty list: ";
    list.printList();
    std::cout << "\n";

    // Test 2: Insert at beginning (single node)
    list.insertAtBeginning(50);
    list.insertAtBeginning(40);
    list.insertAtBeginning(30);
    list.insertAtBeginning(20);
    list.insertAtBeginning(10);
    std::cout << "After inserting 10 at beginning: ";
    list.printList();

    std::cout << endl;
    list.reverse();

    std::cout << "After reversing: ";
    list.printList();

    std::cout << endl;
    std::cout << (list.updateItem(0, 191) ? "Updated" : "Not updated") << endl;
    list.insertAfter(4, 912);
    list.printList();


    std::cout << endl;
    std::cout << endl;
    std::cout << endl;

    return 0;
}