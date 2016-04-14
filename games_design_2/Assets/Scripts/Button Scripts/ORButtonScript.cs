using UnityEngine;
using System.Collections;

public class ORButtonScript : ButtonScript
{
    public GameObject target2;

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
        target.GetComponent<DoorScript>().SetActive(value);
        target2.GetComponent<DoorScript>().SetActive(!value);
    }
}