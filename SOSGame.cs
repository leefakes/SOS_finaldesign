namespace GamesEngine
{

    class SosGame : Game
    {
        public override void LoadConfig()
        {
            rows = 3;
            cols = 3;
            numOfPlayers = 2;
            validPieces = new char[] {'S','O'} ;
        }
        public override string GetGameHelp()
        {
            string welcomeHelp;
            welcomeHelp = "Welcome to the SOS Game!\n";
            welcomeHelp += "Press F2 to save your progress\n";
            welcomeHelp += "Press F5 to load your game\n";
            return welcomeHelp;
        }

        public override void ChoosePiece(Player player)
        // Player chooses piece they wish to play on the board
        {
            string validation = new string(validPieces);
            string input = "_";
            Random randomPiece = new Random();

            if (player.isHuman)
            {
                Console.WriteLine();
                while (!validation.Contains(input))
                {
                    Console.Write($"{player.name} - please enter one of these pieces ");
                    for (int i = 0; i < validPieces.GetLength(0); i++)
                    {
                        Console.Write($"'{validPieces[i]}' ");
                    }
                    Console.Write(": ");
                    input = Console.ReadLine().ToUpper();
                }
                player.piece = input;
            }
            else 
            {
                player.piece = validPieces[randomPiece.Next(validPieces.GetLength(0))].ToString();
            }
        }

        public override int CheckWinner(Player player)
        // Checks after each turn is made if a win, lose or draw condition has been met
        // Return -1 for continue play, 0 for a draw and 1 for a win 
        {
            // Check rows for match
            for (int row = 0; row < this.rows; row++)
            {
                if (_board[row, 0] == "S" && _board[row, 1] == "O" && _board[row, 2] == "S")
                {
                    // Return winner condition
                    return 1;
                }
            }

            // Check columns for match
            for (int col = 0; col < this.cols; col++)
            {
                if (_board[0,col] == "S" && _board[1,col] == "O" && _board[2,col] == "S")
                {
                    // Return winner condition
                    return 1;
                }
            }

            // Check diagonals for match
            if ((_board[0,0] == "S" && _board[1,1] == "O" && _board[2,2] == "S") || (_board[0,2] == "S" && _board[1,1] == "O" && _board[2,0] == "S"))
            {
                return 1;
            }

            // Check board still has moves available
            if (BoardMovesAvailable())
            {
                // Return continue condition
                return -1;
            }
            else
            {
                // Return draw condition
                return 0;
            }

        }
    }
}