using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FSMA_Logic : StateMachineBehaviour
{
    bool isInit = false;

    IAgent currentAgent = null;

    [SerializeField, Header("Find Animator parameter")] string paramSight = "findPlayer";
    [SerializeField, Header("Move to target Animator parameter")] string moveToTargetParam = "isAtTarget";
    [SerializeField, Header("Move to find Animator parameter")] string movementFindParam = "isNotAtFind";
    [SerializeField, Header("Skip param")] string skipParam = "Skip";

    public IAgent Agent => currentAgent;
    public FSMA_AgentSight Sight => currentAgent?.Sight;
    public FSMA_AgentMovement Movement => currentAgent?.Movement;

    public FSMA_Detection Detection => currentAgent?.Detection;

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
      
        if (!Sight.TargetDetected)
        {
            _animator.SetBool(moveToTargetParam, false);
            _animator.SetBool(movementFindParam, !Movement.IsAtPos);
            Movement.SetTarget(Detection.SearchPos);
            Movement.SetSpeed(Detection.Speed);
            if (Detection.IsObstacle)
            {
                _animator.SetBool(movementFindParam, false);
                Detection.UpRadius();
            }
            else
            {
                _animator.SetBool(moveToTargetParam, Movement.IsAtPos);
            }
            // if(Detection.SkipFind)
            //  _animator.SetTrigger(skipParam);
        }
        else
        {
            
            Movement.SetTarget(Sight.Target);
            Detection.GiveReward();
            _animator.SetBool(movementFindParam, false);
            _animator.SetBool(moveToTargetParam, Movement.IsAtPos);
        }
    }
}
