﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FSMA_Logic : StateMachineBehaviour
{
    bool isInit = false;

    IAgent currentAgent = null;

    [SerializeField, Header("Animator parameter")] string paramSight = "findPlayer";
    [SerializeField, Header("Animator parameter")] string paramMovement = "isNotAtTarget";

    public IAgent Agent => currentAgent;
    public FSMA_AgentSight Sight => currentAgent?.Sight;
    public FSMA_AgentMovement Movement => currentAgent?.Movement;

    public bool IsValid => Sight && Movement;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (isInit) return;
        FSMA_State[] _allStates = GetStates(animator);
            for (int i = 0; i < _allStates.Length; i++)
                _allStates[i].InitLogic(this);
        isInit = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetSightData(animator);
        GetMovementData(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       FSMA_State[] _allStates = GetStates(animator);
       // for (int i = 0; i < _allStates.Length; i++)
               // _allStates[i].ResetState();
    }

    FSMA_State[] GetStates(Animator _animator)
    {
        currentAgent = _animator.GetComponent<IAgent>();
        if (currentAgent != null)
            return _animator.GetBehaviours<FSMA_State>(); 
        
        return null;
    }

    void GetSightData(Animator _animator)
    {
        if (!IsValid) return;
        Sight.OnUdpateSight?.Invoke();
        _animator.SetBool(paramSight, Sight.TargetDetected);
    }

    void GetMovementData(Animator _animator)
    {
        if (!IsValid) return;
        if (Sight.TargetDetected)
        {
            Movement.SetTarget(Sight.Target);
            _animator.SetBool(paramMovement, !Movement.IsAtPos);
        }
        else
            _animator.SetBool(paramMovement, true);
    }
}