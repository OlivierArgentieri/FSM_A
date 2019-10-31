 using System.Runtime.InteropServices.WindowsRuntime;
 using UnityEngine;

 public class FSM_A_AgentSightTypeEcoPlus
 {
     public bool GetEcoSight(Vector3 _origin, Vector3 _direction, float _height, float _range, LayerMask _targetLayer, LayerMask _obstacleLayer, ref Transform _targetRef)
     {
         Ray _raySight = new Ray(_origin + Vector3.up * _height, _direction);
         RaycastHit _hit;

         Transform _target;
         bool _hitTarget = Physics.Raycast(_raySight, out _hit, _range, _targetLayer);
         if(_hitTarget)
         {
             Debug.DrawRay(_raySight.origin, _raySight.direction * (_hitTarget ? _hit.distance : _range), _hitTarget ? Color.blue : Color.red);
             _target = _hit.collider.transform;

             if (!Util.HitObstacleBetweenTarget(_hit, _raySight.origin, _target.position, _range, _obstacleLayer))
             {
                 _targetRef = _target;
                 return true;
             }
         }
         Debug.DrawRay(_raySight.origin, _raySight.direction * (_hitTarget ? _hit.distance : _range), _hitTarget ? Color.blue : Color.red);
         _targetRef = null;
         return false;
     }
 }
