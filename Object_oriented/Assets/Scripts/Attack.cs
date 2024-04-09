using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : Destructable, IInterfaceA //INHERITANCE 
{
    [SerializeField] private AudioClip attack;
    private const float border = 8.24f;
    private bool once;
    private float startLives;
    private GameObject[] attackers = new GameObject[3];
    [SerializeField] private GameObject attackProjectile;
    int index;
    private void Start()
    {
        Initialize();
        startLives = lives;
        transform.position = new Vector2(Random.Range(-border, border), transform.position.y);
        StartCoroutine(SetRandomPos());
        StartCoroutine(AttackUFO());
        for (int i = 0; i < attackers.Length; i++)
        {
            attackers[i] = Instantiate(attackProjectile);
            attackers[i].SetActive(false);
        }
    }
    public void IsHit()
    {
        if(once) return;
        if (--lives == 0)
        {
            once = true;
            audioSource.pitch = 0.8f;
            audioSource.PlayOneShot(audioSource.clip);
            Destruction(0.3f, 4f);
        }
        sprite.color = new Color(1, 1, 1, lives / startLives);
        transform.position = new Vector2(Random.Range(-border, border), transform.position.y);
        StopCoroutine(SetRandomPos());
        StartCoroutine(SetRandomPos());
    }
    public void KillMeNow()
    {
        once = true;
        audioSource.pitch = 0.8f;
        audioSource.PlayOneShot(audioSource.clip);
        Destruction(0.3f, 4f);
        sprite.color = new Color(1, 1, 1, lives / startLives);
    }
    public void HealthAid()
    {
        lives += 2;
        sprite.color = new Color(1, 1, 1, lives / startLives);
    }

    private IEnumerator SetRandomPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6f, 8f));
            transform.position = new Vector2(Random.Range(-border, border), transform.position.y);
        }
    }

    private IEnumerator AttackUFO()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(attack);
            if (index == attackers.Length)
                index = 0;
            attackers[index].SetActive(true);
            Vector3 position = transform.position + new Vector3(0f, 2.42f, 0f);
            Vector3 direction = UFO.UFOpos.position - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float randomOffset = Random.Range(-45f, 45f);
            attackers[index].transform.position = position;
            attackers[index++].transform.rotation = Quaternion.AngleAxis(angle - 90 + randomOffset, Vector3.forward);
        }
    }
}
