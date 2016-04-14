using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public bool active = false;
    public GameObject target;

    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        active = anim.GetBool("pressed");
        SetTarget(active);
    }

    void OnTriggerEnter(Collider o)
    {
        anim.SetBool("pressed", true);
    }

    void OnTriggerExit(Collider o)
    {
        anim.SetBool("pressed", false);
    }

    protected virtual void SetTarget(bool value)
    {
        if (target != null)
            target.GetComponent<DoorScript>().SetActive(value);
    }
}
