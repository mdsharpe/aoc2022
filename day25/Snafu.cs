internal static class SnafuConvert
{
    public static long ToInt64(string input)
    {
        var digits = input.Reverse().ToArray();

        var total = 0L;

        for (var i = 0; i < digits.Length; i++)
        {
            var place = (long)Math.Pow(5, i);

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

        var digits = new Dictionary<long, long> { { 0, x } };
        void IncrementDigit(long index, long increment)
        {
            if (!digits.TryGetValue(index, out var value)) value = 0;
            value += increment;
            digits[index] = value;
        }

        while (digits.Any(o => Math.Abs(o.Value) > 2))
        {
            foreach (var digit in digits.OrderBy(o => o.Key))
            {
                if (Math.Abs(digit.Value) > 2)
                {
                    var thisPlace = (long)Math.Pow(5, digit.Key);
                    var nextPlace = (long)Math.Pow(5, digit.Key + 1);

                    var nextAdd = (long)Math.Floor((digit.Value * thisPlace) / (decimal)nextPlace);
                    nextAdd = nextAdd < 0 ? Math.Min(-1, nextAdd) : Math.Max(1, nextAdd);

                    var remainder = (digit.Value * thisPlace) - (nextAdd * nextPlace);
                    var remainderThisPlace = (long)Math.Floor(remainder / (decimal)thisPlace);
                    var remainderOnes = remainder - (remainderThisPlace * thisPlace);

                    var totalAddingToNext = nextAdd * nextPlace;
                    var totalSubtractingFromThis = (digits[digit.Key] - remainderThisPlace) * thisPlace;
                    if (totalAddingToNext != (totalSubtractingFromThis + remainderOnes))
                    {
                        throw new InvalidOperationException($"Change sanity check failed; {totalAddingToNext} != {totalSubtractingFromThis} + {remainderOnes}");
                    }

                    IncrementDigit(digit.Key + 1, nextAdd);
                    digits[digit.Key] = remainderThisPlace;
                    IncrementDigit(0, remainderOnes);
                    break;
                }
            }

            var digitSum = digits.Select(o => Math.Pow(5, o.Key) * o.Value).Sum();
            if (digitSum != x)
            {
                throw new InvalidOperationException($"Digit dictionary sanity check failed; {digitSum} != {x}");
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
