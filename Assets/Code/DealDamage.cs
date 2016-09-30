using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {
	public bool bossAffect=false;
	public bool knightAffect= false;


	private float damageDeal=15f;
	private gameManager GAME;
	private RituelManager RManager;
	// Use this for initialization
	void Start () {
		GAME = GameObject.Find ("_GAMEMANAGER").gameObject.GetComponent<gameManager> ();
		RManager = GameObject.Find ("_GAMEMANAGER").gameObject.GetComponent<RituelManager> ();

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
			GAME.bossPrendDegats (RManager.degatRituel());
			gameObject.SetActive(false);
		} else if (knightAffect && o.GetComponent<KnightActions> () !=null) {
			GAME.playerPrendDegats (damageDeal);
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
