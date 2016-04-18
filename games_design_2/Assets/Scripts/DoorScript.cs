using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{
    private Animator anim;
    private bool active = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetActive(bool value)
    {
        if (active != value)
        {
            if (anim != null)
                anim.SetBool("active", value);

            active = value;
        }
    }
}
