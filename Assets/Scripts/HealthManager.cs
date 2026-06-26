using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image HealthBar;
    private float Damage = 5f;
    private float MaxHealth = 100f;
    private float CurrentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = MaxHealth;
        HealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
        }

        if(CurrentHealth < 100)
        {
            CurrentHealth += 1f * Time.deltaTime;
        }
    }

    public void TakeDamage()
    {
        CurrentHealth -= Damage;
        HealthBar.fillAmount = CurrentHealth/MaxHealth;
        Debug.Log(CurrentHealth);
    }

}
