using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Base_manager : MonoBehaviour
{
    private bool alive = true;
    private bool lost = false;

	public enum Update_level { Update_1, Update_2, Update_3 };
       
	public Ressource_manager Base_ressource;
	public Image Weapon_icone;
	public Image Divin_sponge_icone;

	public Text Weapon_value;
	public Text Divine_sponge_value;

	public Image Update_unit;
	public Image Update_button;
	public Image Change_ressource;
	public Image Change_ressoource_button;

	public Sprite Update_unit_grey;
	public Sprite Update_unit_grey_2;
	public Sprite Update_unit_active_2;

	public Sprite Update_unit_grey_3;
	public Sprite Update_unit_active_3;

	public Sprite Change_ressource_grey;
	public Sprite Change_ressource_active;
    

	public int weapon_value_update_2;
	public int weapon_value_update_3;
	public int divine_sponge_value_update_2;
	public int divine_sponge_value_update_3;

	public int weapon_change_value;
	public int divine_sponge_change_value;

    public int health_point = 3;
	public Slider life;

	public Update_level level_unit;

	private void Start()
	{
		update_ui();

	}

	public void update_ui(){
		Weapon_value.text = Base_ressource.get_weapon().ToString();
        Divine_sponge_value.text = Base_ressource.get_divine_sponge().ToString();
	}


	public void display_option()
	{
		if (level_unit == Update_level.Update_1)
		{

			if (Base_ressource.get_weapon() >= weapon_value_update_2 &&
			   Base_ressource.get_divine_sponge() >= divine_sponge_value_update_2)
			{
				Update_unit.sprite = Update_unit_active_2;
			}
			else
			{
				Update_unit.sprite = Update_unit_grey_2;

			}
		}
		else if (level_unit == Update_level.Update_2)
		{
			if (Base_ressource.get_weapon() >= weapon_value_update_3 &&
				Base_ressource.get_divine_sponge() >= divine_sponge_value_update_3)
			{
				Update_unit.sprite = Update_unit_active_3;

			}
			else
			{
				Update_unit.sprite = Update_unit_grey_3;
			}
		}
		else if (level_unit == Update_level.Update_3)
		{
			Update_unit.sprite = Update_unit_grey;
		}

		if (Base_ressource.get_divine_sponge() >= divine_sponge_change_value)
		{
			Change_ressource.sprite = Change_ressource_active;
		}
		else
		{
			Change_ressource.sprite = Change_ressource_grey;
		}

		Update_unit.enabled = true;
		Update_button.enabled = true;
		Change_ressource.enabled = true;
		Change_ressoource_button.enabled = true;

	}

	public void remove_option()
	{
		Update_unit.enabled = false;
		Update_button.enabled = false;
		Change_ressource.enabled = false;
		Change_ressoource_button.enabled = false;

	}

	public void update_unit()
	{
		update_unit_2();
		//Debug.Log("in update unit");
		/*if (level_unit == Update_level.Update_1)
		{
			update_unit_2();
		}
		else if (level_unit == Update_level.Update_2)
		{
			update_unit_3();
		}*/
	}

	public void update_unit_2()
	{
		//Debug.Log("in update unit 2");
		if (Base_ressource.get_weapon() >= weapon_value_update_2 &&
			Base_ressource.get_divine_sponge() >= divine_sponge_value_update_2)
		{
			level_unit = Update_level.Update_2;
			Base_ressource.remove_weapon(weapon_value_update_2);
			Base_ressource.remove_divine_sponge(divine_sponge_value_update_2);
			heal();

		}
		update_ui();
	}

	public void update_unit_3()
	{
		//Debug.Log("in update unit 3");
		if (Base_ressource.get_weapon() >= weapon_value_update_3 &&
			Base_ressource.get_divine_sponge() >= divine_sponge_value_update_3)
		{
			level_unit = Update_level.Update_3;
			Base_ressource.remove_weapon(weapon_value_update_3);
			Base_ressource.remove_divine_sponge(divine_sponge_value_update_3);
		}
		update_ui();
	}

	public void echange_wepaon(){
		//Debug.Log("in echange weapon");
		if(Base_ressource.get_divine_sponge() >= divine_sponge_change_value){
			Base_ressource.remove_divine_sponge(divine_sponge_change_value);
			Base_ressource.add_weapon(weapon_change_value);
		}
		update_ui();
	}

    public void has_lost()
    {
        if (lost)
        {
            //appelé écran de victoire avec gagnant indiqué en titre
            //exemple : "Jellyman a gagné"
            // string winner = gameObject.tag;
        }
    }
	public void damage(){
		life.value--;
	}
	public void heal(){
		life.value++;
	}
	public int get_life(){
		return (int)life.value;
	}
}
