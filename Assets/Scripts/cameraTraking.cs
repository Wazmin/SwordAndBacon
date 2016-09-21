using UnityEngine;
using System.Collections;

public class cameraTraking : MonoBehaviour {
    public GameObject[] personnages = new GameObject[3];
    public float angleVueHauteur=33;//semble etre bon


    //angles utilise recul zCam et hauteur
    private float angleA;
    private float angleB;

 
    private Vector2 posP1 = new Vector2();
    private Vector2 posP2 = new Vector2();
    private Vector2 posP3 = new Vector2();

    private Vector3 posCamera = new Vector3();
	private Quaternion rotationCam;
    private float hauteurCam;


    // Use this for initialization
    void Start () {
        angleA = 55;
        angleB = angleA - angleVueHauteur / 2;
        posP1 = new Vector2(personnages[0].transform.position.x, personnages[0].transform.position.z);
        posP2 = new Vector2(personnages[1].transform.position.x, personnages[1].transform.position.z);
        posP3 = new Vector2(personnages[2].transform.position.x, personnages[2].transform.position.z);
        posCamera = GetComponent<Transform>().position;
		rotationCam = transform.rotation;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //update angles
        angleB = angleA - angleVueHauteur / 2;
        //refresh Position
        posP1 = new Vector2(personnages[0].transform.position.x, personnages[0].transform.position.z);
        posP2 = new Vector2(personnages[1].transform.position.x, personnages[1].transform.position.z);
        posP3 = new Vector2(personnages[2].transform.position.x, personnages[2].transform.position.z);

        // calcul position
        float minX = Mathf.Min(Mathf.Min(posP1.x, posP2.x), posP3.x);
        float maxX = Mathf.Max(Mathf.Max(posP1.x, posP2.x), posP3.x);
        float minY = Mathf.Min(Mathf.Min(posP1.y, posP2.y), posP3.y);
        float maxY = Mathf.Max(Mathf.Max(posP1.y, posP2.y), posP3.y);

        // look at the center
        posCamera = new Vector3(((maxX - minX) /2 + minX),0, ((maxY - minY) / 2 + minY));
        GetComponent<Transform>().LookAt(posCamera);

        /** CALCUL PAR RAPPORT A ANGLE HAUTEUR DE LA CAM **/
        //distance d hauteur angle cam
        float dAxeZ = (maxY - minY) / 2;

        // angleA est l'angle de rotationX de la cam qui reste fixe

        //calcul du z et y
        float tanAngleA = Mathf.Tan(DegreeToRadian(angleA));
        float tanAngleB = Mathf.Tan(DegreeToRadian(angleB));

        float z = (tanAngleB * dAxeZ) / (tanAngleA - tanAngleB);
        float yAxeZ = tanAngleA * z;// par rapport angle de vue hauteur



        /** CALCUL PAR RAPPORT A ANGLE DE LARGEUR DE LA CAM **/
        float f = Vector2.Distance(Vector2.zero , new Vector2(z,yAxeZ));

        float dAxeX = (maxX - minX) / 2;
        float fPrim = dAxeX / Mathf.Tan(DegreeToRadian(30));
        f = Mathf.Max(f,fPrim);

        float y = Mathf.Cos(DegreeToRadian(35)) * f;
        z = y / Mathf.Tan(DegreeToRadian(55));    

        //integration des valeur position cam
        posCamera.y = y;
        posCamera.z -= z; 
        GetComponent<Transform>().position = posCamera;

    }



    // convertion degree radian
    private float DegreeToRadian (float angle)
    {
        return Mathf.PI * angle / 180.0f;
    }


}
