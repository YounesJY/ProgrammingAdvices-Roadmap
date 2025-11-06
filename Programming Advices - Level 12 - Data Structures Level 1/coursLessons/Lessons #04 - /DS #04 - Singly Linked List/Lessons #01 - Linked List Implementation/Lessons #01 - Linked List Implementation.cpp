#include <iostream>

using namespace std;

// Define the Node structure
struct Node {
    int value;
    Node* next;
};

int main() {
    Node* head;
    Node* Node1 = NULL;
    Node* Node2 = NULL;
    Node* Node3 = NULL;

    // allocate 3 nodes in the heap
    Node1 = new Node();
    Node2 = new Node();
    Node3 = new Node();

    // Assign values
    Node1->value = 1;
    Node2->value = 2;
    Node3->value = 3;

    // Connect nodes
    Node1->next = Node2;
    Node2->next = Node3;
    Node3->next = NULL;

    // print the linked list values
    head = Node1;
    while (head != NULL) {
        cout << head->value << endl;
        head = head->next;
    }

    // Clean up memory (added as good practice, though not in original)
    Node* current = Node1;
    while (current != NULL) {
        Node* next = current->next;
        delete current;
        current = next;
    }

   
    return 0;
}