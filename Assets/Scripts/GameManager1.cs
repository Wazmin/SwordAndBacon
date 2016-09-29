using UnityEngine;
using System.Collections;

public class GameManager1 : MonoBehaviour {
    private const int MAXRUNES = 6;
    private const int RUNESACTIVES = 3;
    private const int MAXMAGICSWORD = 3;

    private int[] tabSequenceRituel = new int[RUNESACTIVES];
    public GameObject[] tabRunes = new GameObject[MAXRUNES];
    private int indiceSequence = 0;

    // Gestion apparition disparition des Magics Swords
    public GameObject[] tabMagicSwords = new GameObject[MAXMAGICSWORD];
    private bool isActiveMagicSword;


	// Use this for initialization
	void Start () {
        CreateSequece();
        InitSpotMagicSword();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnMagicSword();
        }
    }

    //create a random sequence to activate runes
    private void CreateSequece()
    {
        int nbRituelSelectionne = 0;
        int idRituel = -1;
        for (int i = 0; i < RUNESACTIVES; i++) tabSequenceRituel[i] = -1;
        while (nbRituelSelectionne < RUNESACTIVES)
        {
            idRituel = Random.Range(0, MAXRUNES); // random entre 0 et 5 inclus
                                                    // random, si le rituel est inactif on l'active
                                                    //si on a nbRituel a 3 on stop la boucle
            if (!rituelDejaUtilise(idRituel))
            {
                tabSequenceRituel[nbRituelSelectionne] = idRituel;
                nbRituelSelectionne++;
            }
        }
        indiceSequence = 0;
    }

    // sous fonction de recherche d'element tab 3=RUNESACTIVES
    private bool rituelDejaUtilise(int indice)
    {
        bool dejaUtilise = false;
        int i = 0;
        while (!dejaUtilise && i < RUNESACTIVES)
        {
            dejaUtilise = (tabSequenceRituel[i] == indice);
            i++;
        }
        return dejaUtilise;
    }

    //tentative d'activation d'une rune
    public bool TryToActiveRune(GameObject runeTestee)
    {
        bool result = false;
        if (indiceSequence >= 0 && runeTestee == tabRunes[indiceSequence]) // activation OK
        {
            Debug.Log("Activation de la rune "+(indiceSequence+1));
            result = true;
            if (++indiceSequence > 2) indiceSequence = -1;
            //prevoir l'action interface
        }   
        return result;
    }

    //init all the MagicSword
    private void InitSpotMagicSword()
    {
        isActiveMagicSword = false;
        for (int i = 0; i < MAXMAGICSWORD; i++)
        {
            tabMagicSwords[i].SetActive(false);
        }
    }

    //Disable a MagicSword
    public void DisableMagicSword(GameObject magicSword)
    {
        for(int i = 0; i< MAXMAGICSWORD; i++)
        {
            if(tabMagicSwords[i] == magicSword)
            {
                tabMagicSwords[i].SetActive(false);
            }
        }
        isActiveMagicSword = false;
    }

    // Choose randomly a spot to spawn the sword
    private void SpawnMagicSword()
    {
        if (isActiveMagicSword) return;
        int rand = Random.Range(0, MAXMAGICSWORD - 1);
        tabMagicSwords[rand].SetActive(true);
        isActiveMagicSword = true;
    }
}
