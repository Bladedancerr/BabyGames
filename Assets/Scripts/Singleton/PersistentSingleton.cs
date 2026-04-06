using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private bool autoUnparentOnAwake = false;

    protected static T instance;

    public static bool HasInstance { get { return instance != null; } }
    public static T TryGetInstance()
    {
        return HasInstance ? instance : null;
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if (instance == null)
                {
                    var go = new GameObject(typeof(T).Name + " auto-generated");
                    instance = go.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (autoUnparentOnAwake)
        {
            this.transform.SetParent(null);
        }

        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        instance = null;

        if (this != null && gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}