using System.Collections;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    protected AudioSource audioSource;
    Collider2D collider2d;
    [SerializeField] private Transform explosion;
    protected SpriteRenderer sprite;
    private SpriteRenderer explosionSprite;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected int projectileAmount;
    [SerializeField] protected int lives;
    private Vector3[] offsets;
    private Quaternion[] rotations;
    protected void Initialize() //ABSTRACTION
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
        collider2d = GetComponent<Collider2D>(); 
        GameObject obj = Instantiate(explosion.gameObject);
        explosion = obj.transform;
        explosionSprite = obj.GetComponent<SpriteRenderer>();
        obj.SetActive(false);
        sprite = GetComponent<SpriteRenderer>();
        float radius = 1f;
        float step = 180f / projectileAmount;
        offsets = new Vector3[projectileAmount];
        rotations = new Quaternion[projectileAmount];
        for (int i = 0; i < projectileAmount; i++) 
        {
            float angle = i * step;
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            offsets[i] = transform.position + new Vector3(x, y, 0f);
            rotations[i] = Quaternion.Euler(0f, 0f, projectileAmount * -3 + i * 5);
        }
    }


    public virtual void Destruction(float startSize, float scaleFactor) //POLYMORPHISM
    {
        for (int i = 0; i < projectileAmount; i++)
        {
            Instantiate(projectile, offsets[i], rotations[i]);
        }
        StartCoroutine(IncreaseSize(startSize, scaleFactor));
    }

    IEnumerator IncreaseSize(float startSize, float scaleFactor)
    {
        explosion.gameObject.SetActive(true);
        explosion.position = transform.position;
        explosion.localScale = new Vector3(startSize, startSize, 1f);
        collider2d.enabled = false;
        float alpha = 1f;
        float scaleIncrease = scaleFactor;
        while (true)
        {
            alpha -= Time.deltaTime * 3.5f;
            explosionSprite.color = new Color(1f, 1f, 1f, alpha);
            explosion.localScale += Time.deltaTime * new Vector3(scaleIncrease, scaleIncrease, 1f);
            if(alpha <= 0)
            {
                Destroy(explosion.gameObject);
                Destroy(gameObject);
                yield break;
            }
            yield return null;
        }
    }
}
