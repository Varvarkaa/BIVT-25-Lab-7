using System;

namespace Lab7.Purple
{
    public class Task4
    {
        public struct Sportsman
        {
            // Поля
            private string _firstName;
            private string _lastName;
            private double _time;
            private bool _installed; // Устанавливаем флаг для проверки, было ли установлено время

            // Свойства
            public string Name => _firstName;
            public string Surname => _lastName;
            public double Time => _time;

            // Конструктор
            public Sportsman(string firstName, string lastName)
            {
                _firstName = firstName;
                _lastName = lastName;
                _time = 0;
                _installed = false; // Изначально время не установлено
            }

            // Метод для установки времени
            public void Run(double time)
            {
                if (_installed) // Устанавливаем время только один раз
                {
                    return;
                }
                _time = time;
                _installed = true; // Устанавливаем флаг
            }

            // Метод для вывода информации
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Time:F2} сек");
            }
        }

        public struct Group
        {
            // Поля
            private string _name;
            private Sportsman[] _sportsmen;

            // Свойства
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            // Конструкторы
            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }

            public Group(Group other)
            {
                _name = other.Name;
                _sportsmen = new Sportsman[other.Sportsmen.Length];
                for (int i = 0; i < other.Sportsmen.Length; i++)
                {
                    _sportsmen[i] = other.Sportsmen[i];
                }
            }

            // Методы
            public void Add(Sportsman sportsman)
            {
                // Проверяем, если массив не инициализирован, и создаем его
                if (_sportsmen == null)
                {
                    _sportsmen = new Sportsman[1];
                    _sportsmen[0] = sportsman;
                    return;
                }

                // Создаем новый массив с увеличенным размером
                Sportsman[] newSportsmen = new Sportsman[_sportsmen.Length + 1];

                // Копируем старые элементы в новый массив
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    newSportsmen[i] = _sportsmen[i];
                }

                // Добавляем нового спортсмена в конец нового массива
                newSportsmen[^1] = sportsman;

                // Присваиваем новый массив переменной
                _sportsmen = newSportsmen;
            }

            public void Add(Sportsman[] sportsmen)
            {
                // Проверка на null или пустой массив
                if (sportsmen == null || sportsmen.Length == 0)
                {
                    return;
                }

                // Создаем новый массив с увеличенным размером
                Sportsman[] newSportsmen = new Sportsman[_sportsmen.Length + sportsmen.Length];

                // Копируем старые элементы в новый массив
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    newSportsmen[i] = _sportsmen[i];
                }

                // Добавляем новых спортсменов в новый массив
                for (int i = 0; i < sportsmen.Length; i++)
                {
                    newSportsmen[_sportsmen.Length + i] = sportsmen[i];
                }

                // Присваиваем новый массив переменной
                _sportsmen = newSportsmen;
            }

            public void Add(Group other)
            {
                // Проверка на null или пустой массив
                if (other.Sportsmen == null || other.Sportsmen.Length == 0)
                {
                    return;
                }

                // Создаем новый массив с увеличенным размером
                Sportsman[] newSportsmen = new Sportsman[_sportsmen.Length + other.Sportsmen.Length];

                // Копируем старые элементы в новый массив
                for (int i = 0; i < _sportsmen.Length; i++)
                {
                    newSportsmen[i] = _sportsmen[i];
                }

                // Добавляем спортсменов из другой группы
                for (int i = 0; i < other.Sportsmen.Length; i++)
                {
                    newSportsmen[_sportsmen.Length + i] = other.Sportsmen[i];
                }

                // Присваиваем новый массив переменной
                _sportsmen = newSportsmen;
            }

            public void Sort()
            {
                if (_sportsmen == null || _sportsmen.Length <= 1)
                {
                    return;
                }

                int n = _sportsmen.Length;

                // Сортировка пузырьком
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1 - i; j++)
                    {
                        // Сравниваем времена двух спортсменов
                        if (_sportsmen[j].Time > _sportsmen[j + 1].Time)
                        {
                            // Меняем местами, если текущий элемент больше, чем следующий
                            Sportsman temp = _sportsmen[j];
                            _sportsmen[j] = _sportsmen[j + 1];
                            _sportsmen[j + 1] = temp;
                        }
                    }
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group res = new Group("Финалисты");
                res.Add(group1);
                res.Add(group2);
                res.Sort();
                return res;
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {Name}");
                foreach (Sportsman s in _sportsmen) // Явно указываем тип Sportsman
                {
                    s.Print();
                }
                Console.WriteLine();
            }
        }
    }
}
