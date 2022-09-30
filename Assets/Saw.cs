using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Saw : MonoBehaviour
{
    GameObject []Circles;
    bool DistanceCheck;
    Vector3 Distance;
    int DistanceCounter=0;
    bool GoFowardFallBack;
    
    void Start()
    {
        Circles = new GameObject[transform.childCount];
        for (int i = 0; i < Circles.Length; i++)
        {
            Circles[i] = transform.GetChild(0).gameObject;
            Circles[i].transform.SetParent(transform.parent);
        }
    }

    
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 5);
        FollowCircles();
    }
    void  FollowCircles()
    {
        if (DistanceCheck)
        {
           Distance= (Circles[DistanceCounter].transform.position - transform.position).normalized;
            DistanceCheck = false;
        }
        float Lenght = Vector3.Distance(transform.position,Circles[DistanceCounter].transform.position);
            transform.position += (Distance * Time.deltaTime * 10);

        if (Lenght<0.5f)
        {
            DistanceCheck = true;
            if (DistanceCounter==Circles.Length-1)
            {
                GoFowardFallBack = false;
            }
            else if(DistanceCounter==0)
            {
                GoFowardFallBack = true;
            }
            if (GoFowardFallBack)
            {
                DistanceCounter++;
                DistanceCheck = true;
            }
            else
            {
                DistanceCounter--;
            }
          
        }
        
            
        
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position,transform.GetChild(i+1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(Saw))]
[System.Serializable]

class SawEditor :Editor
{
    public override void OnInspectorGUI()
    {
        Saw saw = (Saw)target;
        if (GUILayout.Button("Add",GUILayout.MinWidth(100),GUILayout.MinWidth(100)))
        {
            GameObject newone = new GameObject();
            newone.transform.parent = saw.transform;
            newone.transform.position = saw.transform.position;
            newone.name = saw.transform.childCount.ToString();
            
        }
    }

}
#endif