using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public int offset;
    public Transform _canvas;

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
        offset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (deck.Count <= 5)
        {
            cleanout(discard_pile, deck);
        }

        if (playerscore > target)
        {
            Debug.Log("you went over, you lose");
            playerout = true;
            round_end();
        } else if (aiscore > target)
        {
            Debug.Log("ai went over, you win");
            aiout = true;
            round_end();
        }
        
      // check player and ai score against target score
    }

    void Deal(int handsize)
    {
        for (int i = 0; i < handsize; i++)
        {
            int cardNum = Random.Range(0, deck.Count);
            Card card = Instantiate(deck[cardNum], new Vector3(-797 + offset, 554, 0), quaternion.identity);
            player_hand.Add(card);
            card.transform.SetParent(_canvas);
            deck.RemoveAt(cardNum);
            offset += 200;
            
            int cardNum2 = Random.Range(0, deck.Count);
            Card cardai = Instantiate(deck[cardNum2], new Vector3(-797 + offset, -670, 0), quaternion.identity);
            ai_hand.Add(cardai);
            card.transform.SetParent(_canvas);
            deck.RemoveAt(cardNum2);
            offset += 200;
        }

    }



    void cleanout(List<Card> first, List<Card> second)
    {
        for (int i = 0; i < first.Count; i++)
        {
          second.Add(first[i]);
        }

        first.Clear();
    }

    void AI_Turn()
    {
        // draw card
        if (target - aiscore <= 11)
        {
            player_hand.Add(selectedcard);
            selectedcard = null;
        }
        else
        {
            ai_hand.Add(selectedcard);
            selectedcard = null;
        }
    }

    void round_end()
    {
        playerdif = target - playerscore;
        aidif = target - aiscore;
        if (playerdif < aidif && playerdif >= 0 || aiout)
        {
            Debug.Log("player wins");
            playerpoints += 1;
            // discard all the cards from both players hands
        } else if (aidif < playerdif && aidif >= 0 || playerout) 
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
