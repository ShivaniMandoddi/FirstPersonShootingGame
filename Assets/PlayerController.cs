using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    // Start is called before the first frame update
    public float playerSpeed;
    public float camRotation;
    public Camera cam;
    GunController gunController;
    public EnemyController enemyController;
    public int health=20;
    int maxHealth=50;
    public int ammo = 20;
    public int maxAmmo = 20;
    public bool IsGameover = false;
    public GameObject Steve;
    public Text ammoText;
    public Text healthText;
    
    void Start()
    {
        gunController=GetComponentInChildren<GunController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal")*playerSpeed*Time.deltaTime;          // PLayer Moving left and right
        float inputZ = Input.GetAxis("Vertical")*playerSpeed*Time.deltaTime;           // PLayer moving forward and backward
        this.transform.Translate(new Vector3(inputX, 0f, inputZ));
        ammoText.text = "Ammo :" + ammo;
        healthText.text = "Health :" + health;

        // Player Rotation
        float caminputy = Input.GetAxis("Mouse X") * camRotation; 
        transform.Rotate(0f, caminputy, 0f);
        /*if (Input.GetKey(KeyCode.N))            
        {
            this.transform.Rotate(0f, -1f, 0f);
        }
        if (Input.GetKey(KeyCode.M))      // To rotate player
        {
            this.transform.Rotate(0f,1f, 0f);
        }*/
        // Cam Rotation
        float caminputx = Input.GetAxis("Mouse Y")*camRotation;
        cam.transform.Rotate(-caminputx, 0f, 0f);
        if(health==0)
        {
            IsGameover = true;
            Debug.Log("GameOver");
            PlayerDeath();

        }
       
    }
    public void PlayerDeath()
    {
        GameObject temp = Instantiate(Steve, transform.position, Quaternion.identity);
        temp.GetComponent<Animator>().SetTrigger("Death");
        Destroy(gameObject);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ammo")
        {
            Debug.Log("Ammo is collected");
            ammo = Mathf.Clamp(ammo + 20, 0, maxAmmo);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag=="Medical")
        {
            Debug.Log("Health is collected");
            health = Mathf.Clamp(health + 50, 0, maxHealth);
            Destroy(collision.gameObject);
        }
    }
}
