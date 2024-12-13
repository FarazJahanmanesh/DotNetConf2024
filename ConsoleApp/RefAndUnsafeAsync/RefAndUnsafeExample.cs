using System.Runtime.InteropServices;

// اجازه استفاده از unsafe  ها در متود های iterator 
// به ما اجازه میدهد به صورت مستقیم با حافظه کار کنیم و از ویژگی هایی مثل pointers ها استفاده کنیم 
//شما می‌توانید حافظه را به صورت دینامیک با استفاده از توابع سیستم‌عامل مانند Marshal.AllocHGlobal تخصیص دهید و آن را مدیریت کنید.
//در مواردی که نیاز به دسترسی سریع به داده‌ها دارید
//(مانند پردازش تصویر، کار با فایل‌ها و تعامل با سخت‌افزار)، استفاده از کد غیرایمن می‌تواند به افزایش عملکرد برنامه شما کمک کند

public unsafe class UnsafeRefExample
{
    public void Call()
    {
        int size = 5;
        // تخصیص حافظه برای آرایه‌ای از اعداد صحیح
        int* pArray = (int*)Marshal.AllocHGlobal(size * sizeof(int));

        // مقداردهی اولیه به آرایه
        for (int i = 0; i < size; i++)
        {
            pArray[i] = (i + 1) * 10; // مقداردهی به هر عنصر
        }

        // نمایش مقادیر آرایه
        Console.WriteLine("Array values before modification:");
        PrintArray(pArray, size);

        // دو برابر کردن مقادیر با استفاده از ref
        DoubleValues(ref pArray, size);

        // نمایش مقادیر پس از تغییر
        Console.WriteLine("Array values after modification:");
        PrintArray(pArray, size);

        // آزاد کردن حافظه
        Marshal.FreeHGlobal((IntPtr)pArray);
    }

    static void DoubleValues(ref int* pArray, int size)
    {
        for (int i = 0; i < size; i++)
        {
            pArray[i] *= 2; // دو برابر کردن مقدار با استفاده از اشاره‌گر
        }
    }

    static void PrintArray(int* pArray, int size)
    {
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine(pArray[i]);
        }
    }
}



//unsafe class UnsafeMemoryExample
//{
//    // استفاده از Marshal برای تخصیص حافظه
//    [DllImport("kernel32.dll", SetLastError = true)]
//    private static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

//    // ثابت‌ها برای تخصیص حافظه
//    private const uint MEM_COMMIT = 0x1000;
//    private const uint MEM_RESERVE = 0x2000;
//    private const uint PAGE_READWRITE = 0x04;

//    static void Main()
//    {
//        int size = 5;
//        int* pArray = (int*)AllocateMemory(size); // تخصیص حافظه

//        // مقداردهی اولیه به آرایه
//        for (int i = 0; i < size; i++)
//        {
//            pArray[i] = (i + 1) * 10; // مقداردهی به هر عنصر
//        }

//        // نمایش مقادیر آرایه
//        Console.WriteLine("Array values:");
//        for (int i = 0; i < size; i++)
//        {
//            Console.WriteLine(pArray[i]);
//        }

//        // آزاد کردن حافظه
//        FreeMemory((IntPtr)pArray);
//    }

//    // تابعی برای تخصیص حافظه
//    private static IntPtr AllocateMemory(int size)
//    {
//        IntPtr p = VirtualAlloc(IntPtr.Zero, (uint)(size * sizeof(int)), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
//        return p;
//    }

//    // تابعی برای آزاد کردن حافظه
//    private static void FreeMemory(IntPtr p)
//    {
//        Marshal.FreeHGlobal(p);
//    }
//}
