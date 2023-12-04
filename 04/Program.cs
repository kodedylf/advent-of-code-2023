namespace day4;
class Program
{
    static void Main(string[] args)
    {
        var cards = ReadData("data.txt");
        DoPart1(cards);
        DoPart2(cards);
    }

    private static void DoPart1(IEnumerable<Card> cards) {
        int sum = cards.Select(c => c.PointsValue).Sum();
        Console.WriteLine(sum);
    }

    private static void DoPart2(IEnumerable<Card> cards) {
        var cardsAsArray = cards.ToArray();
        int[] copiesOfCard = new int[cardsAsArray.Count()];
        Array.Fill(copiesOfCard, 1);
        for (int i = 0; i < cardsAsArray.Count(); i++) {
            for (int j=0; j<cardsAsArray[i].NumberOfWinningNumbers; j++) {
                if (i+j+1 < cardsAsArray.Count())
                    copiesOfCard[i+j+1] += copiesOfCard[i];
            }
        }
        Console.WriteLine(copiesOfCard.Sum());
    }
    static IEnumerable<Card> ReadData(string filename) {
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines) {
            var winningNumbers = line.Split("|")[0].Split(":")[1].Split(" ").Where(n => !string.IsNullOrEmpty(n)).Select(int.Parse);
            var myNumbers = line.Split("|")[1].Split(" ").Where(n => !string.IsNullOrEmpty(n)).Select(int.Parse);
            yield return new Card(winningNumbers, myNumbers);
        }
    }
}

public class Card {
    public Card(IEnumerable<int> winningNumbers, IEnumerable<int> cardNumbers) {
        WinningNumbers = winningNumbers;
        CardNumbers = cardNumbers;
    }
    public IEnumerable<int> WinningNumbers;
    public IEnumerable<int> CardNumbers;
    public int NumberOfWinningNumbers => CardNumbers.Where(n => WinningNumbers.Contains(n)).Count();
    public int PointsValue => (int)Math.Pow(2, NumberOfWinningNumbers - 1);
}
