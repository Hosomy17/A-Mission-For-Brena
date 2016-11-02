using UnityEngine;
using System.Collections;

public class ClassParticle : ClassGeneric
{
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        ps.Play();
        Invoke("Kill", ps.duration);
    }

    void OnDisable()
    {
        ps.Stop();
    }

    private void Kill()
    {
        gameObject.Recycle();
    }
}
