using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 aiming;
    private SpriteRenderer fishRenderer;

    public Base_manager base_Manager;
    public Ressource_manager base_ressource;
    public Ressource_manager fish_ressource;
    public GameObject Trident;
    public GameObject aimSprite;

	public float fishSpeed = 1.5f;
    private float compteur;
    private float compteurTir;
    private float angle;
    private bool inside_jellyman_base = false;
    private bool inside_divine_sponge_ressource = false;
    private bool isRecolting_divine_sponge_ressource = false;
    private bool alive = true;

    public float timerTir = 3;
    public float aimingRadius = 0.8f;
    public int timer_recolte;
    public int health_point = 3;

    public GameObject ressource_recolte;
	public int tools_recolte;

	private bool lock_player_value;

    // Start is called before the first frame update
    void Start()
    {
		lock_player_value = false;
        fishRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {		//foreach (string test in Input.GetJoystickNames()){
		//	Debug.Log(test);
		//}
		if(!lock_player_value){
			fishMove();
            aim();
            harpoon();
            gather();
            obstruction();
            if (inside_jellyman_base)
            {
                if (Input.GetButtonDown("ButtonY_2"))
                {
                    //Debug.Log("button y pressed");
                    base_Manager.update_unit();
                    base_Manager.display_option();
                }
                if (Input.GetButtonDown("ButtonB_2"))
                {
                    //Debug.Log("button b pressed");
                    base_Manager.echange_wepaon();
                    base_Manager.display_option();

                }
            }
            compteurTir -= Time.fixedDeltaTime;
            is_alive();
		}
    }

    private void fishMove()
    {
        direction = new Vector3(Input.GetAxis("Horizontal_2"), Input.GetAxis("Vertical_2"), 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.Translate(fishSpeed * direction * Time.deltaTime);
		if (direction.y > 0)
		{
			this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x,-1);
		}
		else if (direction.y < 0)
		{
			this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, 1);
		}
		if(direction.x > 0){
			this.gameObject.transform.localScale = new Vector3(-1, this.gameObject.transform.localScale.y);
		}
		else if (direction.x < 0){
			this.gameObject.transform.localScale = new Vector3(1, this.gameObject.transform.localScale.y);
		}
    }

    private void harpoon()
    {
        if (Input.GetButtonDown("BumperRight_2") && compteurTir <= 0 && aiming != Vector3.zero)
		{
			//Debug.Log(Input.GetJoystickNames());
			//Debug.Log("tir un harpon");
			//Debug.Log(base_ressource.get_weapon());
            if (base_ressource.get_weapon() > 0)
            {
                compteurTir = timerTir;
                //Debug.Log("Harpon !");
                GameObject T = Instantiate(Trident, gameObject.transform, true);
                T.transform.position = transform.position;
                angle = Vector2.Angle(transform.up, aiming);

                if (aiming.x >= 0)
                    angle = -angle;

                T.transform.eulerAngles = new Vector3(0, 0, angle);
                T.GetComponent<Rigidbody2D>().velocity = aiming * 5f;
                Destroy(T, 5);
                T.transform.parent = null;
                base_ressource.remove_weapon(1);
                base_Manager.update_ui();
            }
        }
    }

    private void gather()
    {
		if(inside_divine_sponge_ressource){
			if(Input.GetButtonDown("ButtonA_2"))
            {
				isRecolting_divine_sponge_ressource = true;
			}

			else if (Input.GetButton("ButtonA_2"))
            {
				isRecolting_divine_sponge_ressource = true;
				if (isRecolting_divine_sponge_ressource)
				{
					compteur += Time.fixedDeltaTime;
					//Debug.Log("compteur " + compteur);

					if (compteur < timer_recolte)
					{
						//Debug.Log("Récolte Début !");
					}

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

    private void obstruction()
    {
        if (Input.GetButtonDown("ButtonX_2"))
        {
            // Debug.Log("Entrave !");
        }
    }

    private void aim()
    {
        aiming = new Vector3(Input.GetAxis("RightStickX_2"), Input.GetAxis("RightStickY_2"), 0);
        if (aiming.magnitude > 0)
        {
            aimSprite.transform.position = transform.position + aiming * aimingRadius;
            aimSprite.SetActive(true);
        }
        else
        {
            aimSprite.SetActive(false);
            aimSprite.transform.position = transform.position;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("enter into a trigger");
        if (collision.gameObject.CompareTag("Jellyman_base"))
        {
            //Debug.Log("enter into a jelly base trigger");
            base_ressource = collision.gameObject.GetComponent<Ressource_manager>();
            base_Manager = collision.gameObject.GetComponent<Base_manager>();

			int temp_divine_sponge = this.fish_ressource.get_divine_sponge();
			fish_ressource.remove_divine_sponge(temp_divine_sponge);
            base_ressource.add_divine_sponge(temp_divine_sponge);

            base_Manager.display_option();
			base_Manager.update_ui();

			inside_jellyman_base = true;
        }
		else if (collision.gameObject.CompareTag("Divine_sponge_ressource"))
        {
			ressource_recolte = collision.gameObject;
            inside_divine_sponge_ressource = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Jellyman_base"))
        {
            Base_manager base_Manager = collision.gameObject.GetComponent<Base_manager>();
            base_Manager.remove_option();

			inside_jellyman_base = false;
		}
		else if (collision.gameObject.CompareTag("Divine_sponge_ressource"))
        {
            inside_divine_sponge_ressource = false;
        }
    }

    private void is_alive()
    {
        if (health_point > 0)
            alive = true;
        else
        {
            alive = false;
            gameObject.SetActive(false);
        }
    }
	public void lock_player(){
		lock_player_value = true;
	}
}
