using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollision : MonoBehaviour
{


    public float damage = 20;
    //public Rigidbody2D rb;

 void OnTriggerEnter2D(Collider2D col)
         {
             if (col.CompareTag("Player"))
             {
                PlayerHealthCollision player = col.GetComponent<PlayerHealthCollision>();
                if (player != null) 
		{
			player.TakeDamage(damage);
		}
		Destroy(gameObject);
		
             }
         }


//	void OnCollisionEnter(Collision col)
//	{
//        if (col.transform.tag == "Player")
//        {
//            PlayerHealthCollision player = col.transform.GetComponent<PlayerHealthCollision>();
//            if (player != null)
//            {
//                player.TakeDamage(damage);
//            }
//            Destroy(gameObject);
//        }
//
//	}


}
