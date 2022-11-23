using System.Data;
using System.Numerics;

class Program
{
    const string OutputPath = @"..\..\..\output.txt";
    static string[] tasklines;

    public static void Main(string[] args)
    {
        Console.WriteLine("Write input data path:");
        string path = Console.ReadLine();
        try
        {
            tasklines = File.ReadAllLines(path);
            CheckInput();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        Solve(CreateArray());

    }

    static int[,] CreateArray()
    {
        var str = tasklines[0].Split();
        int n = int.Parse(str[0]) + 2;
        int m = int.Parse(str[0]) + 2;

        int[,] paper = new int[n, m];


        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (j == 0)
                    paper[i, j] = 2;
                else if (i == 0)
                    paper[i, j] = 2;
                else if (i == m - 1)
                    paper[i, j] = 2;
                else if (j == n - 1)
                    paper[i, j] = 2;
            }
        }
        for (int i = 1; i < tasklines.Length; i++)
        {
            string line = tasklines[i];
            for (int j = 0; j < line.Length; j++)
            {
                paper[i, j + 1] = int.Parse(line[j].ToString());
            }
        }
        return paper;

    }

    static void CheckInput()
    {
        if (tasklines.Count() <= 1)
            throw new Exception("Incorrect data.");
        var str = tasklines[0].Split();
        if (!(int.TryParse(str[0], out int _) && int.TryParse(str[1], out int _)))
        {
            throw new Exception("Wrong N or M.");
        }
        for (int i = 1; i < tasklines.Length; i++)
        {
            string line = tasklines[i];
            for (int j = 0; j < line.Length; j++)
            {
                if (int.TryParse(line[j].ToString(), out int _))
                {
                    throw new Exception("Wrong symbols.");
                }
            }
        }
    }

    static void Solve(int[,] array)
    {
        List<Vector2> explored = new List<Vector2>();
        List<Vector2> reachable = new List<Vector2>();
        List<Vector2> blocked = new List<Vector2>();

        int x = 1;
        int y = 1;
        int numberofpapers = 0;
        bool isOver = true;
        reachable.Add(new Vector2(x, y));
        int usedblocks = 0;
        while (explored.Count + blocked.Count != (array.GetLength(0) - 2) * (array.GetLength(1) - 2))
        {
            var node = new Vector2(reachable[0].X, reachable[0].Y);
            var isBlocked = blocked.FirstOrDefault(g => g.X == node.X && g.Y == node.Y);
            if (isBlocked.X == 0 && isBlocked.Y == 0)
            {
                explored.Add(reachable[0]);
            }
            reachable.Add(new Vector2(node.X + 1, node.Y));
            reachable.Add(new Vector2(node.X - 1, node.Y));
            reachable.Add(new Vector2(node.X, node.Y + 1));
            reachable.Add(new Vector2(node.X, node.Y - 1));

            int numberofblocks = 0;

            for (int i = 0; i < reachable.Count; i++)
            {
                if (explored.Contains(reachable[i]))
                {
                    reachable.Remove(reachable[i]);
                    i--;
                }
                else if (array[Convert.ToInt32(reachable[i].X), Convert.ToInt32(reachable[i].Y)] > 1)
                {
                    numberofblocks++;
                    reachable.Remove(reachable[i]);
                    i--;
                }
                else if (array[Convert.ToInt32(reachable[i].X), Convert.ToInt32(reachable[i].Y)] == 1)
                {
                    if (reachable[i].X == node.X && reachable[i].Y == node.Y)
                    {
                        reachable.Remove(reachable[i]);
                        continue;
                    }
                    var block = blocked.FirstOrDefault(g => g.X == reachable[i].X && g.Y == reachable[i].Y);
                    if (block.X == 0 && block.Y == 0)
                        blocked.Add(reachable[i]);
                    reachable.Remove(reachable[i]);
                    i--;
                    numberofblocks++;

                }
            }
            if (numberofblocks == 4)
            {
                numberofpapers++;
                isOver = false;
            }

            if (reachable.Count > 0)
                isOver = true;
            if (reachable.Count == 0)
            {
                reachable.Add(blocked[usedblocks]);
                usedblocks++;
                if (isOver)
                {
                    numberofpapers++;
                    isOver = false;
                }

            }

        }
        Console.WriteLine(numberofpapers);
    }
}