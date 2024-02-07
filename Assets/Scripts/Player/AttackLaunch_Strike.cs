
using UnityEngine;

public class AttackLaunch_Strike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision !=null)
        {
            // Apply Attack Launch STrike Effects Below: TODO


            // Destroy the prefab
            Destroy(gameObject);
        }   
    }
}