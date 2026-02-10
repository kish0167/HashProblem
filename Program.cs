using System.Security.Cryptography;
using System.Text;
using HashProblem;

string? userChoice = "";

while (userChoice != "n")
{
    userChoice = "";
    RockPaperScissors.PlayRound();
    Console.WriteLine("\nContinue? (y/n)");
    while (userChoice != "n" && userChoice != "y") 
    {
        userChoice = Console.ReadLine();
    }
}



