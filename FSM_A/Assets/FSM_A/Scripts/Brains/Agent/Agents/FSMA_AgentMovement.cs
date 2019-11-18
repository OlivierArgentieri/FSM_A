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

    private void Awake() => InitMovement();

    void InitMovement()
    {
        OnUpdateMovement += Move;
        OnUpdateMovement += RotateTo;
    }

    public bool IsAtPos => target?GetDistance(transform.position, target.position) < atPosDistance : false;

    public float GetDistance(Vector3 _from, Vector3 _target) => Vector3.Distance(_from, _target);

    public bool IsValid => target;

    public void Move()
    {
        if (!IsValid) return;
        if (IsAtPos) return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    public void RotateTo()
    {
        if (!IsValid) return;
        if (IsAtPos) return;
        Quaternion _lookAtTarget = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookAtTarget, Time.deltaTime * rotateSpeed);
    }

    public void SetTarget(Transform _t) => target = _t;
    //public void SetTarget(Vector3 _target) => target = _t;
    
}
