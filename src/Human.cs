using System;

namespace CSharpTestProject
{
    public class Human : IStartable
    {
        public string Name
        {
            get => _name;
            set
            {
                if (String.IsNullOrEmpty(value)) return;
                _name = value;
            }
        }
        public string Surname { get; set; }
        public string PersonalID { get; init; }

        private string _name;

        public Human()
        {
            Console.WriteLine("I'm alive!!!!!!11!1!!111!");
        }

        public Human(string name, string surname, string personalID)
            : this()
        {
            this.Name = name;
            this.Surname = surname;
            this.PersonalID = personalID;
        }

        private string GetHumanInfo()
        {
            return $"Imie = {Name}, nazwisko = {Surname}  (id = {PersonalID})";
        }

        public void Start()
        {
            var czlek1 = new Human("Marek", "Hucz", "1234");
            var czlek2 = new Human
            {
                Name = "Janek",
                Surname = "Hucz",
                PersonalID = "4321"
            };
            Console.WriteLine(czlek1.GetHumanInfo());

            czlek1.Name = "Rafał";
            czlek1.Surname = "Nierafał";

            Console.WriteLine(czlek1.GetHumanInfo());
        }
    }
}
