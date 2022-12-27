internal static class SnafuConvert
{
    public static long ToInt64(string input)
    {
        var digits = input.Reverse().ToArray();

        var total = 0L;

        for (var i = 0; i < digits.Length; i++)
        {
            var place = (int)Math.Pow(5, i);

            var digit = digits[i] switch
            {
                '2' => 2,
                '1' => 1,
                '0' => 0,
                '-' => -1,
                '=' => -2,
                _ => throw new InvalidOperationException()
            };

            total += place * digit;
        }

        return total;
    }

    public static string ToSnafuString(long x)
    {
        var maxI = 0;
        while (Math.Pow(5, maxI) < x)
        {
            maxI++;
        }

        var digits = new Dictionary<int, int>();
        void IncrementDigit(int index, int increment)
        {
            if (!digits.TryGetValue(index, out var value)) value = 0;
            value += increment;
            digits[index] = value;
        }

        var remaining = x;
        for (var i = maxI; i >= 0; i--)
        {
            var place = (int)Math.Pow(5, i);
            var digit = (int)Math.Floor(remaining / (decimal)place);
            remaining -= (digit * place);
            IncrementDigit(i, digit);
        }

        foreach (var digit in digits.OrderBy(o => o.Key))
        {
            if (Math.Abs(digit.Value) > 2)
            {
                var thisPlace = (int)Math.Pow(5, digit.Key);
                var nextPlace = (int)Math.Pow(5, digit.Key + 1);
                var nextAdd = Math.Max(1, (int)Math.Floor(digit.Value / (decimal)nextPlace));
                var remainder = ((digit.Value * thisPlace) - (nextAdd * nextPlace)) / thisPlace;
                IncrementDigit(digit.Key + 1, nextAdd);
                digits[digit.Key] = remainder;
            }
        }

        var chars = (from d in digits
                     orderby d.Key descending
                     select d.Value switch
                     {
                         2 => '2',
                         1 => '1',
                         0 => '0',
                         -1 => '-',
                         -2 => '=',
                         _ => throw new InvalidOperationException()
                     }).ToArray();

        return new string(chars).TrimStart('0');
    }
}
