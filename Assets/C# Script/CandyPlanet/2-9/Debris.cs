using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public float fallSpeed = 5f;
    public float destroyY = -3f;

    void Update()
    {

        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
