using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource_generator : MonoBehaviour
{
	public int min_divine_sponge_generate;
	public int max_divine_sponge_generate;

	public int timer_spot_base;
	public int timer_spot_2_min;
	public int timer_spot_2_max;
	public int timer_spot_3_min;
	public int timer_spot_3_max;
	public int timer_spot_4_min;
	public int timer_spot_4_max;

	public GameObject ressource_spot_1;
	public GameObject ressource_spot_2;
	public GameObject ressource_spot_3;
	public GameObject ressource_spot_4;
	public GameObject ressource_spot_5;

	private Animator ressource_spot_animator_1;
	private Animator ressource_spot_animator_2;
	private Animator ressource_spot_animator_3;
	private Animator ressource_spot_animator_4;
	private Animator ressource_spot_animator_5;

	private Ressource_manager ressource_spot_manager_1;
	private Ressource_manager ressource_spot_manager_2;
	private Ressource_manager ressource_spot_manager_3;
	private Ressource_manager ressource_spot_manager_4;
	private Ressource_manager ressource_spot_manager_5;
    
	private IEnumerator coroutine_spot_1;
	private IEnumerator coroutine_spot_2;
	private IEnumerator coroutine_spot_3;
	private IEnumerator coroutine_spot_4;
	private IEnumerator coroutine_spot_5;
    
	private void Start()
	{
		ressource_spot_animator_1 = ressource_spot_1.GetComponent<Animator>();
		ressource_spot_manager_1 = ressource_spot_1.GetComponent<Ressource_manager>();
		ressource_spot_animator_2 = ressource_spot_2.GetComponent<Animator>();
        ressource_spot_manager_2 = ressource_spot_2.GetComponent<Ressource_manager>();
		ressource_spot_animator_3 = ressource_spot_3.GetComponent<Animator>();
        ressource_spot_manager_3 = ressource_spot_3.GetComponent<Ressource_manager>();
		ressource_spot_animator_4 = ressource_spot_4.GetComponent<Animator>();
        ressource_spot_manager_4 = ressource_spot_4.GetComponent<Ressource_manager>();
		ressource_spot_animator_5 = ressource_spot_5.GetComponent<Animator>();
        ressource_spot_manager_5 = ressource_spot_5.GetComponent<Ressource_manager>();
		
		coroutine_spot_1 = Generate(ressource_spot_manager_1,ressource_spot_animator_1,timer_spot_base,timer_spot_base,min_divine_sponge_generate,max_divine_sponge_generate);
		coroutine_spot_2 = Generate(ressource_spot_manager_2,ressource_spot_animator_2,timer_spot_2_min,timer_spot_2_max,min_divine_sponge_generate,max_divine_sponge_generate);
		coroutine_spot_3 = Generate(ressource_spot_manager_3,ressource_spot_animator_3,timer_spot_3_min,timer_spot_3_max,min_divine_sponge_generate,max_divine_sponge_generate);
		coroutine_spot_4 = Generate(ressource_spot_manager_4,ressource_spot_animator_4,timer_spot_4_min,timer_spot_4_max,min_divine_sponge_generate,max_divine_sponge_generate);
		coroutine_spot_5 = Generate(ressource_spot_manager_5,ressource_spot_animator_5,timer_spot_base,timer_spot_base,min_divine_sponge_generate,max_divine_sponge_generate);

		StartCoroutine(coroutine_spot_1);
		StartCoroutine(coroutine_spot_2);
		StartCoroutine(coroutine_spot_3);
		StartCoroutine(coroutine_spot_4);
		StartCoroutine(coroutine_spot_5);


	}

	private IEnumerator Generate(Ressource_manager ressource_Manager, Animator animator, int timer_spot_min,int timer_spot_max, int min_divine_sponge_generate, int max_divine_sponge_generate){
		while (true)
        {
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle_empty"))
			{
				float temp_timer = Random.Range((float)timer_spot_min, (float)timer_spot_max);

				yield return new WaitForSeconds(temp_timer);
				//Debug.Log("Generate a new ressource");
				float temp_divine_sponge = Random.Range((float)min_divine_sponge_generate, (float)max_divine_sponge_generate);
				ressource_Manager.add_divine_sponge((int)temp_divine_sponge);
				animator.SetTrigger("Generate");
			}
			yield return new WaitForSeconds(1);
        }
	}
}
