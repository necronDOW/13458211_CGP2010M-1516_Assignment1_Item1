using UnityEngine;
using System.Collections;

public class ORButtonScript : ButtonScript
{
    public GameObject[] targets2;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        SetTarget(active);
    }

    protected override void SetTarget(bool value)
    {
        for (int i = 0; i < targets.Length; i++)
            targets[i].GetComponent<DoorScript>().SetActive(value);

        for (int i = 0; i < targets.Length; i++)
            targets2[i].GetComponent<DoorScript>().SetActive(!value);
    }
}