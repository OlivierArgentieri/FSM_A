using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using EditoolsUnity;
#endif
[CustomEditor(typeof(FSMA_Detection))]
public class DetectionEditor : EditorCustom<FSMA_Detection>
{
    #region f/p
    private bool setFocus = false;
    #endregion


    #region unity methods 

    private void OnSceneGUI()
    {
        DrawDetectionInfo();
        DrawDetectionScene();
        DrawHotMap();
        UseFocus(setFocus);
    }

    
    private void OnDisable()
    {
        setFocus = false;
    }

    #endregion


    #region custom methods

    void UseFocus(bool _status)
    {
        if (!_status) return;
        SceneView.lastActiveSceneView.pivot = eTarget.transform.position;
    }
    void DrawDetectionInfo()
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(0, 0, Screen.width * .5f, Screen.height));
        GUILayout.Box($"Detection Agent Track {eTarget.name}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Cycle {eTarget.CycleNumber}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Attempt {eTarget.Attempt}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Reward {eTarget.Reward}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Success Percent {eTarget.SuccessPercent}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Panic {eTarget.Panic}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Speed {eTarget.Speed}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Skip {eTarget.Skip}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        GUILayout.Box($"Detection Agent Radius {eTarget.Radius}", SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Box, FontStyle.Bold));
        eTarget.Radius = GUILayout.HorizontalSlider(eTarget.Radius, 0, 20);
        
        EditorGUILayout.Space();
        if (GUILayout.Button("Test Detection", SetStyle(Color.yellow, 12, TextAnchor.MiddleCenter, StyleMode.Button, FontStyle.Bold))) eTarget.Search();
        if (GUILayout.Button("Focus Agent",SetStyle(Color.white, 12, TextAnchor.MiddleCenter, StyleMode.Button, FontStyle.Bold)))
            setFocus = !setFocus;
        

        GUILayout.EndArea();
        Handles.EndGUI();
    }

    void DrawDetectionScene()
    {
        EditoolsHandle.SetColor(Color.magenta);
        EditoolsHandle.DrawWireDisc(eTarget.LastPos, Vector3.up, eTarget.Radius);
        EditoolsHandle.SetColor(Color.green);
        EditoolsHandle.DrawWireCube(eTarget.TargetPos + Vector3.up, Vector3.one * 0.5f);
        EditoolsHandle.DrawDottedLine(eTarget.TargetPos, eTarget.TargetPos + Vector3.up * 2, 1);
        EditoolsHandle.DrawDottedLine(eTarget.TargetPos, eTarget.transform.position, 1);
        EditoolsHandle.SetColor(Color.yellow);
        EditoolsHandle.DrawWireCube(eTarget.SearchPos + Vector3.up * 2, Vector3.one * 0.5f);
        EditoolsHandle.DrawDottedLine(eTarget.SearchPos, eTarget.SearchPos + Vector3.up * 2, 1);
        EditoolsHandle.DrawDottedLine(eTarget.SearchPos, eTarget.transform.position, 1);
    }

    void DrawHotMap()
    {
        for (int i = 0; i < eTarget.SearchZones.Count; i++)
        {
            Handles.color = Color.Lerp(Color.red, Color.green, (float) i / eTarget.SearchZones.Count);
            Handles.DrawWireDisc(eTarget.SearchZones[i], Vector3.up, 3);
            Handles.DrawSolidDisc(eTarget.SearchZones[i], Vector3.up , 1.5f);
            if(i < eTarget.SearchZones.Count -1)
                Handles.DrawDottedLine(eTarget.SearchZones[i], eTarget.SearchZones[i+1], .5f);
        }
        Handles.color = Color.white;

    }

    GUIStyle SetStyle(Color _color, int _font, TextAnchor _alignment, StyleMode _styleMode, FontStyle _style = FontStyle.Bold)
    {
        GUIStyle _labelStyle = new GUIStyle(GUI.skin.label);
        switch (_styleMode)
        {
            case StyleMode.Button:
                _labelStyle = new GUIStyle(GUI.skin.button);
                break;
            case StyleMode.Label:
                _labelStyle = new GUIStyle(GUI.skin.label);
                break;
            
            case StyleMode.Box:
                _labelStyle = new GUIStyle(GUI.skin.box);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_styleMode), _styleMode, null);
        }
        
        _labelStyle.normal.textColor = _color;
        _labelStyle.alignment = _alignment;
        _labelStyle.fontStyle = _style;
        return _labelStyle;
    }
    

    #endregion
}

public enum StyleMode
{
    Button,
    Label,
    Box
}