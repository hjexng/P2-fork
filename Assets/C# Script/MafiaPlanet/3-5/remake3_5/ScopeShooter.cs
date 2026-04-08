using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeShooter : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked!");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Enemy Hit!");
            Destroy(collision.gameObject);
        }
        
    }
}
