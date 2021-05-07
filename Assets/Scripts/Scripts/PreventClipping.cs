using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventClipping : MonoBehaviour
{

    Vector3 _resetPosition;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Debug.Log("Hit Ground");
        }
    }
}
