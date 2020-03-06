using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    private Rigidbody rollerRb;
    private GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rollerRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rollerRb.AddForce(Vector3.left * speed, ForceMode.Acceleration);

        destroyRoller();
    }

    private void destroyRoller()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
