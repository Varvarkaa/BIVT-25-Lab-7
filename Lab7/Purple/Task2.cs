namespace Lab7.Purple
{
    public class Task2
    {
        // СТРУКТУРА УЧАСТНИКА
        public struct Participant
        {
            // ПРИВАТНЫЕ ПОЛЯ
            private string _firstName;      // Имя
            private string _lastName;       // Фамилия
            private int _distance;          // Дальность прыжка (в метрах)
            private int[] _marks;           // Массив оценок судей (5 судей)

            // ПУБЛИЧНЫЕ СВОЙСТВА ТОЛЬКО ДЛЯ ЧТЕНИЯ
            public string Name => _firstName;
            public string Surname => _lastName;
            public int Distance => _distance;
            // ВОЗВРАЩАЕМ КОПИЮ МАССИВА
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return new int[5];

                    int[] copy = new int[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }

            // ПУБЛИЧНОЕ СВОЙСТВО ДЛЯ ВЫЧИСЛЕНИЯ ИТОГОВОГО РЕЗУЛЬТАТА
            public double Result
            {
                get
                {
                    // Если нет оценок, возвращаем 0
                    if (_marks == null || _marks.Length == 0) return 0;
                    // 1. Считаем ОЦЕНКИ ЗА СТИЛЬ (сумма после отбрасывания мин и макс)
                    int maxMark = _marks.Max(); 
                    int minMark = _marks.Min();

                    int sum = 0;
                    int kmax = 0 ;
                    int kmin = 0 ;

                    for (int i = 0; i < 5; i++)
                    {
                        if (kmax == 0 && _marks[i] == maxMark)
                        {
                            sum -= maxMark;
                            kmax++;
                        }
                        if (kmin == 0 && _marks[i] == minMark)
                        {
                            sum -= minMark;
                            kmin++;
                        }
                        sum += _marks[i];
                    }

                    // 2. Считаем ОЧКИ ЗА ДАЛЬНОСТЬ
                    // База: 120 м = 60 очков
                    // За каждый метр сверх +2 очка
                    // За каждый метр ниже -2 очка
                    int distancePoints = 60 + (_distance - 120) * 2;

                    // В итоге должно быть не меньше 0
                    if (distancePoints < 0)
                    {
                        distancePoints = 0;
                    }

                    // 3. ИТОГОВЫЙ РЕЗУЛЬТАТ = стиль + дальность
                    return sum + distancePoints;
                }
            }

            // КОНСТРУКТОР
            public Participant(string firstName, string lastName)
            {
                _firstName = firstName;
                _lastName = lastName;
                _distance = 0;              // начальное значение
                _marks = new int[5];         // создаем массив для 5 оценок (по умолчанию нули)
            }

            // МЕТОД ДЛЯ ЗАПИСИ РЕЗУЛЬТАТА ПРЫЖКА
            public void Jump(int distance, int[] marks)
            {
                _distance = distance;// сохраняем дальность прыжка

                // Сохраняем оценки (если передан массив из 5 элементов)
                if (marks != null && marks.Length == 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _marks[i] = marks[i];
                    }
                }
            }

            // СТАТИЧЕСКИЙ МЕТОД ДЛЯ СОРТИРОВКИ (по убыванию результата)
            public static void Sort(Participant[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Result < array[j + 1].Result)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }

                    }
                }
            }

            // МЕТОД ДЛЯ ВЫВОДА ИНФОРМАЦИИ
            // Метод для вывода информации
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: Дистанция {Distance} м, Итоговый результат {Result}");
            }
        }
    }
}


