using ConsoleApp.Params;

namespace DotNetConf2024.CSharp13.ConsoleApp;
public class Program
{
    public static void Main(string[] args)
    {
        UsingParams usingParams = new UsingParams();

        //در سی شارپ 12 باید همه تایپ ها به ارایه تبدیل میکردیم
        var list = new List<int> { 1, 2, 3 };
        var array = new int[] { 4, 5, 6 };
        var queue = new Queue<int>(new[] { 7, 8 });
        var set = new HashSet<int> { 9, 10 };

        usingParams.PrintItems(list, array, queue, set);

    }
}