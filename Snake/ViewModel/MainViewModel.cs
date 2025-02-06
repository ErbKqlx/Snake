using Snake.Game;
using Snake.Models;
using System.Windows.Input;

namespace Snake.ViewModel
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private int score;
        private int needFood;
        private List<List<Cell>> field;
        private GameProcess game;
        private bool gameOver;
        private bool gameRunning;
        private bool winner;
        private ICommand startCommand;
        private ICommand moveCommand;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }

        public int NeedFood
        {
            get => needFood;
            set
            {
                needFood = value;
                OnPropertyChanged();
            }
        }

        public List<List<Cell>> Field
        {
            get => field;
            set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        public bool GameOver
        {
            get => gameOver;
            set
            {
                gameOver = value;
                OnPropertyChanged();
            }
        }

        public bool GameRunning
        {
            get => gameRunning;
            set
            {
                gameRunning = value;
                OnPropertyChanged();
            }
        }

        public bool Winner
        {
            get => winner;
            set
            {
                winner = value;
                OnPropertyChanged();
            }
        }

        //public ICommand StartCommand => startCommand = new RelayCommand();

        //public ICommand MoveCommand => moveCommand = new RelayCommand();

        public ICommand StartCommand => startCommand = new RelayCommand(parameter =>    
        {
            if (!GameRunning)
            {
                if (GameOver || Winner)
                {
                    
                    NewGame();
                }
                else
                {
                    GameRunning = true;
                    game.Start();
                }
            }
            else
            {
                GameRunning = false;
                game.Stop();
            }
        });

        public ICommand MoveCommand => moveCommand = new RelayCommand(parameter =>       
        {
            if (GameRunning && Enum.TryParse(parameter.ToString(), out Direction direction))
            {
                game.Direction = direction;
            }
        });

        public void EndGame(bool isDied)
        {
            GameRunning = false;
            GameOver = true;
        }

        public void EndGame()
        {
            GameRunning = false;
            Winner = true;
        }

        private void NewGame()
        {
            //if (GameRunning)
            //{
            //    game.Stop();
            //}
            GameOver = false;
            Winner = false;
            Score = 0;
            game = new GameProcess(this);
            Field = game.Field;
        }

        public MainViewModel()
        {
            NewGame();
        }
    }
}
