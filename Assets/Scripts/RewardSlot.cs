using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSlot : MonoBehaviour
{
    [Header ("Assign a rewardInfo prefab")]
    public RewardInfo rewardInfo;

    [Space]
    public Image image;
    public Text amountText;

    public int amount { get; private set; }
    public bool isChest { get; private set; }
    public bool isBomb { get; private set; }

    Vector2 resolutionInitial = new Vector2(84.105f, 37.289f);

    public string rewardID;

    private void OnValidate()
    {
        UpdateThisSlot();
    }

    public void UpdateThisSlot()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        if (amountText == null)
        {
            amountText = transform.GetChild(0).GetComponent<Text>();
        }

        if (rewardInfo != null)
        {
            image.sprite = rewardInfo.sprite;

            amount = rewardInfo.amount;
            SetAmountText();

            isChest = rewardInfo.isChest;

            if (rewardInfo.isBomb)
            {
                image.GetComponent<RectTransform>().sizeDelta = new Vector2(resolutionInitial.x, resolutionInitial.x);
            }
            else
            {
                image.GetComponent<RectTransform>().sizeDelta = resolutionInitial;
            }

            rewardID = rewardInfo.rewardID;
        }
    }


    private void SetAmountText()
    {
        amountText.text = "x" + amount.ToString();
    }

}
