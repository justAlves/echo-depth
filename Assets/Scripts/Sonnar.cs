using UnityEngine;

public class Sonnar : MonoBehaviour
{

    public GameObject sonnarPrefab;
    public float duration = 20;
    public float size = 100;
    public float coolDownTime = 2f;
    private float nextUseTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            if(Time.time >= nextUseTime)
            {
                SpawnSonnar();
                nextUseTime = Time.time + coolDownTime;
            }
            else
            {
                Debug.Log("Skill em cooldown");
            }
        }
    }

    void SpawnSonnar()
    {
        GameObject sonnar = Instantiate(sonnarPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
        ParticleSystem sonnarPs = sonnar.transform.GetChild(0).GetComponent<ParticleSystem>();

        if (sonnarPs != null) 
        {
            var main = sonnarPs.main;
            main.startLifetime = duration;
            main.startSize = size;
        }
        else
        {
            Debug.Log("The first child doesnt have a particle system");
        }

        Destroy(sonnar, duration + 1);
    }
}
