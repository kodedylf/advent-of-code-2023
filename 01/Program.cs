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
            var l = line;
            for (var i = 0; i < l.Length; i++)
            {
                l = ReplaceAtIndex(l, "one", "1", i);
                l = ReplaceAtIndex(l, "two", "2", i);
                l = ReplaceAtIndex(l, "three", "3", i);
                l = ReplaceAtIndex(l, "four", "4", i);
                l = ReplaceAtIndex(l, "five", "5", i);
                l = ReplaceAtIndex(l, "six", "6", i);
                l = ReplaceAtIndex(l, "seven", "7", i);
                l = ReplaceAtIndex(l, "eight", "8", i);
                l = ReplaceAtIndex(l, "nine", "9", i);
            }
            var digits = l.Where(char.IsNumber).ToArray();
            var firstAndLast = "" + digits[0] + digits[^1];
            var number = int.Parse(firstAndLast);
            Console.WriteLine($"{line} - {number}");
            sum += number;
        }
        Console.WriteLine(sum);
    }

    private static string ReplaceAtIndex(string s, string oldString, string newString, int index)
    {
        if (index + oldString.Length > s.Length) return s;
        if (s.Substring(index, oldString.Length) != oldString) return s;
        return s.Substring(0, index) + newString + s.Substring(index + oldString.Length);
    }

    private static string[] ReadData() 
    {
        return File.ReadAllLines("data.txt").ToArray();
    }
}
