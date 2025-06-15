using UnityEngine;

public class CardScript : MonoBehaviour
{
    public ColorScript color;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    public GameObject cam;

    void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        SetColor();
    }

    void SetColor()
    {
        spriteRenderer.sprite = color.sprite;
    }

    void Update()
    {
        SetColor();
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, cam.transform.rotation.z*180f);
        //Debug.Log(cam.transform.rotation.z*180f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
    void OnMouseOver(){
        if (Input.GetMouseButtonDown(0) && transform.parent.tag == "Hand")
        {
            if (!transform.parent.parent.GetComponent<PlayerScript>().turn) return;
            transform.parent.parent.GetComponent<PlayerScript>().Select(transform);
        }
    }

}
