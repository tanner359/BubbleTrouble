using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image cooldownTimer;

    private void Update()
    {
        float cooldownTime = PlayerController.timer;
        cooldownTimer.fillAmount += .01f / cooldownTime;
        if (cooldownTimer.fillAmount == 1) gameObject.SetActive(false);
    }
}
