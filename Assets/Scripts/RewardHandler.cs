using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardHandler : MonoBehaviour
{
    public int spinCount;

    #region Accessbility

    public static RewardHandler instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public RewardSlot[] rewardSlots;
    public List<RewardInfo> rewSlotsTemp = new List<RewardInfo>();

    [SerializeField] private GameObject itemShowdownImage;

    [SerializeField] private Sprite []spinImages;
    [SerializeField] private Image spinImage;

    private void Start()
    {
        itemShowdownImage.SetActive(false);

        spinCount = 0;
    }

    public void StartRewardingProcess()
    {
        ShuffleRewardSlots();
    }

    public void GiveReward()
    {
        //Give reward on index 0
        itemShowdownImage.SetActive(true);
        itemShowdownImage.GetComponent<ItemShowdown>().FillShowdownArray(rewardSlots[0].rewardInfo);
        itemShowdownImage.GetComponent<ItemShowdown>().GetCollectedInfo();

        if (rewardSlots[0].rewardID == "Chest")
        {
            for (int i = 0; i < rewardSlots[0].rewardInfo.chestContent.Length; i++)
            {
                GetComponent<CollectedRewards>().AddCollected(rewardSlots[0].rewardInfo.chestContent[i].rewardID, rewardSlots[0].rewardInfo.chestContent[i].amount);
            }
            return;
        }
        GetComponent<CollectedRewards>().AddCollected(rewardSlots[0].rewardID, rewardSlots[0].amount);
    }

    private void ShuffleRewardSlots()
    {
        StartCoroutine(ShuffleSlots());
    }

    IEnumerator ShuffleSlots()
    {
        #region  Hide for shuffling

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            SetAlphaValues(1 - t);

            yield return new WaitForEndOfFrame();
        }

        SetAlphaValues(0);

        #endregion

        #region Add list and shuffle back to the array

        rewSlotsTemp.Clear();

        for (int i = 0; i < rewardSlots.Length; i++)
        {
            RewardInfo temp = rewardSlots[i].rewardInfo;
            rewSlotsTemp.Add(temp);
        }

        for (int i = 0; i < rewardSlots.Length; i++)
        {
            int chosenRandom = Random.Range(0, rewSlotsTemp.Count);

            rewardSlots[i].rewardInfo = rewSlotsTemp[chosenRandom];
            rewardSlots[i].UpdateThisSlot();

            rewSlotsTemp.RemoveAt(chosenRandom);
        }
        #endregion

        #region Stop hiding values

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;

            SetAlphaValues(t);

            yield return new WaitForEndOfFrame();
        }

        SetAlphaValues(1);

        #endregion
    }

    private void SetAlphaValues(float value)
    {
        for (int i = 0; i < rewardSlots.Length; i++)
        {
            Color imgColor = rewardSlots[i].image.color;
            imgColor.a = value;
            rewardSlots[i].image.color = imgColor;

            Color textColor = rewardSlots[i].amountText.color;
            textColor.a = value;
            rewardSlots[i].amountText.color = textColor;
        }
    }

    public void CheckRiskFreeSpin()
    {
        spinCount++;

        if (spinCount % 30 == 0 && spinCount >= 30)
        {
            //Golden spin
            spinImage.sprite = spinImages[0];
            RiskFree();

            return;
        }

        if (spinCount % 5 == 0 && spinCount >= 5)
        {
            //Silver spin
            spinImage.sprite = spinImages[1];
            RiskFree();

            return;
        }

        //else : Classic spin
        spinImage.sprite = spinImages[2];
        GameStates.isRiskFree = false;

    }

    public void RiskFree()
    {
        Debug.Log("Next spin is risk free");
        GameStates.isRiskFree = true;
    }
}
