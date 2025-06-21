using UnityEngine;

public class CardScript : MonoBehaviour
{
    public ColorScript color;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
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
        transform.rotation = transform.parent.rotation;
    }
    void OnMouseOver(){
        if (Input.GetMouseButtonDown(0) && transform.parent.tag == "Hand")
        {
            if (!transform.parent.parent.GetComponent<PlayerScript>().IsItTurn()) return;
            transform.parent.parent.GetComponent<PlayerScript>().Select(transform);
        }
    }

}
