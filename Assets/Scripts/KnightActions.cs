using UnityEngine;
using System.Collections;

public class KnightActions : MonoBehaviour {
	public string UseControllerName = "Knight1Melee";
	public string MeleeControllerName = "BossMelee";

	public float MeleeCD = 2f;
	public GameObject SwordGrabPoint;

	private float UseCD = 0.5f;
	private float MeleeCDTimeRemaining = 0f;
	private float MeleeActiveTime = 0.3f;
	private float PickupCD= 0.5f;
	private float PickupTimeRemain = 0f;
	private bool MeleeEffectZoneIsActive = false;
	private bool ChargeEffectZoneIsActive= false;
	public bool HasSword = false;
	private bool MeleeTouchedBoss = false;


	private GameObject MeleeEffectZone;
	private Movement MOV;
	private Animator AN;
	private AudioSource AU;
	private SoundCore SC;

    /* AJOUT 23/09/2016 */
    public GameManager1 GameManager;
    private int layerMeleeAction = 1 << 8;
    public float meleeRadius = 2.0f;
    public bool bossAtRange = false;
    private bool testBool=false;


	// Use this for initialization
	void Start () {
		MeleeEffectZone = transform.Find("Mesh").transform.Find ("MeleeEffectZone").gameObject as GameObject;
		MOV = GetComponent<Movement> ();
		AU = GetComponent<AudioSource> ();
		SC = GetComponent<SoundCore> ();
		AN = GetComponentInChildren<Animator> ();
        MeleeEffectZone.SetActive(false);
        SwordGrabPoint.SetActive(false);

    }

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(MeleeControllerName))
        {
            /* AJOUT 23/09/2016 */
            Debug.Log("Action de Melee");
            MeleeAction();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            BreakSword();
        }
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

	public void PickupSword(){
        if (HasSword) return;
		SC.PlaySound (gameObject, "Pickup");
        HasSword = true;
        AN.SetBool("Sword", true);
        SwordGrabPoint.SetActive(true);
        MeleeEffectZone.SetActive(true);
	}

	

	public void BreakSword(){
        if (!HasSword) return;
		HasSword = false;
        AN.SetBool("Sword", false);
        MeleeEffectZone.SetActive(false);
        SwordGrabPoint.SetActive(false);
        SC.PlaySound (gameObject, "BossHit");
	}

	public bool GetHasSword(){
		return HasSword;
	}

    /* AJOUT 23/09/2016 */
    private void MeleeAction()
    {
        AN.SetTrigger("hit");
        Debug.Log("Animation lancée");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeRadius, layerMeleeAction,QueryTriggerInteraction.Collide);
        if(hitColliders.Length > 0)
        {
            GameManager.TryToActiveRune(hitColliders[0].gameObject);
        }
        else
        {
            Debug.Log("Aucun collider detecté");
        }
    }

    private void HitBoss()
    {

    }

}
