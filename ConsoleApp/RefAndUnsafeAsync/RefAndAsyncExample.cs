namespace ConsoleApp.RefAndUnsafeAsync;

public class RefAndAsyncExample
{
    static async IAsyncEnumerable<int> GetNumbersAsync()
    {
        for (int i = 1; i <= 5; i++)
        {
            //در سی شارپ 12 نمیتوانستیم از متغیر های ref در متود های async استفاده کرد
            //همچنین در متود هایی که iterators بودند

            ref int refNumber = ref i; 
            await Task.Delay(500);
            yield return refNumber;  ///**//وقتی که yield return اجرا می‌شود، مقدار مشخص ‌شده بازگردانده می‌شود و وضعیت متد ذخیره می‌شود.
        }
    }

    public async Task Call()
    {
        await foreach (var number in GetNumbersAsync())
        {
            Console.WriteLine(number);
        }
    }
}

