using System;
using System.Collections.Generic;

class CurrencyTradingSystem
{
    private Dictionary<string, decimal> exchangeRates;
    private Dictionary<string, decimal> userBalance;

    public CurrencyTradingSystem()
    {
        exchangeRates = new Dictionary<string, decimal>
        {
            {"USD", 1.0m},
            {"EUR", 0.85m},
            {"GBP", 0.73m},
            {"JPY", 110.33m}
        };

        userBalance = new Dictionary<string, decimal>
        {
            {"USD", 1000m},
            {"EUR", 0m},
            {"GBP", 0m},
            {"JPY", 0m}
        };
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Currency Trading System ---");
            Console.WriteLine("1. View Balance");
            Console.WriteLine("2. View Exchange Rates");
            Console.WriteLine("3. Buy Currency");
            Console.WriteLine("4. Sell Currency");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewBalance();
                    break;
                case "2":
                    ViewExchangeRates();
                    break;
                case "3":
                    BuyCurrency();
                    break;
                case "4":
                    SellCurrency();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void ViewBalance()
    {
        Console.WriteLine("\nYour current balance:");
        foreach (var currency in userBalance)
        {
            Console.WriteLine($"{currency.Key}: {currency.Value}");
        }
    }

    private void ViewExchangeRates()
    {
        Console.WriteLine("\nCurrent exchange rates (base: USD):");
        foreach (var rate in exchangeRates)
        {
            Console.WriteLine($"{rate.Key}: {rate.Value}");
        }
    }

    private void BuyCurrency()
    {
        Console.Write("Enter the currency you want to buy: ");
        string buyCurrency = Console.ReadLine().ToUpper();

        Console.Write("Enter the amount you want to buy: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal buyAmount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        if (!exchangeRates.ContainsKey(buyCurrency))
        {
            Console.WriteLine("Invalid currency.");
            return;
        }

        decimal costInUSD = buyAmount / exchangeRates[buyCurrency];

        if (userBalance["USD"] >= costInUSD)
        {
            userBalance["USD"] -= costInUSD;
            userBalance[buyCurrency] += buyAmount;
            Console.WriteLine($"Successfully bought {buyAmount} {buyCurrency}");
        }
        else
        {
            Console.WriteLine("Insufficient USD balance.");
        }
    }

    private void SellCurrency()
    {
        Console.Write("Enter the currency you want to sell: ");
        string sellCurrency = Console.ReadLine().ToUpper();

        Console.Write("Enter the amount you want to sell: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal sellAmount))
        {
            Console.WriteLine("Invalid amount.");
            return;
        }

        if (!exchangeRates.ContainsKey(sellCurrency))
        {
            Console.WriteLine("Invalid currency.");
            return;
        }

        if (userBalance[sellCurrency] >= sellAmount)
        {
            decimal valueInUSD = sellAmount * exchangeRates[sellCurrency];
            userBalance[sellCurrency] -= sellAmount;
            userBalance["USD"] += valueInUSD;
            Console.WriteLine($"Successfully sold {sellAmount} {sellCurrency}");
        }
        else
        {
            Console.WriteLine($"Insufficient {sellCurrency} balance.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        CurrencyTradingSystem system = new CurrencyTradingSystem();
        system.Run();
    }
}
