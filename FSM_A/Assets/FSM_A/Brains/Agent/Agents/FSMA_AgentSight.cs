using System.Collections;
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
        [SerializeField, Header("Sight tick time"), Range(0.1f, 10)] float tickMax = 1;
        [SerializeField, Header("Sight Type")] AgentSightType sightType = AgentSightType.EcoPlus;
        [SerializeField, Header("Angle Sight"), Range(1f, 360)] int sightAngle = 45;
        [SerializeField, Header("Range Sight"), Range(1, 100)] float sightRange = 5;
        [SerializeField, Header("Height Sight"), Range(0.1f, 10)] float sightHeight = 1;
        [SerializeField, Header("Target layer")] LayerMask targetLayer = 0;
        [SerializeField, Header("Obstacle Layer")] LayerMask obstacleLayer = 0;
        public bool TargetDetected { get; private set; } = false;
        public Transform Target => target;

        public FSM_A_AgentSightTypeEcoPlus EcoPlus { get; private set; } = null;
        public FSM_A_AgentSightTypeMultiRay MultiRay { get; private set; } = null;

        #endregion



    #region unity methods

    private void Awake()
    {
        OnUdpateSight += UpdateSight;
        EcoPlus = new FSM_A_AgentSightTypeEcoPlus();
        MultiRay= new FSM_A_AgentSightTypeMultiRay();
        
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
                    break;
            }
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
                Ray _raySight = new Ray(transform.position + Vector3.up * sightHeight, transform.forward);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(_raySight.origin, _raySight.direction * sightRange);
                Gizmos.color = Color.white;
                break;
            case AgentSightType.MultiRay:
                for (int i = -sightAngle/2; i <sightAngle/2; i++)
                {
                    Ray _toTargetRay = new Ray(transform.position  + Vector3.up * sightHeight, Quaternion.Euler(Mathf.Sin(Time.time) * 20, i, 0) * transform.forward);

                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(_toTargetRay.origin, _toTargetRay.direction * sightRange);
                    Gizmos.color = Color.white;

                }
                break;
            case AgentSightType.Overlap:
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
