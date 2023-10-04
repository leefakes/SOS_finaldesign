using System;

namespace GamesEngine
{

    public class Player
    {
        public string name = "";
        public string piece = "";
        // public List<Piece> listOfPieces = new();
        public bool isHuman = true;

        // public void AddPlayerPiece()
        // {
        //     Piece playerPiece = new();
        //     listOfPieces.Add(playerPiece);
        // }

        public void SetPlayerDetails(int playerNumber)
        // Gets and sets player names and isHuman value
        {
            Console.Write("Please enter a name for player {0} or leave blank for AI: ", playerNumber);
            {
                this.name = Console.ReadLine();
                if (String.IsNullOrEmpty(this.name))
                {
                    this.isHuman = false;
                    this.name = "Player " + playerNumber + "(AI)";
                }
            }
        }
    }
}