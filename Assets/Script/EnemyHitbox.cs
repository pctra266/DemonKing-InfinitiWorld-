using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private List<string> deadlyTags;
    [SerializeField] GameObject enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (deadlyTags.Contains(other.gameObject.tag))
        {
            enemy.SetActive(false);
        }
    }
}
