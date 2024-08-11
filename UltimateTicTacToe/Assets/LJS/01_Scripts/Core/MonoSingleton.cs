using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool isDestroyed = false;

    public static T Instance
    {
        get
        {
            if (isDestroyed)
                _instance = null; //이미 파괴되었다면 null처리하고 다시 찾아라.

            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                    Debug.LogError($"{typeof(T).Name} singleton is not exist");
                else
                    isDestroyed = false;
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            if(transform.parent != null){  }
            else{
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}
