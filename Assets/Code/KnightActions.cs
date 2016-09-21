using UnityEngine;
using System.Collections;

public class KnightActions : MonoBehaviour {
	public string UseControllerName = "Knight1Melee";
	public string MeleeControllerName = "BossMelee";

	public float MeleeCD = 2f;
	private GameObject MagicSword;
	private GameObject SwordGrabPoint;

	private float UseCD = 0.5f;
	private float MeleeCDTimeRemaining = 0f;
	private float MeleeActiveTime = 0.3f;
	private float PickupCD= 0.5f;
	private float PickupTimeRemain = 0f;
	private bool MeleeEffectZoneIsActive = false;
	private bool ChargeEffectZoneIsActive= false;
	private bool HasSword = false;
	private bool MeleeTouchedBoss = false;
	public GameObject SwordObj;

	private RituelManager RManager;

	private GameObject ChargeEffectZone;
	private GameObject MeleeEffectZone;
	private Movement MOV;
	private Animator AN;
	private AudioSource AU;
	private SoundCore SC;
	// Use this for initialization
	void Start () {
		MeleeEffectZone = transform.Find("Mesh").transform.Find ("MeleeEffectZone").gameObject as GameObject;
		RManager = GameObject.Find ("_GAMEMANAGER").GetComponent<RituelManager> ();
		SwordGrabPoint = transform.Find ("Mesh").transform.Find ("SwordGrabPoint").gameObject as GameObject;
		MOV = GetComponent<Movement> ();
		AU = GetComponent<AudioSource> ();
		SC = GetComponent<SoundCore> ();
		ChargeEffectZone = transform.Find ("Mesh").transform.Find ("BodyEffectZone").gameObject as GameObject;
		AN = GetComponentInChildren<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		MeleeManage ();
		UseManage ();
		ChargeManage ();
	}


	void MeleeManage(){
		MeleeCDTimeRemaining -= Time.deltaTime;

		if (Input.GetButtonDown (MeleeControllerName) && HasSword && MeleeCDTimeRemaining <= 0f && !MOV.InputLocked) {
			MeleeEffectZone.SetActive (true);
			MeleeCDTimeRemaining = MeleeCD;
			MeleeEffectZoneIsActive = true;
			SC.PlaySound (gameObject, "Attack");
			AN.SetBool ("Sword", true);
			AN.SetInteger ("State", 3);
		}
		if (MeleeEffectZoneIsActive && (MeleeCDTimeRemaining <= (MeleeCD - MeleeActiveTime))) {
			MeleeEffectZoneIsActive = false;
			MeleeEffectZone.SetActive (false);
			AN.SetBool ("Sword", false);
			if (MeleeTouchedBoss) {
				BreakSword ();
				MeleeTouchedBoss = false;
			}
		}

	}

	void ChargeManage(){
		if (!ChargeEffectZoneIsActive && GetComponent<Movement>().IsDashing()) {
			ChargeEffectZone.SetActive (true);
			ChargeEffectZoneIsActive = true;
			SC.PlaySound (gameObject, "Dash");
//			AN.SetInteger ("State", 2);
		}
		else if (ChargeEffectZoneIsActive && !GetComponent<Movement>().IsDashing()) {
			ChargeEffectZoneIsActive = false;
			ChargeEffectZone.SetActive (false);
		}

	}

	void UseManage(){
		PickupTimeRemain -= Time.deltaTime;
		if (Input.GetButtonDown (UseControllerName) && HasSword  && !MOV.InputLocked) {
			DropSword ();
		}
	}

	void OnTriggerStay(Collider other){
		if (Input.GetButtonDown(UseControllerName) && other.tag == "MagicSword"  && !MOV.InputLocked) {
			PickupSword (other.gameObject);
		}

		if (ChargeEffectZoneIsActive && other.tag == "Player") {
			other.GetComponent<KnightActions> ().DropSword ();
		}
	}


	public void PickupSword(GameObject sword){
		if (HasSword || PickupTimeRemain > 0f) {
			return;
		}
		HasSword = true;
		PickupTimeRemain = PickupCD;
	/*	MagicSword = sword;
		MagicSword.transform.position = SwordGrabPoint.transform.position;
		MagicSword.transform.rotation = SwordGrabPoint.transform.rotation;
		MagicSword.transform.SetParent (SwordGrabPoint.transform);
		MagicSword.SetActive (false); */
		SwordObj.SetActive (true);
		RManager.NotifySwordWasPickedUp ();
		RManager.NotifySwordWasRemovedFromShrine ();
		SC.PlaySound (gameObject, "Pickup");

		return;
	}

	public void DropSword(){
		if (!HasSword || PickupTimeRemain > 0f) {
			return;
		}
		HasSword = false;
		SwordObj.SetActive (false);
	/*	MagicSword.SetActive (true);
		MagicSword.transform.position = transform.position;
		MagicSword.transform.rotation = transform.rotation;
		MagicSword.transform.SetParent (null);
		MagicSword = null; */
		RManager.NotifySwordWasDropped ();
		PickupTimeRemain = PickupCD;
		SC.PlaySound (gameObject, "Pickup");

	}

	public void BreakSword(){
		HasSword = false;
		Destroy (MagicSword);
		SwordObj.SetActive (false);
		RManager.resetNbRituel ();
		SC.PlaySound (gameObject, "BossHit");
	}

	public void NotifyMeleeTouchedBoss(){
		MeleeTouchedBoss = true;
	}

	public bool GetHasSword(){
		return HasSword;
	}

}
