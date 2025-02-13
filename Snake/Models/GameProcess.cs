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
        private bool isUpdated = false;
        private CancellationTokenSource cts = null;

        private Direction currentDirection = Direction.Right;

        public List<List<Cell>> Field { get; }

        public Direction Direction
        {
            get => currentDirection;
            set
            {
                if (value != currentDirection && (int)value % 2 != (int)currentDirection % 2)
                {
                    currentDirection = value;
                    isUpdated = true;
                    Update();
                }
                
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
            viewModel.NeedFood = Field.Count;
        }

        private async void Run()
        {
            using (cts = new CancellationTokenSource())
            {
                try
                {
                    while (true)
                    {
                        if (snake.Died)
                        {
                            viewModel.EndGame(snake.Died);
                            break;
                        }
                        else if (snake.length == Field.Count)
                        {
                            viewModel.EndGame();
                            break;
                        }
                        else
                        {
                            Update();

                        }
                        await Task.Delay(delay, cts.Token);
                        if (isUpdated)
                        {
                            isUpdated = false;
                            await Task.Delay(delay / 2, cts.Token);
                        }


                    }
                }
                catch (TaskCanceledException) { }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            cts = null;
        } 

        public void AddScore()
        {
            viewModel.Score = snake.length;
        }

        public void Start()
        {
            Run();
        }

        public void Stop()
        {
            cts.Cancel();
        }

        public void Update()
        {
            snake.Move(Direction);
            food.Draw();
        }
    }
}
