using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera playerCamera;
    public TMP_Dropdown SeedChoice;
    private int seedType = -1;
    public float speed = 8;
    private float xRange = 10;
    private float zRange = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                PlantTileInterface target =hitInfo.collider.gameObject.GetComponent<PlantTileInterface>(); 
                if (target != null)
                {
                    target.Clicked(seedType); //include seedType
                }
            }
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        transform.position = transform.position+new Vector3(speed*horizontalInput*Time.deltaTime, 0, speed*verticalInput*Time.deltaTime);
        
        if (transform.position.x>xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        } else if (transform.position.x<-xRange){
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        
        if (transform.position.z>zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        } else if (transform.position.z<-zRange){
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }
    }
    
    public void ChangeSeedType(){
        seedType = SeedChoice.value-1; //as "none" = -1, flower = 0, pumpkin =1;
        
    }
}
