using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int maxHealth;
    public int currentHealth;

    public delegate void NoHealthAction();
    public event NoHealthAction NoHealth;

    private LifeBarManager lifeBar;

    public bool isProtected;

    private void Awake()
    {
        lifeBar = GetComponent<LifeBarManager>();
    }

    public void SetUp(int health) {
        maxHealth = health;
        currentHealth = health;
    }

    public void TakeDamage(int damage) {
        if (isProtected) { return; }
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
        if (currentHealth == 0 && NoHealth != null)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal(int healValue)
    {
        currentHealth += healValue;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (lifeBar)
        {
            float lifePercent = ((float)currentHealth) / maxHealth;
            print(lifePercent);
            lifeBar.UpdateBar(lifePercent);
        }
    }

    public bool CanHeal()
    {
        return currentHealth < maxHealth;
    }
}
