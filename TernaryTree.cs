/* Ternary Trees
 * 
 * Written By: Saalar Faisal on March 25, 2021
 * 
 * The Purpose is write a Remove Method for the Ternary Tree to remove a value.
 * It is to be implmented, tested, and be able to print.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TERNARY_TREE
{
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool Empty();
        int Size();
    }

    //-------------------------------------------------------------------------

    public interface ITrie<T> : IContainer<T>
    {
        bool Insert(string key, T value);
        T Value(string key);
    }

    //-------------------------------------------------------------------------

    class Trie<T> : ITrie<T>
    {
        private Node root;                 // Root node of the Trie
        private int size;                  // Number of values in the Trie

        class Node
        {
            public char ch;                // Character of the key
            public T value;                // Value at Node; otherwise default
            public Node low, middle, high; // Left, middle, and right subtrees

            // Node
            // Creates an empty Node
            // All children are set to null
            // Time complexity:  O(1)

            public Node(char ch)
            {
                this.ch = ch;
                value = default(T);
                low = middle = high = null;
            }
        }

        // Trie
        // Creates an empty Trie
        // Time complexity:  O(1)

        public Trie()
        {
            MakeEmpty();
            size = 0;
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise

        public bool Insert(string key, T value)
        {
            return Insert(ref root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored
        // Time complexity:  O(n+L) where n is the number of nodes and 
        //                                L is the length of the given key

        private bool Insert(ref Node p, string key, int i, T value)
        {
            if (p == null)
                p = new Node(key[i]);

            // Current character of key inserted in left subtree
            if (key[i] < p.ch)
                return Insert(ref p.low, key, i, value);

            // Current character of key inserted in right subtree
            else if (key[i] > p.ch)
                return Insert(ref p.high, key, i, value);

            else if (i + 1 == key.Length)
            // Key found
            {
                // But key/value pair already exists
                if (!p.value.Equals(default(T)))
                    return false;
                else
                {
                    // Place value in node
                    p.value = value;
                    size++;
                    return true;
                }
            }

            else
                // Next character of key inserted in middle subtree
                return Insert(ref p.middle, key, i + 1, value);
        }

        // Value
        // Returns the value associated with a key; otherwise default
        // Time complexity:  O(d) where d is the depth of the trie

        public T Value(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return the value if all characters of the key have been visited 
                    if (++i == key.Length)
                        return p.value;

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return default(T);   // Key too long
        }

        // Contains
        // Returns true if the given key is found in the Trie; false otherwise
        // Time complexity:  O(d) where d is the depth of the trie

        public bool Contains(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return true if the key is associated with a non-default value; false otherwise 
                    if (++i == key.Length)
                        return !p.value.Equals(default(T));

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return false;        // Key too long
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)

        public void MakeEmpty()
        {
            root = null;
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)

        public bool Empty()
        {
            return root == null;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)

        public int Size()
        {
            return size;
        }

       

        // Public Print
        // Calls private Print to carry out the actual printing

        public void Print()
        {
            Print(root, "");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys
        // Time complexity:  O(n) where n is the number of nodes

        private void Print(Node p, string key)
        {
            if (p != null)
            {
                Print(p.low, key);
                if (!p.value.Equals(default(T)))
                    Console.WriteLine(key + p.ch + " " + p.value);
                Print(p.middle, key + p.ch);
                Print(p.high, key);
            }
        }

        

        // ----THESE METHODS I AM IMPLEMENTING----

        // Is it a leaf Node
        private bool LeafNode(ref Node p) {

            // if terminal node then true
            if (p.low == null && p.middle == null && p.high == null)
                return true;
            else
                return false;

        }

        // Public Remove
        // Calls the private Remove which carries out the actual removal
        // Returns true if successful; false otherwise
        // References: https://www.geeksforgeeks.org/ternary-search-tree/
        //           --Ternary Notes posted by Brian
        public bool Remove(string key)
        {
            return Remove(ref root, key, 0);
        }

        // Remove
        // Remove the given key (value) from the Trie
        // Returns true if the removal was successful; false otherwise
        private bool Remove(ref Node p, string key, int i)
        {

            // if node is null
            if (p == null)
                return false;

            // if key does not exist in Node
            if (!Contains(key))
                return false;

            // when we have reached end of key
            if (i+1 == key.Length)
            {
                // if this is a leaf Node
                if (LeafNode(ref p))
                {
                    // reset its value
                    p.value = default(T);
                    return true;

                }
                else
                {
                    // set value to default
                    p.value = default(T);
                    return false;
                }
                
            }

            // if its less than current node
            if (key[i] < p.ch)
                return Remove(ref p.low, key, i);

            // if its more than current node
            if (key[i] > p.ch)
                return Remove(ref p.high, key, i);
            
            // if it matches
            if(key[i] == p.ch)
            {
                // recursive delete if all values are leafNode after deletion
                // and values must be default
                // if they are not default that means there is a parallel key
                // we will only remove value in that case
                // or if the parallel key has an extra, then only delete the ends

                if(Remove(ref p.middle, key, i + 1))
                {
                    p.middle = null;
                    string temp = key[i].ToString();
                    return Val(ref p) && LeafNode(ref p);
                }
            }

            // default
            return false;
        }

        // This method will return 'true' if value of node is default
        private bool Val(ref Node p)
        {
            return p.value.Equals(default(T));
        }

        // This Prints the entire tree
        // in pre-order traversal
        public void PrintAll()
        {
            PrintAll(ref root);
        }

        // the recursive part of PrintAll
        private void PrintAll(ref Node root)
        {
            if (root != null)
            {
                // print the character stored in the root
                Console.Write(root.ch + " ");
                // print its children
                PrintAll(ref root.low);
                PrintAll(ref root.middle);
                PrintAll(ref root.high);

            }
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            Trie<int> T;
            T = new Trie<int>();

            // Insert Key into Ternary Tree
            T.Insert("SPACE", 10);
            T.Insert("APPLE", 20);
            T.Insert("TIGER", 70);
            T.Insert("SPACES", 30);
            T.Insert("APPS", 80);
            

            Console.WriteLine("-----First print invocation-----");
            T.Print();
            Console.WriteLine();
            T.PrintAll();
            Console.WriteLine();
            Console.WriteLine();

            ////Test 1: A key that is not present
            //Console.WriteLine(T.Remove("Golden"));
            //Console.WriteLine();

            ////Test 2: Remove key that is within another key
            //T.Remove("SPACE");

            ////Test 3: Remove key that is contains another key
            //T.Remove("SPACES");

            ////Test 4: Remove key that contains no other key
            //T.Remove("TIGER");
            //Console.WriteLine();

            //------------------------------------
            // Enable below when doing the testing
            // to show results after removal
            //-----------------------------------

            //Console.WriteLine("-----Second print after remove" +
            //    " method invocation-----");
            //T.Print();
            //T.PrintAll();
            //Console.WriteLine();

            Console.ReadKey();
        }
    }
}

