using UnityEngine;

public class SingletonTest : Singleton<SingletonTest>
{
    public int a = 10;
    public int b = 20;
    public void Print()
    {
        print(a + ", " + b);
    }
}