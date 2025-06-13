using UnityEngine;

public class CardScript : MonoBehaviour
{
    public ColorScript color;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    float hover;
    public GameObject cam;

    void Awake()
    {
        cam = GameObject.FindWithTag("MainCamera");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        hover = 0f;
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
        transform.position = new Vector3(transform.position.x, transform.position.y + hover*Time.deltaTime, transform.position.z);
    }
    void OnMouseEnter()
    {
        hover = Mathf.Max(Mathf.Abs(transform.position.x - Input.mousePosition.x), 2);
    }
    void OnMouseExit()
    {
        hover = 0;
    }
}
