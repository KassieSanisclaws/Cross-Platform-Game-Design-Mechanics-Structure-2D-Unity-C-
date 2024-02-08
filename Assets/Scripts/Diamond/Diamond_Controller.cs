using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Diamond_Controller : MonoBehaviour
{
    //Component
    Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            anim.SetTrigger("Diamond_Destroyed");

            Destroy(gameObject);
        }
    }
}
