using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour
{
    public bool active = false;
    public GameObject[] targets;

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
        for (int i = 0; i < targets.Length; i++)
        {
            DoorScript ds = targets[i].GetComponent<DoorScript>();
            if (ds != null)
                ds.SetActive(value);
            else
            {
                BlockDispenser bd = targets[i].GetComponent<BlockDispenser>();
                if (bd != null && value)
                    bd.Dispense();
            }
        }
    }
}
