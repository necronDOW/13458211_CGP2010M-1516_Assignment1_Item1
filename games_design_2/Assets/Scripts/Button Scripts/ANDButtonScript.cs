using UnityEngine;
using System.Collections;

public class ANDButtonScript : ButtonScript
{
    public GameObject linked;

    private ANDButtonScript linkedAbs;

    protected override void Start()
    {
        base.Start();

        if (linked != null)
        {
            linkedAbs = linked.GetComponent<ANDButtonScript>();
            linkedAbs.SetLinked(this);
            linkedAbs.target = target;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (linked != null)
            SetTarget(linkedAbs.active && active);
    }

    public void SetLinked(ANDButtonScript abs)
    {
        linked = abs.gameObject;
        linkedAbs = abs;
    }
}
