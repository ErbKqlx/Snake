using Snake.Game;

namespace Snake.Models
{
    public class Cell : NotifyPropertyChanged
    {
        private CellType type;

        public CellType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }

        }
    }
}
