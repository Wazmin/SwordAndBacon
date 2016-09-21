using UnityEngine;
using System.Collections;

public class BossActions : MonoBehaviour {
    public string DeathRayControllerName = "BossDeathRay";
    public string MeleeControllerName = "BossMelee";
    public string BossAimXAxisControllerName = "BossAimX";
    public string BossAimYAxisControllerName = "BossAimY";

	public float DeathRayDamage = 10f;
	public float MeleeDamage = 10f;
	public float ChargeDamage = 10f;

    public float DeathRayCD = .2f;
    public float MeleeCD = 2f;


    public GameObject DeathRayProjectile;

	private float DeathRayCDTimeRemaining = 0f;
	private float MeleeCDTimeRemaining = 0f;
	private float MeleeActiveTime = 0.1f;
	private bool MeleeEffectZoneIsActive = false;
	private bool ChargeEffectZoneIsActive= false;

    private GameObject DeathRayCastPoint;
	private GameObject DeathRayAimer;
	private GameObject MeleeEffectZone;
	private GameObject ChargeEffectZone;
	private Animator AN;
	private AudioSource AU;
	private SoundCore SC;
    // Use this for initialization
    void Start () {
		DeathRayCastPoint = transform.Find("Mesh").transform.Find("DeathRayCastPoint").gameObject as GameObject;
		MeleeEffectZone = transform.Find("Mesh").transform.Find ("MeleeEffectZone").gameObject as GameObject;
		MeleeEffectZone.GetComponent<DealDamage> ().SetDamage (MeleeDamage);
		ChargeEffectZone = transform.Find ("Mesh").transform.Find ("BodyEffectZone").gameObject as GameObject;
		ChargeEffectZone.GetComponent<DealDamage> ().SetDamage (ChargeDamage);
		AN = GetComponentInChildren<Animator> ();
		AU = GetComponent<AudioSource> ();
		SC = GetComponent<SoundCore> ();
	}
	
	// Update is called once per frame
	void Update () {
		DeathRayManage ();
		MeleeManage ();
		ChargeManage ();
	}

	void DeathRayManage(){
		DeathRayCDTimeRemaining -= Time.deltaTime;


		if (Input.GetButtonDown(DeathRayControllerName) && DeathRayCDTimeRemaining <= 0f){
			GameObject go = Instantiate(DeathRayProjectile, DeathRayCastPoint.transform.position, DeathRayCastPoint.transform.rotation) as GameObject;
			if (go != null) {
				go.GetComponent<DealDamage> ().SetDamage (DeathRayDamage);
			}
			AN.SetInteger ("State", 4);
			SC.PlaySound (gameObject, "BossStomp");
			DeathRayCDTimeRemaining = DeathRayCD;
		}
	}

	void MeleeManage(){
		MeleeCDTimeRemaining -= Time.deltaTime;

		if (Input.GetButtonDown (MeleeControllerName) & MeleeCDTimeRemaining <= 0f) {
			MeleeEffectZone.SetActive (true);
			MeleeCDTimeRemaining = MeleeCD;
			MeleeEffectZoneIsActive = true;
			AN.SetInteger ("State", 3);
			SC.PlaySound (gameObject, "BossPuke");
		}
		if (MeleeEffectZoneIsActive && (MeleeCDTimeRemaining <= (MeleeCD - MeleeActiveTime))) {
			MeleeEffectZoneIsActive = false;
			MeleeEffectZone.SetActive (false);
		}

	}

	void ChargeManage(){
		if (!ChargeEffectZoneIsActive && GetComponent<Movement>().IsDashing()) {
			ChargeEffectZone.SetActive (true);
			ChargeEffectZoneIsActive = true;
			AN.SetInteger ("State", 2);
			SC.PlaySound (gameObject, "BossDash");

		}
		else if (ChargeEffectZoneIsActive && !GetComponent<Movement>().IsDashing()) {
			ChargeEffectZoneIsActive = false;
			ChargeEffectZone.SetActive (false);
		}

	}


}
