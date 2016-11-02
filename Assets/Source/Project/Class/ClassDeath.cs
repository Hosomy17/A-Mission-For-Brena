using UnityEngine;
using System.Collections;

public class ClassDeath : ClassGeneric {

    private ParticleSystem ps;
    private float speed;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        speed = 50 + (50 * PlayerPrefs.GetInt("SP", 0));
        BehaviourPhysics.Move(gameObject, Vector2.down, speed);
        BehaviourAnimation.Play(gameObject, "Death");
    }

    void OnEnable()
    {
        ps.Play();
        BehaviourPhysics.Move(gameObject, Vector2.down, speed);
        Invoke("Kill", ps.duration);
    }

    void OnDisable()
    {
        BehaviourPhysics.Move(gameObject, Vector2.one, 0);
        ps.Stop();
    }

    private void Kill()
    {
        gameObject.Recycle();
    }	
}
