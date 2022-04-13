using System;

namespace CSharpTestProject
{
    public class StringToTitle : IStartable
    {
        private string ToTitle(string oldName)
        {
            string name = String.Empty;
            foreach (string w in oldName.Split('_'))
            {
                name += String.Concat(w[0].ToString().ToUpper(), w.AsSpan(1), " ");
            }
            return name.Trim();
        }

        public void Start()
        {
            string oldName = "Lost_legacy_lala";
            string newName = ToTitle(oldName);

            Console.WriteLine($"Name = {newName}");
        }
    }
}
