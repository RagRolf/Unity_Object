using UnityEngine;

public class Building : Destructable, IInterfaceA //INHERITANCE 
{
    private bool once;
    private float startLives, startSize = 0.2f, scaleFactor = 3f, pitch = 0.9f;
    private static int killedBuildings, amountOfBuildings;
    private Attack[] attackers;
    int attackersCount;
    private void Start()
    {
        attackers = transform.parent.GetComponentsInChildren<Attack>();
        attackersCount = attackers.Length;
        amountOfBuildings++;
        if (transform.localScale.x == 0.8f)
        {
            startSize = 0.8f;
            scaleFactor = 6f;
            pitch = 0.6f;
        }
        startLives = lives;
        Initialize();
    }
    public void IsHit()
    {
        if (once) return;
        sprite.color = new Color(1, 1, 1, --lives / startLives);
        if (lives == 0)
        {
            once = true;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioSource.clip);
            Destruction(startSize, scaleFactor);
        }
    }

    public override void Destruction(float startSize, float scaleFactor)
    {
        if (++killedBuildings == amountOfBuildings) //9 is amount of buildings
        {
            for (int i = 0; i < attackersCount; i++)
            {
                if (attackers[i] != null)
                    attackers[i].KillMeNow(); //So not null
            }
            IfWon.won.StartChangeScene();
        }
        base.Destruction(startSize, scaleFactor);
    }

    public void HealthAid()
    {
        lives += 3;
        sprite.color = new Color(1, 1, 1, lives / startLives);
    }
}
