using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image cooldownTimer;
    float cooldownTime = 10f;

    private void FixedUpdate()
    {
        cooldownTime -= Time.deltaTime;
        cooldownTimer.fillAmount += .01f / cooldownTime;
        if (cooldownTimer.fillAmount == 1) Destroy(gameObject);
    }
}
