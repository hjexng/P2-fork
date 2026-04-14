using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] prefabs;

    public float spawnX = 10f;
    public float spawnY = 1f;
    public float spacing = 0.5f;

    [SerializeField] private Transform parent;

    private GameObject lastCloud;

    public void TrySpawn()
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];

        if (!CanSpawn(prefab)) return;

        Vector3 pos = new Vector3(spawnX, spawnY, 0);
        GameObject newCloud = Instantiate(prefab, pos, Quaternion.identity, parent);

        lastCloud = newCloud;
    }

    bool CanSpawn(GameObject prefab)
    {
        if (lastCloud == null) return true;

        float lastLeft = GetLeft(lastCloud);
        float newHalf = GetHalfWidth(prefab);

        float newRight = spawnX + newHalf;

        return newRight < lastLeft - spacing;
    }

    float GetHalfWidth(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
        return sr.bounds.size.x / 2f;
    }

    float GetLeft(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponentInChildren<SpriteRenderer>();
        return sr.bounds.min.x;
    }
}