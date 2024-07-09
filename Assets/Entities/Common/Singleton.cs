using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    static T s_Instance;
    public static T Instance
    {
        get
        {
            if (s_Instance == null)
                new GameObject(typeof(T).Name + " Singleton").AddComponent<T>();
            return s_Instance;
        }
    }
    void Awake()
    {
        s_Instance = this as T;
    }
    public virtual void Destroy()
    {
        if (s_Instance != null)
        {
            Destroy(gameObject);
            s_Instance = null;
        }
    }
    void OnDestroy()
    {
        if (s_Instance != null)
            s_Instance = null;
    }
}