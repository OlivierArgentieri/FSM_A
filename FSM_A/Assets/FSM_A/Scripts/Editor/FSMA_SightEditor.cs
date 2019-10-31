using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FSMA_AgentSight))]
public class FSMA_SightEditor : Editor
{
    #region f/p

    private FSMA_AgentSight eTarget = null;
    #endregion

    #region unity methods

    private void OnEnable()
    {
        eTarget = (FSMA_AgentSight) target;
        eTarget.name = "Agent sight [EDITOR]";
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (eTarget.SightType != FSMA_AgentSight.AgentSightType.Overlap) return;
        Handles.color = eTarget.TargetDetected ? Color.green : Color.red;
        Handles.color -= new Color(0,0,0,.8f);
        
        Handles.DrawWireDisc(eTarget.transform.position, eTarget.transform.up, eTarget.SightRange);
        Handles.DrawSolidArc(eTarget.transform.position, eTarget.transform.up, Quaternion.Euler(0,-eTarget.SightAngle/2,0) * eTarget.transform.forward, eTarget.SightAngle, eTarget.SightRange);
    }

    #endregion

    #region custom methods

    #endregion

}
