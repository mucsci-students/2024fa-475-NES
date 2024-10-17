using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVolumeAdjust : MonoBehaviour
{
    public GameObject audioSource;
    private int volume;
    // Start is called before the first frame update

    void Start()
    {
        volume = PlayerPrefs.GetInt("Volume");
        audioSource.GetComponent<AudioSource>().volume = volume / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        volume = PlayerPrefs.GetInt("Volume");
        audioSource.GetComponent<AudioSource>().volume = volume / 100f;
    }
}
