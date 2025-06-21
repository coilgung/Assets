using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float mainRadius;
    Transform hand;
    Transform prep;

    [SerializeField]
    bool turn;

    [SerializeField]
    EventManagerScript manager;

    [SerializeField]
    Transform selected;

    [SerializeField]
    int actionsLeft;

    List<SpellEffect> spellsEffects;

    void Start()
    {
        this.InitializeChilds();
        turn = false;
    }

    void Update()
    {
        VisualizeHand(hand, turn);
        VisualizePrep(prep, 6);
        if (this.CantPlaceCard())
        {
            return;
        }
        ChooseCardPlacement();
        
    }

    void ChooseCardPlacement()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Place(selected, prep.childCount);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Place(selected, 0);
            return;
        }
    }

    bool CantPlaceCard()
    {
        return (selected == null || !turn || this.actionsLeft <= 0 || prep.childCount >=2);
    }

    void InitializeChilds()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Hand")
                hand = child;
            if (child.tag == "Prep")
                prep = child;
        }
    }
    void VisualizeHand(Transform holder, bool visible = true)
    {
        float radius = this.mainRadius;
        int holdLen = holder.childCount;
        float angleStep = 120f / (holdLen + 1);
        float initialAngle = 30f;
        for (int i = 0; i < holdLen; i++)
        {
            radius = mainRadius;
            if (selected)
            {
                if (holder.GetChild(i).gameObject == selected.gameObject)
                {
                    radius = 1.2f * mainRadius;
                }
            }
            float angle = (initialAngle + (i + 1) * angleStep) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            holder.GetChild(i).position = new Vector3(
                holder.position.x + x, holder.position.y + y, holder.position.z
            );
            holder.GetChild(i).GetComponent<SpriteRenderer>().enabled = visible;
            holder.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = i;
        }
    }

    void VisualizePrep(Transform holder, int minWidth, bool visible = true)
    {
        int holdLen = holder.childCount;
        int width = holdLen;
        for (int i = 0; i < holdLen; i++)
        {
            holder.GetChild(i).position = new Vector3(
                (holder.position.x - width / 2 + width / holdLen * ((float)i + ((holdLen % 2 == 0) ? 0.5f : 0f))) * Mathf.Max(minWidth, holdLen) / 4, holder.position.y, holder.position.z
            );
            holder.GetChild(i).GetComponent<SpriteRenderer>().enabled = visible;
            holder.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    public void TakeTurn()
    {
        this.turn = true;
        this.actionsLeft = this.manager.getMaxActions();
    }

    public void EndTurn()
    {
        this.turn = false;
    }

    public void SetManager(EventManagerScript eventManagerScript)
    {
        this.manager = eventManagerScript;
    }
    public void Select(Transform card)
    {
        selected = card;
    }

    void Place(Transform card, int order)
    {
        card.SetParent(prep);
        card.SetSiblingIndex(order);
        this.CardPlaced();
    }

    public bool IsItTurn()
    {
        return this.turn;
    }

    void CardPlaced()
    {
        this.actionsLeft--;
    }

    void SpellCasted()
    {
        this.actionsLeft--;
    }

    public void Cast()
    {
        if (CantCastSpell()) return;

        int[] cardColors = new int[2]{
            (int)(prep.GetChild(0).GetComponent<CardScript>().color.color),
            (int)(prep.GetChild(1).GetComponent<CardScript>().color.color)
        };
        manager.CastSpell(cardColors);
        foreach (Transform child in prep)
        {
            GameObject.Destroy(child.gameObject);
        }
        this.SpellCasted();
    }

    bool CantCastSpell()
    {
        return (!turn || prep.childCount < 2 || this.actionsLeft <= 0);
    }

    public void TakeCard()
    {
        this.manager.GiveCard(this);
    }
}
