using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    #endregion



    #region unity methods

    private void Awake() => OnUdpateSight += UpdateSight;

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
                    TargetDetected = GetEcoSight();
                    break;
                case AgentSightType.MultiRay:
                    break;
                case AgentSightType.Overlap:
                    break;
            }
            sightTickTimer = 0;
        }
    }

    private bool GetEcoSight()
    {
        Ray _raySight = new Ray(transform.position + Vector3.up * sightHeight, transform.forward);
        RaycastHit _hit;
        Transform _target;
        bool _hitTarget = Physics.Raycast(_raySight, out _hit, sightRange, targetLayer);
        if(_hitTarget)
        {
            Debug.DrawRay(_raySight.origin, _raySight.direction * (_hitTarget ? _hit.distance : sightRange), _hitTarget ? Color.blue : Color.red);
            float _distanceToTarget = _hit.distance;
            _target = _hit.collider.transform;
            bool _hitObstacle = Physics.Raycast(_raySight, out _hit, sightRange, obstacleLayer);
            Debug.DrawRay(_raySight.origin, _raySight.direction * (_hitTarget ? _hit.distance : sightRange), _hitTarget ? Color.blue : Color.red);
            if (!_hitObstacle)
            {
                target = _target;
                return true;
            }
            
            float _distanceToObstacle = _hit.distance;
            if (_distanceToObstacle < _distanceToTarget)
            {
                target = null;
                return false;
            }
            return true;
        }
        Debug.DrawRay(_raySight.origin, _raySight.direction * (_hitTarget ? _hit.distance : sightRange), _hitTarget ? Color.blue : Color.red);
        target = null;
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
        if (Application.isPlaying) return;
        
        switch (sightType)
        {
            case AgentSightType.EcoPlus:
                Ray _raySight = new Ray(transform.position + Vector3.up * sightHeight, transform.forward);
                Gizmos.color = Color.blue;
                Gizmos.DrawRay(_raySight.origin, _raySight.direction * sightRange);
                Gizmos.color = Color.white;
                break;
            case AgentSightType.MultiRay:
                break;
            case AgentSightType.Overlap:
                break;
        }
    }

    #endregion
  
}
