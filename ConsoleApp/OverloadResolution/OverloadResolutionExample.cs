namespace ConsoleApp.OverloadResolution;
public class OverloadResolutionExample
{
    public int Add(int a, int b) { return a + b; }
    public double Add(double a, double b) { return a + b; }
    public T Add<T>(T a, T b) where T : struct { throw new Exception(); }//حتی این هم برای اعداد در نسخه قدیمی چک میشد
}
