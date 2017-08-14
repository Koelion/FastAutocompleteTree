using System;
using System.Collections.Generic;
using System.Text;

namespace FastAutocompleteTree
{
    public class Node
    {
        public char value;
        //26 chars 1 space 1 wildcard
        private Node[] nodes = new Node[28];

        public Node this[int index]
        {
            get
            {
                return nodes[index];
            }
            set
            {
                nodes[index] = value;
            }
        }

        public Node()
        {

        }

        public Node(char value)
        {
            this.value = value;
        }


        public int Start { get; set; }
        public int End { get; set; }
    }
}
