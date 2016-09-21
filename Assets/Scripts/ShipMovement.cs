using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

    /*
        Les mouvements du vaisseau sont basés sur celui
        d'un helicoptere
        */

    public float m_SpeedHautBas = 2.0f;         // vitesse de deplacement avant/arriere
    public float m_SpeedGaucheDroite = 2.0f;
    public float m_X_Axis = 2.0f;              // vitesse d'inclinaison haut/bas
    public float m_Y_Axis = 2.0f;              // vitesse de rotation droite/gauche

    // rigidbody de reference (ship)
    private Rigidbody m_Rigidbody;

    /* --- VARIABLE DES INPUTS --- */
    private float m_HorizontalInputValue;
    private float m_VerticalInputValue;
    private float m_MousseXInputValue;
    private float m_MousseYInputValue;

    void Awake()
    {
        // recuperation du rigid body
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        m_HorizontalInputValue = Input.GetAxis("Horizontal");
        m_VerticalInputValue = Input.GetAxis("Vertical");
        m_MousseXInputValue = Input.GetAxis("Mouse X");
        m_MousseYInputValue = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        MouvementsDroits();
        MouvementsRotations();
    }

    void MouvementsDroits()
    {
        // premier vecteur vers l'avant
        Vector3 mvtHaut = transform.forward * m_VerticalInputValue * m_SpeedHautBas * Time.deltaTime;
        // second vecteur gauche droite
        Vector3 mvtDroite = transform.right * m_HorizontalInputValue * m_SpeedGaucheDroite * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + mvtHaut + mvtDroite);
    }

    void MouvementsRotations()
    {
        float sourisY = m_Y_Axis * m_MousseYInputValue * Time.deltaTime;
        float sourisX = m_X_Axis * m_MousseXInputValue * Time.deltaTime;

        //Quaternion turn = Quaternion.Euler(sourisY, sourisX, 0.0f);


        m_Rigidbody.rotation = Quaternion.Euler(m_Rigidbody.rotation.eulerAngles + new Vector3(-sourisY, sourisX, 0.0f)); 
    }


}
