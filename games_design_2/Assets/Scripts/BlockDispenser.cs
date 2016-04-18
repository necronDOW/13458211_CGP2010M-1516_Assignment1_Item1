using UnityEngine;
using System.Collections;

public class BlockDispenser : MonoBehaviour
{
    public Object prefab;

    private GameObject block;
    private ParticleSystem particles;
    private Transform dispensePosition;

    void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        dispensePosition = transform.FindChild("dispense_point");
    }
    
    void Update()
    {
        if (block == null)
            Dispense();
    }

    public void Dispense()
    {
        if (prefab != null)
        {
            if (block == null)
                block = (GameObject)Instantiate(prefab);

            particles.Play();
            block.transform.position = dispensePosition.position;
            block.transform.rotation = dispensePosition.rotation;
        }
    }
}