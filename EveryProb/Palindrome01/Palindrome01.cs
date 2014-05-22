using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Term
{
    public string term;
    public int start;
}

class Palindrome
{
    static bool IsPalin(string p)
    {
        int i = 0;
        int j = p.Length - 1;

        while (i < j)
        {
            if (p[i] == p[j])
            {
                i++;
                j--;
            }
            else
                break;
        }

        if (i >= j)
            return true;
        else
            return false;
    }

    static bool AreAllPalin(string standard, string input)
    {
        string[] loc = standard.Split(' ');
        int[] cutLoc = loc.Select(x => int.Parse(x)).ToArray();
        int len = cutLoc.Length;
        int i = 0;
        int start = 0;
        
        while (true)
        {
            if (i == len - 1)
            {
                if (IsPalin(input.Substring(cutLoc[i] + 1)) 
                    && IsPalin(input.Substring(start, cutLoc[i] - start + 1)))
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                if (IsPalin(input.Substring(start, cutLoc[i] - start + 1)))
                {
                    start = cutLoc[i] + 1;
                    i++;
                }
                else
                    return false;
            }
        }
    }

    static void PrintPalin(string standard, string input)
    {
        string[] loc = standard.Split(' ');
        int[] cutLoc = loc.Select(x => int.Parse(x)).ToArray();
        int len = cutLoc.Length;
        int i = 0;
        int start = 0;

        while (true)
        {
            if (i == len - 1)
            {
                Console.Write(input.Substring(start, cutLoc[i] - start + 1) + " ");
                Console.Write(input.Substring(cutLoc[i] + 1));
                break;
            }
            else
            {
                Console.Write(input.Substring(start, cutLoc[i] - start + 1) + " ");
                start = cutLoc[i] + 1;
                i++;
            }
        }
        Console.WriteLine();
    }

    static void ShowPalindrome(string[] baseLocOfCut, string input)
    {
        Term[] list = new Term[(int)Math.Pow(2, baseLocOfCut.Length)];
        Term first = new Term();
        first.term = "";
        first.start = 0;
        list[0] = first;

        if (IsPalin(input))
            Console.WriteLine(input);


        int i = 0;
        int j = 0;
        
        while (i < list.Length-1)
        {
            for (int k = 0; k < baseLocOfCut.Length - list[j].start; k++)
            {
                if (j == 0)
                {
                    Term temp = new Term();
                    i++;
                    temp.start = list[j].start + k + 1;
                    temp.term = list[j].term + baseLocOfCut[list[j].start + k];
                    list[i] = temp;

                    if (AreAllPalin(temp.term, input))
                        PrintPalin(temp.term, input);
                }
                else
                {
                    Term temp = new Term();
                    i++;
                    temp.start = list[j].start + k + 1;
                    temp.term = list[j].term + " " + baseLocOfCut[list[j].start + k];
                    list[i] = temp;

                    if (AreAllPalin(temp.term, input))
                        PrintPalin(temp.term, input);
                }
            }
            j++;
        }
    }

    static void Main()
    {
        string input = Console.ReadLine();
        int len = input.Length;
        string[] baseLocOfCut = new string[len-1];

        for (int i = 0; i < len-1; i++)
        {
            baseLocOfCut[i] = Convert.ToString(i);
        }

        ShowPalindrome(baseLocOfCut, input);

       
    }
}