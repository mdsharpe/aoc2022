static class ItemPrioritisation
{
    public static int GetPriority(char item)
    {
        if (char.IsLower(item))
        {
            return (int)item - 96;
        }
        else
        {
            return (int)item - 38;
        }
    }
}
