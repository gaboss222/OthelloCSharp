namespace OthelloAlainGabriel
{
    public class Player
    {
        public Token Token { get; set; }
        int NbTokenRestant { get; set; }

        public Player(Token t)
        {
            Token = t;
            NbTokenRestant = 1;
        }

        public void LeftToken(int nbToken)
        {
            NbTokenRestant -= nbToken;
        }
    }
}