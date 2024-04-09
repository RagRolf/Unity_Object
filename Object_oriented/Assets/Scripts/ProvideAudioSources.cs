using UnityEngine;

public class ProvideAudioSources : MonoBehaviour
{
    public static AudioSource source { get; private set; } //ENCAPSULATION
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

}
