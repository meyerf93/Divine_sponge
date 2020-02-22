using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ressource_manager : MonoBehaviour{
	
	public int Weapon_global_ressource;
	public int Divine_sponge_global_ressource;

	public int get_weapon(){
		return Weapon_global_ressource;
	}

	public int get_divine_sponge(){
		return Divine_sponge_global_ressource;
	}

	public void add_weapon(int weapon){
		if(weapon > 0){
			Weapon_global_ressource += weapon;
		}
	}

	public void add_divine_sponge(int divine_sponge){
		if(divine_sponge > 0){
			Divine_sponge_global_ressource += divine_sponge;
		}
	}

	public void remove_weapon(int weapon){
		if(weapon > 0){
			Weapon_global_ressource -= weapon;
		}
	}

	public void remove_divine_sponge(int divine_sponge){
		if (divine_sponge > 0)
		{
			Divine_sponge_global_ressource -= divine_sponge;
		}
	}
}