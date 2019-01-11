namespace OthelloAlainGabriel
{
    /// <summary>
    /// Class who represents a player
    /// Player has a name, number and token (black/white)
    /// </summary>
    public class Player
    {
        public string Name { get;}
        public int Number { get; }
        public Token Token { get; set; }

        public int Score { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t">Player as a token</param>
        /// <param name="name">Player as a name</param>
        /// <param name="number">Player as a number (1 or 2)</param>
        public Player(Token t, string name, int number)
        {
            Token = t;
            Name = name;
            Number = number;
            Score = 0;
        }

        /// <summary>
        /// Override ToString() to return Name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}