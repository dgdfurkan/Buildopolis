using UnityEngine;

namespace GunduzDev
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static volatile T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                }
                return instance;
            }
        }
    }

    //public class Singleton<T> : MonoBehaviour where T : Component
    //{
    //    private static T instance;
    //    public static T Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = FindObjectOfType<T>();
    //                if (instance == null)
    //                {
    //                    GameObject obj = new GameObject();
    //                    obj.name = typeof(T).Name;
    //                    instance = obj.AddComponent<T>();

    //                    DontDestroyOnLoad(instance.gameObject);
    //                }
    //            }
    //            return instance;
    //        }
    //    }

    //}
}

