namespace ConsoleApp.PatternsMatching;
public class PatternMatching
{
        
    //هندل کردن همه حالت های ممکن باعث میشود دیگر به مقدار پیش فرض نیازی نباشد
    public bool Nand(bool a, bool b) =>
        (a, b) switch
        {
            (true, true) => true,
            (false, false) => false,
            (true, false) => false,
            (false, true) => false
        };


    //c# 9
    //میتوان تنها حالت مد نظر خودمون بنویسیم و حالات دیگر را با همان پیش فرض هندل کنیم
    public bool Nand2(bool a, bool b) =>
     (a, b) switch
     {
         (true, true) => true,
         _ => false
     };

    public static bool IsNotPerfectSquare(int num)
        => num is not 4 or 9;

    //متود بالا در واقع این کار را میکند
    //public static bool IsNotPerfectSquare(int num)
    //    => num is (not 4) or 9;

    //روش صحیح نوشتن
    //public static bool IsNotPerfectSquare(int num)
    //    => num is not (4 or 9);

    public void RunTest()
    {
        for (int i = 0; i < 10; i++)
            Console.WriteLine($"{i} is perfect square ? {IsNotPerfectSquare(i)}");
    }
}
