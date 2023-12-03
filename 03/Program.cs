namespace day3;
class Program
{
    static void Main(string[] args)
    {
        var data = ReadData("data.txt");
        DoPart1(data);
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
