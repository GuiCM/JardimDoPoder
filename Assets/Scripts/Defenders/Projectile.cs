using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject particleHit;
    private float particleDuration;
    private float speed;
    private float damageCaused;

    private void Start()
    {
        ParticleSystem ps = particleHit.GetComponentInChildren<ParticleSystem>();
        particleDuration = ps.main.duration + ps.main.startLifetime.constantMax;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Attacker")
            return;

        //Debug.Log("Colisão de projétil " + name + " com " + collision);
        collision.GetComponent<Attacker>().ReceiveDamage(damageCaused);
        GameObject particle = Instantiate(particleHit, transform.position, Quaternion.identity);
        Destroy(particle, particleDuration);
        Destroy(gameObject);
    }

    public void SetAttributes(float speed, float damageCaused)
    {
        this.speed = speed;
        this.damageCaused = damageCaused;
    }
}
