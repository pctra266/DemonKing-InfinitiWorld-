using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class Enemy : ScriptableObject
{

    [SerializeField]
    private float rate;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private bool flyable;

    public float GetRate()
    {
        return rate;
    }
    public GameObject GetPrefab()
    {
        return prefab;
    }
    public bool IsFlyable()
    {
        return flyable;
    }

}
