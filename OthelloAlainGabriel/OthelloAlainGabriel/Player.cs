namespace OthelloAlainGabriel
{
    public class Player
    {
        public string Name { get;}
        public int Number { get; }
        public Token Token { get; set; }
        public int Score { get; set; }

        public Player(Token t, string name, int number)
        {
            Token = t;
            Name = name;
            Number = number;
            Score = 0;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}