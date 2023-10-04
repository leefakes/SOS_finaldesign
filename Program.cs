namespace GamesEngine

{
    class GameSelect
    // Allows selection of game to play and starts it
    {
        static void Main(string[] args)
        {
            int gameToPlay = 0;

            while (gameToPlay != 4)
            {
                Console.Clear();
                Console.WriteLine("Enter the number of the game you wish to play");
                Console.WriteLine("1 - SOS");
                Console.WriteLine("2 - Tic Tac Toe");
                Console.WriteLine("3 - Connect 4");
                Console.WriteLine("4 - Quit");

                string input = Console.ReadLine();
                bool success = int.TryParse(input, out gameToPlay);
                while (!success)
                {
                    Console.WriteLine("Invalid Input. Try again...");
                    input = Console.ReadLine();
                    success = int.TryParse(input, out gameToPlay);
                }

                switch (gameToPlay)
                {
                    case 1:
                        SosGame game1 = new();
                        game1.StartGame();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                    case 2:
                        TicTacToeGame game2 = new();
                        game2.StartGame();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Connect4Game game3 = new();
                        game3.StartGame();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine(">>>>>>>>>>>>>> Thanks for playing <<<<<<<<<<<<<<<<");
                        break;
                }
            }
        }
    }
}