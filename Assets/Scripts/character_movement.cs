using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld == true)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.Find("Shoot_cursor").gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
            
            
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            isBeingHeld = true;
        
        }

    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
        float x_force = this.gameObject.transform.position.x - this.gameObject.transform.Find("Shoot_cursor").gameObject.transform.position.x;
        float y_force = this.gameObject.transform.position.y - this.gameObject.transform.Find("Shoot_cursor").gameObject.transform.position.y;
        this.gameObject.transform.Find("Shoot_cursor").gameObject.transform.position = this.gameObject.transform.position;
        Vector2 shoot_force = new Vector2(x_force, y_force);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(shoot_force, ForceMode2D.Impulse);

        print(x_force);
        print(y_force);
    }
}
