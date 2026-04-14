using UnityEngine;

public class Bawmquhen2_6 : MonoBehaviour
{
    public float jumpHeight = 200f;
    public float jumpSpeed = 800f;
    public float rotationSpeed = -90f;

    private bool isJumping = false;
    private bool isFalling = false;

    private float targetY;
    private float originalY;


    public float moveSpeed = 500f;
    public float returnSpeed = 300f;
    public float maxX = 300f;

    private float targetX = 0f;
    private bool returning = false;
    private float returnDelay = 0.3f;
    private float returnTimer = 0f;


    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            Debug.LogError("RectTransform 컴포넌트를 찾을 수 없습니다!");
        }
        else
        {
            originalY = rectTransform.anchoredPosition.y;
        }
    }

    void Update()
    {
        if (rectTransform == null) return;

        // 점프 입력
        if (Input.GetMouseButtonDown(0) && !isJumping && !isFalling)
        {
            Jump();
        }

        // 좌우 입력
        float input = Input.GetAxis("Horizontal");

        if (Mathf.Abs(input) > 0.1f)
        {
            returning = false;
            returnTimer = 0f;

            targetX += input * moveSpeed * Time.deltaTime;
            targetX = Mathf.Clamp(targetX, -maxX, maxX);
        }
        else
        {
            returnTimer += Time.deltaTime;

            if (returnTimer >= returnDelay)
            {
                returning = true;
            }
        }

        float currentX = rectTransform.anchoredPosition.x;

        if (returning)
            currentX = Mathf.MoveTowards(currentX, 0f, returnSpeed * Time.deltaTime);
        else
            currentX = Mathf.MoveTowards(currentX, targetX, moveSpeed * Time.deltaTime);

        rectTransform.anchoredPosition = new Vector2(currentX, rectTransform.anchoredPosition.y);

        HandleJump();
    }

    void Jump()
    {
        isJumping = true;
        originalY = rectTransform.anchoredPosition.y;
        targetY = originalY + jumpHeight;
    }

    void HandleJump()
    {
        if (isJumping)
        {
            float newY = Mathf.MoveTowards(rectTransform.anchoredPosition.y, targetY, jumpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);

            if (Mathf.Approximately(newY, targetY))
            {
                // 점프 끝 → 낙하 시작
                isJumping = false;
                isFalling = true;
            }
        }
        else if (isFalling)
        {
            float newY = Mathf.MoveTowards(rectTransform.anchoredPosition.y, originalY, jumpSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);

            if (Mathf.Approximately(newY, originalY))
            {
                // 낙하 끝
                isFalling = false;
            }
        }
    }
}
