using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Game_manager : MonoBehaviour
{
	public FishController fish_player;
	public ShipController ship_player;
	public Base_manager human_base;
	public Base_manager jellyman_base;

	public Image win_human;
	public Image win_jellyman;
	public Text win_text;


	private void Update()
	{
		if (human_base.get_life() == 0 || jellyman_base.get_life() == 0)
		{
			fish_player.lock_player();
			ship_player.lock_player();
			if (jellyman_base.get_life() == 0)
			{
				win_text.text = "Human WIN !";
				win_human.enabled = true;
				win_text.enabled = true;
				if (Input.GetButton("ButtonA_1") || Input.GetButton("ButtonA_2"))
				{
					SceneManager.LoadScene("Menu");

				}
			}
			else if (human_base.get_life() == 0)
			{
				win_text.text = "JellyMan WIN !";
				win_jellyman.enabled = true;
				win_text.enabled = true;
				if (Input.GetButton("ButtonA_1") || Input.GetButton("ButtonA_2"))
				{
					SceneManager.LoadScene("Menu");
				}
			}
			else
			{
				win_human.enabled = false;
				win_jellyman.enabled = false;
				win_text.enabled = false;
			}
		}

	}
}
