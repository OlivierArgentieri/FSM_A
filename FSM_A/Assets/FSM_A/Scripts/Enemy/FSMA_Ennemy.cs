using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FSMA_AgentSight),(typeof(FSMA_AgentMovement)))]
public class FSMA_Ennemy : MonoBehaviour, IAgent
{
    [SerializeField, Header("Agent sight")] FSMA_AgentSight sight = null;
    [SerializeField, Header("Agent movement")] FSMA_AgentMovement movement = null;

    public FSMA_AgentSight Sight => sight;
    public FSMA_AgentMovement Movement => movement;

    public bool IsValid => sight && movement;

    private void Start()
    {
        AgentInit();
    }

    public void AgentInit()
    {
        sight = GetComponent<FSMA_AgentSight>();
        movement = GetComponent<FSMA_AgentMovement>();
    }
}