using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UFO : MonoBehaviour
{
    bool activateOnce = false;
    private AudioSource[] sources;
    private SpriteRenderer sprite;
    PolygonCollider2D polyCollider;
    private Transform outer, inner;
    private SpriteRenderer outerSprite, innerSprite;
    public static Transform UFOpos { get; private set; } //ENCAPSULATION
    private GameObject[] allBombs;
    [SerializeField] private Transform targetParent;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject healthDrop;
    [SerializeField] private InputAction dropBomb;
    [SerializeField] private InputAction move;
    private const float border = 8.9f, offset = 0.985f, under = 1.7f;
    private const int amountOfBombs = 5, ufoSpeed = 4;
    private int index;
    private float counter;
    [SerializeField] private int lives = 30;
    private float startLives;

    private void OnEnable()
    {
        dropBomb.Enable();
        move.Enable();
    }

    private void OnDisable()
    {
        dropBomb.Disable();
        move.Disable();
    }
    void Start()
    {
        sources = GetComponents<AudioSource>();
        outer = transform.GetChild(0).transform;
        inner = transform.GetChild(1).transform;
        outerSprite = outer.gameObject.GetComponent<SpriteRenderer>();
        innerSprite = inner.gameObject.GetComponent<SpriteRenderer>();
        polyCollider = GetComponent<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        startLives = lives;
        UFOpos = transform;
        StartCoroutine(DropIt());
        healthDrop = Instantiate(healthDrop);
        healthDrop.transform.GetChild(0).GetComponent<HealthDrop>().attack = targetParent.GetComponentsInChildren<IInterfaceA>();
        int index = 0;
        dropBomb.canceled += DropBomb_Canceled;
        allBombs = new GameObject[amountOfBombs];
        while (index < amountOfBombs)
        {
            GameObject obj = Instantiate(bomb);
            OnTrigger trigger = obj.transform.GetChild(0).transform.GetChild(0).GetComponent<OnTrigger>();
            trigger.attack = targetParent.GetComponentsInChildren<IInterfaceA>();
            allBombs[index++] = obj.transform.GetChild(0).gameObject;
        }
        for(int i = 0; i < targetParent.childCount; i++)
        {
            targetParent.GetChild(i).name = (char)(' ' + i) + targetParent.GetChild(i).name;
        }
    }

    private void DropBomb_Canceled(InputAction.CallbackContext obj)
    {
        counter = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        sprite.color = new Color(1, 1, 1, lives / startLives);
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            sources[0].pitch = 0.8f;
            sources[0].PlayOneShot(sources[0].clip);
            lives -= 3;
        }
        else
        {
            other.gameObject.SetActive(false);
            sources[0].pitch = 1f;
            sources[0].PlayOneShot(sources[0].clip);
            lives -= 2;
        }
        if (lives < 1)
        {
            StartCoroutine(Killed());
        }
    }

    void Update()
    {
        float movement = move.ReadValue<float>();
        if (transform.position.x > border || transform.position.x < -border)
            transform.position = new Vector3(-transform.position.x * offset, transform.position.y, 0);
        transform.Translate(new Vector3(movement * Time.deltaTime * ufoSpeed, 0, 0));
        if(!activateOnce && movement != 0f)
        {
            sources[1].Play();
            sources[1].loop = true;
            activateOnce = true;
        }
        else if(activateOnce && movement == 0f)
        {
            sources[1].loop = false;
            activateOnce = false;
        }
        if (dropBomb.IsPressed())
        {
            counter += Time.deltaTime;
            if (counter > 1f)
            {
                counter = 0f;
                allBombs[index].transform.position = transform.position - new Vector3(0f, under);
                allBombs[index].transform.rotation = Quaternion.identity;
                allBombs[index++].SetActive(true);
                sources[2].pitch = 1f;
                sources[2].PlayOneShot(sources[2].clip);
                if (index == amountOfBombs)
                    index = 0;
            }
        }
    }

    private IEnumerator Killed()
    {
        float alpha = 1f;
        float scaleInner = 1.2f;
        float scaleOuter = 1.2f;
        polyCollider.enabled = false;
        sprite.enabled = false;
        inner.gameObject.SetActive(true);
        outer.gameObject.SetActive(true);
        dropBomb.Disable();
        move.Disable();
        while (alpha > 0)
        {
            scaleInner += Time.deltaTime * 6f;
            alpha -= Time.deltaTime;
            inner.localScale += Time.deltaTime * new Vector3(scaleInner, scaleInner,1f);
            outer.localScale += Time.deltaTime * new Vector3(scaleOuter, scaleOuter, 1f);
            innerSprite.color = new Color(1f, 1f, 1f, alpha);
            outerSprite.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator DropIt()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 15));
            sources[2].pitch = 1.2f;
            sources[2].PlayOneShot(sources[2].clip);
            healthDrop.SetActive(true);
            healthDrop.transform.position = transform.position - new Vector3(0f, under);
            healthDrop.transform.rotation = Quaternion.identity;
        }
    }
}
