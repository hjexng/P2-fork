using UnityEngine;

public class Enemy2_6 : MonoBehaviour
{
    public MiniGame2_6 _minigame2_6;
    public string _minigameName = "2-6minigame";
    public string targetName = "TargetObject"; // 씬 안에서 찾을 타겟 오브젝트 이름
    private Transform target;
    public float speed = 5f;   // 이동 속도

    private bool isHit = false;

    void Start()
    {
        GameObject found = GameObject.Find(targetName);
        if (found != null)
        {
            target = found.transform;
        }
        else
        {
            Debug.LogWarning($"씬 안에서 '{targetName}' 오브젝트를 찾지 못했습니다.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );
        }
    }

    public void Init(MiniGame2_6 minigame)
    {
        _minigame2_6 = minigame;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) return;
        if (!collision.CompareTag("Player")) return;

        isHit = true;

        if (_minigame2_6 != null)
            _minigame2_6.OnPlayerHit();

        //Destroy(gameObject);
    }
}
