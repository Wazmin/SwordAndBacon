using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
    private const int  DIVISION = 100;
    private float vieBoss;
    private float viePlayers;
	private bool playerIntro = false;

	private bool jouer = true;
	private int numTuto =0;

    // lien vers le rituel Manager
    public GameObject ceGO;
    private RituelManager rm;

    // le jeu est actif ou stoppé
    // pour fin partie ou pause
    private bool partieActive =true;

    //lien vers GameObject Personnage pour gestion position
    // dans l'ordre boss, knight1, knight2
    public GameObject[] model3DPerso = new GameObject[3];
    private Vector3 spawnPosBoss = new Vector3();
    private Vector3 spawnPosKn1 = new Vector3();
    private Vector3 spawnPosKn2 = new Vector3();

    private float scaleBoss;
    private float scalePlayers;

    //barres de vies
    public Slider barreBoss;
    public Slider barrePlayer;
	private AudioSource AU;
	private SoundCore SC;

	//scene de fin de jeu
	public GameObject[] screenFin = new GameObject[3]; 

	//affiche tutorial
	public GameObject[] screenTuto = new GameObject[3];

	// Use this for initialization
	void Start () {
		AU = GetComponent<AudioSource> ();
		SC = GetComponent<SoundCore> ();
        rm = ceGO.GetComponent<RituelManager>();
		takeAndSaveSpawn();
        initPartie();
		tutorial ();
		stopJoueurs ();
    }
	
	// Update is called once per frame
	void Update () {
		if (partieActive) {
			if (numTuto < 3) {
				tutorial ();
				screenTuto [numTuto].SetActive (true);
				if (Input.GetButtonUp ("Fire1"))
					numTuto++;
			} else {
				tutorial ();
				deStopJoueurs ();
			}
		} else {
			stopJoueurs ();

		}
		
        //mise a jour taille barre boss
        Vector3 scale = barreBoss.transform.localScale;
        scale.x = vieBoss * scaleBoss / DIVISION;
        barreBoss.transform.localScale = scale;

        //mise a jour taille barre player
        scale = barrePlayer.transform.localScale;
        scale.x = viePlayers * scalePlayers / DIVISION;
        barrePlayer.transform.localScale = scale;

		if (!playerIntro && !AU.isPlaying) {
			AU.loop = true;
			playerIntro = true;
			SC.PlaySound (gameObject, "Loop");
		}
    }

    // methodes de degats
    public void bossPrendDegats(float nbDegats)
    {
        vieBoss -= nbDegats;
        if (vieBoss <= 0.0) finPartie(1);
    }
    public  void playerPrendDegats(float nbDegats)
    {
        viePlayers -= nbDegats;
		if (viePlayers <= 0.0) finPartie(0);
		
    }

    // initialise la partie avec les points de vie et les spawn
    void initPartie()
    {
        rm.nouvellePartie();
		SC.PlaySound (gameObject, "Intro");
		playerIntro = false;
        vieBoss = 100;
        viePlayers = 100;
        scaleBoss = barreBoss.transform.localScale.x;
        scalePlayers = barrePlayer.transform.localScale.x;
		Debug.Log ("SCALE BOSS"+scaleBoss);

        model3DPerso[0].GetComponent<Transform>().position = spawnPosBoss;
        model3DPerso[1].GetComponent<Transform>().position = spawnPosKn1;
        model3DPerso[2].GetComponent<Transform>().position = spawnPosKn2;

		//desactive les screen de fin
		for (int i = 0; i < 3; i++) {
			screenFin [i].SetActive (false);
			screenTuto [i].SetActive (false);
		}

    }

    // fonction qui sauvgarde la position des spawns au chargement
    private void takeAndSaveSpawn()
    {
        spawnPosBoss = model3DPerso[0].GetComponent<Transform>().position;
        spawnPosKn1 = model3DPerso[1].GetComponent<Transform>().position;
        spawnPosKn2 = model3DPerso[2].GetComponent<Transform>().position;
    }

    //lors que le Boss ou un joueur gagne
    // numFin indique qui a gagné
    void finPartie(int numFin)
    {
		stopJoueurs ();
		screenFin [numFin].SetActive (true);
    }

	//stop les joueurs
	void stopJoueurs(){
		for (int i = 0; i < 3; i++) {
			model3DPerso[i].GetComponent<Movement>().InputLocked = true;
		}
	}
	//deStop les joueurs
	void deStopJoueurs(){
		for (int i = 0; i < 3; i++) {
			model3DPerso[i].GetComponent<Movement>().InputLocked = false;
		}
	}
		

	void tutorial (){
			screenTuto [0].SetActive (false);
			screenTuto [1].SetActive (false);
			screenTuto [2].SetActive (false);	
			}


}
