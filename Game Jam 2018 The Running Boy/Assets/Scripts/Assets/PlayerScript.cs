using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float maximumHealth = 100.0f;
    public float currentHealth;
    public float healthRatio;
    public float maximumStamina = 100.0f;
    public float currentStamina;
    public float maximumExhaust = 100.0f;
    public float currentExhaust;
    public float exhaustRatio;
    public float staminaRatio;
    public float staminaDecreaseFactor = 10.0f;
    public float damageHitLevel = 10.0f;
    public float tripDamageFactor = 10.0f;

    public bool isDead = false;

    public GameObject UIManager;


    void Start()
    {
        currentHealth = maximumHealth;
        currentExhaust = 0;
        currentStamina = maximumStamina;
        healthRatio = currentHealth / maximumHealth; //sets the ratio
        staminaRatio = currentStamina / maximumStamina;
        exhaustRatio = currentExhaust / maximumExhaust;
    }

    void Update()
    {
        healthRatio = currentHealth / maximumHealth; //sets the ratio
        staminaRatio = currentStamina / maximumStamina;
        exhaustRatio = currentExhaust / maximumExhaust;

        if (currentHealth <= 0)
        {
            UIManagerScript ui = UIManager.GetComponent<UIManagerScript>();
            Debug.Log("you ded");
            isDead = true;

            ui.gameRunning = false;
            gameObject.GetComponent<PlayerTrips>().tripMaxCoolDown = 20.0f;
            gameObject.GetComponent<PlayerTrips>().ToggleTrip(true);

            //Destroy(gameObject);
        }

        staminaExhaustAdjuster();
    }

    private void OnCollisionStay(Collision col)
    {
        //takes away kit points/health from player if colliding with an object with the tag "Enemy"
        Debug.Log("in the collision");
        if (col.gameObject.tag == "Enemy")
        {
            currentHealth -= damageHitLevel;
            Debug.Log("hit enemey");
        }
    }

    private void staminaExhaustAdjuster() //when sprinting stats
    {
        UIManagerScript ui = UIManager.GetComponent<UIManagerScript>();
        //takes away stamina if the player sprints too long
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            if (currentStamina > 0)
            {
                currentStamina -= staminaDecreaseFactor;
                Debug.Log("losing stamina");
            }

            if (currentStamina <= 0)
            {
                if (currentExhaust < maximumExhaust)
                {
                    currentExhaust += staminaDecreaseFactor;
                    Debug.Log("getting exahusted");
                }
                else
                {
                    currentHealth -= damageHitLevel; //takes away health when over exhausted
                }
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            if (currentStamina > 0)
            {
                currentStamina -= staminaDecreaseFactor;
                Debug.Log("losing stamina");
            }
            if (currentStamina <= 0)
            {
                if (currentExhaust < maximumExhaust)
                {
                    currentExhaust += staminaDecreaseFactor;
                    Debug.Log("getting exahusted");
                }
                else
                {
                    currentHealth -= damageHitLevel; //takes away health when over exhausted
                }
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            if (currentStamina > 0)
            {
                currentStamina -= staminaDecreaseFactor;
                Debug.Log("losing stamina");
            }
            if (currentStamina <= 0)
            {
                if (currentExhaust < maximumExhaust)
                {
                    currentExhaust += staminaDecreaseFactor;
                    Debug.Log("getting exahusted");
                }
                else
                {
                    currentHealth -= damageHitLevel; //takes away health when over exhausted
                }
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            if (currentStamina > 0)
            {
                currentStamina -= staminaDecreaseFactor;
                Debug.Log("losing stamina");
            }
            if (currentStamina <= 0)
            {
                if (currentExhaust < maximumExhaust)
                {
                    currentExhaust += staminaDecreaseFactor;
                    Debug.Log("getting exahusted");
                }
                else
                {
                    currentHealth -= damageHitLevel; //takes away health when over exhausted
                }
            }
        }

        //restores stamina and exhaust levels to be rested
        if (!(Input.GetKey(KeyCode.LeftShift)))
        {
            if ((currentExhaust <= maximumExhaust) && (currentExhaust >= 0))
            {
                currentExhaust--;
                Debug.Log("resting exhuast");
            }

            if ((currentStamina <= maximumStamina) && (currentStamina >= 0))
            {
                currentStamina++;
                Debug.Log("resting stamina");
            }
        }
    }
}
