using System;

namespace learn_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Band newBand = new Band();
            Console.WriteLine("What is the name of your band?");
            newBand.Name = Console.ReadLine();
            
            var repeat = true;
            while(repeat)
            {

                Console.WriteLine("Add, Announce,  or Quit?");
                var action = Console.ReadLine();

                switch(action.ToUpper()) 
                {
                    case "ADD":
                        newBand.AddMusician();
                        break;
                    case "ANNOUNCE":
                        newBand.Announce();
                        break;
                    case "QUIT":
                    default:
                        repeat = false;
                        break;
                }

            }
        }

       
    }
}
