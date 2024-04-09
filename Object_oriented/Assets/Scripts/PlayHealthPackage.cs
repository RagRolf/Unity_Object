using System.Collections;
using UnityEngine;

public class PlayHealthPackage : MonoBehaviour
{
    [SerializeField] AudioClip hit;
    private bool allow = true;

    private void OnEnable()
    {
        allow = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (allow && gameObject.activeSelf) //For health package getting inactive before this is called
        {
            ProvideAudioSources.source.PlayOneShot(hit); //A bit of wait
            StartCoroutine(Hit());
        }
    }

    private IEnumerator Hit()
    {
        allow = false;
        yield return new WaitForSeconds(0.3f);
        allow = true;
    }
}
