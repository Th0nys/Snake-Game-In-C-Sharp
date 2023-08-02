using System.Collections.Generic;

namespace SnakeGame
{
    // Represents the possible directions that the snake can move
    public class Directions
    {
        // Static fields representing the four cardinal directions
        public readonly static Directions Left = new Directions(0, -1);
        public readonly static Directions Right = new Directions(0, 1);
        public readonly static Directions Up = new Directions(-1, 0);
        public readonly static Directions Down = new Directions(1, 0);

        // Row and Column offsets to represent the movement in each direction
        public int RowOffset { get; }
        public int ColOffset { get; }

        // Private constructor to initialize the direction with row and column offsets
        private Directions(int rowOffset, int colOffset)
        {
            RowOffset = rowOffset;
            ColOffset = colOffset;
        }

        // Returns the opposite direction to the current one
        public Directions Opposite()
        {
            return new Directions(-RowOffset, -ColOffset);
        }

        // Override Equals method to compare two Directions objects
        public override bool Equals(object obj)
        {
            return obj is Directions directions &&
                   RowOffset == directions.RowOffset &&
                   ColOffset == directions.ColOffset;
        }

        // Override GetHashCode method to provide a unique hash code for the object
        public override int GetHashCode()
        {
            int hashCode = -1482510490;
            hashCode = hashCode * -1521134295 + RowOffset.GetHashCode();
            hashCode = hashCode * -1521134295 + ColOffset.GetHashCode();
            return hashCode;
        }

        // Override the equality operator to compare two Directions objects
        public static bool operator ==(Directions left, Directions right)
        {
            return EqualityComparer<Directions>.Default.Equals(left, right);
        }

        // Override the inequality operator to compare two Directions objects
        public static bool operator !=(Directions left, Directions right)
        {
            return !(left == right);
        }
    }
}
