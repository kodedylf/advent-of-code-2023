namespace day3;
class Program
{
    static void Main(string[] args)
    {
        var data = ReadData("data.txt");
        // DoPart1(data);
        DoPart2(data);
    }

    private static void DoPart2(string[] data) {
        Dictionary<int, List<int>> adjacentNumbersByGear = new Dictionary<int, List<int>>();
        FindGears(adjacentNumbersByGear, data);
        FindNumbersAdjacentToGears(adjacentNumbersByGear, data);
        int sum = 0;
        foreach (var kvp in adjacentNumbersByGear) {
            if (kvp.Value.Count == 2) {
                sum += kvp.Value[0] * kvp.Value[1];
            }
        }
        Console.WriteLine(sum);
    }

    private static void FindGears(Dictionary<int, List<int>> adjacentNumbersByGear, string[] data) {
        for (int i = 0; i < data.Length; i++) {
            var line = data[i];
            for (int ix = 0; ix < line.Length; ix++) {
                if (line[ix] == '*') {
                    adjacentNumbersByGear[i * 1000 + ix] = new List<int>();
                }
            }
        }
    }

    private static void FindNumbersAdjacentToGears(Dictionary<int, List<int>> adjacentNumbersByGear, string[] data) {
        for (int i = 0; i < data.Length; i++) {
            var line = data[i];
            int ix = 0;
            int number = 0;
            HashSet<int> adjacentGears = new HashSet<int>();
            while (ix < line.Length) {
                if (char.IsNumber(line[ix])) {
                    number = number * 10 + int.Parse("" + line[ix]);
                    FindAdjacentGears(adjacentGears, data, i, ix);
                } 
                if ((!char.IsNumber(line[ix])) || (ix == line.Length-1)) {
                    if (number != 0) {
                        foreach (var gear in adjacentGears) {
                            adjacentNumbersByGear[gear].Add(number);
                        }
                    }
                    number = 0;
                    adjacentGears = new HashSet<int>();
                }
                ix++;
            }
        }
    }

    private static void FindAdjacentGears(HashSet<int> gears, string[] data, int y, int x) {
        if (PositionIsGear(data, y-1, x-1)) gears.Add((y-1)*1000 + (x-1));
        if (PositionIsGear(data, y-1, x)) gears.Add((y-1)*1000 + x);
        if (PositionIsGear(data, y-1, x+1)) gears.Add((y-1)*1000 + (x+1));
        if (PositionIsGear(data, y, x-1)) gears.Add(y*1000 + (x-1));
        if (PositionIsGear(data, y, x+1)) gears.Add(y*1000 + (x+1));
        if (PositionIsGear(data, y+1, x-1)) gears.Add((y+1)*1000 + (x-1));
        if (PositionIsGear(data, y+1, x)) gears.Add((y+1)*1000 + x);
        if (PositionIsGear(data, y+1, x+1)) gears.Add((y+1)*1000 + (x+1));
    }

    private static bool PositionIsGear(string[] data, int y, int x) {
        if ((y < 0) || (x < 0) || (y >= data.Length) || (x >= data[0].Length)) return false;
        return (data[y][x] == '*');
    }

    private static void DoPart1(string[] data) {
        int sum = 0;
        for (int i = 0; i < data.Length; i++) {
            var line = data[i];
            int ix = 0;
            int number = 0;
            bool nextToSymbol = false;
            while (ix < line.Length) {
                if (char.IsNumber(line[ix])) {
                    number = number * 10 + int.Parse("" + line[ix]);
                    nextToSymbol = nextToSymbol || PositionIsNextToSymbol(data, i, ix);
                } 
                if ((!char.IsNumber(line[ix])) || (ix == line.Length-1)) {
                    if (number != 0) {
                        Console.WriteLine($"{number} {nextToSymbol}");
                        if (nextToSymbol) {
                            sum += number;
                        }
                    }
                    number = 0;
                    nextToSymbol = false;
                }
                ix++;
            }
        }
        Console.WriteLine(sum);
    }

    private static bool PositionIsNextToSymbol(string[] data, int y, int x) {
        return PositionIsSymbol(data, y-1, x-1) ||
               PositionIsSymbol(data, y-1, x) ||
               PositionIsSymbol(data, y-1, x+1) ||
               PositionIsSymbol(data, y, x-1) ||
               PositionIsSymbol(data, y, x+1) ||
               PositionIsSymbol(data, y+1, x-1) ||
               PositionIsSymbol(data, y+1, x) ||
               PositionIsSymbol(data, y+1, x+1);
    }

    private static bool PositionIsSymbol(string[] data, int y, int x) {
        if ((y < 0) || (x < 0) || (y >= data.Length) || (x >= data[0].Length)) return false;
        return IsSymbol(data[y][x]);
    }

    private static bool IsSymbol(char c) {
        if (char.IsNumber(c)) return false;
        if (c == '.') return false;
        return true;
    }

    private static string[] ReadData(string filename) {
        return File.ReadAllLines(filename);
    }
}
