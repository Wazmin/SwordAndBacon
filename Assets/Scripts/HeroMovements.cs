using UnityEngine;
using System.Collections;

public class HeroMovements : MonoBehaviour {
    // sert à identifié les différents input de mouvements
    private string mouvementHorizontal = "Horizontal";
    private string mouvementVertical = "Vertical";
    public string idHeroMouvement = "Boss/Knight1/Knight2";

    //reglage pour gameplay
    public float moveSpeed;
    private bool peutBouger;

    //variable utile au personnage
    private Rigidbody RB;


    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void AllowMove(bool move)
    {
        peutBouger = move;
    }
}
