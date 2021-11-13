using System;
using System.Collections.Generic;
using System.Linq;

namespace ru.TechDevelop.Orlov
{
    class Program
    {
        /*
        public class FIO : IComparable<FIO>
        {
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Secname { get; set; }
 
            public FIO(){ }
            public FIO(string surname, string name, string secname)
            {
                this.Surname = surname;
                this.Name = name;
                this.Secname = secname;
            }
            public int CompareTo(FIO other)
            {
                if (String.Compare(this.Surname, other.Surname) == 0)
                {
                    if (String.Compare(this.Name, other.Name) == 0)
                    {
                        return String.Compare(this.Secname, other.Secname);
                    }
                    else
                    {
                        return String.Compare(other.Name, this.Name);
                    }
                }
                else
                    return String.Compare(this.Surname, other.Surname);
            }
            public override string ToString()
            {
                return $"{this.Surname}  {this.Name}  {this.Secname}";
            }
 
        }
        static void Main(string[] args)
        {
            List<FIO> list = new List<FIO>();
            string line = Console.ReadLine();
            while (!String.IsNullOrEmpty(line))
            {
                string[] vs = line.Split();
                list.Add(new FIO(vs[0], vs[1], vs[2]));
                line = Console.ReadLine();
            }
            list.Sort();
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }*/

        
        public static void Main(string[] args)
        { 
            List<int> list = new List<int>();
            string line = Console.ReadLine();
            List<string> vs = new List<string>();
            int size = 0;
            int j = 1;
            
            while (!String.IsNullOrEmpty(Console.ReadLine()))
            {
                vs = line.Split().ToList<string>();
                for (int i = 0; i < vs.Count; i++)
                {
                    list.Add(int.Parse(vs[i]));
                }
                size = vs.Count;
            }
            int[] sum = new int[(int)Math.Pow(2, size)];
            for (int i = 0; i < sum.Length; i++)
            {
                sum[i] = 0;
            }
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                j = vs.Count - i;
                while (j >= 0)
                {
                    sum[k] += list[j];
                    if (j % 2 == 0)
                    {
                        j /= 2;
                    }
                    else
                    {
                        j = (j - 1) / 2;
                    }
                }
            }
        }
    }

}
