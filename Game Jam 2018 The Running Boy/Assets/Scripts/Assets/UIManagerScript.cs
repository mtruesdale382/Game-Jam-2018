using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public Image healthBarFiller;
    public Image staminaFiller;
    public Image exhaustFiller;
    public float totalTimeUntilGameOver = 70;
    private float startingTime;
    private float rotateRatio;
    public Text timer;
    public Text gameOver;
    public GameObject player;
    public GameObject light;
    public int messagesDelivered;
    public int totalMessages;
    public Text messageCounter;
    public bool gameRunning = true;
    public float gameEndTimer;

    void Start()
    {
        startingTime = totalTimeUntilGameOver;
        gameOver.enabled = false;
        changeHealthDisplay();
        staminaFiller.enabled = true;
        exhaustFiller.enabled = false;
    }

    void Update()
    {
        changeHealthDisplay();
        staminaAndExhaustBar();
        displayAlternater();
        displayTimer();
        /*if (totalTimeUntilGameOver > 0)
            lightingChange();*/
        ChangeMessageDisplay();
        if(!gameRunning)
        {
            ReturnToMenuAfterGameEnd();
        }
    }

    void changeHealthDisplay()
    {
        PlayerScript ps = player.GetComponent<PlayerScript>();

        if (ps.isDead)
        {
            healthBarFiller.enabled = false;
        }

        healthBarFiller.rectTransform.localScale = new Vector3(ps.healthRatio, 1, 1);
    }

    void staminaAndExhaustBar()
    {
        PlayerScript ps = player.GetComponent<PlayerScript>();

        staminaFiller.rectTransform.localScale = new Vector3(ps.staminaRatio, 1, 1);
        exhaustFiller.rectTransform.localScale = new Vector3(ps.exhaustRatio, 1, 1);
    }
    void displayAlternater()
    {
        PlayerScript ps = player.GetComponent<PlayerScript>();

        if (ps.currentStamina <= 0)
        {
            staminaFiller.enabled = false;
            exhaustFiller.enabled = true;
        }
        else if (ps.currentStamina > 0)
        {
            staminaFiller.enabled = true;
            exhaustFiller.enabled = false;
        }
    }

    void displayTimer()
    {
        totalTimeUntilGameOver -= Time.deltaTime;

        rotateRatio = startingTime / totalTimeUntilGameOver;
        float minutes = Mathf.Floor(totalTimeUntilGameOver / 60);
        float seconds = Mathf.Round(totalTimeUntilGameOver % 60);

        if (seconds.ToString().Length == 1)
        {
            timer.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timer.text = minutes.ToString() + ":" + seconds.ToString();
        }

        if (totalTimeUntilGameOver <= 0 && gameRunning)
        {
            /*timer.enabled = false;
            gameOver.enabled = true;*/
            gameRunning = false;
        }

        Debug.Log(rotateRatio);
    }

    public void lightingChange()
    {
        Transform trans;
        trans = light.GetComponent<Transform>();
        trans.RotateAround(Vector3.zero, Vector3.right, rotateRatio / 150);
    }

    void ChangeMessageDisplay()
    {
        messageCounter.text = messagesDelivered + "/" + totalMessages;

        if(messagesDelivered >= totalMessages && gameRunning)
        {
            //Game Won
            gameRunning = false;
            gameOver.text = "All Messages Delivered!";
            gameOver.color = Color.green;
        }
    }

    void ReturnToMenuAfterGameEnd()
    {
        timer.enabled = false;
        gameOver.enabled = true;

        gameEndTimer -= Time.deltaTime;

        if (gameEndTimer <= 0)
        {
            player.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().mouseLook.SetCursorLock(false);
            SceneManager.LoadScene("TitleScreen");
        }
    }
}