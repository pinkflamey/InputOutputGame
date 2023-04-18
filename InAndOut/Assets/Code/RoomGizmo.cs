using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomGizmo : MonoBehaviour
{
    public float lineThickness;
    private void OnDrawGizmos()
    {
        foreach (Transform c in transform)
        {
            if (c.gameObject.tag == "RoomParent")
            {
                /*Handles.color = Color.red;
                Handles.Label(c.position, c.gameObject.name);*/
                
            }
            else
            {
                Gizmos.DrawCube(c.position, Vector3.one);
            }
        }
    }
}
