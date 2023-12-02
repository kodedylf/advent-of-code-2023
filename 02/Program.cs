namespace day02;

class Program
{
    static void Main(string[] args)
    {
        var games = ReadData();
        // DoPart1(games);
        DoPart2(games);
    }

    private static void DoPart1(IEnumerable<Game> games) {
        int sum = 0;
        foreach (var game in games) {
            if (GameIsValid(game)) {
                sum += game.Id;
            }
        }
        Console.WriteLine(sum);
    }

    private static void DoPart2(IEnumerable<Game> games) {
        int sum = 0;
        foreach (var game in games) {
            sum += PowerOfGame(game);
        }
        Console.WriteLine(sum);
    }

    private static int PowerOfGame(Game game) {
        var minRed = game.Pulls.SelectMany(p => p.Cubes).Where(c => c.Item1 == Color.red).Select(c => c.Item2).Max();
        var minGreen = game.Pulls.SelectMany(p => p.Cubes).Where(c => c.Item1 == Color.green).Select(c => c.Item2).Max();
        var minBlue = game.Pulls.SelectMany(p => p.Cubes).Where(c => c.Item1 == Color.blue).Select(c => c.Item2).Max();
        return minRed * minGreen * minBlue;
    }

    private static bool GameIsValid(Game game) {
        foreach (var pull in game.Pulls) {
            foreach (var cubes in pull.Cubes) {
                if (cubes.Item1 == Color.red && cubes.Item2 > 12) return false;
                if (cubes.Item1 == Color.green && cubes.Item2 > 13) return false;
                if (cubes.Item1 == Color.blue && cubes.Item2 > 14) return false;
            }
        }
        return true;
    }

    private static void PrintData(IEnumerable<Game> games) {
        foreach (var game in games) {
            Console.Write(game.Id + ": ");
            foreach (var pull in game.Pulls) {
                foreach (var cube in pull.Cubes) {
                    Console.Write($"{cube.Item2} {cube.Item1} ");
                }
                Console.Write("; ");
            }
            Console.WriteLine("");
        }
    }

    private static IEnumerable<Game> ReadData() {
         var lines = File.ReadAllLines("data.txt");
         foreach (var line in lines) {
            var game = new Game();
            game.Id = int.Parse(line.Split(": ")[0].Split(" ")[1]);
            var pullsAsString = line.Split(": ")[1].Split("; ");
            foreach (var pullAsString in pullsAsString) {
                var cubes = new List<(Color, int)>();
                foreach (var cubesAsString in pullAsString.Split(", ")) {
                    var noOfCubes = int.Parse(cubesAsString.Split(" ")[0]);
                    var colorOfCubes = (Color)Enum.Parse<Color>(cubesAsString.Split(" ")[1]);
                    cubes.Add((colorOfCubes, noOfCubes));
                }
                game.Pulls.Add(new Pull {Cubes = cubes});
            }
            yield return game;
         }
    }
}

public enum Color {
    red,
    green,
    blue
}

public class Game {
    public int Id;
    public List<Pull> Pulls = new List<Pull>();
}

public class Pull {
    public List<(Color, int)> Cubes = new List<(Color, int)>();
}