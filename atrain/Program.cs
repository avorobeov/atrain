using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atrain
{
    class Program
    {
        static void Main(string[] args)
        {
            Dispatcher dispatcher = new Dispatcher();

            string userInput;
            bool isExit = false;

            while (isExit == false)
            {
                ShowMenu();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        dispatcher.CreateDirection();
                        break;

                    case "2":
                        dispatcher.SendTrain();
                        break;

                    case "3":
                        isExit = true;
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nСформировать поезд 1\n" +
                              "\nОтправить поезд 2\n" +
                              "\nДля выхода нажмите 3\n");
        }
    }

    class Train
    {
        private int _maximumRandomNumber = 100000;

        public string StartingPoint { get; private set; }

        public string EndPoint { get; private set; }

        public int NumberWagons { get; private set; }

        public int Number { get; private set; }

        public Train(string startingPoint, string endPoint, int numberWagons)
        {
            Random random = new Random();

            StartingPoint = startingPoint;
            EndPoint = endPoint;
            NumberWagons = numberWagons;

            Number = random.Next(0, _maximumRandomNumber);
        }

        public void StartMoving()
        {
            Console.WriteLine($"\nНомер  поезда [{TrainNumber}]\nПоезд начал движение", ConsoleColor.Red);
        }
    }

    class Dispatcher
    {
        private List<Train> _trains = new List<Train>();

        private string _startingPoint;
        private string _endPoint;

        private int _numberWagons;
        private int _numberTicketsSold;

        public void CreateDirection()
        {
            ShowMessage("Ведите куда следует поезд", ConsoleColor.Cyan);
            _startingPoint = Console.ReadLine();

            ShowMessage("Ведите от куда следует поезд", ConsoleColor.Cyan);
            _endPoint = Console.ReadLine();

            _numberWagons = CalculationNumberWagons();

            _trains.Add(new Train(_startingPoint, _endPoint, _numberWagons));

            ShowMessage($"\n\n\nНаправление {_startingPoint}-{_endPoint}\nКоличество проданных билетов: {_numberTicketsSold} \nКоличество вагонов в поезде {_numberWagons}\n\nНомер  поезда [{_trains.LastOrDefault().TrainNumber}]\n\n\n\nПоезд готов к отправке !!!", ConsoleColor.Green);
        }

        public void SendTrain()
        {
            if (_startingPoint != null && _endPoint != null && _numberWagons != 0 && _trains.Count != 0)
            {
                _trains[0].StartMoving();
                _trains.RemoveAt(0);
            }
            else
            {
                ShowMessage("\nПоезд не готов к отправке\n", ConsoleColor.Red);
            }

        }

        private void ShowMessage(string message, ConsoleColor color)
        {
            ConsoleColor preliminaryColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);

            Console.ForegroundColor = preliminaryColor;
        }

        private int CalculationNumberWagons()
        {
            int numberPassengers = ToNumberPassengers();
            int maximumNumberSeatsCarriage = 20;
        
            return (int)Math.Ceiling((double)numberPassengers / maximumNumberSeatsCarriage);
        }

        private int ToNumberPassengers()
        {
            Random random = new Random();

            int maximumTicketsSold = 300;
            int minimumTicketsSold = 20;

            _numberTicketsSold = random.Next(minimumTicketsSold, maximumTicketsSold);

            return _numberTicketsSold;
        }
    }
}
