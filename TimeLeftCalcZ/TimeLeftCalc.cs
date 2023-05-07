using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeLeftCalcZ
{
    /// <summary>
    /// Класс рассчёта оставшегося времени по итерациям
    /// </summary>
    public class TimeLeftCalc
    {
        /// <summary>
        /// Константа дефолтной длинны буфера итераций
        /// </summary>
        private const int ITERAIONS_BUFF_DEFAULT_LENGTH = 10;

        /// <summary>
        /// Длинна буфера итераций
        /// </summary>
        private readonly int _iterationBuffLength;
        /// <summary>
        /// Буфер итераций
        /// </summary>
        private readonly List<double> _iterationsBuff;
        /// <summary>
        /// Количество итераций для рассчёта
        /// </summary>
        private int _iterationsCount;
        /// <summary>
        /// Счётчик количества оставшихся итераций
        /// </summary>
        private int _iterationsLeft;


        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="iterationBuffLength">
        ///     Значение размера буфера итераций.
        ///     Чем больше буфер, тем более точной будет оценка оставшегося времени (меньше скачков между значениями).
        ///     Значение подбирается опытным путём, и зависит от разброса длительности в итерациях.
        ///     Значение в 10 итераций - идеально подходит для буфера, у которого минимальное и максимальное значения отличаются в 10 раз.
        /// </param>
        public TimeLeftCalc(int iterationBuffLength = ITERAIONS_BUFF_DEFAULT_LENGTH)
        {
            //Сохраняем полученную длинну буфера итераций
            _iterationBuffLength = iterationBuffLength;
            //Инициализируем буфер итераций
            _iterationsBuff = new List<double>();
            //Сбрасываем значение количества итераций
            _iterationsCount = 0;
            //Сбрасываем значение количества оставшихся итераций
            _iterationsLeft = 0;
        }

        /// <summary>
        /// Получаем оставшееся время
        /// </summary>
        /// <returns>Значение оставшегося времени, или Null, в случае, если буфер ещё не набран</returns>
        public double? GetTimeLeft()
        {
            //Если буфер ещё не набран
            if(_iterationsBuff.Count < _iterationBuffLength)
                //Возвращаем пустое значение
                return null;
            //Считаем оставшееся время
            return (_iterationsBuff.Sum() / _iterationBuffLength) * _iterationsLeft;
        }

        /// <summary>
        /// Добавление итерации в рассчёт
        /// </summary>
        /// <param name="id">Идентификатор итерации. Должен начинаться с 0, для корректного рассчёта.</param>
        /// <param name="time">Длительность выполненной итерации.</param>
        public void AddIteration(int id, double time)
        {
            //Рассчитываем количество оставшихся итераций
            _iterationsLeft = _iterationsCount - id - 1;
            //Добавляем время в буфер итераций
            _iterationsBuff.Add(time);
            //Если буфер итераций больше лимита
            if(_iterationsBuff.Count > _iterationBuffLength)
                //Удаляем самое старое значение буфера
                _iterationsBuff.RemoveAt(0);
        }

        /// <summary>
        /// Метод запуска подсчёта итераций
        /// </summary>
        /// <param name="count">Количество итераций</param>
        public void Start(int count)
        {
            //Проставляем новые значения количества итераций
            _iterationsLeft = _iterationsCount = count;            
            //Очищаем буфер итераций
            _iterationsBuff.Clear();
        }
    }
}
