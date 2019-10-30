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
                    TargetDetected = GetMultiRay();
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
            _target = _hit.collider.transform;

            if (!HitObstacleBetweenTarget(_hit, _raySight.origin, _target.position, sightRange, obstacleLayer))
            {
                target = _target;
                return true;
            }
            target = null;
            return false;
        }
        Debug.DrawRay(_raySight.origin, _raySight.direction * (_hitTarget ? _hit.distance : sightRange), _hitTarget ? Color.blue : Color.red);
        target = null;
        return false;
    }

    private bool GetMultiRay()
    {
        for (int i = -sightAngle/2; i <sightAngle/2; i++)
        {
            Ray _toTargetRay = new Ray(transform.position, (Quaternion.AngleAxis(i, Vector3.up)*transform.forward));
            RaycastHit _hit;

            bool _hitTarget = Physics.Raycast(_toTargetRay, out _hit, sightRange , targetLayer);
            Debug.DrawRay(transform.position, _toTargetRay.direction * sightRange, _hitTarget ? Color.blue : Color.red);

            if (!_hitTarget) continue; // target not found
            if(!HitObstacleBetweenTarget(_hit,transform.position , (Quaternion.AngleAxis(i, Vector3.up)*transform.forward),sightRange, obstacleLayer ))
            {
                target = _hit.transform;
                return true;
            }
        }
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
                for (int i = -sightAngle/2; i <sightAngle/2; i++)
                {
                    Ray _sightRay = new Ray(transform.position, (Quaternion.AngleAxis(i, Vector3.up)*transform.forward));
                    
                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(_sightRay.origin, _sightRay.direction * sightRange);
                    Gizmos.color = Color.white;

                }
                break;
            case AgentSightType.Overlap:
                break;
        }
    }

    #endregion
  
}
