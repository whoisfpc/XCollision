using UnityEngine;
using XCollision.XComponent;

public class SimpleController : MonoBehaviour
{

    public float force = 10f;
    private XCollision.Core.XRigidbody xrb;

    private void Start()
    {
        xrb = GetComponent<XCollider>().XRigidbody;
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        xrb.AddForce(new Vector2(h, v) * force);
    }
}
