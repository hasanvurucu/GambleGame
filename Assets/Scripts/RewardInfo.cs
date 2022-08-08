using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardInfo : MonoBehaviour
{
    public Sprite sprite;
    public int amount;
    public bool isChest;

    public RewardInfo[] chestContent;

    public bool isBomb;

    public string rewardID; //This lets us know what actually the reward is
}
