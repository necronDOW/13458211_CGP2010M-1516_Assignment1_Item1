using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour
{
    public Transform out_transform;

    private GameObject linkedPortal;
    private GameObject player;
    private Camera cam;

	void Awake()
    {
        out_transform = transform.FindChild("out_transform").GetComponent<Transform>();

        int number = int.Parse(name.Substring(name.Length - 1, 1));
        number = number == 0 ? 1 : 0;

        linkedPortal = GameObject.Find("portal_" + number);

        player = GameObject.FindGameObjectWithTag("Player");
        cam = GetComponentInChildren<Camera>();
    }
	
	void Update()
    {
        cam.transform.LookAt(player.transform);
    }

    void OnTriggerEnter(Collider other)
    {
        if (linkedPortal != null)
        {
            Transform target = linkedPortal.GetComponent<PortalScript>().out_transform;
            Transform otherTrans = other.gameObject.transform;

            otherTrans.position = target.position - new Vector3(0f, out_transform.position.y - other.transform.position.y, 0f);

            float yRotation = -(linkedPortal.transform.eulerAngles.y - transform.eulerAngles.y);
            Rigidbody otherRigid = other.GetComponent<Rigidbody>();
            otherRigid.velocity = Quaternion.Euler(0f, yRotation, 0f) * otherRigid.velocity;

            Vector3 newRotation = other.transform.eulerAngles;
            newRotation.y = linkedPortal.transform.eulerAngles.y + 180f;
            otherTrans.eulerAngles = newRotation;
        }
    }
}
