using UnityEngine;
using System.Collections;

public class activateRituel : MonoBehaviour {
    public bool canActivate;
    public GameObject RituelManager;
    private RituelManager rm;
    private int indice;


    // si le joueur est dans la zone et clique sur 5
    // il active une rune
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
			if (Input.GetButton(other.GetComponent<KnightActions>().UseControllerName) && canActivate &&
				!other.GetComponent<KnightActions>().GetHasSword())
            {
                rm.addRituel(indice);
            }
        }
 
            
    }

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
