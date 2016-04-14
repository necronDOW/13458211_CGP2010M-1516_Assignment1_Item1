using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed = 3.0f;
    public float turnSpeed = 0.5f;
    public float jumpForce = 1.0f;

    private Vector3 lastMousePos;
    private GameObject obj = null;
    private Rigidbody objRigid = null;

    private Rigidbody rigidBody;
    private int objectLayerMask;
    private int boundariesLayerMask;
    private Animator anim;
    private Color[] portalColors =
    {
       new Color(0f, 0.584f, 1f),
       new Color(1f, 0.682f, 0f)
    };
    private int currentPortal = 0;
    private int jumps = 0;
    private Image reticleImg;

    void Awake()
    {
        Cursor.visible = false;
        lastMousePos = Input.mousePosition;

        rigidBody = GetComponent<Rigidbody>();
        objectLayerMask = LayerMask.GetMask("Objects");
        boundariesLayerMask = LayerMask.GetMask("Boundaries");
        anim = GetComponentInChildren<Animator>();
        reticleImg = GameObject.FindGameObjectWithTag("Reticle").GetComponent<Image>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (obj != null)
            {
                Vector3 pushDirection = transform.position - obj.transform.position;
                objRigid.isKinematic = false;
                objRigid.AddForce(-pushDirection.normalized * 10.0f, ForceMode.VelocityChange);
                SetPickup(null);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (obj == null)
            {
                RaycastHit info;
                SetPickup(GetObject(objectLayerMask, 5f, out info));
            }
            else SetPickup(null);
        }
        else if (Input.GetMouseButtonDown(2))
            FirePortal();

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            currentPortal = currentPortal == 0 ? 1 : 0;
            reticleImg.color = portalColors[currentPortal];
        }

        lastMousePos = Input.mousePosition;
    }

	void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Turn();

        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            rigidBody.AddForce(new Vector3(0f, 1f * jumpForce, 0f), ForceMode.VelocityChange);
            jumps--;
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.GetMask("Boundaries"))
            jumps = 2;
    }

    void Move(float h, float v)
    {
        Vector3 movement = new Vector3(h, 0f, v) * speed * Time.deltaTime;

        transform.position += transform.forward * movement.z;
        transform.position += transform.right * movement.x;

        if (movement.z != 0.0f)
            anim.SetBool("walking", true);
        else anim.SetBool("walking", false);
    }

    void Turn()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x > 0 && mousePos.y > 0
            && mousePos.x < Screen.width && mousePos.y < Screen.height)
        {
            float h = lastMousePos.x - mousePos.x;
            float v = lastMousePos.y - mousePos.y;

            transform.Rotate(new Vector3(0f, -h, 0f) * turnSpeed);
            Camera.main.transform.Rotate(new Vector3(v, 0f, 0f) * turnSpeed);
        }
    }

    GameObject GetObject(int mask, float maxDistance, out RaycastHit outInfo)
    {
        Ray origin = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(origin, out outInfo, maxDistance, mask))
            return outInfo.transform.gameObject;
        else return null;
    }

    void SetPickup(GameObject o)
    {
        if (o != null)
        {
            o.transform.parent = Camera.main.transform;
            objRigid = o.GetComponent<Rigidbody>();
            objRigid.useGravity = false;
            objRigid.isKinematic = true;
        }
        else
        {
            obj.transform.parent = null;
            objRigid.useGravity = true;
            objRigid.isKinematic = false;
        }

        obj = o;
    }

    void FirePortal()
    {
        RaycastHit info;
        GameObject obj = GetObject(boundariesLayerMask, 100f, out info);

        if (obj != null)
        {
            GameObject portal = GameObject.Find("portal_" + currentPortal);

            portal.transform.position = info.point;

            Vector3 rotation = portal.transform.eulerAngles;
            rotation.y = obj.transform.eulerAngles.y;

            portal.transform.eulerAngles = rotation;
        }
    }
}
