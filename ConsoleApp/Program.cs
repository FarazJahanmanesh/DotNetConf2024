using ConsoleApp.OverloadPriority;
using ConsoleApp.Params;
using ConsoleApp.PatternsMatching;
using ConsoleApp.Records;

namespace DotNetConf2024.CSharp13.ConsoleApp;
public class Program
{
    //System.Threading
    //نوع obj که لاک شده است object 
    // نسخه قدیمی استفاده از Monitor.Enter() و Monitor.Exit()
    ////////////اگر به اشتباه Monitor.Exit() فراموش شود، lock آزاد نمی‌شود که منجر به مشکلات می‌شود.

    //System.Threading.Lock
    //نسخه جدید پشتیبانی از متد EnterScope() برای مدیریت بهتر
    ////////////کامپایلر تضمین می‌کند که قفل در پایان محدوده آزاد شود.
    // قابلیت Dispose ندارد


    static readonly object oldLock = new object();
    static Lock newLock = new Lock();
    static int sharedCounter = 0;

    static void NewLockIncrement()
    {
        lock (newLock) 
        {
            sharedCounter++;
            Console.WriteLine($"New Lock: Counter = {sharedCounter}");
        }
    }
    static void OldLockIncrement()
    {
        lock (oldLock)
        {
            sharedCounter++;
            Console.WriteLine($"Old Lock: Counter = {sharedCounter}");
        }
    }
    static string GetEscapeSequence()
    {
        return "\e";
    }
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

        #region PatternMatching

        PatternMatching patternMatching = new PatternMatching();

        Console.WriteLine(patternMatching.Nand(true, true));

        Console.WriteLine(patternMatching.Nand2(false, true));

        patternMatching.RunTest();

        #endregion

        #region Animal

        var firstAnimal = new Animal("pig", 8);

        var secondAnimal = firstAnimal with { Name = "cat" };

        Console.WriteLine(firstAnimal.Name);

        Console.WriteLine(secondAnimal.Name);

        #endregion

        #region Lock System.Threading.Lock

        NewLockIncrement();
        NewLockIncrement();
        OldLockIncrement();
        OldLockIncrement();

        #endregion

        #region ANSIEscape

        //میتوانیم از کد های ANSI Escape که معمولا برای تغییر رنگ کنسول است استفاده کنیم 
        //یا رفتار های مختلف برای ترمینال خود تعریف کنیم

        Console.WriteLine($"{GetEscapeSequence()}[31mThis text is red.{GetEscapeSequence()}[0m");

        Console.WriteLine($"{GetEscapeSequence()}[2J");//پاک کردن صفحه
        #endregion
    }
}