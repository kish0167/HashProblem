using System.Security.Cryptography;
using System.Text;

namespace HashProblem;

public static class RockPaperScissors
{
    private static readonly string[] Moves = { "rock", "paper", "scissors" };

    private static readonly int[][] ResultTable =
    [
        [0, -1, 1],
        [1, 0, -1],
        [-1, 1, 0],
    ];
    public static void PlayRounds()
    {
        while (true)
        {
            byte[] keyBytes = RandomNumberGenerator.GetBytes(32);
            int computerMove = RandomNumberGenerator.GetInt32(3);
            ShowHashToUser(computerMove, keyBytes);
            int userMove = GetUserMove();
            ShowGameResultMessage(computerMove, userMove);
            ShowKeyToUser(keyBytes);
            if (UserWantToLeave()) return;
        }
    }

    private static void ShowHashToUser(int computerMove, byte[] keyBytes)
    {
        using HMACSHA3_256 hmac = new HMACSHA3_256(keyBytes);
        Console.Write("\n\nLet's play!\nMy next move is hashed here -> ");
        string computerMoveString = Moves[computerMove];
        Console.WriteLine(BytesArrayToSting(hmac.ComputeHash(Encoding.UTF8.GetBytes(computerMoveString))));
    }

    private static string BytesArrayToSting(byte[] array)
    {
        return String.Join("", array.ToList().Select(b => b.ToString("x2")));
    }

    private static int GetUserMove()
    {
        string? userChoice;
        do
        {
            Console.WriteLine("Input your move:\n0 - rock\n1 - paper\n2 - scissors");
            userChoice = Console.ReadLine();
        }
        while (!Int32.TryParse(userChoice, out int move) || move < 0  || move > 2) ;
        return Int32.Parse(userChoice);
    }

    private static void ShowGameResultMessage(int computerMove, int userMove)
    {
        int result = ResultTable[userMove][computerMove];
        switch (result)
        {
            case 0:
                Console.WriteLine($"We both choose {Moves[computerMove]}");
                break;
            case -1:
                Console.WriteLine($"My move was {Moves[computerMove]} - i won!");
                break;
            case 1:
                Console.WriteLine($"My move was {Moves[computerMove]} - you won!");
                break;
        }
    }

    private static void ShowKeyToUser(byte[] key)
    {
        Console.WriteLine("You can check my move with this  HMACSHA3_256 key -> "
            + BytesArrayToSting(key));
    }

    private static bool UserWantToLeave()
    {
        Console.WriteLine("Type 'exit' to exit or anything else to continue");
        string? userChoice = Console.ReadLine();
        return !string.IsNullOrEmpty(userChoice) && string.Equals(userChoice, "exit");
    }
}