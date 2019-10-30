using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_AgentSight : MonoBehaviour
{
    #region f/p

     public Action OnUdpateSight = null;
    
        [SerializeField, Header("Target")] Transform target = null;
        [SerializeField, Header("Sight tick time"), Range(0.1f, 10)] private float tickMax = 1;
        [SerializeField, Header("Sight Type")] protected AgentSightType sightType = AgentSightType.EcoPlus;
        [SerializeField, Header("Target layer")]protected LayerMask targetLayer = 0;
        [SerializeField, Header("Obstacle Layer")]protected LayerMask obstacleLayer = 0;
        private FSM_A_AgentSightBehaviour behaviour;

        private bool IsValid => behaviour;
        public enum AgentSightType
        {
            EcoPlus,
            MultiRay,
            Overlap
        }
    
        float sightTickTimer = 0;
       
        public bool TargetDetected { get; private set; } = false;
        public Transform Target => target;
    #endregion



    #region unity methods

    private void Awake()
    {
        OnUdpateSight += UpdateSight;
        InitBehaviour();
    }


    #endregion


    #region custom methods
    void UpdateSight()
    {
        if (!IsValid) return;
        sightTickTimer += Time.deltaTime;
        if (sightTickTimer > tickMax)
        {
            TargetDetected  = behaviour.TargetDetected();
            target = behaviour.Target;
            sightTickTimer = 0;
        }
    }

    void InitBehaviour()
    {
        switch (sightType)
        {
            case AgentSightType.EcoPlus:
                behaviour = gameObject.AddComponent<FSM_A_AgentSightEcoPlusBehaviour>();
                break;
            case AgentSightType.MultiRay:
                behaviour = gameObject.AddComponent<FSM_A_AgentSightMultiRayBehaviour>();
                break;
            case AgentSightType.Overlap:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        behaviour?.Init(targetLayer, obstacleLayer);
    }
   
    #endregion
    
    
    #region debug
    private void OnDrawGizmos()
    {
    //    DrawDebugRay();
    }


    #endregion
  
}
