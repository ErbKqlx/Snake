using Snake.Game;

namespace Snake.Models
{
    public class Snake
    {
        private readonly GameProcess game;
        private readonly Food food;
        private readonly Queue<Position> tail;

        private Position snakeHead;
        
        public int length;
        public bool Died { get; private set; }

        public Position SnakeHead
        {
            get => snakeHead;
            private set
            {
                snakeHead = value;
                game.Field[value.Y][value.X].Type = CellType.Head;
            }
        }

        public Snake(GameProcess game, int length, Position startPos, Food food)
        {
            this.game = game;
            this.length = length;
            SnakeHead = startPos;
            tail = new Queue<Position>();
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

            Check(ref position);

            if (!Died)
            {
                tail.Enqueue(SnakeHead);
                game.Field[SnakeHead.Y][SnakeHead.X].Type = CellType.Snake;
                SnakeHead = position;

                while (tail.Count > length)
                {
                    Position lastPos = tail.Dequeue();
                    game.Field[lastPos.Y][lastPos.X].Type = CellType.None;
                }
            }
        }

        private void Check(ref Position position)
        {
            if (position.X >= game.Field[0].Count)
            {
                position = new Position(0, position.Y);
            }
            else if (position.X < 0)
            {
                position = new Position(game.Field[0].Count-1, position.Y);
            }
            else if (position.Y >= game.Field.Count)
            {
                position = new Position(position.X, 0);
            }
            else if (position.Y < 0)
            {
                position = new Position(position.X, game.Field.Count-1);
            }

            if (game.Field[position.Y][position.X].Type == CellType.Snake)
            {
                Died = true;
            }
            else if (game.Field[position.Y][position.X].Type == CellType.Food)
            {
                food.Count--;
                length++;
                game.AddScore();
            }
        }
    }
}
