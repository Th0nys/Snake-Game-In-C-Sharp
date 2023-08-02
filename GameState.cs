using System;
using System.Collections.Generic;

namespace SnakeGame
{
    // Represents the game state of the Snake game
    public class GameState
    {
        // Properties to store the number of rows and columns in the game grid
        public int Rows { get; }
        public int Cols { get; }

        // 2D array representing the game grid with various grid values
        public GridValue[,] Grid { get; }

        // Property to store the current direction of the snake
        public Directions Dir { get; private set; }

        // Property to store the current score of the player
        public int Score { get; private set; }

        // Property to determine if the game is over
        public bool GameOver { get; private set; }

        // LinkedList to store the direction changes made by the player
        private readonly LinkedList<Directions> dirChanges = new LinkedList<Directions>();

        // LinkedList to store the positions of the snake's body
        private readonly LinkedList<Positions> snakePositions = new LinkedList<Positions>();

        // Random object to generate random positions for adding food
        private readonly Random random = new Random();

        // Constructor to initialize the game state with rows and columns
        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Directions.Right;

            AddSnake();
            AddFood();
        }

        // Method to add the initial snake positions
        private void AddSnake()
        {
            int r = Rows / 2;

            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;
                snakePositions.AddFirst(new Positions(r, c));
            }
        }

        // Method to get all empty positions on the game grid
        private IEnumerable<Positions> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Positions(r, c);
                    }
                }
            }
        }

        // Method to add food at a random empty position on the grid
        private void AddFood()
        {
            List<Positions> empty = new List<Positions>(EmptyPositions());

            if (empty.Count == 0)
            {
                return;
            }

            Positions pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;
        }

        // Method to get the position of the snake's head
        public Positions HeadPosition()
        {
            return snakePositions.First.Value;
        }

        // Method to get the position of the snake's tail
        public Positions TailPosition()
        {
            return snakePositions.Last.Value;
        }

        // Method to get all the positions of the snake's body
        public IEnumerable<Positions> SnakePositions()
        {
            return snakePositions;
        }

        // Method to add a new head position to the snake's body
        private void AddHead(Positions pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        // Method to remove the tail position from the snake's body
        private void RemoveTail()
        {
            Positions tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        // Method to get the last direction change made by the player
        private Directions GetLastDirection()
        {
            if (dirChanges.Count == 0)
            {
                return Dir;
            }

            return dirChanges.Last.Value;
        }

        // Method to check if the player can change the snake's direction
        private bool CanChangeDirection(Directions newDir)
        {
            if (dirChanges.Count == 2)
            {
                return false;
            }

            Directions lastDir = GetLastDirection();
            return newDir != lastDir && newDir != lastDir.Opposite();
        }

        // Method to update the snake's direction
        public void ChangeDirection(Directions dir)
        {
            if (CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            }
        }

        // Method to check if a position is outside the game grid
        private bool OutsideGrid(Positions pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

        // Method to check if a position will hit a wall, the snake's body, or an empty position
        private GridValue WillHit(Positions newHeadPos)
        {
            if (OutsideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }

            if (newHeadPos == TailPosition())
            {
                return GridValue.Empty;
            }

            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        // Method to move the snake based on the current direction
        public void Move()
        {
            if (dirChanges.Count > 0)
            {
                Dir = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }

            Positions newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                AddHead(newHeadPos);
                Score++;
                AddFood();
            }
        }
    }
}
