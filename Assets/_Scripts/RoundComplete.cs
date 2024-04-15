using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundComplete : MonoBehaviour
{
    public TMP_Text[] cardTitles;
    public TMP_Text[] cardTexts;
    public GameObject[] cardObjects;

    [SerializeField] private Button continueButton;

    private List<DifficultyCard> allCards = new List<DifficultyCard>();
    private List<DifficultyCard> selectedCards;
    private DifficultyCard selectedCard;

    // Start is called before the first frame update
    void Start()
    {
        // Add all card types
        allCards.Add(new DetectRangeCard());
        allCards.Add(new DetectAngleCard());
        allCards.Add(new DetectTimeCard());
        allCards.Add(new SpeedCard());
        if(StatsManager.Instance.totalQuota <= 4)
            allCards.Add(new QuotaCard());

        continueButton.onClick.AddListener(OnContinueClicked);

        // Select 3 random cards
        selectedCards = SelectRandomCards(3);

        // Display selected cards on HUD
        DisplaySelectedCards(selectedCards);

        // ContinueButton starts non-interactable
        continueButton.interactable = false;
    }

    // Randomly select cards
    private List<DifficultyCard> SelectRandomCards(int count)
    {
        List<DifficultyCard> selectedCards = new List<DifficultyCard>();

        Shuffle(allCards);

        for (int i = 0; i < count && i < allCards.Count; i++)
        {
            selectedCards.Add(allCards[i]);
        }

        return selectedCards;
    }

    private void Shuffle<T>(List<T> list)
    {
        for(int i = 0; i <list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void DisplaySelectedCards(List<DifficultyCard> selectedCards)
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            // Display cards texts
            cardTitles[i].text = selectedCards[i].Title;
            cardTexts[i].text = selectedCards[i].Info;

            // Enable card gameobjects
            cardObjects[i].SetActive(true);
        }
    }

    public void OnCardClicked(int card)
    {
        // Deselect previous card
        if (selectedCard != null)
        {

        }

        // Select clicked card
        selectedCard = selectedCards[card];
        continueButton.interactable = true;

        Debug.Log("Selected card: " + selectedCards[card].Title);
    }

    public void OnContinueClicked()
    {
        if(selectedCard != null)
        {
            selectedCard.ApplyDifficulty();
            Debug.Log("Difficulty applied");
        }
    }
}
