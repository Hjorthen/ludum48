using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public ParticleSystem bullet;
    public ParticleSystem flamethrower;
    private float bulletCooldown;
    public Transform playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * 0.01f;

        if(Input.GetKey(KeyCode.B))
        {
            flamethrower.Play();
        }

        if (Input.GetKeyDown(KeyCode.F) && bulletCooldown <= 0) {
            {
                bullet.Play();
                bulletCooldown = 1;
            }

        }

        if (bulletCooldown > 0)
        {
            bulletCooldown -= Time.deltaTime;
        }
    }
}
