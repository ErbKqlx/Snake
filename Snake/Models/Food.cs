using Snake.Game;

namespace Snake.Models
{
    public class Food
    {
        private readonly Random rnd;
        public readonly List<List<Cell>> field;

        public int Count { get; set; }

        public Food(List<List<Cell>> field)
        {
            rnd = new Random();
            this.field = field;
        }

        public void Draw()
        {
            if (Count < 2)
            {
                while (true)
                {
                    Position position = new Position(rnd.Next(field[0].Count), rnd.Next(field.Count));

                    if (field[position.Y][position.X].Type == CellType.None)
                    {
                        field[position.Y][position.X].Type = CellType.Food;
                        Count++;
                        break;
                    }
                }
            }
        }
    }
}
