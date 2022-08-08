using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedRewards : MonoBehaviour
{
    public List<CollectedItem> collectedItems = new List<CollectedItem>();

    [SerializeField] SetGridItems setGridItems;

    [SerializeField] InputHandler inputHandler;

    public void AddCollected(string ID, int amount)
    {
        if (ID == "Bomb")
        {
            if (!GameStates.isRiskFree)
            {
                inputHandler.FailedAppearence();
            }
            else
            {
                Debug.Log("Saved by risk free spin");
            }

            return;
        }

        for (int i = 0; i < collectedItems.Count; i++)
        {
            if (collectedItems[i].ID == ID)
            {
                collectedItems[i].amount += amount;
                setGridItems.UpdateGridElements(collectedItems);
                return;
            }
        }

        CollectedItem temp = new CollectedItem();
        temp.ID = ID;
        temp.amount = amount;

        collectedItems.Add(temp);

        setGridItems.UpdateGridElements(collectedItems);
    }
}

[System.Serializable]
public class CollectedItem
{
    public string ID;
    public int amount;
}