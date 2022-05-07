using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GAFree_Controller))]
[RequireComponent(typeof(GAHold_Controller))]
[RequireComponent(typeof(GA_Movement))]
[RequireComponent(typeof(GA_Shout))]
[RequireComponent(typeof(VisibilityInteraction))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GA_AnimationController))]
public class GoodAlienMain : MonoBehaviour
{
    VisibilityInteraction visibility;
    public HealthManager healthManager;
    public bool IsVisible { get { return visibility.IsVisible; } }

    [SerializeField] GameOverManager GO_Manager;
    [SerializeField] GoodAlienScriptable _alien ;
    [SerializeField] AudioSource healAudioSource;
    public GoodAlienScriptable Alien { get { return _alien; } }

    GAFree_Controller freeController;
    GAHold_Controller holdController;

    GA_AnimationController animator;

    public bool isFree { get { return freeController.isActiveAndEnabled; } }
    Coroutine healingCoroutineReference;

    private void Awake() {
        visibility = GetComponent<VisibilityInteraction>();
        healthManager = GetComponent<HealthManager>();
        freeController = GetComponent<GAFree_Controller>();
        holdController = GetComponent<GAHold_Controller>();
        animator = GetComponent<GA_AnimationController>();
    }

    void Start() {
        freeController.enabled = true;
        holdController.enabled = false;

        visibility.InLight += EnterLight;
        visibility.OutLight += ExitLight;

        InitHealthManager();
    }
    private void InitHealthManager() {
        healthManager.SetUp(_alien.Health);
        healthManager.NoHealth += Die;
    }

    private void OnDestroy() {
        visibility.InLight -= EnterLight;
        visibility.OutLight -= ExitLight;
        healthManager.NoHealth -= Die;
    }

    private void Die() {
        GO_Manager.GameOver();
    }

    private void EnterLight() {
        if (healingCoroutineReference != null) {
            StopCoroutine(healingCoroutineReference);
            healingCoroutineReference = null;
        }
        healAudioSource.Stop();
    }
    private void ExitLight() {
        if (healingCoroutineReference == null) {
            healingCoroutineReference = StartCoroutine(Healing());
        }
    }

    public void SwitchController() {
        freeController.enabled = !freeController.isActiveAndEnabled;
        holdController.enabled = !holdController.isActiveAndEnabled;
    }

    private IEnumerator Healing()
    {
        healAudioSource.Play();
        while(healthManager.CanHeal()) {
            healthManager.Heal(_alien.HealValue);
            yield return new WaitForSeconds(_alien.waitBetweenHeal);
        }
        healingCoroutineReference = null;
        healAudioSource.Stop();
    }

    public void ThrowLight()
    {
        animator.ThrowLight();
    }


    public void RecieveLight()
    {
        animator.RecieveLight();
    }
}
