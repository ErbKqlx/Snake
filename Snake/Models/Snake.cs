using Snake.Game;
using System.Threading;
using System.Windows.Media;

namespace Snake.Models
{
    public class Snake
    {
        private readonly GameProcess game;
        private readonly Food food;


        private Position snakeHead;
        List<Position> tail = new List<Position>();

        private int length;

        public bool Died { get; private set; }

        public Position SnakeHead
        {
            get => snakeHead;
            private set
            {
                snakeHead = value;
                game.Field[value.Y][value.X].Type = CellType.Snake;
                
                
                
                
            }
        }

        public Snake(GameProcess game, int length, Position snake, Food food)
        {
            this.game = game;
            this.length = length;
            SnakeHead = snake;
            tail.Add(snakeHead);
            this.food = food;
        }

        public void Move(Direction direction)
        {
            Position position = SnakeHead;
            switch (direction)
            {
                case Direction.Left:
                    position = new Position(position.X - 1, position.Y);
                    break;
                case Direction.Right:
                    position = new Position(position.X + 1, position.Y);
                    break;
                case Direction.Up:
                    position = new Position(position.X, position.Y - 1);
                    break;
                case Direction.Down:
                    position = new Position(position.X, position.Y + 1);
                    break;
            }

            Check(position);

            if (!Died)
            {
                SnakeHead = position;
                for (int i = 1; i < tail.Count; i++)
                {
                    tail[i] = tail[i - 1];
                    game.Field[tail[i].Y][tail[i].X].Type = CellType.Snake;
                }

                game.Field[tail[].Y][tail[].X].Type = CellType.None;
                //tail.Dequeue();
            }
        }

        private void Check(Position position)
        {
            if (position.X >= game.Field[0].Count || position.X < 0
                || position.Y >= game.Field.Count || position.Y < 0
                || game.Field[position.Y][position.X].Type == CellType.Snake)
            {
                Died = true;
            }

            else if (game.Field[position.Y][position.X].Type == CellType.Food)
            {
                food.Count--;
                game.AddScore();
                length++;
                tail.Add(position);
            }
        }

        //private bool IsDied()
        //{
        //    return position.X >= game.field.GetLength(0) || position.X < 0
        //        || position.Y >= game.field.GetLength(1) || position.Y < 0
        //        || game.field[position.X, position.Y].Type == CellType.Snake;
        //}

        //private bool IsFood()
        //{
        //    return game.field[position.Y, position.X].Type == CellType.Food;
        //}
    }
}
