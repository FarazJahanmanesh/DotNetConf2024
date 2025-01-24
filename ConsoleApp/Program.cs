using ConsoleApp.ImplicitIndexAccess;
using ConsoleApp.OverloadPriority;
using ConsoleApp.OverloadResolution;
using ConsoleApp.Params;
using ConsoleApp.PatternsMatching;
using ConsoleApp.Records;
using ConsoleApp.RefAndUnsafeAsync;

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

        #region OverloadResolutionExample

        //در واقع این همان Overload است در polymorphism 
        //تغییرات ان 
        //الگوریتم انتخاب متد و بهینه‌ سازی زمان و کارایی و مدیریت شرایط و محدودیت‌ها

        //قدیم
        //در روش قدیمی کامپایلر ما اولش همه ی متود های هم نام جمع اوری میکرد و از بین ان ها متود را انتخاب میکرد
        //چون همه را چک میکرد ممکن بود زمان پردازش بیشتر شود
        //روش قدیمی به تمام متدهای کاندید با فرض اینکه ممکن است شرایط آن‌ها برآورده شود، نگاه می‌کرد
        //و در نهایت بررسی می‌کرد که آیا شرایط خاصی (مثل محدودیت‌های generic) وجود دارد یا خیر

        //روش جدید 
        //در روش جدید، کامپایلر در هر محدوده (scope) به طور پیوسته متدهای غیرقابل استفاده را از مجموعه متدهای کاندید حذف می‌کند
        //این فرایند به این شکل انجام می‌شود که ابتدا متدهای نامناسب حذف می‌شوند و تنها متدهای کاربردی باقی می‌مانند
        //متدهایی که شرایط خاص آن‌ها برآورده نمی‌شود (مانند محدودیت‌های generic) به‌طور مستقیم حذف می‌شوند
        //با کاهش تعداد متدهای کاندید در هر مرحله، کامپایلر می‌تواند سریع‌تر تصمیم بگیرد و در نتیجه زمان پردازش را بهینه کند


        OverloadResolutionExample overloadResolutionExample = new OverloadResolutionExample();

        overloadResolutionExample.Add(2, 4);

        #endregion

        #region ImplicitIndexAccess

        //کد کوتاه‌تر و خواناتر
        //دسترس ‌پذیری آسان‌تر
        ///////////می‌توانید به عناصر آرایه به‌راحتی از انتها دسترسی پیدا کنید، بدون نیاز به محاسبه ایندکس‌ها
        //خانوایی بالاتر کد 
        //در واقع با طور داخلی از همان روش استفاده میکند 
        // ولی استفاده از این روش overhead ندارد

        //C# 13
        var newCountdown = new ImplicitIndexAccessExample()
        {
            buffer =
            {
                [^1] = 0,  // آخرین عنصر (ایندکس 9)
                [^2] = 1,  // عنصر قبل از آخرین (ایندکس 8)
                [^3] = 2,  // عنصر بعدی (ایندکس 7)
                [^4] = 3,  // عنصر بعدی (ایندکس 6)
                [^5] = 4,  // عنصر بعدی (ایندکس 5)
                [^6] = 5,  // عنصر بعدی (ایندکس 4)
                [^7] = 6,  // عنصر بعدی (ایندکس 3)
                [^8] = 7,  // عنصر بعدی (ایندکس 2)
                [^9] = 8,  // عنصر بعدی (ایندکس 1)
                [^10] = 9  // اولین عنصر (ایندکس 0)
            }
        };

        int newWaylastElement = newCountdown.buffer[^1];

        //C# 12 
        var oldCountdown = new ImplicitIndexAccessExample();
        oldCountdown.buffer[0] = 9;  // ایندکس 0
        oldCountdown.buffer[1] = 8;  // ایندکس 1
        oldCountdown.buffer[2] = 7;  // ایندکس 2
        oldCountdown.buffer[3] = 6;  // ایندکس 3
        oldCountdown.buffer[4] = 5;  // ایندکس 4
        oldCountdown.buffer[5] = 4;  // ایندکس 5
        oldCountdown.buffer[6] = 3;  // ایندکس 6
        oldCountdown.buffer[7] = 2;  // ایندکس 7
        oldCountdown.buffer[8] = 1;  // ایندکس 8
        oldCountdown.buffer[9] = 0;  // ایندکس 9

        // برای دسترسی به آخرین عنصر
        int oldWaylastElement = oldCountdown.buffer[oldCountdown.buffer.Length - 1];  // محاسبه ایندکس

        //C# 13
        List<int> numbersList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int lastElementList = numbersList[^1];

        #endregion

        #region RefAndUnsafeAsync

        //RefAndAsyncExample unsafeAsyncExample = new RefAndAsyncExample();
        //unsafeAsyncExample.Call().GetAwaiter().GetResult();

        //UnsafeRefExample unsafeRefExample = new UnsafeRefExample();
        //unsafeRefExample.Call();

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
        //توسعه ابزارهای CLI (Command Line Interface)

        //دستورات خاص در برخی کتابخانه‌ها
        // در برخی از کتابخانه‌های کار با ترمینال، \e ممکن است برای ارسال دستورات کنترلی خاص به ترمینال یا شناسایی ورودی‌های خاص استفاده شود

        Console.WriteLine($"{GetEscapeSequence()}[31mThis text is red.{GetEscapeSequence()}[0m");//جاپ پیغام به رنگ قرمز

        Console.WriteLine($"{GetEscapeSequence()}[2J");//پاک کردن صفحه
        #endregion
    }
}