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
                        dispatcher.ToCreateDirection();
                        break;

                    case "2":
                        dispatcher.ToPrepareTrain();
                        break;

                    case "3":
                        dispatcher.ToSendTrain();
                        break;

                    case "4":
                        isExit = true;
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\nСоздать направление нажмите 1\n" +
                              "\nСформировать поезд 2\n" +
                              "\nОтправить поезд 3\n" +
                              "\nДля выхода нажмите 4\n");
        }
    }

    class Train
    {
        public string StartingPoint { get; private set; }

        public string EndPoint { get; private set; }

        public int NumberWagons { get; private set; }

        public int TrainNumber { get; private set; }

        private int _maximumRandomNumber = 100000;

        public Train(string startingPoint, string endPoint, int numberWagons)
        {
            Random random = new Random();

            StartingPoint = startingPoint;
            EndPoint = endPoint;
            NumberWagons = numberWagons;

            TrainNumber = random.Next(0, maximumRandomNumber);
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

        public void ToCreateDirection()
        {
            ShowMessage("Ведите куда следует поезд", ConsoleColor.Cyan);
            _startingPoint = Console.ReadLine();

            ShowMessage("Ведите от куда следует поезд", ConsoleColor.Cyan);
            _endPoint = Console.ReadLine();
        }

        public void ToPrepareTrain()
        {
            _numberWagons = CalculationNumberWagons();

            _trains.Add(new Train(_startingPoint, _endPoint, _numberWagons));

            ShowMessage($"\n\n\nНаправление {_startingPoint}-{_endPoint}\nКоличество проданных билетов: {_numberTicketsSold} \nКоличество вагонов в поезде {_numberWagons}\n\nНомер  поезда [{_trains.LastOrDefault().TrainNumber}]\n\n\n\nПоезд готов к отправке !!!", ConsoleColor.Green);
        }

        public void ToSendTrain()
        {
            if (_startingPoint != null && _endPoint != null && _numberWagons != 0 && _trains.Count != 0)
            {
                _trains.LastOrDefault().StartMoving();
                _trains.Remove(_trains.Last());
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
