using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharpTestProject
{
    public class HashOfStringValue : IStartable
    {
        public void Start()
        {
            string text1 = "jakis tekst";
            string text2 = "jakis inny tekst";
            string text3 = "jakis tekst";

            Console.WriteLine($"tekst1 = {text1}\t hash = {GetHashOfStringValue(text1)}");
            Console.WriteLine($"tekst1 = {text1}\t hash = {GetHashOfStringValue(text1)}");
            Console.WriteLine($"tekst2 = {text2}\t hash = {GetHashOfStringValue(text2)}");
            Console.WriteLine($"tekst3 = {text3}\t hash = {GetHashOfStringValue(text3)}");
        }

        private string GetHashOfStringValue(string text)
        {
            string hashValueString = String.Empty;

            try
            {
                using var hash = MD5.Create();

                byte[] stringValue = Encoding.UTF8.GetBytes(text.Trim());
                byte[] hashValueBytes = hash.ComputeHash(stringValue);
                hashValueString = BitConverter.ToString(hashValueBytes).Replace("-", String.Empty).Trim().ToUpper();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                hashValueString = "Error";
            }

            return hashValueString;
        }
    }
}
