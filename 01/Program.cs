namespace day01;
class Program
{
    static void Main(string[] args)
    {
        var data = ReadData();
        //DoPart1(data);
        DoPart2(data);
    }

    private static void DoPart1(string[] data)
    {
        int sum = 0;
        foreach (var line in data) {
            var digits = line.Where(char.IsNumber).ToArray();;
            var firstAndLast = "" + digits[0] + digits[^1];
            sum += int.Parse(firstAndLast);
        }
        Console.WriteLine(sum);
    }

    private static void DoPart2(string[] data)
    {
        int sum = 0;
        foreach (var line in data) {
            var firstDigit = SearchForDigit(line, 0, 1);
            var lastDigit = SearchForDigit(line, line.Length-1, -1);
            var number = int.Parse(firstDigit + lastDigit);
            Console.WriteLine($"{line} - {number}");
            sum += number;
        }
        Console.WriteLine(sum);
    }

    private static string SearchForDigit(string line, int startIx, int increment)
    {
        var i = startIx;
        while (i >= 0 && i < line.Length) {
            if (char.IsNumber(line[i])) return "" + line[i];
            if (i + 3 <= line.Length && line.Substring(i, 3) == "one") return "1";
            if (i + 3 <= line.Length && line.Substring(i, 3) == "two") return "2";
            if (i + 5 <= line.Length && line.Substring(i, 5) == "three") return "3";
            if (i + 4 <= line.Length && line.Substring(i, 4) == "four") return "4";
            if (i + 4 <= line.Length && line.Substring(i, 4) == "five") return "5";
            if (i + 3 <= line.Length && line.Substring(i, 3) == "six") return "6";
            if (i + 5 <= line.Length && line.Substring(i, 5) == "seven") return "7";
            if (i + 5 <= line.Length && line.Substring(i, 5) == "eight") return "8";
            if (i + 4 <= line.Length && line.Substring(i, 4) == "nine") return "9";
            i += increment;
        }
        throw new ArgumentException(line);
    }

    private static string[] ReadData() 
    {
        return File.ReadAllLines("data.txt").ToArray();
    }
}
