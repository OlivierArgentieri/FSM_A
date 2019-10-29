using System;
using UnityEngine;

public class FSM_A_PlayerComponent : MonoBehaviour
{
    #region f/p
    
    [SerializeField, Header("ID")] private int id = 0;
    
    public int ID => id;
    public bool IsValid => id >= 0;

    #endregion






    #region unity methods

    private void Start() => FSM_A_PlayerManager.Instance.Register(this);
    private void OnDestroy() => FSM_A_PlayerManager.Instance.UnRegister(this);


    #endregion





    #region custom methods

    #endregion

}