using UnityEngine;

public class FSM_A_AgentSightTypeMultiRay
{
    public bool GetMultiRay(Transform _origin, Vector3 _direction, float _height, float _range, float _angle, LayerMask _targetLayer, LayerMask _obstacleLayer, ref Transform _targetRef)
    {
        for (int i = -(int)_angle / 2; i < _angle / 2; i++)
        {
            //Ray _toTargetRay = new Ray(transform.position, (Quaternion.AngleAxis(i, Vector3.up)*transform.forward));
            Ray _toTargetRay = new Ray(_origin.position + Vector3.up * _height,Quaternion.Euler(Mathf.Sin(Time.time) *20, i, 0) * _origin.forward);
            RaycastHit _hit;


            bool _hitTarget = Physics.Raycast(_toTargetRay, out _hit, _range, _targetLayer);
            //.DrawRay(_origin.position, _toTargetRay.direction * _range, _hitTarget ? Color.blue : Color.red);

            if (!_hitTarget) continue; // target not found
            if (!Util.HitObstacleBetweenTarget(_hit, _origin.position,(Quaternion.AngleAxis(i, Vector3.up) * _origin.forward), _range, _obstacleLayer))
            {
                _targetRef= _hit.transform;
                return true;
            }
        }

        _targetRef = null;
        return false;
    }
}