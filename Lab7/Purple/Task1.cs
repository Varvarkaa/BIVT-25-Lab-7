using System;
using System.Linq;

namespace Lab7.Purple
{
    public class Task1
    {
        public struct Participant
        {
            // ПРИВАТНЫЕ ПОЛЯ 
            private string _firstName;           // Имя
            private string _lastName;            // Фамилия
            private double[] _coefs;              // Массив коэффициентов сложности (4 прыжка)
            private int[,] _marks;                 // Матрица оценок: [4 прыжка, 7 судей]
            private int _jumpCounter;              // Счетчик прыжков 

            // ПУБЛИЧНЫЕ СВОЙСТВА ТОЛЬКО ДЛЯ ЧТЕНИЯ (get - только возвращают значения)
            public string Name => _firstName;              // Возвращает имя
            public string Surname => _lastName;            // Возвращает фамилию
            // ВОЗВРАЩАЕМ КОПИЮ МАССИВА КОЭФФИЦИЕНТОВ 
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return new double[4];

                    double[] copy = new double[_coefs.Length];
                    Array.Copy(_coefs, copy, _coefs.Length);
                    return copy;
                }
            }

            // ВОЗВРАЩАЕМ КОПИЮ МАТРИЦЫ ОЦЕНОК 
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return new int[4, 7];

                    int[,] copy = new int[4, 7];
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }               

            // ПУБЛИЧНОЕ СВОЙСТВО ТОЛЬКО ДЛЯ ЧТЕНИЯ - вычисляет итоговый результат
            public double TotalScore
            {
                get
                {
                    // Если нет коэффициентов или оценок, возвращаем 0
                    if (_coefs == null || _marks == null) return 0;

                    double total = 0; // общая сумма за 4 прыжка

                    // Цикл по 4 прыжкам
                    for (int jump = 0; jump < 4; jump++)
                    {
                        // Собираем оценки текущего прыжка в отдельный массив
                        int[] jumpMarks = new int[7];
                        for (int judge = 0; judge < 7; judge++)
                        {
                            jumpMarks[judge] = _marks[jump, judge];
                        }

                        // Находим лучшую и худшую оценки
                        int maxMark = jumpMarks.Max();
                        int minMark = jumpMarks.Min();

                        // Суммируем оставшиеся оценки (отбрасываем одну лучшую и одну худшую)
                        int sum = 0;
                        int kmax = 0;
                        int kmin = 0;
                        for (int judge = 0; judge < 7; judge++)
                        {
                            if (jumpMarks[judge] == maxMark & kmax == 0)
                            {
                                sum -= jumpMarks[judge];
                                kmax++;

                            }
                            if (jumpMarks[judge] == minMark & kmin == 0)
                            {
                                sum -= jumpMarks[judge];
                                kmin++;
                            }
                            sum += jumpMarks[judge];
                        }
                       
                        // Умножаем сумму на коэффициент сложности прыжка и добавляем к общему результату
                        total += sum * _coefs[jump];
                    }
                    return total; // возвращаем итоговый результат
                }
            }

            // КОНСТРУКТОР 
            public Participant(string firstName, string lastName)
            {
                _firstName = firstName; // сохраняем имя
                _lastName = lastName;   // сохраняем фамилию
                _coefs = new double[4]; // создаем массив для 4 коэффициентов
                _marks = new int[4, 7]; // создаем матрицу для 4 прыжков и 7 судей
                _jumpCounter = 0;       // начинаем с первого прыжка
            }

            // МЕТОД ДЛЯ УСТАНОВКИ КОЭФФИЦИЕНТОВ СЛОЖНОСТИ
            public void SetCriterias(double[] coefs)
            {
                if (coefs != null && coefs.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        _coefs[i] = coefs[i];
                    }
                }
            }

            // МЕТОД для записи результатов одного прыжка
            public void Jump(int[] marks)
            {
                if (_jumpCounter >= 4) return; // защита от превышения
                if (marks == null || marks.Length != 7) return; // проверка длины
                // Записываем оценки 7 судей в матрицу
                for (int i = 0; i < 7; i++)
                {
                    _marks[_jumpCounter, i] = marks[i]; // _jumpCounter указывает номер прыжка
                }
                _jumpCounter++; // увеличиваем счетчик для следующего прыжка
            }

            // СТАТИЧЕСКИЙ МЕТОД для сортировки массива участников по убыванию результата
            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                // Пузырьковая сортировка
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine($"Итоговый результат: {TotalScore:F2}");
            }
        }
    }
}
