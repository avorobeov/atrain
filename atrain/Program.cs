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
            Atrain atrain = null;

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
                        dispatcher.PreparingTrainDeparture(out atrain);
                        break;

                    case "3":
                        dispatcher.TrainDispatch(atrain);
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

    class Atrain
    {
        public string StartingPoint { get; private set; }

        public string EndPoint { get; private set; }

        public int NumberWagons { get; private set; }

        public Atrain(string startingPoint, string endPoint, int numberWagons)
        {
            StartingPoint = startingPoint;
            EndPoint = endPoint;
            NumberWagons = numberWagons;
        }

        public void StartMoving()
        {
            Console.WriteLine("Поезд начал движение", ConsoleColor.Red);
        }
    }

    class Dispatcher
    {
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
        }

        public int NumberPassengers()
        {
            Random random = new Random();

            int maximumTicketsSold = 300;
            int minimumTicketsSold = 20;

            _numberTicketsSold = random.Next(minimumTicketsSold, maximumTicketsSold);
      
            return _numberTicketsSold;
        }

        public void PreparingTrainDeparture(out Atrain atrain)
        {
            _numberWagons = CalculationNumberWagons();

            atrain = new Atrain(_startingPoint, _endPoint, _numberWagons);

            ShowMessage($"\n\n\nНаправление {_startingPoint}-{_endPoint}\nКоличество проданных билетов: {_numberTicketsSold} \nКоличество вагонов в поезде {_numberWagons}\n\n\n\nПоезд готов к отправке !!!", ConsoleColor.Green);
        }

        public void TrainDispatch(Atrain atrain)
        {
            if (_startingPoint != null && _endPoint != null && _numberWagons != 0)
            {
                atrain.StartMoving();
            }

            ShowMessage("\nПоезд не готов к отправке\n", ConsoleColor.Red);
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
            int numberPassengers = NumberPassengers();
            int maximumNumberSeatsCarriage = 20;
        
            return (int)Math.Ceiling((double)numberPassengers / maximumNumberSeatsCarriage);
        }
    }
}
