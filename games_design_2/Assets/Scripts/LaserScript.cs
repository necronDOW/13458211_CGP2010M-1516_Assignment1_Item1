using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    Vector3 spawnCoords;

    void Awake()
    {
        spawnCoords = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.transform.position = spawnCoords;
        else
            Destroy(other.gameObject);
    }
}