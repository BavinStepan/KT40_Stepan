using System;
using System.Collections.Generic;

namespace GcExperiment
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Эксперимент по работе сборщика мусора (GC) в .NET");
            Console.WriteLine("Нажмите Enter для начала...");
            Console.ReadLine();

            long initialHeap = GC.GetTotalMemory(true);
            int initialGen0 = GC.CollectionCount(0);
            int initialGen1 = GC.CollectionCount(1);
            int initialGen2 = GC.CollectionCount(2);

            Console.WriteLine($"Начальные счетчики сборок: Gen0={initialGen0}, Gen1={initialGen1}, Gen2={initialGen2}");
            Console.WriteLine($"Начальный размер кучи: {initialHeap / 1024.0:F0} КБ");
            Console.WriteLine();

            Console.WriteLine("Шаг 1: Выделение 200 000 коротких массивов (byte[50])");
            Console.WriteLine("Нажмите Enter для выполнения...");
            Console.ReadLine();

            for (int i = 0; i < 200000; i++)
            {
                byte[] arr = new byte[50];
            }

            long afterShortHeap = GC.GetTotalMemory(true);
            int afterShortGen0 = GC.CollectionCount(0);
            int afterShortGen1 = GC.CollectionCount(1);
            int afterShortGen2 = GC.CollectionCount(2);

            Console.WriteLine($"Добавилось сборок: Gen0={afterShortGen0 - initialGen0}, Gen1={afterShortGen1 - initialGen1}, Gen2={afterShortGen2 - initialGen2}");
            Console.WriteLine($"Размер кучи после шага 1: {afterShortHeap / 1024.0:F0} КБ");
            Console.WriteLine();

            Console.WriteLine("Шаг 2: Выделение 5 больших объектов (byte[90000])");
            Console.WriteLine("Нажмите Enter для выполнения...");
            Console.ReadLine();

            List<byte[]> largeObjects = new List<byte[]>();
            for (int i = 0; i < 5; i++)
            {
                largeObjects.Add(new byte[90000]);
            }

            long afterLargeHeap = GC.GetTotalMemory(true);
            int afterLargeGen0 = GC.CollectionCount(0);
            int afterLargeGen1 = GC.CollectionCount(1);
            int afterLargeGen2 = GC.CollectionCount(2);

            Console.WriteLine($"Добавилось сборок: Gen0={afterLargeGen0 - afterShortGen0}, Gen1={afterLargeGen1 - afterShortGen1}, Gen2={afterLargeGen2 - afterShortGen2}");
            Console.WriteLine($"Размер кучи после шага 2: {afterLargeHeap / 1024.0:F0} КБ");

            Console.WriteLine();
            Console.WriteLine("Эксперимент завершен. Нажмите Enter для выхода.");
            Console.ReadLine();
        }
    }
}