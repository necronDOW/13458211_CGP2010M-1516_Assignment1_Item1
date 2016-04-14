using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorScript : MonoBehaviour
{
    private GameObject portal;
    private Animator anim;
    private bool active = false;

    void Awake()
    {
        portal = transform.Find("active_portal").gameObject;
        anim = GetComponent<Animator>();
    }

    public void SetActive(bool value)
    {
        if (active != value)
        {
            anim.SetBool("active", value);
            active = value;
        }
    }
}
