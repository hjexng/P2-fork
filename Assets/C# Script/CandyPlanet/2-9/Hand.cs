using UnityEngine;

public class Hand : MonoBehaviour
{
    public float downY = 2f;
    public float upY = 5f;
    public float speed = 10f;

    private bool isMoving = false;
    private bool isDown = false;

    private Cloud grabbedCloud;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            isMoving = true;
            isDown = true;
        }

        if (isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        float targetY = isDown ? downY : upY;

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(transform.position.x, targetY, 0),
            speed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            if (isDown)
            {
                isDown = false; // 다시 올라감
            }
            else
            {
                isMoving = false;

                // ⭐ 올라온 후 처리
                if (grabbedCloud != null)
                {
                    grabbedCloud.ReleaseAndBreak();
                    grabbedCloud = null;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDown) return;

        Cloud cloud = col.GetComponent<Cloud>();

        if (cloud != null && grabbedCloud == null)
        {
            grabbedCloud = cloud;
            cloud.Grab(transform);
        }
    }
}