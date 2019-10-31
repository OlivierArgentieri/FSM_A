using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent
{
    FSMA_AgentSight Sight { get;}
    FSMA_AgentMovement Movement { get;}
    bool IsValid { get;}

    void AgentInit();
}
