using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Mace : MonoBehaviour
{
    GameObject[] Circles;
    bool DistanceCheck;
    Vector3 Distance;
    int DistanceCounter = 0;
    bool GoFowardFallBack;
    GameObject Tarcın;
    RaycastHit2D RaycastHit2D;
   public LayerMask layerMask;
    int speed = 5;

   public Sprite MaceFace;
    public Sprite MaceBack;
    SpriteRenderer spriteRenderer;

    public GameObject Bullet;
    float bullettime;
    


    void Start()
    {
        Tarcın = GameObject.FindGameObjectWithTag("Player");
        Circles = new GameObject[transform.childCount];
        spriteRenderer = GetComponent<SpriteRenderer>();
        for (int i = 0; i < Circles.Length; i++)
        {
            Circles[i] = transform.GetChild(0).gameObject;
            Circles[i].transform.SetParent(transform.parent);
        }
    }


    void FixedUpdate()
    {
        TarcınSpotted();
        if (RaycastHit2D.collider.tag=="Player")
        {
            speed = 10;
            spriteRenderer.sprite = MaceFace;
            Fire();
        }
        else
        {
            speed = 8;
            spriteRenderer.sprite = MaceBack;
        }
        FollowCircles();
    }
    void Fire()
    {
        bullettime += Time.deltaTime;
        if (bullettime>Random.Range(0.2f,1))
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            bullettime = 0;

        }
    }
    void TarcınSpotted()
    {
        Vector3 RayWay = Tarcın.transform.position - transform.position;
        RaycastHit2D = Physics2D.Raycast(transform.position,RayWay,1000,layerMask);
        Debug.DrawLine(transform.position,RaycastHit2D.point,Color.magenta);
    }
   public Vector2 getYon()
    {
        return (Tarcın.transform.position - transform.position).normalized;
    }
    void FollowCircles()
    {
        if (DistanceCheck)
        {
            Distance = (Circles[DistanceCounter].transform.position - transform.position).normalized;
            DistanceCheck = false;
        }
        float Lenght = Vector3.Distance(transform.position, Circles[DistanceCounter].transform.position);
        transform.position += (Distance * Time.deltaTime * speed);

        if (Lenght < 0.5f)
        {
            DistanceCheck = true;
            if (DistanceCounter == Circles.Length - 1)
            {
                GoFowardFallBack = false;
            }
            else if (DistanceCounter == 0)
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
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}
#if UNITY_EDITOR
[CustomEditor(typeof(Mace))]
[System.Serializable]

class MaceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Mace mac = (Mace)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Add", GUILayout.MinWidth(100), GUILayout.MinWidth(100)))
        {
            GameObject newone = new GameObject();
            newone.transform.parent = mac.transform;
            newone.transform.position = mac.transform.position;
            newone.name = mac.transform.childCount.ToString();

        }
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("MaceFace"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("MaceBack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Bullet"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }

}
#endif