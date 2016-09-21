using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HoverAroundZones : MonoBehaviour {
	public GameObject FairyObj;
	public float HoverSpeedMin=0.3f;
	public float HoverSpeedMax=0.6f;
	public float FarHoverSpeed= 10f;
	public float ZoneChangeFrequencyMin= 5f;
	public float ZoneChangeFrequencyMax= 10f;
	public float FarDistance = 10f;

	public List<GameObject> HoverZones;

	private int HoverZone_i=0;
	private Vector3 Destination;
	private float TimeLeftForZoneChange;
	private float HoverSpeed;

	// Use this for initialization
	void Start () {
		FairyObj = Instantiate (FairyObj, transform.position, Quaternion.identity) as GameObject;
		SetNewHoverZone ();
		SetNewDestination ();
	}
	
	// Update is called once per frame
	void Update () {
		TimeLeftForZoneChange -= Time.deltaTime;
		if (Vector3.Distance (FairyObj.transform.position, Destination) < 0.01f) {
			SetNewDestination ();
		}
		if (Vector3.Distance (FairyObj.transform.position, Destination) > FarDistance) {
			SetNewDestination ();
			HoverSpeed = FarHoverSpeed;
		}

		if (TimeLeftForZoneChange <= 0f) {
			SetNewHoverZone ();
		}

		FairyObj.transform.position = Vector3.MoveTowards (FairyObj.transform.position, Destination, HoverSpeed * Time.deltaTime);
	}

	void SetNewDestination(){
		GameObject zone = HoverZones [HoverZone_i] as GameObject;
		Vector3 zoneExtents = zone.GetComponent<BoxCollider> ().bounds.extents;
		Vector3 nd = zone.transform.position;
		nd.x += Random.Range (-zoneExtents.x, zoneExtents.x);
		nd.y += Random.Range (-zoneExtents.y, zoneExtents.y);
		nd.z += Random.Range (-zoneExtents.z, zoneExtents.z);

		Destination = nd;
		HoverSpeed = Random.Range (HoverSpeedMin, HoverSpeedMax);
	}

	void SetNewHoverZone(){
		HoverZone_i = Random.Range (0, HoverZones.Count);
		TimeLeftForZoneChange = Random.Range (ZoneChangeFrequencyMin, ZoneChangeFrequencyMax);
	}

}
