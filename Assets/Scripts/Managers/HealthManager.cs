using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int maxHealth;
    private int currentHealth;

    public delegate void NoHealthAction();
    public event NoHealthAction NoHealth;

    public bool isProtected;

    public void SetUp(int health) {
        maxHealth = health;
        currentHealth = health;
    }

    public void TakeDamage(int damage) {
        if (isProtected) { return; }
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth == 0 && NoHealth != null) {
            NoHealth.Invoke();
        }
    }

    public void Heal(int healValue) {
        currentHealth += healValue;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public bool CanHeal() {
        return currentHealth < maxHealth;
    }
}