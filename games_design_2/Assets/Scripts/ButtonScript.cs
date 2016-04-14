using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public bool active;
    public GameObject linked;
    public int linkType = 0;
    public GameObject target;

    private Animator anim;
    private ButtonScript linkedScript;

    void Start()
    {
        active = false;
        anim = GetComponentInChildren<Animator>();

        if (linked != null)
        {
            linkedScript = linked.GetComponent<ButtonScript>();
            linkedScript.SetLinked(this);
        }
    }

    void Update()
    {
        active = anim.GetBool("pressed");

        if (linked != null)
        {
            if (linkType == 0 && (active && linkedScript.active))
                SetTarget(true);
            else if (linkType == 1 && (active || linkedScript.active))
                SetTarget(true);
            else SetTarget(false);
        }
        else
        {
            if (active)
                SetTarget(true);
            else SetTarget(false);
        }
    }

    void OnTriggerEnter(Collider o)
    {
        anim.SetBool("pressed", true);
    }

    void OnTriggerExit(Collider o)
    {
        anim.SetBool("pressed", false);
    }

    public void SetLinked(ButtonScript bS)
    {
        linked = bS.gameObject;
        linkType = bS.linkType;
        linkedScript = bS;
    }

    private void SetTarget(bool value)
    {
        if (target != null)
            target.GetComponent<DoorScript>().SetActive(value);
    }
}
