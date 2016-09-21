using UnityEngine;
using System.Collections;

public class DeathRayProjectile : MonoBehaviour {
	public float MoveSpeed= 15f;
	public float LifeTime= 1f;

	private Rigidbody RB;
	private float lf;
	void Start () {
		RB = GetComponent<Rigidbody> ();
		lf = LifeTime;
	}
	
	// Update is called once per frame
	void Update () {		
		if (lf <= 0f) {
			GameObject.Destroy (this.gameObject);
		}

		lf -= Time.deltaTime;
		Vector3 v = new Vector3 ();
		v += MoveSpeed * transform.forward * Time.deltaTime;
		RB.velocity = v;


	}
}
