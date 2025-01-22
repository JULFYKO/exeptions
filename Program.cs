using System;
using System.Text.RegularExpressions;

namespace _11_Exception
{
    public class InvalidCreditCardException : Exception
    {
        public InvalidCreditCardException(string message) : base(message) { }
    }

    public class CreditCard
    {
        public string CardHolderName { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public string CVV { get; private set; }

        public CreditCard(string cardHolderName, string cardNumber, DateTime expirationDate, string cvv)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CVV = cvv;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(CardHolderName))
                throw new InvalidCreditCardException("Card holder name cannot be empty.");

            if (!Regex.IsMatch(CardNumber, "^\\d{16}$"))
                throw new InvalidCreditCardException("Card number must be a 16-digit number.");

            if (ExpirationDate < DateTime.Now)
                throw new InvalidCreditCardException("Card expiration date must be in the future.");

            if (!Regex.IsMatch(CVV, "^\\d{3}$"))
                throw new InvalidCreditCardException("CVV must be a 3-digit number.");
        }

        public override string ToString()
        {
            return $"Card Holder: {CardHolderName}, Card Number: **** **** **** {CardNumber[^4..]}, Expiration: {ExpirationDate:MM/yy}, CVV: ***";
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter card holder name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter card number (16 digits):");
                string number = Console.ReadLine();

                Console.WriteLine("Enter expiration date (MM/yyyy):");
                string expDateInput = Console.ReadLine();
                DateTime expDate = DateTime.ParseExact(expDateInput, "MM/yyyy", null);

                Console.WriteLine("Enter CVV (3 digits):");
                string cvv = Console.ReadLine();

                CreditCard card = new CreditCard(name, number, expDate, cvv);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Credit card created successfully:");
                Console.WriteLine(card);
                Console.ResetColor();
            }
            catch (InvalidCreditCardException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
