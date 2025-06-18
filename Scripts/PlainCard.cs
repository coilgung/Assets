using UnityEngine;

public class PlainCard : MonoBehaviour
{
    Vector3 distance;
    bool held;
    Camera cam;
    Vector3 mousepos;

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }
    void Update()
    {
        mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (held)
        {
            transform.position = new Vector3(
                mousepos.x + distance.x,
                mousepos.y + distance.y,
                transform.position.z
            );
        }
        if (Input.GetMouseButtonUp(0))
        {
            held = false;
        }
    }
    void OnMouseOver()
    {
        distance = new Vector3(
            transform.position.x - mousepos.x,
            transform.position.y - mousepos.y,
            transform.position.z
        );
        if (Input.GetMouseButtonDown(0))
        {
            held = true;
        }
    }
}
