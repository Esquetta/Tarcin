using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Mace mace;
    Rigidbody2D rigidbody2D;
    void Start()
    {
        mace = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Mace>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(mace.getYon()*1000);

    }

}
