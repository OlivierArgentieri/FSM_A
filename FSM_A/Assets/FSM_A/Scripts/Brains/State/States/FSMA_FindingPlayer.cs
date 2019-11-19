using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_FindingPlayer : FSMA_State
{
    private float tick = 0;
    
    public override void InitLogic(FSMA_Logic _logic)
    {
        base.InitLogic(_logic);
        OnFSMA_StateUpdate += _logic.Detection.OnUpdateDetection;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tick += Time.deltaTime;
        if (tick > 0.5f)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            tick = 0;
        }
    }
}