using System;

namespace Gameplay.String
{
    //тк StringHandler несколько на сцене иенты в 1 классе
    public class StringEvents
    {
        public static event Action PerfectHit;
        public static event Action NiceHit;
        public static event Action Away;

        public static void InvokePerfect() => PerfectHit?.Invoke();
        public static void InvokeNice() => NiceHit?.Invoke();
        public static void InvokeAway() => Away?.Invoke();
    }
}