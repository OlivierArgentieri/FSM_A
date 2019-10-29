using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMA_AgentSight : MonoBehaviour
{
    public Action OnUdpateSight = null;

    [SerializeField, Header("Target")] Transform target = null;
    [SerializeField, Header("Range"), Range(0.5f, 50)] float range = 10;

    public bool FindPlayer => target?GetDistance(transform.position, target.position) < range : false;

    public Transform Target => target;

    public float GetDistance(Vector3 _from, Vector3 _target) => Vector3.Distance(_from, _target);

    private void Awake() => OnUdpateSight += UpdateSight;

    void UpdateSight()
    {
        if (!target) target = FSM_A_PlayerManager.Instance.PlayerOne?.transform;
    }
}
