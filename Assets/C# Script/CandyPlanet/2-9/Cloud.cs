using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveStep = 1.5f; // 비트당 이동 거리
    public float destroyX = 12f; // 좌표 넘으면 삭제

    private Transform followTarget;
    public bool isGrabbed = false;

    public GameObject debrisPrefab;
    public int debrisCount = 5;

    void OnEnable()
    {
        Debug.Log("Cloud Enable");
        BeatManager.Instance.OnBeat += Move;
    }

    void OnDisable()
    {
        BeatManager.Instance.OnBeat -= Move;
    }

    void Move()
    {
        Debug.Log("Move 실행됨");
        if (isGrabbed) return;
        transform.position += Vector3.right * moveStep;

        if (transform.position.x > destroyX)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isGrabbed) return;

        if (followTarget != null)
        {
            transform.position = followTarget.position;
        }
    }

    public void Grab(Transform hand)
    {
        isGrabbed = true;
        followTarget = hand;
    }

    public void ReleaseAndBreak()
    {
        for (int i = 0; i < debrisCount; i++)
        {
            GameObject debris = Instantiate(debrisPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 force = new Vector2(
                    Random.Range(-2f, 2f),
                    Random.Range(3f, 6f)
                );

                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }
}