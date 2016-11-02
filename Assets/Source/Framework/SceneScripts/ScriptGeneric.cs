using UnityEngine;
using System.Collections;

public class ScriptGeneric : MonoBehaviour
{

    protected GameManagerGeneric gameManager;

	void Awake ()
    {
        gameManager = GameManagerGeneric.Instance;
	}
}
