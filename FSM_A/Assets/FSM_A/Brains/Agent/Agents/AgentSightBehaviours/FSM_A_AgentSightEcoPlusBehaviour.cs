using UnityEngine;

public class FSM_A_AgentSightEcoPlusBehaviour : FSM_A_AgentSightBehaviours
{
    
    
    
    
    
    #region custom methods

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

    #endregion
}