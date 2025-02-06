using Snake.Game;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Windows.Media;

namespace Snake.Models
{
    public class Snake
    {
        private readonly GameProcess game;
        private readonly Food food;
        private readonly Queue<Position> tail;

        private Position snakeHead;
        //private List<Position> tail = new List<Position>();
        

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
            //tail.Enqueue(SnakeHead);
            //tail.Add(SnakeHead);
            this.food = food;

            //while (tail.Count < length)
            //{
            //    Move(Direction.Right);
            //}
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
                //if (tail.Count == 1)
                //{
                //    tail[0] = SnakeHead;
                //}

                //tail[0] = SnakeHead;
                //Position lastPos = tail[length - 1];
                //Position lastPos;
                //if (length == 1)
                //{
                //    lastPos = SnakeHead;
                //}
                //else
                //{
                //    lastPos = tail[length - 1];
                //}
                //Position lastPos = length == 1 ? SnakeHead : tail[length - 1];
                //Position prevPos = SnakeHead;

                //game.Field[SnakeHead.Y][SnakeHead.X].Type = CellType.None;
                //tail.Dequeue();





                //МАССИВ
                /*for (int i = 0; i < length; i++)
                {
                    //if (length > 1 && i == 1) 
                    //{
                    //    tail[i]
                    //}
                    if (length == 1) 
                    {
                        tail[i] = prevPos;
                        continue;
                    }

                    tail[i] = tail[i - 1];
                    game.Field[tail[i].Y][tail[i].X].Type = CellType.Snake;
                }
                game.Field[lastPos.Y][lastPos.X].Type = CellType.None;*/

                //ОЧЕРЕДЬ
                tail.Enqueue(SnakeHead);
                game.Field[SnakeHead.Y][SnakeHead.X].Type = CellType.Snake;
                SnakeHead = position;

                while (tail.Count > length)
                {
                    Position lastPos = tail.Dequeue();
                    game.Field[lastPos.Y][lastPos.X].Type = CellType.None;
                }







                //tail.Dequeue();
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
                /*if (length == 0)
                {
                    tail.Add(SnakeHead);
                }
                else
                {
                    tail.Add(tail[length - 1]);
                }*/

                //tail.Enqueue(position);
                food.Count--;
                length++;
                game.AddScore();

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
