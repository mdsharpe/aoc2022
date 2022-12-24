abstract class Actor
{
    public required Coordinate Coordinate { get; set; }
    public abstract char ToChar();
    public abstract void MoveWithin(Valley valley);
}
