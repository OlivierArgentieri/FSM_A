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

    List<Vector3> searchZones = new List<Vector3>();

    public List<Vector3> SearchZones => searchZones;

    public int Attempt { get; private set; } = 0;
    public int Panic { get; private set; } = 0;
    public int Skip { get; private set; } = 0;
    public int Reward { get; private set; } = 0;
    public float Speed => Panic * 2f;
    public int ResetCount { get; private set; } = 10;
    
    public float SuccessPercent => Attempt > 0 ? (((float) Reward / Attempt ) * 100) : 0;

    private bool targetDetected = false;
    
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

    public void GiveReward()
    {
        if(!targetDetected)
            Reward++;
        targetDetected = true;
    }

    public void AddPanic()
    {
        Panic++;
        Panic = Panic > ResetCount ? 1 : Panic;
        if (Panic == 1) Radius = initRadius;
        radius += .25f * Panic;
    }
    
    public void Search()
    {
        LastPos = GetPositionOnCircle(TargetPos, Radius, GetRandomAngle(0, 360));
        SearchPos = GetPositionOnCircle(LastPos, Radius, GetRandomAngle(0, 360));
        searchZones.Add(SearchPos);
        Attempt++;
        targetDetected = false;
        ResetDetection();
    }

    void ResetDetection()
    {
        if (Attempt >= ResetCount)
        {
            searchZones.Clear();
            Attempt = 0;
            Reward = 0;
        }
    }

    Vector3 GetPositionOnCircle(Vector3 _center, float _radius, float _angle)
    {
        float _x = _center.x + Mathf.Cos(_angle) * radius;
        float _y = _center.y;
        float _z = _center.z + Mathf.Sin(_angle) * radius;

        return new Vector3(_x, _y, _z);
    }
    
    float GetRandomAngle(float _angleMin, float _angleMax) => UnityEngine.Random.Range(_angleMin, _angleMax);
    
    #endregion
}