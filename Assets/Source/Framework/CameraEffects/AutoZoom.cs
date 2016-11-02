using UnityEngine;
using System.Collections;

public class AutoZoom : MonoBehaviour
{
    public float limit;
    public float velocity;
    private float origin;
    private float size;

	void Start ()
    {
        origin = Camera.main.orthographicSize;
        size = origin;
	}
	
	void Update ()
    {
        if(Camera.main.orthographicSize - size <= 1)
            size = Random.Range(origin, origin + limit);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, size, Time.deltaTime * velocity);
	}
}
