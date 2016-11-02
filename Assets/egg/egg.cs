using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class egg : MonoBehaviour
{
    void Start()
    {
        Invoke("fakeEnd", 17f);
    }

    private void fakeEnd()
    {
        SceneManager.LoadScene("End");
    }
}
