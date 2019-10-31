using UnityEngine;

public class FSM_A_AgentSightTypeOverlap
{

    public bool GetOverlapSight(Transform _transform, ref Transform _target, LayerMask _obstacleLayer, LayerMask _targetLayer, float _range, float _angle)
    {
        Collider[] _targetsInRange = Physics.OverlapSphere(_transform.position, _range, _targetLayer);
        bool _targetFound = _targetsInRange.Length > 0;

        if (_targetFound)
        {
            Transform _currentTarget = _targetsInRange[0].transform;
            Vector3 _direction = (_currentTarget.position - _transform.position).normalized;
            float _angleBetweenTarget = Vector3.Angle(_transform.forward, _direction);

            if (_angleBetweenTarget < _angle / 2)
            {
                float _distanceToTarget = Vector3.Distance(_currentTarget.position, _transform.position);
                bool _isObstacle = Physics.Raycast(_transform.position, _direction, _distanceToTarget, _obstacleLayer);
                _target = _isObstacle ? null : _currentTarget;
                return !_isObstacle;
            }
            return false;
        }
        return false;
    }

    public void DrawGizmo(Transform _transform, float _range, Transform _detedtedTarget)
    {
        //Gizmos.DrawWireSphere(_transform.position, _range);

        if (_detedtedTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_transform.position, _detedtedTarget.position);
            Gizmos.color = Color.white;
        }
    }
}