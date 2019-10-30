using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class FSM_A_AgentSightBehaviour : MonoBehaviour
{
    #region f/p
    [SerializeField, Header("Angle Sight"), Range(1f, 360)] protected int sightAngle = 45;
    [SerializeField, Header("Range Sight"), Range(1, 100)]protected float sightRange = 5;
    [SerializeField, Header("Height Sight"), Range(0.1f, 10)]protected float sightHeight = 1;
    [SerializeField, Header("Obstacle Layer")]protected Transform target = null;
    [SerializeField, Header("Target layer")]protected LayerMask targetLayer = 0;
    [SerializeField, Header("Obstacle Layer")]protected LayerMask obstacleLayer = 0;

    public Transform Target => target;
    #endregion
    
    
    
    
    #region custom method

    public void Init(LayerMask _targetLayer, LayerMask _obstacleLayer)
    {
        targetLayer = _targetLayer;
        obstacleLayer = _obstacleLayer;
    }
    
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

    protected virtual void OnDrawGizmos()
    {
        
    }
}