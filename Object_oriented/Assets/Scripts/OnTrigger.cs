using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    public IInterfaceA[] attack;
    List<int> allIndices = new List<int>(5);
    bool dead;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AudioClip[] explode;
    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         allIndices.Add(other.gameObject.name[0] - ' ');
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!dead)
        allIndices.Remove(other.gameObject.name[0] - ' ');
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.95f);
        ProvideAudioSources.source.PlayOneShot(explode[Random.Range(0, 2)]);
        dead = true;
        int count = allIndices.Count;
        for(int i = 0; i < count; i++)
        {
            attack[allIndices[i]].IsHit();
        }
        allIndices.Clear();
        particles.transform.position = transform.parent.position;
        particles.Play();
        transform.parent.gameObject.SetActive(false);
    }
}
