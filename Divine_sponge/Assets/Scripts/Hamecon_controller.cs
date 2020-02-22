using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamecon_controller : MonoBehaviour
{
    private bool inside_divine_sponge_ressource = false;
    private bool isRecolting_divine_sponge_ressource = false;
    private float compteur;
	public LineRenderer ligne;
	public BoxCollider2D colider_ligne;

    public Ressource_manager ship_ressource;
	public Ressource_manager base_ressource;
	public Base_manager base_Manager;
    // public ShipController shipController;
    public GameObject ressource_recolte;
    public int timer_recolte = 2;
    public int tools_recolte;

    

    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gather();
        ligne.SetPosition(0, new Vector3(transform.parent.position.x, transform.parent.position.y - 0.5f, 0));
        ligne.SetPosition(1, new Vector3(transform.position.x, transform.position.y + 0.1f, 0));

		colider_ligne.size = new Vector2(colider_ligne.size.x, (transform.parent.position.y - transform.position.y));
        colider_ligne.offset = new Vector2(colider_ligne.offset.x, -(transform.parent.position.y - transform.position.y) / 2);       
    }

    private void gather()
    {
        if (inside_divine_sponge_ressource)
        {
            if (Input.GetButtonDown("ButtonA_1"))
            {
                isRecolting_divine_sponge_ressource = true;
            }

            else if (Input.GetButton("ButtonA_1"))
            {
				isRecolting_divine_sponge_ressource = true;
                if(isRecolting_divine_sponge_ressource)
                {
                    compteur += Time.fixedDeltaTime;

					if (compteur < timer_recolte){
						
					}
                        //Debug.Log("Récolte Début !");

                    else if (compteur >= timer_recolte)
                    {
                        //Debug.Log("Récolte Fin !");
                        Ressource_manager temp_ressource_manager = ressource_recolte.GetComponent<Ressource_manager>();
                        Animator temp_ressource_animator = ressource_recolte.GetComponent<Animator>();
                        int temp_ressource = temp_ressource_manager.get_divine_sponge();
                        //Debug.Log("there is " + temp_ressource + " divine sponge inside");

                        if (tools_recolte >= temp_ressource)
                        {
                            temp_ressource_manager.remove_divine_sponge(temp_ressource);
							base_ressource.add_divine_sponge(temp_ressource);
                            temp_ressource_animator.SetTrigger("Gather");
							base_Manager.update_ui();
                        }

                        else
                        {
                            temp_ressource_manager.remove_divine_sponge(tools_recolte);
							base_ressource.add_divine_sponge(tools_recolte);
                            temp_ressource_animator.SetTrigger("Gather_not_empty");
							base_Manager.update_ui();
                        }

                        temp_ressource_manager.get_divine_sponge();
                        isRecolting_divine_sponge_ressource = false;
                    }
                }
            }

            else
            {
                compteur = 0;
                isRecolting_divine_sponge_ressource = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Divine_sponge_ressource"))
        {
            ressource_recolte = collision.gameObject;
            inside_divine_sponge_ressource = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Divine_sponge_ressource"))
            inside_divine_sponge_ressource = false;
    }
}
