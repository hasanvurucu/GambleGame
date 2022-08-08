using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShowdown : MonoBehaviour
{
    [SerializeField] private Image squareImage;
    [SerializeField] private Image rectangularImage;

    public List<Item> items = new List<Item>();

    public int index;

    public void ClearItemList()
    {
        items.Clear();
        index = 0;
    }

    public void FillShowdownArray(RewardInfo rewardInfo)
    {
        if (rewardInfo.rewardID == "Bomb")
        {
            gameObject.SetActive(false);
            return;
        }


        Item tempItem = new Item();
        tempItem.isSquare = rewardInfo.isChest;

        if (!rewardInfo.isChest)
        {
            tempItem.amount = rewardInfo.amount;
            tempItem.sprite = rewardInfo.sprite;

            items.Add(tempItem);
        }
        else //isChest
        {
            for (int i = 0; i < rewardInfo.chestContent.Length; i++)
            {
                Item temp = new Item();
                temp.isSquare = true;
                temp.amount = rewardInfo.chestContent[i].amount;
                temp.sprite = rewardInfo.chestContent[i].sprite;

                items.Add(temp);
            }
        }
    }

    public void GetCollectedInfo()
    {
        if (index >= items.Count)
        {
            ClearItemList();
            gameObject.SetActive(false);
            return;
        }

        squareImage.gameObject.SetActive(false);
        rectangularImage.gameObject.SetActive(false);

        if (items[index].isSquare)
        {
            ShowItemOnImage(items[index].sprite, items[index].amount, squareImage);
        }
        else
        {
            ShowItemOnImage(items[index].sprite, items[index].amount, rectangularImage);
        }

        index++;
    }

    private void ShowItemOnImage(Sprite sprite, int amount, Image chosenImage)
    {
        chosenImage.gameObject.SetActive(true);

        chosenImage.sprite = sprite;
        chosenImage.transform.GetChild(0).GetComponent<Text>().text = "x" + amount.ToString();
    }

    public class Item
    {
        public bool isSquare;
        public Sprite sprite;
        public int amount;
    }
}
