using System;
using UnityEngine;

public class FSM_A_AgentSightMultiRayBehaviour : FSM_A_AgentSightBehaviour
{
    public override bool TargetDetected()
    {
        return GetMultiRay();
    }
    
    #region custom methods
    
    private bool GetMultiRay()
    {
        for (int i = -sightAngle/2; i <sightAngle/2; i++)
        {
            Ray _toTargetRay = new Ray(transform.position, (Quaternion.AngleAxis(i, Vector3.up)*transform.forward));
            RaycastHit _hit;

            bool _hitTarget = Physics.Raycast(_toTargetRay, out _hit, sightRange , targetLayer);
           // Debug.DrawRay(transform.position, _toTargetRay.direction * sightRange, _hitTarget ? Color.blue : Color.red);

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

    
    protected override void OnDrawGizmos()
    {
        for (int i = -sightAngle/2; i < sightAngle/2; i++)
        {
            Quaternion _direction = Quaternion.Euler(Mathf.Sin(Time.time) * 20, i, 0);
            bool _isHit = Physics.Raycast(transform.position, _direction * transform.forward * sightRange, targetLayer);
            
            Gizmos.color = _isHit? Color.blue : Color.red;
            Gizmos.DrawRay(transform.position, _direction * transform.forward* sightRange);
            Gizmos.color = Color.white;
        }
    }


    #endregion
}