using UnityEngine;

public class SingletonTestTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SingletonTest.Instance.a += 1;
            SingletonTest.Instance.b += 1;
            SingletonTest.Instance.Print();
        }
    }
}