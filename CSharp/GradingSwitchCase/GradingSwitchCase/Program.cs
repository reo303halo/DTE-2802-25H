namespace GradingSwitchCase;

internal abstract class Program
{   
    // private/public static output(type) FunctionName(input(type) variable)
    private static string GetGrade(int score)
    {
        const int best = 100;
        return score switch
        {
            >= best - 10 => "A",
            >= best - 20 => "B",
            >= best - 30 => "C",
            >= best - 40 => "D",
            >= best - 50 => "E",
            _ => "F"
        };
    }

    private static void Main()
    {
        Console.Write("Enter scores: ");
        var input = Console.ReadLine(); // 80 68 99 100 56 74 57 80
        var items = new[] { "0" };

        if (!string.IsNullOrEmpty(input))
        {
            items = input.Split();
        }
        var scores = new int[items.Length]; // Not necessary, but used in example to show that arrays/lists cant contain multiple types.

        for (var i = 0; i < items.Length; i++)
        {
            if (int.TryParse(items[i], out var score))
            {
                // if item i, is parsed to an int:
                scores[i] = score;
                Console.WriteLine($"Student: {i + 1}, Scores: {scores[i]}, Grade: {GetGrade(scores[i])}");
            }
            else
            {
                Console.WriteLine($"Invalid input: {items[i]}");
            }
        }
    }
}
