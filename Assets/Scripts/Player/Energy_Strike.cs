
using UnityEngine;

public class Energy_Strike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && 
            !collision.gameObject.CompareTag("Collectible") &&
            !collision.gameObject.CompareTag("AttackLaunch_Strike"))
        {
            Destroy(gameObject);
        }
    }
} 