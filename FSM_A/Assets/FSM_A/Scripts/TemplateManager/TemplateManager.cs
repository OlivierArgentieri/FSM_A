using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateManager<T> : MonoBehaviour where T : MonoBehaviour
{
    #region f/p

    [SerializeField, Header("Keep it though levels ?")]
    private bool keep = false;
    
    private static T instance = default(T);
    public static T Instance => instance;
    #endregion
    
    
    
    #region unity methods
    protected virtual void Awake() => InitSingleton();
    #endregion
    
    
    
    
    #region custom methods

    private void InitSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        
        instance = this as T;
        name += $"{typeof(T).Name}";
        if(keep) DontDestroyOnLoad(this);
    }
    #endregion
}
