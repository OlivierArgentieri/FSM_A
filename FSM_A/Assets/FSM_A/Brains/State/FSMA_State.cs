using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_State : StateMachineBehaviour
{
    FSMA_Logic logic = null;

    public Action OnFSMA_StateUpdate = null;
    public Action OnFSMA_StateBegin = null;
    public Action OnFSMA_StateEnd = null;

    public virtual void InitLogic(FSMA_Logic _logic) => logic = _logic;
    public FSMA_Logic Logic => logic;
    public virtual bool IsValid => logic;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnFSMA_StateBegin?.Invoke();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnFSMA_StateUpdate?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //OnFSMA_StateEnd += ResetState;
        OnFSMA_StateEnd?.Invoke();
    }

    public void ResetState()
    {
        OnFSMA_StateUpdate = null;
        OnFSMA_StateBegin = null;
        OnFSMA_StateEnd = null;
    }
}
