using System;

    class Musician
    {
        public string Name;
        public string Instrument;

        internal void Announce() {
            Console.WriteLine($"{Name} on the {Instrument}!");
        }
    }
