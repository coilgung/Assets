using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    public float radius;
    public Transform hand;
    public Transform prep;
    public bool turn;
    public EventManagerScript manager;

    Transform selected;

    void Start()
    {

        foreach (Transform child in transform)
        {
            if (child.tag == "Hand")
                hand = child;
            if (child.tag == "Prep")
                prep = child;
        }
        turn = false;

    }

    void Update()
    {
        VisualizeHand(hand, 6, turn);
        VisualizeHand(prep, 6);
        if (selected == null || !turn)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.position.x - Input.mousePosition.x < 0)
            {
                Place(selected, prep.childCount);
                selected = null;
                return;
            }
            Place(selected, 0);
            selected = null;
            return;
        }
    }

    void VisualizeHand(Transform holder, int minWidth, bool visible = true)
    {
        int holdLen = holder.childCount;
        int width = holdLen;
        for (int i = 0; i < holdLen; i++)
        {
            holder.GetChild(i).position = new Vector3(
                (holder.position.x - width / 2 + width / holdLen * ((float)i + ((holdLen % 2 == 0) ? 0.5f : 0f))) * Mathf.Max(minWidth, holdLen) / 4, holder.position.y, holder.position.z
            );
            holder.GetChild(i).GetComponent<SpriteRenderer>().enabled = visible;
            holder.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = i;
        }
    }

    public void Select(Transform card)
    {
        selected = card;
    }

    void Place(Transform card, int order)
    {
        card.SetParent(prep);
        card.SetSiblingIndex(order);
    }

    public void Cast()
    {
        if (!turn || prep.childCount < 2) return;

        int[] cardColors = new int[2]{
            (int)(prep.GetChild(0).GetComponent<CardScript>().color.color),
            (int)(prep.GetChild(1).GetComponent<CardScript>().color.color)
        };
        manager.CastSpell(cardColors);
        foreach (Transform child in prep)
        {
            GameObject.Destroy(child.gameObject);
        }

    }
    
    

    // public void Pass()
    // {
    //     if (!turn) return;
    //     Debug.Log("passing");
    //     manager.TurnEnded();
    // }
}
