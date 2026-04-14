using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame2_6 : MiniGameBase
{
    protected override float TimerDuration => 5f;
    protected override string MinigameExplain => "좌우로 피해라!";

    public override float perfectWindowOverride => 0.1f;
    public override float goodWindowOverride => 0.3f;
    public override float hitWindowOverride => 0.5f;

    private bool ended;
    private bool inputOpen;          // Input 구간인지
    private bool awaitingJudge;      // 입력 후 판정 대기중(중복 입력 방지용)

    private const int MaxNodes = 5;

    private List<bool> nodeResults; // 각 노드 성공 여부 저장
    private int currentNode = 0;

    public EnemySpawner2_6 spawner;

    private int hitCount = 0;

    public override void StartGame()
    {
        base.StartGame();

        nodeResults = new List<bool>();
        currentNode = 0;
        ended = false;
    }

    public override void OnRhythmEvent(string action)
    {
        if (ended) return;
        if (string.IsNullOrEmpty(action)) return;

        Debug.Log($"{gameObject.name} 리듬메세지: {action}");

        action = action.Trim();

        switch (action)
        {
            case "Show":
                Debug.Log("Show");
                spawner.SpawnObstacle();
                break;
        }
    }


    public void OnPlayerHit()
    {
        if (ended) return;

        hitCount++;

        Debug.Log($"충돌 횟수: {hitCount}");

        if (hitCount >= 3)
        {
            ended = true;
        }
    }
}
