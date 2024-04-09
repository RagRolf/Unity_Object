using System.Collections;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [HideInInspector] public IInterfaceA[] attack;
    [SerializeField] private AudioClip clip;


    private void OnEnable()
    {
        StartCoroutine(Die());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        attack[other.name[0] - ' '].HealthAid();
        transform.parent.gameObject.SetActive(false);
        ProvideAudioSources.source.PlayOneShot(clip);
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);
        transform.parent.gameObject.SetActive(false);
    }
}
