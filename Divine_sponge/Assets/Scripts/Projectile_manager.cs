using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_manager : MonoBehaviour
{
	private Base_manager human_base;
	private Base_manager jellyman_base;

	private void Start()
	{
		human_base = GameObject.FindWithTag("Human_base").GetComponent<Base_manager>();
		jellyman_base = GameObject.FindWithTag("Jellyman_base").GetComponent<Base_manager>();
	}
	private void OnCollisionEnter2D(Collision2D collision)
    {
		PolygonCollider2D temp_collider = this.GetComponentInChildren<PolygonCollider2D>();
		temp_collider.enabled = false;
		//Debug.Log("enter in a collider" + collision.gameObject.tag);
		projectile_contact("Trident", "Ship", collision,human_base);
		projectile_contact("Trident", "Human_base", collision,human_base);
		projectile_contact("Harpoon", "Fish", collision,jellyman_base);
		projectile_contact("Harpoon", "Jellyman_base", collision,jellyman_base);       
    }

	private void projectile_contact(string gameObject_type, string object_collision, Collision2D collision,Base_manager base_manager)
    {
        if (gameObject.tag == gameObject_type)
        {
            if (collision.gameObject.tag == object_collision)
            {
                transform.parent = collision.transform;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().isKinematic = true;
				Debug.Log(base_manager.tag);
				base_manager.damage();
            }
        }
    }
}
