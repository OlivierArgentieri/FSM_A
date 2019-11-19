using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class FSMA_Detection : MonoBehaviour
{
    #region f/p

    public Action OnUpdateDetection = null;

    [SerializeField, Header("Radius"), Range(0, 500)]
    private float radius = 10;

    [SerializeField, Header("Target")] private Transform target = null;

    [SerializeField, Header("Panic Mode")] private bool isReduce = false;
    [SerializeField, Header("Reset limit"), Range(1,100)] private int resetCount = 50;
    [SerializeField, Header("Layer Obstacle")] private LayerMask layerObstacle = 0;
    // List<ITarget> targets = new List<ITarget>(); todo

    public Vector3 TargetPos => target ? target.position : Vector3.zero;
    public Vector3 LastPos { get; private set; }
    public Vector3 SearchPos { get; private set; }
    
    private float initRadius = 0;
    private  bool isObstacle = false;
    List<Vector3> searchZones = new List<Vector3>();

    public List<Vector3> SearchZones => searchZones;

    public int Attempt { get; private set; } = 0;
    public int CycleNumber { get; private set; } = 1;
    
    public int Panic { get; private set; } = 0;
    public int Skip { get; private set; } = 0;
   // public bool SkipFind { get; private set; } = false;
    public int Reward { get; private set; } = 0;
    public float Speed => Panic * 2f;
    public int ResetCount => resetCount;
    
    public float SuccessPercent => Attempt > 0 ? (((float) Reward / Attempt ) * 100) : 0;

    private bool targetDetected = false;

    public bool IsObstacle => isObstacle;

    public float Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    #endregion


    #region unity methods 

    private void Awake()
    {
        target = FSM_A_PlayerManager.Instance.PlayerOne.transform;
        OnUpdateDetection += Search;
        initRadius = Radius;
    }

    private void Update()
    {
      isObstacle = AgentDetection();   
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
        if(!isReduce)
            Radius += .25f * Panic;
        else
        {
            Radius -= .25f * Panic;
            Radius =  Radius < 5 ? 5 : Radius;
        }
    }

    public void UpRadius()
    {
        Radius += .25f * Panic;
        Radius =  Radius > 20 ? 20 : Radius;

    }

    public void Search()
    {
        // AddPanic();
       // SkipFind = false;
       AddPanic();
       LastPos = GetPositionOnCircle(TargetPos, Radius, GetRandomAngle(0, 360));
        Vector3 _tempSearch = GetPositionOnCircle(LastPos, Radius, GetRandomAngle(0, 360));
        if (CantAddNavPoint(_tempSearch, searchZones , 1))
        {
          
        }
        else
        {
            SearchPos = _tempSearch;
            searchZones.Add(SearchPos);
            ResetDetection();
        }

        Attempt++;
        targetDetected = false;
    }

    bool CantAddNavPoint(Vector3 _point, List<Vector3> _navPoints, float _minimalDistance,  float _minDistance = 4.5f) 
    {
        bool _removePoint = false;
        for (int i = 0; i < _navPoints.Count; i++)
        {
            float _dist = Vector3.Distance(_navPoints[i], _point);
            _removePoint =  _dist < _minDistance || _dist < _minimalDistance;
            if (_removePoint) break;
        }

        return _removePoint;
    }

    void ResetDetection()
    {
        if (Attempt >= ResetCount)
        {
            searchZones.Clear();
            Attempt = 0;
            //Skip = 0;
            Reward = 0;
            CycleNumber++;

        }
    }

    bool AgentDetection()
    {
        Ray _ray = new Ray(transform.position, SearchPos - transform.position);
        bool _hit = Physics.Raycast(_ray, 5, layerObstacle);


        Debug.DrawRay(_ray.origin + Vector3.up * 1, (SearchPos - transform.position).normalized * 5,
            _hit ? Color.green : Color.red);
        return _hit;
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