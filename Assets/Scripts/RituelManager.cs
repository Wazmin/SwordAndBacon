using UnityEngine;
using System.Collections;

public class RituelManager : MonoBehaviour {
    private const int MAXRITUELS = 6;
    private const int RITUELACTIF = 3;
	private bool peutLancerRituel;

    //la sequence des rituels dans l'ordre
    public int[] sequenceRituel = new int[3];

    // contient les degats selon le rang de rituel
    public float[] tableauDegats = new float[3];

    public GameObject[] rituel= new GameObject[6];
    public GameObject[] interfaceRituel = new GameObject[3];

	public GameObject[] MagicSword_prefabs = new GameObject[3];
	public GameObject[] MagicSwordSpawnSpots = new GameObject[1];
	private GameObject MagicSwordLastSpawned= null;
	private bool SwordIsPickedUp = false;
	private bool SwordWasMovedFromShrine = false;

    private int nbRituelActif;

	// Use this for initialization
	void Start () {
        nouvellePartie();
    }
	
    // genre une sequence et active les totems
    //a utiliser pour une nouvelle partie
    public void nouvellePartie()
    {
        peutLancerRituel = false;
        nbRituelActif = 0;
        genereSequenceRituel();
        initRituel();
    }


	// Update is called once per frame
	void Update () {
	
	}

    // active un niveau de rituel
    public void addRituel(int indiceRituel)
    {
		if(nbRituelActif < 3 & !SwordIsPickedUp) //securité
        {
            if(sequenceRituel[nbRituelActif] == indiceRituel)
            {
                interfaceRituel[nbRituelActif].SetActive(true);
                rituel[indiceRituel].GetComponent<activateRituel>().desactivate();
                nbRituelActif++;
				peutLancerRituel = true;

				if (SwordWasMovedFromShrine) { //sword was picked up and is in gameworld
					GameObject go = Instantiate (MagicSword_prefabs [nbRituelActif - 1], MagicSwordLastSpawned.transform.position, Quaternion.identity) as GameObject;
					GameObject.Destroy (MagicSwordLastSpawned);
					MagicSwordLastSpawned = go;
				}

				else if (MagicSwordLastSpawned != null) { //sword is in gameworld but is still at spawnPoint
					GameObject go = Instantiate (MagicSword_prefabs [nbRituelActif - 1], MagicSwordSpawnSpots [Random.Range (0, MagicSwordSpawnSpots.Length-1)].transform.position, Quaternion.identity) as GameObject;
					GameObject.Destroy (MagicSwordLastSpawned);
					MagicSwordLastSpawned = go;
				}
				else  { // first instance of sword
					MagicSwordLastSpawned = Instantiate (MagicSword_prefabs [nbRituelActif-1], MagicSwordSpawnSpots [Random.Range (0, MagicSwordSpawnSpots.Length-1)].transform.position, Quaternion.identity) as GameObject;

				}

            }
            
        }
        
    }

    // reset le statut des objets rituels
    public void resetNbRituel()
    {
        nbRituelActif = 0;
        initRituel();
		peutLancerRituel = false;
		SwordIsPickedUp = false;
		SwordWasMovedFromShrine = false;
		MagicSwordLastSpawned = null;
        for (int i = 0;i < 3; i++)
        {
            interfaceRituel[i].SetActive(false);
        }
    }

    //revoie le nombre degats correspondant au niveau de rituel 
    public float degatRituel()
    {
        return tableauDegats[nbRituelActif-1];
    }

    //remet les totems a desactiver et active ceux de la sequence
    public void initRituel()
	{
		//init des rituels desactiver
		for (int i = 0; i < 6; i++) {
			rituel [i].GetComponent<activateRituel> ().desactivate ();
			rituel [i].GetComponent<activateRituel> ().setIndice (i);
		}

		//activation des rituels selectionnes
		for (int i = 0; i < 3; i++) {
			rituel [sequenceRituel [i]].GetComponent<activateRituel> ().activate ();
		}
	}

    // genere la sequuence de rituel a effectuer dans l'ordre
    private void genereSequenceRituel()
    {
        int nbRituelSelectionne = 0;
        int idRituel = -1;
        for (int i = 0; i < 3; i++) sequenceRituel[i] = -1;
        while (nbRituelSelectionne < 3)
        {
            idRituel = Random.Range(0, 6); // random entre 0 et 5 inclus
                                           // random, si le rituel est inactif on l'active
                                           //si on a nbRituel a 3 on stop la boucle
            if (!rituelDejaUtilise(idRituel))
            {
                sequenceRituel[nbRituelSelectionne] = idRituel;
                nbRituelSelectionne++;
            }
        }
    }

    // sous fonction de recherche d'element tab 3
    private bool rituelDejaUtilise(int indice)
    {
        bool dejaUtilise = false;
        int i = 0;
        while(!dejaUtilise && i < 3)
        {
            dejaUtilise = (sequenceRituel[i]==indice);
            i++;
        }
        return dejaUtilise;
    }

	public bool canCastRituel(){
		return peutLancerRituel;
	}

	public void NotifySwordWasPickedUp(){
		SwordIsPickedUp = true;
	}	
	public void NotifySwordWasDropped(){
		SwordIsPickedUp = false;
	}
	public void NotifySwordWasRemovedFromShrine(){
		SwordWasMovedFromShrine = true;
	}
}


