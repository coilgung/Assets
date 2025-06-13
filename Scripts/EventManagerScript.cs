using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerScript : MonoBehaviour
{
    DeckScript deckScript;
    public StarterDeck starterDeck;

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

    void Start()
    {
        this.InitializeStarterDeck();
        this.InitializePlayers();
        this.InitializeCamera();
        this.InitializeEffects();
    }

    void InitializeCamera()
    {
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        this.mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.x, mainCamera.transform.rotation.y, 360/players.Length*(currentPlayerId+1));
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
        for (int j = 0; j < 1; j++)
        {
            players[j].GetComponent<PlayerScript>().manager = gameObject.GetComponent<EventManagerScript>();
            this.GiveAll();
        }
        players[currentPlayerId].GetComponent<PlayerScript>().turn = true;
    }

    void IncrementPlayerId()
    {
        currentPlayerId = (currentPlayerId+1) % players.Length;
    }

    int AsignFirstPlayer()
    {
        currentPlayerId = (new System.Random()).Next(0, players.Length);
        return currentPlayerId;
    }

    void InitializeStarterDeck()
    {
        deckScript = GameObject.FindWithTag("Deck").GetComponent<DeckScript>();
        deckScript.InitializeDeck(starterDeck);
    }

    public void TurnEnded()
    {
        Debug.Log(currentPlayerId);
        this.ChangeTurn();
    }
    void ChangeTurn()
    {
        players[currentPlayerId].GetComponent<PlayerScript>().turn = false;
        this.IncrementPlayerId();
        this.mainCamera.transform.rotation = Quaternion.Euler(mainCamera.transform.rotation.x, mainCamera.transform.rotation.y, 360/players.Length*(currentPlayerId+1));
        players[currentPlayerId].GetComponent<PlayerScript>().turn = true;
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
        //text = pairEffects[cardTypes[0], cardTypes[1]]
        StartCoroutine(Wait());
        //text = "";
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
