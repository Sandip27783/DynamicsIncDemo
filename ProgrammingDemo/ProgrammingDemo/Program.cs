
using ProgrammingDemo;

// Ask user to choose for which operation they want to move ahead.
Console.WriteLine("Please enter your choice:");
Console.WriteLine("1 : PrimeMode");
Console.WriteLine("2 : EncryptionMode");

var userInput = Console.ReadLine();
int userChoice;

if (!Int32.TryParse(userInput, out userChoice))
{
    Console.WriteLine("Incorrect choice.");
}
else
{
    if (userChoice == 1)
    {
        var primeMode = new PrimeMode(1, 5);
        primeMode.Execute();

        primeMode = new PrimeMode(7, 11);
        primeMode.Execute();

        primeMode = new PrimeMode(1, 11);
        primeMode.Execute();

        primeMode = new PrimeMode(1, 1000);
        primeMode.Execute();
    }
    else if (userChoice == 2)
    {
        var encryptionMode = new EncryptionMode();
        encryptionMode.Execute();
    }
    else
    {
        Console.WriteLine("Incorrect choice!");
    }

    Console.WriteLine("Press any key to continue.");
    Console.ReadLine();
}