using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private Image spinParentImage;
    [SerializeField] private AnimationClip spinAnimation;

    [SerializeField] private Image failedBG;
    [SerializeField] private Image successBG;
    [SerializeField] private Button []restartButtons;

    [SerializeField] private Button leaveGameButton;

    void Awake()
    {
        spinButton.onClick.AddListener(OnClick_SpinButton);

        restartButtons[0].onClick.AddListener(OnClick_RestartButton);
        restartButtons[1].onClick.AddListener(OnClick_RestartButton);

        leaveGameButton.onClick.AddListener(OnClick_LeaveGameButton);

        failedBG.gameObject.SetActive(false);
        successBG.gameObject.SetActive(false);
    }

    private void OnClick_SpinButton()
    {
        spinButton.interactable = false;

        spinParentImage.GetComponent<Animator>().Play("Spin");
        float animLength = spinAnimation.length;
        StartCoroutine(WaitForRewarding(animLength));

        //Disable button interactibility
        //Shuffle items with disappear&appear
        //Determine chosen item (the first "0th" element of array
        //Give rewards at the end of the anim (event)
    }

    IEnumerator WaitForRewarding(float clipLength)
    {
        RewardHandler.instance.StartRewardingProcess();

        yield return new WaitForSeconds(clipLength);

        spinParentImage.GetComponent<Animator>().Play("Default");
        RewardHandler.instance.GiveReward();

        spinButton.interactable = true;

        RewardHandler.instance.CheckRiskFreeSpin();
    }

    public void FailedAppearence()
    {
        failedBG.gameObject.SetActive(true);
    }

    private void OnClick_RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnClick_LeaveGameButton()
    {
        successBG.gameObject.SetActive(true);
    }
}
