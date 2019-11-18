using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_AgentMovement : MonoBehaviour
{
    public Action OnUpdateMovement = null;

    [SerializeField, Header("Target")] Transform target = null;
    [SerializeField, Header("At pos distance"), Range(0.1f, 10)] float atPosDistance = 1;
    [SerializeField, Header("Speed"), Range(0.1f, 15)] float speed = 2;
    [SerializeField, Header("Speed rotation"), Range(100, 600)] float rotateSpeed = 200;

    public Vector3 TargetPosition => target ? target.transform.position : targetVector;
    [SerializeField, Header("Target vector")] Vector3 targetVector = Vector3.zero;
    private void Awake() => InitMovement();

    void InitMovement()
    {
        OnUpdateMovement += Move;
        OnUpdateMovement += RotateTo;
    }

    public bool IsAtPos => GetDistance(transform.position, TargetPosition) < atPosDistance;

    public float GetDistance(Vector3 _from, Vector3 _target) => Vector3.Distance(_from, _target);

    public bool IsValid => target;

    public void Move()
    {
       // if (!IsValid) return;
   
        if (IsAtPos) return;
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Time.deltaTime * speed);
        
    }

    public void RotateTo()
    {
        //if (!IsValid) return;
        if (IsAtPos) return;
        Quaternion _lookAtTarget = Quaternion.LookRotation(TargetPosition - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookAtTarget, Time.deltaTime * rotateSpeed);
    }

    public void SetTarget(Transform _t) => target = _t;

    public void SetTarget(Vector3 _target)
    {
        target = null;
        targetVector = _target ;
    }

    public void SetSpeed(float _speed) => speed = _speed;
}
