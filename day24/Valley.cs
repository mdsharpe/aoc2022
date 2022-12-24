internal class Valley
{
    public Location[,] Map { get; init; }
    public int Width => Map.GetLength(0);
    public int Height => Map.GetLength(1);
    public List<Actor> Occupants { get; } = new List<Actor>();

    public Valley(int width, int height)
    {
        Map = new Location[width, height];
    }
}
