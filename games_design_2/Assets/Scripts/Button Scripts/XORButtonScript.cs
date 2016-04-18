using UnityEngine;
using System.Collections;

public class XORButtonScript : ButtonScript
{
    public GameObject linked;

    private XORButtonScript linkedAbs;

    protected override void Start()
    {
        base.Start();

        if (linked != null)
        {
            linkedAbs = linked.GetComponent<XORButtonScript>();
            linkedAbs.SetLinked(this);
            linkedAbs.targets = targets;
        }
    }

    protected override void Update()
    {
        base.Update();

        bool xorActive = linkedAbs.active || active;
        xorActive = linkedAbs.active && active ? false : xorActive;

        if (linked != null)
            SetTarget(xorActive);
    }

    public void SetLinked(XORButtonScript abs)
    {
        linked = abs.gameObject;
        linkedAbs = abs;
    }
}
