using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
   
    [SerializeField] private float speed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movementDirection;
        Vector2 position = transform.position;
        
        if (horizontal > 0.5 || horizontal < -0.5) {
            movementDirection = new Vector2(horizontal, 0);
        } else {
            movementDirection = new Vector2(0,vertical);
        }
    
        transform.position = position + speed * movementDirection * Time.deltaTime;
    }
}
