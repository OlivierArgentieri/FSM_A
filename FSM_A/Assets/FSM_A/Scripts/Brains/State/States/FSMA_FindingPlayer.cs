using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_FindingPlayer : FSMA_State
{
    public override void InitLogic(FSMA_Logic _logic)
    {
        base.InitLogic(_logic);
        OnFSMA_StateBegin += _logic.Detection.OnUpdateDetection;
        OnFSMA_StateEnd += _logic.Detection.AddPanic;
    }
}