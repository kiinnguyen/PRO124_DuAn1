using UnityEngine;

public class Music : MonoBehaviour
{
    [Header("------------ Audio Source-----------")]
    [SerializeField] AudioSource musicSource;

    [Header("------------ Audio Clip-----------")]
    public AudioClip background;
    public AudioClip river;
    public AudioClip waterfall;
    public AudioClip coins;
    public AudioClip birds;
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

}
