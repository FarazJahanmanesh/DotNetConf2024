

using System.Runtime.CompilerServices;

namespace ConsoleApp.OverloadPriority
{
    public class Temp
    {
        public int Id { get; set; }
    }
    public class Test
    {
        public int Id { get; set; }
    }
    public class OverloadPriorityExample
    {
        //اولویت بندی اورلود های ما
        [OverloadResolutionPriority(1)]
        public void Show(Temp entity)
        {
            Console.WriteLine("Method Show With High Priority : " + entity.Id);
        }
        [OverloadResolutionPriority(0)]
        public void Show(Test entity)
        {
            Console.WriteLine("Method Show With Low Priority : " + entity.Id);
        }
    }
}
