using Snake.Game;
using Snake.ViewModel;
using System.Windows;

namespace Snake.Models
{
    public class GameProcess
    {
        private const int delay = 200;

        private readonly MainViewModel viewModel;
        private readonly Food food;
        private readonly Snake snake;
        //private readonly Snake snake1;
        private bool isUpdated = false;

        private Direction currentDirection = Direction.Right;

        public List<List<Cell>> Field { get; }

        public Direction Direction
        {
            get => currentDirection;
            set
            {
                currentDirection = value;
                Update();
                //isUpdated = true;
            }
        }

        public GameProcess(MainViewModel viewModel)
        {
            this.viewModel = viewModel;

            int width = 15;
            int height = 15;

            Field = new List<List<Cell>>();
            for (int i = 0; i < height; i++)
            {
                List<Cell> row = new List<Cell>();
                for (int j = 0; j < width; j++)
                {
                    row.Add(new Cell());
                }
                Field.Add(row);
            }


            food = new Food(Field);
            snake = new Snake(this, 0, new Position(0, 0), food);
            //snake1 = new Snake(this, 1, new Position(0, 4), food);
        }

        private async void Run()
        {
            try
            {
                while (true)
                {
                    //if (snake.Died || snake1.Died)
                    if (snake.Died)
                    {
                        viewModel.EndGame();
                        break;
                    }
                    else
                    {
                        //if (isUpdated == false)
                        //{
                        //    Update();

                        //}
                        Update();
                        
                    }
                    //isUpdated = false;
                    await Task.Delay(delay);
                    

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        } 

        public void AddScore()
        {
            viewModel.Score++;
        }

        public void Start()
        {
            Run();
        }

        public void Stop()
        {
            
        }

        public void Update()
        {
            snake.Move(Direction);
            //snake1.Move(Direction);
            food.Draw();
        }
    }
}
