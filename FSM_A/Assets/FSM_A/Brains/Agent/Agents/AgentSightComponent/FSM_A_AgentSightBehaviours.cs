using UnityEngine;

public class FSM_A_AgentSightBehaviours : MonoBehaviour
{
    #region f/p
    [SerializeField, Header("Sight tick time"), Range(0.1f, 10)] float tickMax = 1;
    [SerializeField, Header("Sight Type")] FSMA_AgentSight.AgentSightType sightType = FSMA_AgentSight.AgentSightType.EcoPlus;
    [SerializeField, Header("Angle Sight"), Range(1f, 360)] int sightAngle = 45;
    [SerializeField, Header("Range Sight"), Range(1, 100)] float sightRange = 5;
    [SerializeField, Header("Height Sight"), Range(0.1f, 10)] float sightHeight = 1;
    [SerializeField, Header("Target layer")] LayerMask targetLayer = 0;
    [SerializeField, Header("Obstacle Layer")] LayerMask obstacleLayer = 0;
    #endregion
    
    
    
    
    #region custom method
    
    public virtual bool TargetDetected()
    {
        return false;
    }

    
    public bool HitObstacleBetweenTarget(RaycastHit _targetHit, Vector3 _origin, Vector3 _dicrection, float _maxDistance, LayerMask _obstacleLayer)
    {
        float _distantToTarget = _targetHit.distance;
        Ray _toTargetRay = new Ray(_origin, _dicrection);
        RaycastHit _hit;
        bool _hitObstacle = Physics.Raycast(_toTargetRay, out _hit, _maxDistance, _obstacleLayer);
        if (!_hitObstacle) return false; // obstacle not found
        
        float _distanceToObstacle = _hit.distance;

        return _distanceToObstacle < _distantToTarget;
    }
    #endregion
}