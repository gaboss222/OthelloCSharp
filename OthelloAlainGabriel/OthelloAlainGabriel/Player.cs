using System.ComponentModel;

namespace OthelloAlainGabriel
{
    /// <summary>
    /// Class who represents a player
    /// Player has a name, number and token (black/white)
    /// </summary>
    public class Player : INotifyPropertyChanged
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
            OnPropertyChanged(Score.ToString());
        }

        public Player()
        {

        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        protected virtual void OnPropertyChanged(string property)
        {
            var propertyChanged = PropertyChanged;
            if(propertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
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