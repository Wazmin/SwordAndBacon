using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {
	public bool bossAffect=false;
	public bool knightAffect= false;


	private float damageDeal=15f;
	private GameManager1 GAME;

	// Use this for initialization
	void Start () {
		GAME = GameObject.Find ("_GAMEMANAGER").gameObject.GetComponent<GameManager1> ();
		if (GAME == null) {
			Debug.LogError ("GAME MANAGER NOT FOUND");
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDamage(float dmg){
		damageDeal = dmg;
	}

	void OnTriggerEnter(Collider o){
		if (bossAffect && o.GetComponent<BossActions> () != null) {
			GAME.bossPrendDegats ();
			gameObject.SetActive(false);
		} else if (knightAffect && o.GetComponent<KnightActions> () !=null) {
			GAME.playerPrendDegats ();
			if (gameObject.tag == "pike") {
				DestroyObj ();
			}
			gameObject.SetActive(false);
		}
	}

	public void DestroyObj(){
		Destroy (gameObject);
	}
}
