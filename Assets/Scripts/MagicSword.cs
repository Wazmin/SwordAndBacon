using UnityEngine;
using System.Collections;

public class MagicSword : MonoBehaviour {
    private GameManager1 GM;

    void Start()
    {
        GM = GameObject.Find("_GAMEMANAGER").GetComponent<GameManager1>();
    }

        void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            other.GetComponent<KnightActions>().PickupSword();
            GM.DisableMagicSword(this.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
      
    }

}
