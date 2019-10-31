using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_MoveTo : FSMA_State
{
    public override void InitLogic(FSMA_Logic _logic)
    {
        base.InitLogic(_logic);
        OnFSMA_StateUpdate += Logic.Movement.OnUpdateMovement;
    }
}
