using System;
using System.Collections.Generic;

namespace Task1.CustomDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomDictionary<string, int> cusDic = new CustomDictionary<string, int>();
            cusDic.Add("Four", 4);
            cusDic.Add("One", 1);
            cusDic.Add("Two", 2);
            cusDic.Add("Three", 3);
            cusDic.Add("Five", 5);
            foreach (var item in cusDic)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }
        }
    }
}
