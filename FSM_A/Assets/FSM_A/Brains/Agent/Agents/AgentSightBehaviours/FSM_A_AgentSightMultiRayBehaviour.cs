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

    
    protected override void OnDrawGizmos()
    {
        for (int i = -sightAngle/2; i < sightAngle/2; i++)
        {
            Ray _sightRay = new Ray(transform.position, (Quaternion.AngleAxis(i, Vector3.up)*transform.forward));
                    
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_sightRay.origin, _sightRay.direction * sightRange);
            Gizmos.color = Color.white;
        }
    }


    #endregion
}