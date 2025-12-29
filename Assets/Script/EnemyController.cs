using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }


}
