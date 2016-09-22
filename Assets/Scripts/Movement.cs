using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public string MoveHoriControllerName = "Horizontal";
    public string MoveVertControllerName = "Vertical";
    public string DashControllerName = "Dash";

    public float MoveSpeed= 10f;
    public float DashStrength = 40f;
    public float DashDuration = 0.3f;
    public float DashCDTimeSec = 1f;
	public bool InputLocked  = false;



	private Rigidbody RB;
	private GameObject Mesh;
    private Vector3 DashVelocity;
    private float DashCD;
    private float DashTimeRemaining;
	private Animator AN;
	// Use this for initialization
	void Start () {
		RB = GetComponent<Rigidbody> ();
		Mesh = transform.Find ("Mesh").gameObject as GameObject;
		AN = GetComponentInChildren<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (DashCD > 0f)
        {
            DashCD -= Time.deltaTime;
            DashTimeRemaining -= Time.deltaTime;
        }


		ListenKeys2 ();
		
	}


    void ListenKeys2() {
        Vector3 newMove = new Vector3();
		if (!InputLocked) {
			newMove -= transform.forward * Input.GetAxis (MoveVertControllerName) * MoveSpeed;
			newMove += transform.right * Input.GetAxis (MoveHoriControllerName) * MoveSpeed;
		}
        RB.AddForce(newMove * 50f);

		if (Input.GetButtonDown(DashControllerName) && DashCD <= 0f && !InputLocked)
        {
            Vector3 dashf = new Vector3();
			dashf -= transform.forward * Input.GetAxis(MoveVertControllerName) * DashStrength;
			dashf += transform.right * Input.GetAxis(MoveHoriControllerName) * DashStrength;
            RB.AddForce(dashf);
            DashCD = DashCDTimeSec;
            DashTimeRemaining = DashDuration;
        }

        if (DashTimeRemaining <= 0f) { 
            Vector3 velocity = RB.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -MoveSpeed, MoveSpeed);
            velocity.z = Mathf.Clamp(velocity.z, -MoveSpeed, MoveSpeed);
            RB.velocity = newMove;
			if (newMove.Equals (Vector3.zero)) {
				AN.SetInteger ("State", 0);
			} else {
				AN.SetInteger ("State", 1);
			}
        }

		//Orient
		Vector3 meshLookAt = Mesh.transform.position;
		meshLookAt.x += Input.GetAxis (MoveHoriControllerName) * 1f;
		meshLookAt.z += Input.GetAxis (MoveVertControllerName) * -1f;
		Mesh.transform.LookAt (meshLookAt);

	}


	public bool IsDashing(){
		return DashTimeRemaining > 0f;
	}
		
}
