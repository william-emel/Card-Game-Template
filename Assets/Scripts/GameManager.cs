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
    public List<Card> player_hand = new List<Card>();
    public List<Card> ai_hand = new List<Card>();
    public List<Card> discard_pile = new List<Card>();
    public int playerscore, aiscore, offset, offset2, playerpoints, aipoints, roundcount = 0;
    public int target, playerdif, aidif;
    public bool playerout, aiout, destroythis = false;
    public Card selectedcard;
    public Transform _canvas;
    public Card cardselect;
    public Vector3 middle = new Vector3(-528, -82, 0);
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
        target = 21;
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
            Card cardai = Instantiate(deck[cardNum2], new Vector3(-797 + offset2, -670, 0), quaternion.identity);
            ai_hand.Add(cardai);
            card.transform.SetParent(_canvas);
            deck.RemoveAt(cardNum2);
            offset2 += 200;
        }

    }



    void cleanout(List<Card> first, List<Card> second)
    {
        if (second == discard_pile)
        {
            destroythis = true;
        }
        else destroythis = false;

        for (int i = 0; i < first.Count; i++)
        {
          second.Add(first[i]);
          if (destroythis)
          {
              cardselect = first[i];
              Destroy(cardselect);
          }
        }

        first.Clear();
    }

    void AI_Turn()
    {
        int idk = Random.Range(0, deck.Count);
        selectedcard = deck[idk];
        deck.RemoveAt(idk);
        Instantiate(selectedcard, middle, quaternion.identity);
        if (target - aiscore <= 7)
        {
            selectedcard.transform.position = new Vector3(-797 + offset, 554, 0);
            playerscore += selectedcard.health;
            offset += 200;
            player_hand.Add(selectedcard);
            selectedcard = null;
        }
        else
        {
            selectedcard.transform.position = new Vector3(-797 + offset2, -670, 0);
            aiscore = selectedcard.health;
            offset2 += 200;
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
            cleanout(player_hand, discard_pile);
            cleanout(ai_hand, discard_pile);
        } else if (aidif < playerdif && aidif >= 0 || playerout) 
        {
            Debug.Log("AI wins");
            aipoints += 1;
            cleanout(player_hand, discard_pile);
            cleanout(ai_hand, discard_pile);
        } else if (aidif == playerdif)
        { 
            Debug.Log("tie, no one gets any points");
            cleanout(player_hand, discard_pile);
            cleanout(ai_hand, discard_pile);
        }
        else
        {
            Debug.Log("This shouldn't have came up, something is wrong.");
        }
        newround();
        
        
    }

    void newround()
    {
        Deal(2);
        aiscore = 0;
        playerscore = 0;
    }

    void playerturn()
    {
        Debug.Log("Press 1 to continue playing or 2 to end your turn.");
        if (Input.GetKeyDown("1"))
        {
            int smth = Random.Range(0, deck.Count);
            selectedcard = deck[smth];
            deck.RemoveAt(smth);
            Instantiate(selectedcard, middle, quaternion.identity);
        }
        else
        {
            round_end();
        }
    }


}
