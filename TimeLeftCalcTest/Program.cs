using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeLeftCalcZ;

namespace TimeLeftCalcTest
{
    internal class Program
    {
        /// <summary>
        /// Основной метод программы
        /// </summary>
        static void Main(string[] args)
        {
            //Инициализируем рандомайзер для разброса тестового времени
            Random r = new Random();
            //Инициализируем класс калькулятора
            TimeLeftCalc calc = new TimeLeftCalc();
            //Переменные для работы
            int randTime;
            double iterationTime;
            double? timeLeft;
            //Запускаем калькулятор, указав 50 итераций для работы
            calc.Start(50);
            //Будет цикл из 50 иткраций
            for (int i = 0; i < 50; i++)
            {
                //ПОлучаем разброс для времени
                randTime = r.Next(-30, 30);
                //Получаем время итерации с разбросом
                iterationTime = 5 + (randTime / 10.0);
                //Ждём 1/10 часть от этого времени (для красоты вывода)
                Thread.Sleep((int)(iterationTime * 100));
                //Добавляем итерацию в калькулятор
                calc.AddIteration(i, iterationTime);
                //Получаем оставшееся время
                timeLeft = calc.GetTimeLeft();
                //Если расчёт времени уже возможен
                if (timeLeft.HasValue)
                    //Выводим оставшееся время
                    Console.WriteLine($"Time left: {timeLeft.Value.ToString("F2")}s");
                //В противном случае
                else
                    //Выводим инфу о том, что нужно ещё подождать
                    Console.WriteLine("Time left: Calculation...");
            }
            //Запрещаем закрывать окно консоли
            Console.ReadLine();
        }
    }
}
