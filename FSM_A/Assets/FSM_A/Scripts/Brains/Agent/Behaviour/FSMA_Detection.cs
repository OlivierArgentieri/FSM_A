using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FSMA_Detection : MonoBehaviour
{
    #region f/p

    public Action OnUpdateDetection = null;

    [SerializeField, Header("Radius"), Range(0, 500)]
    private float radius = 10;

    [SerializeField, Header("Target")] private Transform target = null;
    // List<ITarget> targets = new List<ITarget>(); todo

    public Vector3 TargetPos => target ? target.position : Vector3.zero;
    public Vector3 LastPos { get; private set; }
    public Vector3 SearchPos { get; private set; }
    
    private float initRadius = 0;


    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    #endregion


    #region unity methods  

    private void Awake()
    {
        OnUpdateDetection += Search;
        initRadius = Radius;
    }

    #endregion

    #region custom methods

    public void Search()
    {
        LastPos = GetPositionOnCircle(TargetPos, Radius, GetRandomAngle(0, 360));
        SearchPos = GetPositionOnCircle(LastPos, Radius, GetRandomAngle(0, 360));
    }


    Vector3 GetPositionOnCircle(Vector3 _center, float _radius, float _angle)
    {
        float _x = _center.x + Mathf.Cos(_angle) * radius;
        float _y = _center.y;
        float _z = _center.z + Mathf.Sin(_angle) * radius;

        return new Vector3(_x, _y, _z);
    }

    float GetRandomAngle(float _angleMin, float _angleMax) => UnityEngine.Random.Range(_angleMin, _angleMax);

    private void OnDrawGizmos()
    {
    }

    #endregion
}