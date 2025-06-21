using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManagerScript : MonoBehaviour
{
    DeckScript deckScript;
    public StarterDeck starterDeck;

    [SerializeField]
    int maxActions;

    [SerializeField]
    GameObject[] players;

    [SerializeField]
    GameObject CardPrefab;

    [SerializeField]
    int currentPlayerId;

    [SerializeField]
    int initialHandCardNumber = 8;

    [SerializeField]
    GameObject mainCamera;

    string[,] pairEffects;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    TMP_Text text;
    void Update()
    {
        text = GameObject.FindWithTag("text").GetComponent<TMP_Text>();
    }

    void Start()
    {
        this.maxActions = 1;
        this.InitializeStarterDeck();
        this.InitializePlayers();
        this.InitializeCamera();
        this.InitializeEffects();
    }

    void InitializeCamera()
    {
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.x, mainCamera.transform.rotation.y, 360/players.Length*(currentPlayerId));
    }

    void InitializeEffects()
    {
        pairEffects = new string[3, 3] {
            {
                "TAKE A CARD!",
                "DOUBLE TAKE!",
                "EXTRA TAKE!"
            },
            {
                "DISPELL!",
                "STUN!",
                "STEAL!"
            },
            {
                "REACTION TAKE",
                "REACTION STUN?!",
                "SHIELD"
            }
        };
    }
    void InitializePlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        this.AsignFirstPlayer();
        for (int j = 0; j < players.Length; j++)
        {
            players[j].GetComponent<PlayerScript>().SetManager(gameObject.GetComponent<EventManagerScript>());
        }
        this.GiveAll();
        players[currentPlayerId].GetComponent<PlayerScript>().TakeTurn();
    }

    public int getMaxActions()
    {
        return this.maxActions;
    }

    void IncrementPlayerId()
    {
        currentPlayerId = (currentPlayerId+1) % players.Length;
    }

    int AsignFirstPlayer()
    {
        //currentPlayerId = (new System.Random()).Next(0, players.Length);
        currentPlayerId = 0;
        return currentPlayerId;
    }

    void InitializeStarterDeck()
    {
        deckScript = GameObject.FindWithTag("Deck").GetComponent<DeckScript>();
        deckScript.InitializeDeck(starterDeck);
    }

    public void TurnEnded()
    {
        text.text = "";
        this.ChangeTurn();
    }
    void ChangeTurn()
    {
        players[currentPlayerId].GetComponent<PlayerScript>().EndTurn();
        this.IncrementPlayerId();
        this.mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.x, mainCamera.transform.rotation.y, 360/players.Length*(currentPlayerId));
        players[currentPlayerId].GetComponent<PlayerScript>().TakeTurn();
    }

    void GiveAll()
    {
        foreach(GameObject player in players){
            foreach (Transform child in player.transform)
            {
                if (child.tag == "Hand")
                {
                    for (int i = 0; i < initialHandCardNumber; i++)
                    {
                        GameObject newCard = Instantiate(CardPrefab);
                        newCard.GetComponent<CardScript>().color = deckScript.GiveCard();
                        newCard.transform.parent = child;
                    }
                    break;
                }
            }
        }
    }

    public void CastSpell(int[] cardTypes)
    {
        text.text = pairEffects[cardTypes[0], cardTypes[1]];
        //StartCoroutine(Wait());
        //text.text = "";
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4000f);
    }
}
