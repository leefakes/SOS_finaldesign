namespace GamesEngine
{

    class TicTacToeGame : Game
    {
        private int matchLength;
        public override void LoadConfig()
        {
            this.rows = 9;
            this.cols = 9;
            this.numOfPlayers = 2;
            this.validPieces = new char[] {'X','O'} ;
            this.matchLength = 4;
        }

        public override string GetGameHelp()
        {
            return "Welcome to the TicTacToe Game!";
        }

        public override void ChoosePiece(Player player)
        // Player chooses piece they wish to play on the board
        {
            player.piece = validPieces[(currentPlayerIndex%2)].ToString();
        }

        public override int CheckWinner(Player player)
        // Checks after each turn is made if a win, lose or draw condition has been met
        {

            // Match the rows to the player.piece and find the matching length of pieces
            int matchCounter;
            int _col;
            int _row;

            // Check each row
            for (_row = 0; _row < this.rows; _row++)
            {
                matchCounter = 0;
                for (_col = 0; _col < this.cols; _col++)
                {
                    if (_board[_row, _col] == player.piece)
                    {
                        ++matchCounter;
                        if (matchCounter == this.matchLength)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        matchCounter = 0;
                    };
                }
            }

            // Check each column
            for (_col = 0; _col < this.cols; _col++)
            {
                matchCounter = 0;
                for (_row = 0; _row < this.rows; _row++)
                {
                    if (_board[_row, _col] == player.piece)
                    {
                        ++matchCounter;
                        if (matchCounter == this.matchLength)
                        {
                            return 1;
                        }
                    }
                    else
                    {
                        matchCounter = 0;
                    };
                }
            }

            // Check diagonals left to right
            // This first checks that the diagonal row is long enough to have a matching
            // set of the players piece. Then calls a helper function to perform the 
            // count.
            for (_col = 0; (this.cols - _col) >= this.matchLength; _col++)
            {
                if (CountLeftDiagonalMatches(0, _col, this.cols - _col, player.piece))
                {
                    return 1;
                }
            }
            for (_row = 1; (this.rows - _row) >= this.matchLength; _row++)
            {
                if (CountLeftDiagonalMatches(_row, 0, this.rows - _row, player.piece))
                {
                    return 1;
                }
            }


            // Check diagonals right to left
            // This first checks that the diagonal row is long enough to have a matching
            // set of the players piece. Then calls a helper function to perform the 
            // count.
            for (_col = this.cols - 1; (this.cols - _col) <= this.matchLength; _col--)
            {
                if (CountRightDiagonalMatches(0, _col, _col + 1, player.piece))
                {
                    return 1;
                }
            }

            for (_row = 1; (this.rows - _row) >= this.matchLength; _row++)
            {
                if (CountRightDiagonalMatches(_row, this.rows - 1, (this.rows - _row), player.piece))
                {
                    return 1;
                }
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
        public bool CountLeftDiagonalMatches(int row, int col, int length, string piece)
        {
            int matchCounter = 0;

            for (int _step = 0; _step < length; _step++, row++, col++)
            {
                if (_board[Math.Min(row,this.rows-1), Math.Min(col,this.cols-1)] == piece)
                {
                    ++matchCounter;
                    if (matchCounter == this.matchLength)
                    {
                        return true;
                    }
                }
                else
                {
                    matchCounter = 0;
                };
            }

            return false;
        }
        public bool CountRightDiagonalMatches(int row, int col, int length, string piece)
        {
            int matchCounter = 0;

            for (int _step = 0; _step < length; _step++, row++, col--)
            {
                if (_board[Math.Min(row,this.rows-1), Math.Min(col,this.cols-1)] == piece)
                {
                    ++matchCounter;
                    if (matchCounter == this.matchLength)
                    {
                        return true;
                    }
                }
                else
                {
                    matchCounter = 0;
                }
            }

            return false;
        }

    }
}