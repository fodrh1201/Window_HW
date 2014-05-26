using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList_Cycle
{
    class ListNode
    {
        class Term
        {
            public int val;
            public Term next;
        }

        class LinkedList
        {
            Term first;

            public LinkedList()
            {
                first = null;
            }

            public void AddTerm(int x)
            {
                Term t = new Term();
                t.val = x;
                
                if (first == null)
                {
                    first = t;
                    t.next = null;
                }
                else
                {
                    t.next = first;
                    first = t;
                }
            }

            public Term IsCycle() 
            {
                if (first == null)
                    return null;

                if (first.next == first)
                {
                    return first;
                }

                for (Term t = first.next; t != null; t = t.next)
                {
                    for (Term s = first; s != t; s = s.next)
                    {
                        if (s == t.next)
                        {
                            return s;
                        }
                    }
                }
                return null;
            }
        }
    }
}
