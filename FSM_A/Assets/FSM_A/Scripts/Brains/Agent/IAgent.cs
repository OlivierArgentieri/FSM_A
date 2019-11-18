using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent
{
    FSMA_AgentSight Sight { get;}
    FSMA_AgentMovement Movement { get;}
    FSMA_Detection Detection { get;}
    bool IsValid { get;}

    void AgentInit();
}
