using System;

public class Term
{
	public string term;
	public int start;
}

class Combination
{
    static Term[] Combi(string input)
	{
		Term[] list = new Term[(int)Math.Pow(2,input.Length)];
		Term first = new Term();
        first.term = "";
        first.start = 0;
        list[0] = first;


        int i = 0;
        int j = 0;

        while (i < list.Length-1)
        {
                for (int k = 0; k < input.Length - list[j].start; k++)
                {
                    Term temp = new Term();
                    i++;
                    temp.start = list[j].start + k + 1;
                    temp.term = list[j].term + new string(input[list[j].start + k], 1);
                    list[i] = temp;
                }
                j++;

        }
        return list;
    }

    static void PrintTerm(Term[] list)
    {
        foreach (Term a in list)
            Console.WriteLine(a.term);
    }


    static void Main()
    {
        string input = Console.ReadLine();
        Term[] list = Combi(input);
        PrintTerm(list);
    }
}