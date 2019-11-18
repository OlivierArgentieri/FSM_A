using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FSMA_Detection))]
public class DetectionEditor : EditoolsUnity.EditorCustom<FSMA_Detection>
{
    #region f/p


    #endregion


    #region unity methods 
    private void OnSceneGUI()
    {
        EditoolsUnity.EditoolsHandle.SetColor(Color.magenta);
        EditoolsUnity.EditoolsHandle.DrawWireDisc(eTarget.LastPos, Vector3.up, eTarget.Radius);
        EditoolsUnity.EditoolsHandle.SetColor(Color.green);
        EditoolsUnity.EditoolsHandle.DrawWireCube(eTarget.TargetPos + Vector3.up, Vector3.one * 0.5f);
        EditoolsUnity.EditoolsHandle.DrawDottedLine(eTarget.TargetPos, eTarget.TargetPos + Vector3.up *2, 1);
        EditoolsUnity.EditoolsHandle.DrawDottedLine(eTarget.TargetPos, eTarget.transform.position, 1);
        EditoolsUnity.EditoolsHandle.SetColor(Color.yellow);

        EditoolsUnity.EditoolsHandle.DrawWireCube(eTarget.SearchPos + Vector3.up *2, Vector3.one * 0.5f);
        EditoolsUnity.EditoolsHandle.DrawDottedLine(eTarget.SearchPos, eTarget.SearchPos + Vector3.up * 2,1);
        EditoolsUnity.EditoolsHandle.DrawDottedLine(eTarget.SearchPos, eTarget.transform.position,1);

    }

    #endregion


    #region custom methods
    
    
    #endregion
 
}
