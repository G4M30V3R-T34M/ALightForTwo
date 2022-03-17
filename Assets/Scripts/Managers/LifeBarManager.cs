using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarManager : MonoBehaviour
{
    [SerializeField] Image mainBar;
    [SerializeField] Image secondaryBar;
    [SerializeField] LifeBarScriptable _lifeBar;

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
            secondaryBar.fillAmount -= _lifeBar.ammount * _lifeBar.speed * Time.deltaTime;
        }

        // Heal
        if(mainBar.fillAmount - secondaryBar.fillAmount >= _lifeBar.ammount)
        {
            secondaryBar.fillAmount = mainBar.fillAmount;
        }

    }
}
