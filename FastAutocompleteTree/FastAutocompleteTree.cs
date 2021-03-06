﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FastAutocompleteTree
{
    public class FastAutocompleteTree
    {
        private Node Head = new Node();
        private List<string> dictionary;
        //Zrobic wyszukiwanie dla oddzielnych slow tez w danym wyrazeniu
        // poprostu rozdzielic po spacji i dawac te slowa z tym samym indeksem w slowniku

        public FastAutocompleteTree(IEnumerable<string> dictionary, bool searchByWordsInExpression = false)
        {
            this.dictionary = dictionary.OrderBy(s => s.ToLower()).ToList();
            for(int i=0; i< this.dictionary.Count; i++)
            {
                //splits expression, and inserts word to serach tree so we can get results by words in expression not just by prefix of expression
                if (searchByWordsInExpression)
                {
                    var words = this.dictionary[i].Split(' ');
                    foreach(var word in words)
                    {
                        this.Insert(word, i); //all words for same index as we want to get this expression
                    }
                }
                else
                {
                    this.Insert(this.dictionary[i], i);
                }
            }
        }

        private void Insert(string word, int index)
        {
            var node = Head;
            word = word.ToLower();
            foreach (var sign in word)
            {
                int value = this.GetValue(sign);
                if(node[value] == null)
                {
                    node[value] = new Node(sign);
                    node[value].Start = index;
                }
                node[value].End = index;
            }
        }

        public IEnumerable<string> Search(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                return this.dictionary;

            var lastNode = this.SearchForNode(prefix);
            if (lastNode == null)
                return new List<string>();

            return this.dictionary.GetRange(lastNode.Start, lastNode.End - lastNode.Start + 1);
        }

        private Node SearchForNode(string prefix)
        {
            Node result = Head;
            prefix = prefix.ToLower();

            foreach(var sign in prefix)
            {
                int value = this.GetValue(sign);           

                result = result[value];
                if (result == null)
                {
                    return null;
                }
            }

            return result;
        }

        private int GetValue(char c)
        {
            int value = (int)c - 'a';
            if (value < 0 || value > 25)
            {
                //-65 == space 32-97
                if (value == -65)
                {
                    //space
                    value = 26;
                }
                else
                {
                    //wild card
                    value = 27;
                }
            }

            return value;
        }

    }
}
