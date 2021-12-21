using System;

namespace Task2.DependResolution
{
    class Program
    {
        interface IA
        {
        }
        class A : IA
        {
            private static int cnt = 0; 
            public A()
            {
                Console.WriteLine($"A {cnt}");
                cnt++;
            }
        }
        interface IB
        {

        }
        class B : IB
        {
            private static int cnt = 0;
            public B(IA a)
            {
                Console.WriteLine($"B {cnt}");
                cnt++;
            }
        }

        static void Main(string[] args)
        {
            var dep = new DependenciesInjec();
            //Первый пример, тут просто на работу проверяем
            dep.AddTransient<IA, A>();
            dep.Get<IA>();
            dep.Get<IA>();
            Console.WriteLine();
            //Второй пример, тут проверим синглтон
            dep.AddSingleton<IA, A>();
            dep.Get<IA>();
            dep.Get<IA>();
            Console.WriteLine();
            //Третий пример, тут проверим на ошибку цикличности
            /*dep.AddTransient<IB, B>();
            dep.Get<IA>();
            dep.Get<IA>();*/
            //закоменчено чтобы не можно было сначала другие проверить
            Console.WriteLine();
        }
    }
}
