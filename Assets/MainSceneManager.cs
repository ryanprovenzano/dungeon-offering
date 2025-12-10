using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{

    CombatManager combatManager;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void Start()
    {
        combatManager = CombatManager.instance;
        combatManager.GameFinished += GameFinishedHandler;
    }

    private void GameFinishedHandler(object sender, EventArgs e)
    {
        if (combatManager.turnStatus == "PlayerWon")
        {
            GameObject.FindGameObjectWithTag("WinText").GetComponent<TextMeshProUGUI>().text = "You won! Offer his head to the king!(Thank you for playing!)";
        }
        else
        {
            GameObject.FindGameObjectWithTag("WinText").GetComponent<TextMeshProUGUI>().text = "You lost! Your king won't be very happy. (Thank you for playing!)";
        }
    }

    private void OnDisable()
    {
        combatManager.GameFinished -= GameFinishedHandler;
    }
}
