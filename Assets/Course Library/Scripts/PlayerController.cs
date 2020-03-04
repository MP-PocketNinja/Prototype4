using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    public bool isOnGround;
    public float jumpForce = 10f;
    public float dodgeSpeed;
    public bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isOnGround = false;
            canJump = false;
            StartCoroutine(jumpCooldown());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerRb.AddForce(focalPoint.transform.right * dodgeSpeed, ForceMode.VelocityChange);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerRb.AddForce(focalPoint.transform.right * -dodgeSpeed, ForceMode.VelocityChange);
        } 

        movement();
        powerupIndication();
    }

    private void powerupIndication()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void movement()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        { 
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Player collided with " + collision.gameObject + " with powerup set to " + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator jumpCooldown()
    {
        yield return new WaitForSeconds(5);
        canJump = true;
    }
}
