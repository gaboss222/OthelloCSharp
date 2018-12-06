namespace OthelloAlainGabriel
{
    public class Player
    {
        string Name { get; }
        public Token Token { get; set; }
        int NbTokenRestant { get; set; }

        public Player(Token t, string name)
        {
            Token = t;
            NbTokenRestant = 30;
            Name = name;
        }

        public void LeftToken(int nbToken)
        {
            NbTokenRestant -= nbToken;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}