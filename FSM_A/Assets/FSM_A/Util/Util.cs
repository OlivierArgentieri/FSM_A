using UnityEngine;

public class Util
{
    public static bool HitObstacleBetweenTarget(RaycastHit _targetHit, Vector3 _origin, Vector3 _dicrection, float _maxDistance, LayerMask _obstacleLayer)
    {
        float _distantToTarget = _targetHit.distance;
        Ray _toTargetRay = new Ray(_origin, _dicrection);
        RaycastHit _hit;
        bool _hitObstacle = Physics.Raycast(_toTargetRay, out _hit, _maxDistance, _obstacleLayer);
        if (!_hitObstacle) return false; // obstacle not found
        
        float _distanceToObstacle = _hit.distance;

        return _distanceToObstacle < _distantToTarget;
    }
}