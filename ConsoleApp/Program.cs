using ConsoleApp.OverloadPriority;
using ConsoleApp.Params;

namespace DotNetConf2024.CSharp13.ConsoleApp;
public class Program
{
    public static void Main(string[] args)
    {

        #region Params

        UsingParams usingParams = new UsingParams();

        //در سی شارپ 12 باید همه تایپ ها به ارایه تبدیل میکردیم
        var list = new List<int> { 1, 2, 3 };
        var array = new int[] { 4, 5, 6 };
        var queue = new Queue<int>(new[] { 7, 8 });
        var set = new HashSet<int> { 9, 10 };

        usingParams.PrintItems(list, array, queue, set);

        #endregion

        #region OverloadPriorityExample

        OverloadPriorityExample example = new OverloadPriorityExample();

        Temp temp = new Temp
        {
            Id = 1
        };

        Test test = new Test
        {
            Id = 1
        };

        example.Show(test);

        example.Show(temp);

        #endregion

    }
}