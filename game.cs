namespace GamesEngine
{

    abstract class Game
    {
        // Properties
        public string[,] _board;
        public bool gameOver = false;
        public int currentPlayerIndex = -1;
        public int numOfPlayers;
        public int cols;
        public int rows;
        public char[] validPieces = new char[] {'X','O'} ;
        public List<Player> listOfPlayers = new();
       
        // Methods
        public abstract string GetGameHelp();
        // Returns a help string for display at the top of the game
        public abstract void ChoosePiece(Player player);
        // Select piece for player to play
        // public abstract void MakeMove(Player player);
        // Load game configuration
        public abstract void LoadConfig();
        // Load game configuration
        public abstract int CheckWinner(Player player);
        // Checks after each turn is made if a win, lose or draw condition has been met
        // Return -1 for continue play, 0 for a draw and 1 for a win 
        public void LoadPlayers(int numOfPlayers)
        {
            // Generate a player object for the number of players required
            for (int p = 0; p < numOfPlayers; p++)
            {
                Player player = new Player();
                player.SetPlayerDetails(p + 1);
                listOfPlayers.Add(player);
            }
        }
        public void StartGame()
        // Responsible for the game logic
        {
            LoadConfig();
            DisplayHelp(GetGameHelp());
            LoadPlayers(numOfPlayers);
            InitGameBoard();
            DrawBoard();

            int gameStatus = -1;
            while (gameStatus == -1) {
                currentPlayerIndex = NextPlayer();
                Player player = listOfPlayers[currentPlayerIndex];
                ChoosePiece(player);
                MakeMove(player);
                DrawBoard();
                gameStatus = CheckWinner(player);
            }

            DisplayGameResult(gameStatus);
        }
        public void InitGameBoard()
        {
            _board = new string[this.rows, this.cols];

            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.cols; col++)
                {
                    _board[row, col] = " ";
                }
            }
        }

        public bool BoardMovesAvailable()
        {
            for (int row = 0; row < this.rows; row++)
            {
                for (int col = 0; col < this.cols; col++)
                {
                    if (_board[row, col] == " ")
                    {
                        return true;
                    };
                }
            }
            return false;
        }
        public void DisplayGameResult(int gameStatus)
        {
            if (gameStatus == 1)
            {
                Console.WriteLine($"{listOfPlayers[currentPlayerIndex].name} won!!!");
            }
            else
            {
                Console.WriteLine("Draw - No more moves available");
            }
        }

        public void SaveGame()
        // Responsible for saving the game on command
        {
            using (StreamWriter writer = new StreamWriter("saved_game.txt"))
            {
                // writer.WriteLine(Player1);
                // writer.WriteLine(Player2);
                // writer.WriteLine(currentPlayerIndex);
                // writer.WriteLine(movesCount);

                // for (int i = 0; i < 3; i++)
                // {
                //     for (int j = 0; j < 3; j++)
                //     {
                //         writer.Write(board[i, j]);
                //     }
                //     writer.WriteLine();
                // }
            }
        }

        public void LoadGame()
        // Responsible for loading the game from a file
        {
            // 
        }

        public int NextPlayer()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex >= numOfPlayers)
            {
                currentPlayerIndex = 0;
            }
            return currentPlayerIndex;
        }

        public void DisplayHelp(string helpText)
        {
            // Display help on how to play the game
            Console.Clear();
            Console.WriteLine(helpText);
        }

        private void DrawBoardHeaderRow()
        {
            int column;
            string position;
            string rowToWrite;

            int index = 0;
            foreach (Player player in listOfPlayers)
            {
                index++;
                Console.WriteLine($"Player {index} - {player.name} - [{player.piece}]");
            }

            Console.WriteLine("");
            rowToWrite = "    ";

            for (int col = 0; col < this.cols; col++)
            {
                column = col + 1;
                // Pad the column number to balance space
                switch (column)
                {
                    case > 99:
                        position = "" + (col+1).ToString() + "";
                        break;
                    case > 9:
                        position = "" + (col+1).ToString() + " ";
                        break;
                    default:
                        position = " " + (col+1).ToString() + " ";
                        break;
                }
                rowToWrite += position + " ";
            }
            Console.WriteLine(rowToWrite);
        }

        public void DrawBoard()
        {
            String spacer;
            String rowToWrite;

            Console.Clear();

            DrawBoardHeaderRow();

            for (int row = 0; row < this.rows; row++)
            {
                // Pad the column number to balance space
                switch (row+1)
                {
                    case > 99:
                        spacer = "";
                        break;
                    case > 9:
                        spacer = " ";
                        break;
                    default:
                        spacer = "  ";
                        break;
                }
                rowToWrite = spacer + (row+1) + " ";
                for (int col = 0; col < this.cols; col++)
                {
                    rowToWrite += " " + _board[row, col] + " ";
                    if (col < this.cols -1)
                    {
                        rowToWrite += "║";
                    }
                }
                Console.WriteLine(rowToWrite);

                if (row < this.rows - 1)
                {
                    rowToWrite = "    ";
                    for (int col = 0; col < this.cols; col++)
                    {
                        rowToWrite += "═══";
                        if (col < this.cols -1)
                        {
                            rowToWrite += "╬";
                        }
                    }
                    Console.WriteLine(rowToWrite);
                }
            }
        }

        public bool ValidMove(int checkRow, int checkCol)
        {
            // Determine if position empty
            return this._board[checkRow, checkCol] == " ";
        }
        public virtual void MakeMove(Player player)
        // Allows player to make move on the board
        {
            // Set method variables
            Random randomColRow = new Random();
            bool isValidMove = false;
            int chosenCol;
            int chosenRow;

            // Loop until validmove
            while (!isValidMove)
            {
                if (player.isHuman)
                {
                    Console.Write($"{player.name} - please enter the row: ");
                    if (!int.TryParse(Console.ReadLine(), out chosenRow))
                    {
                        Console.WriteLine();
                        Console.Write($"{player.name} - please enter the row: ");
                        Console.Read();
                        continue;
                    }
                    Console.Write($"{player.name} - please enter the column: ");
                    if (!int.TryParse(Console.ReadLine(), out chosenCol))
                    {
                        Console.WriteLine();
                        Console.Write($"{player.name} - please enter the column: ");
                        Console.Read();
                        continue;
                    }

                    Console.WriteLine($"Row: {chosenRow}");
                    Console.WriteLine($"Col: {chosenCol}");

                    // Convert human selected values to array index values
                    --chosenRow;
                    --chosenCol;
                } 
                else
                {
                    // Chance AI  random row col selection
                    chosenRow = randomColRow.Next(this.rows);  // creates a random row
                    chosenCol = randomColRow.Next(this.cols);  // creates a random col
                }

                // Check if board space is free
                if (ValidMove(chosenRow,chosenCol))
                {
                    _board[chosenRow,chosenCol] = player.piece;
                    isValidMove = true;
                } else {
                    if (player.isHuman)
                    {
                        Console.WriteLine("This position has already been taken please try again!");
                    }
                }
            }
        }
    }
}
