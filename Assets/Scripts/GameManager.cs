using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card> deck = new List<Card>();
    public List<Card> player_deck = new List<Card>();
    public List<Card> ai_deck = new List<Card>();
    public List<Card> player_hand = new List<Card>();
    public List<Card> ai_hand = new List<Card>();
    public List<Card> discard_pile = new List<Card>();
    public int playerscore;
    public int aiscore;
    public int target;
    public int playerdif;
    public int aidif;
    public int playerpoints;
    public int aipoints;
    public bool playerout;
    public bool aiout;
    public Card selectedcard;

    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerout = false;
        aiout = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (deck.Count <= 5)
        {
            Shuffle();
        }

        if (playerscore > target)
        {
            Debug.Log("you went over, you lose");
            playerout = true;
            round_end();
        }
        
      // check player and ai score against target score
    }

    void Deal()
    {
        // give each player two cards, hiding 
    }

    void Shuffle()
    {
        // return all cards to the deck 
    }

    void AI_Turn()
    {
        // draw card
        if (target - aiscore <= 11)
        {
            player_hand.Add(selectedcard);
        }
    }

    void round_end()
    {
        playerdif = target - playerscore;
        aidif = target - aiscore;
        if (playerdif < aidif && playerdif >= 0)
        {
            Debug.Log("player wins");
            playerpoints += 1;
            // discard all the cards from both players hands
        } else if (aidif < playerdif && aidif < 0) 
        {
            Debug.Log("AI wins");
            aipoints += 1;
            // discard all the cards from both players hands
        } else if (aidif == playerdif)
        {
            Debug.Log("tie, no one gets any points");
           // discard all the cards from bother players hands
        }
        else
        {
            Debug.Log("This shouldn't have came up, something is wrong.");
        }
    }



    
}
