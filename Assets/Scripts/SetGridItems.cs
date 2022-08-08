using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetGridItems : MonoBehaviour
{
    [SerializeField] private GameObject gridElementPrefab;

    [SerializeField] private RewardInfo []allRewardInfoPrefabs;

    public void UpdateGridElements(List<CollectedItem> collectedItems)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < collectedItems.Count; i++)
        {
            for (int j = 0; j < allRewardInfoPrefabs.Length; j++)
            {
                if (collectedItems[i].ID == allRewardInfoPrefabs[j].rewardID)
                {
                    GameObject gridElement = Instantiate(gridElementPrefab, transform);

                    if (gridElement.transform.GetChild(0).GetComponent<Image>() != null)
                    {
                        gridElement.transform.GetChild(0).GetComponent<Image>().sprite = allRewardInfoPrefabs[j].sprite;
                    }

                    if (gridElement.transform.GetChild(1).GetComponent<Text>() != null)
                    {
                        gridElement.transform.GetChild(1).GetComponent<Text>().text = "x" + collectedItems[i].amount.ToString();
                    }
                }
            }
        }
    }
}
