using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] internal int playerHealth, enemyHealth;
    [SerializeField] Image[] playerhearts;
    [SerializeField] Image[] enemyhearts;
    [SerializeField] GameObject winPanel, losePanel;

    private void Awake()
    {
        playerHealth = 10;
        enemyHealth = 10;
    }
    private void Update()
    {
        if (playerHealth <= 0)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
        if (enemyHealth <= 0)
        {
            Time.timeScale = 0;
            losePanel.SetActive(true);
        }
    }
    internal void PlayerHealthUpdate()
    {
        playerHealth--;
        for (int i = playerHealth; i < 10; i++)
        {
            playerhearts[i].gameObject.SetActive(false);
        }
    }
    internal void EnemyHealthUpdate()
    {
        enemyHealth--;
        for (int i = enemyHealth; i < 10; i++)
        {
            enemyhearts[i].gameObject.SetActive(false);
        }
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
