using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrips : MonoBehaviour
{
    public float tripMaxCoolDown = 10.0f;
    public float tripLowerBound = 0.1f;
    public float tripUpperBound = 0.4f;
    public float rediculousFactor = 4.0f;

    private UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController fpsController;
    private CapsuleCollider fpsCollider;
    Rigidbody fpsRigidBody;

    private KeyCode sprintKey = KeyCode.LeftShift;
    private float tripCurrentCoolDown = 0.0f;

    // Use this for initialization
    void Start()
    {
        fpsController = gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>();
        fpsCollider = gameObject.GetComponent<CapsuleCollider>();
        fpsRigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tripCurrentCoolDown > 0.0f)
        {
            tripCurrentCoolDown -= Time.deltaTime;
        }
        else
        {
            ToggleTrip(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float yMin = (tripLowerBound - 0.5f) * fpsCollider.height + gameObject.transform.position.y;
        float yMax = (tripUpperBound - 0.5f) * fpsCollider.height + gameObject.transform.position.y;

        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.point.y > yMin && contact.point.y < yMax && Input.GetKey(sprintKey) && tripCurrentCoolDown <= 0)
            {
                fpsRigidBody.velocity += (collision.relativeVelocity * rediculousFactor * -1.0f);
                ToggleTrip(true);
            }
        }
    }

    public void ToggleTrip(bool isTripping)
    {
        PlayerScript ps = gameObject.GetComponent<PlayerScript>();
        if (isTripping)
        {
            fpsController.enabled = false;
            fpsRigidBody.freezeRotation = false;

            ps.currentHealth -= ps.tripDamageFactor;

            tripCurrentCoolDown = tripMaxCoolDown;
        }
        else
        {
            fpsController.enabled = true;
            fpsRigidBody.freezeRotation = true;
        }
    }
}
