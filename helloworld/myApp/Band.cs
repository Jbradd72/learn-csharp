using System;
using System.Collections.Generic;

    class Band
    {
        public string Name;
        public List<Musician> Musicians = new List<Musician>();

        public void Announce() {
            Console.WriteLine($"Welcome {Name} to the stage!");
            foreach (Musician m in Musicians)
            {
                m.Announce();
            }
        }

        public void AddMusician()
        {
            Console.WriteLine("What is the name of the musician to be added?");
            var name = Console.ReadLine();
            Console.WriteLine("What instrument does " +name + " play?");
            var instrument = Console.ReadLine();
            AddMusician(name, instrument);
        }

        public void AddMusician(string name, string instrument)
        {
            Musician musician = new Musician();
            musician.Name = name;
            musician.Instrument = instrument;
            Musicians.Add(musician);
        }
    }
