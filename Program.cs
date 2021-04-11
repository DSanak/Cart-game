using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bogus;
using Bogus.DataSets;


//Zasady:
// 1 -> 10, 5 -> 5
// -100 pkt. jesli w 1 jest 0
// 2 2 2 -> 20 pkt.
// 3 3 3 -> 30 pkt.
// ...
// 1 1 1 -> 100 pkt.
// ---------------


namespace Grawkosci
{

    class RollGenerator
    {
        private static Random rnd = new Random();
        public int Roll() => rnd.Next(1, 7);
    }

    class Dice
    {
        private readonly RollGenerator _rollGenerator;

        public int Value { get; private set; }//wartosc która wylosujemy
        public int GameValue => Value switch
        {
            1 => 10,
            5 => 5,
            _ => 0
        };  //liczba punktow z losu

        public Dice(RollGenerator rollGenerator)
        {
            _rollGenerator = rollGenerator;

        }

        public void Roll()
        {
            Value = _rollGenerator.Roll();
        }
    }



    class Program
    {

        static IEnumerable  <Dice> Generator(int n)
        {
            for (int i = 0; i < n; i++)
            {
                yield return new Dice(new RollGenerator());

            }
        }



        static void Main(string[] args)
        {
            var freeDice = 5; //wolne kostki
            var totalPoints = 0;

            var roundPoints = 0;

            var rzut = 0;

           

            do
            {

                Console.Write("Rudna: {0}", rzut++);
                Console.WriteLine("");

                var dices = Generator(freeDice).ToArray();



                foreach (var dice in dices)
                {
                    dice.Roll();
                }



                Console.WriteLine(String.Join(" ", dices.Select(x => x.Value)));
                Console.WriteLine(String.Join(" ", dices.Select(x => x.GameValue)));






                var point = dices.Sum(x => x.GameValue);

                
                
                roundPoints += point;
          
                //Zasady cdn.
                if (rzut == 1 && point == 0)
                    {
                        roundPoints -= 100;
                        Console.WriteLine("Wooow");
                    }




             



                Console.WriteLine("Points: {0}", point);
                    Console.WriteLine("Round Points: {0}", roundPoints);

                    freeDice = dices.Count(x => x.GameValue == 0);

                    if (freeDice == 0)
                        freeDice = 5;

                    if (point == 0)
                    {
                        roundPoints = 0;
                        break;
                    }

                    Console.WriteLine("czy chcesz kontynuować?");

                


                var consoleKey = Console.ReadKey();

                    if (consoleKey.Key == ConsoleKey.N)
                        break;

                } while (true) ;

                totalPoints += roundPoints;

                Console.WriteLine("Ogólna liczba punktow {0}", totalPoints);



            }
    }
}
