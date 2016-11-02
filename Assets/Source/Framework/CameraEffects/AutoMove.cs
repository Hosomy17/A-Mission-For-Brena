using UnityEngine;
using System.Collections;

public class AutoMove : MonoBehaviour
{
    private Vector3 destination;
    private Vector3 origin;
    public float distance;
    public float velocity;

    void Awake()
    {
        destination = Vector3.zero;
        origin = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, destination) < 1)
        {
            destination = transform.position + Random.onUnitSphere * velocity;
            destination.z = origin.z;

            if (Vector3.Distance(destination, origin) > distance)
                destination = transform.position;
        }

        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime*10);
    }
}
