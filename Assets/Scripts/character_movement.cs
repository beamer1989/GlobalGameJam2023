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

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private LineRenderer shootingLine;
    [SerializeField] private GameObject shootCursor;

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
            shootCursor.transform.position = new Vector2(mousePos.x, mousePos.y);
            shootingLine.SetPosition(0, transform.position);
            shootingLine.SetPosition(1, mousePos);
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            // set mouse to being held
            isBeingHeld = true;

            // enable visibility of shooting line and cursor
            shootingLine.enabled = true;
            shootCursor.GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    private void OnMouseUp()
    {
        // release mouse held
        isBeingHeld = false;

        // get force to send character
        float x_force = this.gameObject.transform.position.x - shootCursor.transform.position.x;
        float y_force = this.gameObject.transform.position.y - shootCursor.transform.position.y;
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

        if (rigidbody2D.drag != newDrag)
        {
            rigidbody2D.drag = newDrag;
            foreach (var softBodyReference in softBodyReferences)
            {
                softBodyReference.GetComponent<Rigidbody2D>().drag = newDrag;
            }
        }

        // get rotation to send character
        Vector3 diff = shootCursor.transform.position - this.gameObject.transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        // return target cursor to character
        shootCursor.transform.position = this.gameObject.transform.position;

        // apply force and rotation to character
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
        rigidbody2D.AddForce(shoot_force, ForceMode2D.Impulse);

        // remove the shooting line and cursor
        shootingLine.enabled = false;
        shootCursor.GetComponent<SpriteRenderer>().enabled = false;

        // reduce the stroke counter by 1
        StrokeCounter.ReduceStrokes();


    }
}
