using System.Collections.Generic;

namespace SnakeGame
{
    // Represents a position with row and column values
    public class Positions
    {
        public int Row { get; }
        public int Col { get; }

        // Constructor to initialize the position with row and column values
        public Positions(int row, int col)
        {
            Row = row;
            Col = col;
        }

        // Translates the position based on the given direction
        public Positions Translate(Directions dir)
        {
            return new Positions(Row + dir.RowOffset, Col + dir.ColOffset);
        }

        // Override Equals method to compare two Positions objects
        public override bool Equals(object obj)
        {
            return obj is Positions positions &&
                   Row == positions.Row &&
                   Col == positions.Col;
        }

        // Override GetHashCode method to provide a unique hash code for the object
        public override int GetHashCode()
        {
            int hashCode = 1084646500;
            hashCode = hashCode * -1521134295 + Row.GetHashCode();
            hashCode = hashCode * -1521134295 + Col.GetHashCode();
            return hashCode;
        }

        // Override the equality operator to compare two Positions objects
        public static bool operator ==(Positions left, Positions right)
        {
            return EqualityComparer<Positions>.Default.Equals(left, right);
        }

        // Override the inequality operator to compare two Positions objects
        public static bool operator !=(Positions left, Positions right)
        {
            return !(left == right);
        }
    }
}
