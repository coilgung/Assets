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
        Debug.Log(mousepos);
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
            Debug.Log("C");
        }
    }
    void OnMouseOver()
    {
        Debug.Log("A");
        distance = new Vector3(
            transform.position.x - mousepos.x,
            transform.position.y - mousepos.y,
            transform.position.z
        );
        if (Input.GetMouseButtonDown(0))
        {
            held = true;
            Debug.Log("B");
        }
    }
}
