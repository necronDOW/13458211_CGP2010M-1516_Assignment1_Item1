using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelScript : MonoBehaviour
{
    public string nextLevel = "";

    void OnTriggerEnter(Collider other)
    {
        if (nextLevel != "")
            SceneManager.LoadScene(nextLevel);
    }
}