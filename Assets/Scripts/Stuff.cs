using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 扔出去的东西
/// </summary>
public class Stuff : MonoBehaviour {
    [SerializeField]
    //private int damage;
	private bool hurt = false;
	//public AudioClip hurtEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			//Destroy(gameObject);
			if (!hurt) {
				//GameObject
				//AudioSource.PlayClipAtPoint (hurtEffect, other.transform.localPosition);
				other.gameObject.GetComponent<Player> ().TakeDamage ();
				hurt = true;
			}

        }

    }

}
