using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    // Events a mettre dans la classe cible  qui declenche l'event
    public delegate void MajBoostUI(int _nbBoost);
    public static event MajBoostUI OnChangeNbBoost;
    // declenche l'event
    //if (OnChangeNbBoost != null)
    //       OnChangeNbBoost(nbBoostDispo);
    //
    //enregistrement des composants externes dans le start du UIManager
    //BoostPack.OnChangeNbBoost += MajBoost;

    public Image _barreBoss;
    public Image _barreKnights;

    static UIManager _manager;
    public static UIManager Get
    {
        get
        {
            if (_manager == null)
                _manager = GameObject.FindObjectOfType<UIManager>();
            return _manager;
        }
    }



    // Use this for initialization
    void Start () {
        GameManager1.OnChangeBossLife += MajLifeBoss;
        GameManager1.OnChangeKnightsLife += MajLifeKnights;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void MajLifeBoss(float _lifeBoss)
    {
        _barreBoss.fillAmount = _lifeBoss / GameManager1.MAXLIFE;
    }

    private void MajLifeKnights(float _lifeKnights)
    {
        _barreKnights.fillAmount = _lifeKnights / GameManager1.MAXLIFE;
    }
}
