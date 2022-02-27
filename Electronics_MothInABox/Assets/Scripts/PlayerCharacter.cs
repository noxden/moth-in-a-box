//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : EntityClass
{
    public DirectionIndicator directionIndicator { private set; get; }
    private Vector3 pos;

    private void Start()
    {
        pos = this.transform.position;
    }

    /*
    const float MOVE_INTERVAL = 0.5f;

    private List<float> lightLevels;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move(MOVE_INTERVAL));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // quick workaround
        if (Input.GetAxis("Horizontal") > 0)
        {
            movement.x = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            movement.x = -1;
        }
        else
        {
            movement.x = 0;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            movement.z = 1;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            movement.z = -1;
        }
        else
        {
            movement.z = 0;
        }

        Debug.Log(movement);

        movement *= moveStepSize;   // apply movement speed to movement input values
    }

    

    private IEnumerator Move(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            transform.Translate(movement.x, 0, movement.z);  // modify this gameobject's position based on current movement vector
        }
    }
    */
    private void Update()
    {
        this.transform.position = pos;
    }

    public Vector3 MoveTowards(Vector3 newPos)
    {
        Vector3 moveDir = newPos - this.transform.position;
        //Debug.Log($"moveDir is {moveDir}.");
        //pos = Vector3.Lerp(pos, newPos, 1f);
        pos = newPos;
        return pos;
    }

    public void SetDirectionIndicator(DirectionIndicator _directionIndicator)
    {
        directionIndicator = _directionIndicator;
    }
}
