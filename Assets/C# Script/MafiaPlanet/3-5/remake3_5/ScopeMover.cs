using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeMover : MonoBehaviour
{
    [Header("이동할 좌표들 순서대로 입력")]
    public Vector3[] movePoints;

    [Header("한 구간당 걸리는 시간")]
    public float moveTimePerStep = 0.5f;

    [Header("시작할 때 바로 실행할지")]
    public bool playOnStart;

    [Header("마지막 점까지 간 뒤 반복할지")]
    public bool loop = false;

    private bool isMoving = false;

    

    // 외부에서 호출해서 이동 시작할 때 사용
    public void StartMove()
    {
        if (isMoving) return;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        // 좌표가 없으면 종료
        if (movePoints == null || movePoints.Length == 0)
            yield break;

        isMoving = true;

        do
        {
            // 첫 시작 위치를 첫 번째 좌표로 맞추고 싶으면 여기 주석 해제
            // transform.position = movePoints[0];

            // 현재 위치에서 첫 번째 점으로 갈지,
            // 아니면 첫 점부터 시작할지는 원하는 방식에 따라 조절 가능
            for (int i = 0; i < movePoints.Length; i++)
            {
                Vector3 startPos = transform.position;
                Vector3 targetPos = movePoints[i];

                float elapsed = 0f;

                // 현재 위치 -> 목표 위치까지 정확히 moveTimePerStep 동안 이동
                while (elapsed < moveTimePerStep)
                {
                    elapsed += Time.deltaTime;

                    // 0~1 사이 비율
                    float t = elapsed / moveTimePerStep;
                    if (t > 1f) t = 1f;

                    transform.position = Vector3.Lerp(startPos, targetPos, t);

                    yield return null;
                }

                // 마지막 위치 오차 방지
                transform.position = targetPos;
            }

        } while (loop);

        isMoving = false;
    }
}
