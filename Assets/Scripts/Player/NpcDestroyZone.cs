using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Npc"))
        {
            Destroy(collision.gameObject);
        }
    }
}
