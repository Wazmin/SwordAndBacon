using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundCore : MonoBehaviour {
    public List<SoundShard> SoundShards = new List<SoundShard>(1);


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //SoundShard s = SoundShards.Find(System.Predicate<string>, "this");

	}

    public void PlaySound(GameObject s, string name)
    {
        AudioSource AU = s.GetComponent<AudioSource>();
        if (AU.isPlaying) { AU.Stop(); }
        AU.clip = GetShard(name).GetRandom();
        AU.Play();
    }

    public void PlaySoundF(GameObject s, string name)
    {
        AudioSource AU = s.GetComponent<AudioSource>();
        if (AU.isPlaying) { return; }
        AU.clip = GetShard(name).GetRandom();
        AU.Play();
    }

    public SoundShard GetShard(string name)
    {
        for (int i = 0; i < SoundShards.Count; i++)
        {
            if (SoundShards[i].name == name)
            {
                return SoundShards[i];
            }
        }
        Debug.LogError("SoundCore: SoundShard not found: " + name);
        return null;
    }



    [System.Serializable]
    public class SoundShard
    {
        public string name = "";
        public List<AudioClip> AudioClips = new List<AudioClip>(1);

        public AudioClip GetRandom()
        {
            return AudioClips[Random.Range(0, AudioClips.Count)];
        }

    }
}
