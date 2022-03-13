using System;
using System.Collections.Generic;
using System.IO;
using Search1;

namespace Search;


class search1
{

    static void Main(string[] args)
    {
        Graph g = new Graph(); 
        GraphAlgorithm ga = new GraphAlgorithm();
        System.Console.WriteLine("Путь - 1");
        System.Console.WriteLine("Анализ - 2");
        int choice1 = Convert.ToInt32(Console.ReadLine());
        switch (choice1)
        {
            case 1:
                g.fill();
                Console.WriteLine( g.v);
                g.pin(g.list);
                Console.WriteLine("С какой вершины начнем?");
                int t = Convert.ToInt32(Console.ReadLine());
                System.Console.WriteLine("Что ищем?");
                System.Console.WriteLine("DFS-1");
                System.Console.WriteLine("BFS-2");
                System.Console.WriteLine("Связанность-3");
                System.Console.WriteLine("Цикличность-4");
                System.Console.WriteLine("Предок-5");
                System.Console.WriteLine("Компоненты SCC-6");
                System.Console.WriteLine("Конденсация-7");
                System.Console.WriteLine("Топология-8");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
            {
                case 1:
                    g.DFS(t);
                    System.Console.WriteLine("DFS");
                    g.pinFS(g.wereD);
                break;

                case 2:
                    g.BFS(t);
                    System.Console.WriteLine("BFS");
                    g.pinFS(g.wereB);
                break;

                case 3:
                    g.connection(t);
                break;

                case 4:
                    System.Console.WriteLine("Мой метод - 1");
                    System.Console.WriteLine("Цветастый метод - 2");
                    int cy = Convert.ToInt32(Console.ReadLine());
                    switch (cy)
                    {
                        case 2:
                        g.colorcycle(t);
                        break;

                        case 1:
                        g.totalcycle(t);
                        break;
                    }

                break;

                case 5:
                    System.Console.WriteLine("Введите начальную вершину");
                    int start = Convert.ToInt32(Console.ReadLine());
                    int end = Convert.ToInt32(Console.ReadLine());
                    g.Sancestor(start, end);
                break;

                case 6:
                    g.SCC();
                    g.pin(g.SCCMAS);
                break;

                case 7:
                    g.Condensation();
                    g.pin(g.listCon);
                break;

                case 8:
                    g.topology();
                break;


                case 10:
                    g.SCC();
                    g.pin(g.listCH);
                break;
            }
            break;

            case 2:
                Console.WriteLine("С какой вершины начнем?");
                int t1 = Convert.ToInt32(Console.ReadLine());
                ga.fill();
                ga.BFSv2(t1);
                ga.pinFS(ga.wereB);
            break;
            }        
        
    }



}