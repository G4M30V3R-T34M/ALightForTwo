using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour
{
    [SerializeField] Image mainBar;
    [SerializeField] Image secondaryBar;

    public void UpdateBar(float totalLife)
    {
        // update main bar
        mainBar.fillAmount = totalLife;
    }

    private void Update()
    {
        // Damage
        if(mainBar.fillAmount < secondaryBar.fillAmount)
        {
            secondaryBar.fillAmount -= 0.1f * 4 * Time.deltaTime;
        }

        // Heal
        if(mainBar.fillAmount - secondaryBar.fillAmount >= 0.1f)
        {
            secondaryBar.fillAmount = mainBar.fillAmount;
        }

    }
}
