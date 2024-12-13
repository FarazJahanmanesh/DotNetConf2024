namespace ConsoleApp.Params;
public class UsingParams
{
    public void PrintItems(params IEnumerable<int>[] collections)
    {
        foreach (IEnumerable<int> collection in collections)
        {
            foreach (int item in collection)
            {
                Console.Write(item + " ");
            }
        }
        Console.WriteLine();
    }

    //    کلمه کلیدی params برای تعریف متود هایی با تعداد ورودی متغیر استفاده می شود
    //و هر متود تنها اجازه دارد یک ورودی از این نوع داشته باشد اگر موقع فراخوانی مقداری داده نشود
    //به عنوان ارایه خالی در نظر گرفته میشود و باید اخرین ورودی متود باشد

    //public void Concat<T>(params ReadOnlySpan<T> items, params ReadOnlySpan<T> items2 , string item3)
    //{
    //    for (int i = 0; i < items.Length; i++)
    //    {
    //        Console.Write(items[i]);
    //        Console.Write(" ");
    //    }
    //    Console.WriteLine();
    //}

    //نحوه تعریف در سی شارپ 12
    //    در سی شارپ 12 فقط آرایه ها را میپذیرفت
    //یعنی مجبور بودیم سایر collection ها را به آرایه تغییر دهیم

    public void ConcatenateStrings(params string[] strings)
    {
        string result = string.Join(" ", strings);
        Console.WriteLine(result);
    }
}
