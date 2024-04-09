using System.Collections;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [HideInInspector] public Collider2D[] allEnemies;
    public IInterfaceA[] attack;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AudioClip[] explode;
    [SerializeField] AudioClip hit;
    private bool allow = true;
    private void OnEnable()
    {
        allow = true;
        StartCoroutine(Die());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (allow)
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
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.95f);
        int numberOfColliders = Physics2D.OverlapCircleNonAlloc(new Vector2(transform.position.x, transform.position.y), 1f, allEnemies, layerMask);
        ProvideAudioSources.source.PlayOneShot(explode[Random.Range(0, 2)]);
        for (int i = 0; i < numberOfColliders; i++)
        {
            attack[allEnemies[i].gameObject.name[0] - ' '].IsHit() ;
        }
        particles.transform.position = transform.position;
        particles.Play();
        gameObject.SetActive(false);
    }
}
