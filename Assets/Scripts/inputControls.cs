using UnityEngine;
using System.Collections;


public class inputControls : MonoBehaviour {
    private bool degatBoss;
    private bool degatP1;

    public float nbDegatBoss;
    public float nbDegatPlayer;

    private  gameManager gm;


	// Use this for initialization
	void Start () {
        gm = GetComponent<gameManager>();
        degatBoss = false;
        degatP1 = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            gm.bossPrendDegats(nbDegatBoss);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            gm.playerPrendDegats(nbDegatPlayer);
        }
            


    }
}
