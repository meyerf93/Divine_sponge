using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Vector3 direction;
    private Vector3 aiming;
    private Vector3 hameconDirection;
    private SpriteRenderer shipRenderer;

    public Base_manager base_Manager;
    public Ressource_manager base_ressource;
    public Ressource_manager ship_ressource;
    public GameObject Harpoon;
    public GameObject aimSprite;
    public GameObject hamecon;
    public GameObject Cage;

    public float shipSpeed = 1.5f;
    private float hameconSpeed = 0.1f;
    private float compteur;
    private float compteurTir;
    private float angle;
    private bool inside_human_base = false;
    private bool inside_divine_sponge_ressource = false;
    private bool isRecolting_divine_sponge_ressource = false;
    private bool alive = true;

    public float timerTir = 3;
	public int timer_recolte;
    public float aimingRadius = 0.8f;
    public float respawn_timer = 10;
    public int health_point = 3;

    public GameObject ressource_recolte;
    public int tools_recolte;

	private bool lock_player_value;

    // Start is called before the first frame update
    void Start()
    {
		lock_player_value = false;
    }
   
    private void FixedUpdate()
    {
		if(!lock_player_value){
			shipMove();
            aim();
            harpoon();
            obstruction();
            hameconMove();
            if (inside_human_base)
            {
                if (Input.GetButtonDown("ButtonY_1"))
                {
                    Debug.Log("button y pressed");
                    base_Manager.update_unit();
                    base_Manager.display_option();
                }
                if (Input.GetButtonDown("ButtonB_1"))
                {
                    Debug.Log("button b pressed");
                    base_Manager.echange_wepaon();
                    base_Manager.display_option();

                }
            }
            compteurTir -= Time.fixedDeltaTime;
            is_alive();
		}
    }

	private void harpoon()
    {
        throwing("BumperRight_1", Harpoon);
    }

    private void obstruction()
    {
        throwing("BumperLeft_1", Cage);
    }

	private void aim()
    {
        aiming = new Vector3(Input.GetAxis("RightStickX_1"), Input.GetAxis("RightStickY_1"), 0);
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

    private void shipMove()
    {
		direction = new Vector3(Input.GetAxis("Horizontal_1"), 0, 0);
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		if(direction.x > 0){
			if(!(transform.position.x >= 5.7)){
				transform.Translate(shipSpeed * direction * Time.fixedDeltaTime);
			}
		}
		else if (direction.x < 0){
			if(!(transform.position.x <= -6.49f)){
				transform.Translate(shipSpeed * direction * Time.fixedDeltaTime);
			}
		}
		if (direction.x > 0)
        {
            this.gameObject.transform.localScale = new Vector3(-1, this.gameObject.transform.localScale.y);
        }
        else if (direction.x < 0)
        {
            this.gameObject.transform.localScale = new Vector3(1, this.gameObject.transform.localScale.y);
        }
    }

    private void hameconMove()
    {
        hameconDirection = new Vector3(0, Input.GetAxis("Vertical_1"), 0);
		if (hamecon.transform.localPosition.y <= -0.73f)
        {
			if(hamecon.transform.localPosition.y >= -7.01f){
				hamecon.transform.localPosition = new Vector3(0, hamecon.transform.localPosition.y + hameconDirection.y * hameconSpeed, 0);
			}
			else{
				hamecon.transform.localPosition = new Vector3(0, -7f, 0);
			}
        }
        else
        {
            // hameconDirection = new Vector3(0, 0, 0);
            hamecon.transform.localPosition = new Vector3(0, -0.73f, 0);
        }
    }

    private void throwing(string button, GameObject throwable)
    {
        if (Input.GetButtonDown(button) && aiming != Vector3.zero)
        {
            if (base_ressource.get_weapon() > 0)
            {
                compteurTir = timerTir;
                GameObject C = Instantiate(throwable, gameObject.transform, true);
                C.transform.position = transform.position;
                angle = Vector2.Angle(transform.up, aiming);

                if (aiming.x >= 0)
                    angle = -angle;

                C.transform.eulerAngles = new Vector3(0, 0, angle);
                C.GetComponent<Rigidbody2D>().velocity = aiming * 5f;
                Destroy(C, 5);
                C.transform.parent = null;
                base_ressource.remove_weapon(1);
                base_Manager.update_ui();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("enter into a trigger");
    	if (collision.gameObject.CompareTag("Human_base"))
    	{
			Debug.Log("enter into a human base trigger");
    		base_ressource = collision.gameObject.GetComponent<Ressource_manager>();
			base_Manager = collision.gameObject.GetComponent<Base_manager>();
            
			int temp_divine_sponge = ship_ressource.get_divine_sponge();
            ship_ressource.remove_divine_sponge(temp_divine_sponge);
			base_ressource.add_divine_sponge(temp_divine_sponge);

    		base_Manager.display_option();
			base_Manager.update_ui();

			inside_human_base = true;

    	}
		else if (collision.gameObject.CompareTag("Divine_sponge_ressource"))
        {
            // Debug.Log("hamecon contre éponge !");
            ressource_recolte = collision.gameObject;
            inside_divine_sponge_ressource = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Human_base"))
		{
			Base_manager base_Manager = collision.gameObject.GetComponent<Base_manager>();
			base_Manager.remove_option();

			inside_human_base = false;
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
