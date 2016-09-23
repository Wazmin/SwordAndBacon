using UnityEngine;
using System.Collections;

public class activateRituel : MonoBehaviour {
    public bool canActivate;
    public GameObject RituelManager;
    private RituelManager rm;
    private int indice;

    // Use this for initialization
    void Start () {
        rm = RituelManager.GetComponent<RituelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            rm.resetNbRituel();
        }
    }

    public void activate()
    {
        canActivate = true;
    }

    public void desactivate()
    {
        canActivate = false;
    }

    public bool isActive()
    {
        return canActivate;
    }

    public void setIndice(int numIndice)
    {
        indice = numIndice;
    }
}
