﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class FSMA_AgentSight : MonoBehaviour
{
    #region f/p

     public Action OnUdpateSight = null;
    
        [SerializeField, Header("Target")] Transform target = null;
        //[SerializeField, Header("Range"), Range(0.5f, 50)] float range = 10;

        public enum AgentSightType
        {
            EcoPlus,
            MultiRay,
            Overlap
        }
    
        float sightTickTimer = 0;
        [SerializeField, Header("Sight tick time"), Range(0.1f, 10)] float tickMax = 0.2f;
        [SerializeField, Header("Sight Type")] AgentSightType sightType = AgentSightType.EcoPlus;
        [SerializeField, Header("Angle Sight"), Range(1f, 360)] int sightAngle = 45;
        [SerializeField, Header("Range Sight"), Range(1, 100)] float sightRange = 5;
        [SerializeField, Header("Height Sight"), Range(0.1f, 10)] float sightHeight = 1;
        [SerializeField, Header("Target layer")] LayerMask targetLayer = 0;
        [SerializeField, Header("Obstacle Layer")] LayerMask obstacleLayer = 0;
        public bool TargetDetected { get; private set; } = false;
        public Transform Target => target;

        public FSM_A_AgentSightTypeEcoPlus EcoPlus { get; private set; } = new FSM_A_AgentSightTypeEcoPlus();
        public FSM_A_AgentSightTypeMultiRay MultiRay { get; private set; } = new FSM_A_AgentSightTypeMultiRay();
        public FSM_A_AgentSightTypeOverlap OverlapRay { get; private set; } = new FSM_A_AgentSightTypeOverlap();
        public AgentSightType SightType => sightType;
        public float SightRange => sightRange;
        public float SightAngle => sightAngle;
        #endregion



    #region unity methods

    private void Awake()
    {
        OnUdpateSight += UpdateSight;
    }
        

    #endregion
    //public bool FindPlayer => target?GetDistance(transform.position, target.position) < range : false;
    //public float GetDistance(Vector3 _from, Vector3 _target) => Vector3.Distance(_from, _target);


    #region custom methods
    void UpdateSight()
    {
        sightTickTimer += Time.deltaTime;
        if (sightTickTimer > tickMax)
        {
            switch (sightType)
            {
                case AgentSightType.EcoPlus:
                    TargetDetected = EcoPlus.GetEcoSight(transform.position, transform.forward, sightHeight, sightRange, targetLayer, obstacleLayer, ref target);
                    break;
                case AgentSightType.MultiRay:
                    TargetDetected = MultiRay.GetMultiRay(transform, transform.forward, sightHeight, sightRange, sightAngle, targetLayer, obstacleLayer, ref target);
                    break;
                case AgentSightType.Overlap:
                    TargetDetected = OverlapRay.GetOverlapSight(transform, ref target, obstacleLayer, targetLayer, sightRange, sightAngle);
                    break;
            }

            if (!TargetDetected) target = null;
            sightTickTimer = 0;
        }
    }

    
    bool HitOverlap()
    {
        Physics.OverlapSphere(transform.position, sightRange, targetLayer);
        return false;
    }
    #endregion
    
    
    #region debug
    private void OnDrawGizmos()
    {
        DrawDebugRay();
    }

    private void DrawDebugRay()
    {
        //if (Application.isPlaying) return;
        
        switch (sightType)
        {
            case AgentSightType.EcoPlus:
                EcoPlus.DrawGizmos(transform, sightHeight, sightRange);
                break;
            case AgentSightType.MultiRay:
                MultiRay.DrawGizmos(transform, sightHeight, sightRange, sightAngle, targetLayer, obstacleLayer);
                break;
            case AgentSightType.Overlap:
                OverlapRay.DrawGizmo(transform, sightRange, target);
                break;
        }
    }

    #endregion
}



public struct MyRay
{
    private Ray ray;
    private bool isObstacle;
    private float distanceTarget;
    private float distanceObstacle;
    private float range;


    public bool IsObstacle => isObstacle;
    public MyRay(Ray _ray, bool _isObstacle, float _distanceTarget, float _distanceObstacle, float _range)
    {
        ray = _ray;
        isObstacle = _isObstacle;
        distanceTarget = _distanceTarget;
        distanceObstacle = _distanceObstacle;
        range = _range;
    }

    public void DrawRay()
    {
        
    }
}
