using UnityEngine;
using System.Collections;

public class detectBoss : MonoBehaviour {
    public KnightActions KA;
    
    void OnTriggerEnter(Collider other)
    {
        if(KA != null)
        {
            KA.bossAtRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (KA != null)
        {
            KA.bossAtRange = false;
        }
    }

}
