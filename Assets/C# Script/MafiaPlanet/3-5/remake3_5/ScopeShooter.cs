using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeShooter : MonoBehaviour
{
    private bool isEnemyInTrigger = false;
    private Collider2D currentEnemy;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Clicked!");

            if (isEnemyInTrigger && currentEnemy != null)
            {
                Debug.Log("Enemy Hit!");
                Destroy(currentEnemy.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isEnemyInTrigger = true;
            currentEnemy = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isEnemyInTrigger = false;
            currentEnemy = null;
        }
    }
}