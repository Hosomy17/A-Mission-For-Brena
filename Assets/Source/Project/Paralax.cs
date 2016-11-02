using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour
{
    public GameObject layer;
    private float speed;

	void Start ()
    {
        speed = 50 +( 50 * PlayerPrefs.GetInt("SP", 0));
        layer.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
	}
	
	void Update ()
    {

        if (layer.transform.localPosition.y < -69.5)
        {
            layer.transform.localPosition = Vector3.up * 218f;
        }
	}
}
