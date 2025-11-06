// ProgrammingAdvices.com
// Mohammed Abu-Hadhoud

#include <iostream>
#include "../../headerFiles/DynamicArray.h"

int main() {
	DynamicArray<int> dynamicArray(5);

	dynamicArray.setItem(0, 10);
	dynamicArray.setItem(1, 20);
	dynamicArray.setItem(2, 30);
	dynamicArray.setItem(3, 40);
	dynamicArray.setItem(4, 50);

	std::cout << "\nIs Empty?  " << dynamicArray.isEmpty();
	std::cout << "\nArray Size: " << dynamicArray.size() << "\n";

	std::cout << "\nArray Items:\n";
	dynamicArray.printList();

	std::cout << "\n\nArray Items after reversing:\n";
	dynamicArray.reverse();
	dynamicArray.printList();

	dynamicArray.resize(2);
	std::cout << "\n\nArray Size after resize to 2: " << dynamicArray.size() << "\n";
	dynamicArray.printList();

	dynamicArray.resize(10);
	std::cout << "\n\nArray Size after resize to 10: " << dynamicArray.size() << "\n";

	dynamicArray.setItem(0, 10);
	dynamicArray.setItem(1, 20);
	dynamicArray.setItem(2, 30);
	dynamicArray.setItem(3, 40);
	dynamicArray.setItem(4, 50);
	dynamicArray.setItem(5, 60);
	dynamicArray.setItem(6, 70);
	dynamicArray.setItem(7, 80);
	dynamicArray.setItem(8, 90);
	dynamicArray.setItem(9, 100);
	dynamicArray.printList();

	std::cout << "\n\nDeleting first and last items:\n";
	dynamicArray.deleteFirstItem();
	dynamicArray.deleteLastItem();
	dynamicArray.printList();

	std::cout << "\n\nInserting at beginning and end:\n";
	dynamicArray.insertAtBeginning(9);
	dynamicArray.insertAtEnd(12);
	dynamicArray.printList();

	std::cout << "\n\nInserting before 8 and after 12222:\n";
	dynamicArray.insertBefore(0, 123123);
	dynamicArray.insertAfter(dynamicArray.size(), 87789); // This will fail silently if 12222 not found
	dynamicArray.printList();

	system("pause>0");
	return 0;
}
