using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_movement : MonoBehaviour
{

    private float startPosX;
    private float startPosY;
    public bool isBeingHeld = false;

    public float sunPowerIncrease = 5f;
    public float waterPowerIncrease = 0.1f;
    public float dragDefault = 1f;
    public float shootingForceDefault = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isBeingHeld == true)
        {
            // get the position of the mouse
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // set the position of the shooting line and cursor
            this.gameObject.transform.Find("ShootCursor").gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
            this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<LineRenderer>().SetPosition(0,this.gameObject.transform.position);
            this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<LineRenderer>().SetPosition(1,mousePos);

            
            
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            // set mouse to being held
            isBeingHeld = true;

            // enable visibility of shooting line and cursor
            this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<LineRenderer>().enabled = true;
            this.gameObject.transform.Find("ShootCursor").gameObject.GetComponent<SpriteRenderer>().enabled = true;
            
            
        }

    }

    private void OnMouseUp()
    {
        // release mouse held
        isBeingHeld = false;

        // get force to send character
        float x_force = this.gameObject.transform.position.x - this.gameObject.transform.Find("ShootCursor").gameObject.transform.position.x;
        float y_force = this.gameObject.transform.position.y - this.gameObject.transform.Find("ShootCursor").gameObject.transform.position.y;
        Vector2 shoot_force = new Vector2(x_force, y_force);    

        // set SUN POWER
        if(this.gameObject.GetComponent<CharacterElementData>().CurrentElementState == ElementState.Sun){
            shoot_force = shoot_force * sunPowerIncrease;
        }
        else
        {
            shoot_force = shoot_force * shootingForceDefault;
        }

        // set WATER POWER
        var softBodyReferences= this.gameObject.GetComponent<CharacterSoftBody>().referencePoints;
        float newDrag;
        if(this.gameObject.GetComponent<CharacterElementData>().CurrentElementState == ElementState.Water)
        {
            newDrag = waterPowerIncrease;
        }
        else
        {
            newDrag = dragDefault;
        }

        if (this.gameObject.GetComponent<Rigidbody2D>().drag != newDrag)
        {
            this.gameObject.GetComponent<Rigidbody2D>().drag = newDrag;
            foreach (var softBodyReference in softBodyReferences)
            {
                softBodyReference.GetComponent<Rigidbody2D>().drag = newDrag;
            }
        }

        // get rotation to send character
        Vector3 diff = this.gameObject.transform.Find("ShootCursor").gameObject.transform.position - this.gameObject.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // return target cursor to character
        this.gameObject.transform.Find("ShootCursor").gameObject.transform.position = this.gameObject.transform.position;

        // apply force and rotation to character
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(shoot_force, ForceMode2D.Impulse);

        // remove the shooting line and cursor
        this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<LineRenderer>().enabled = false;
        this.gameObject.transform.Find("ShootCursor").gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // reduce the stroke counter by 1
        StrokeCounter.ReduceStrokes();


    }
}
