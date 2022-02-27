//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 27-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : EntityClass
{
    [Tooltip("The GameObject that will indicate which direction this GameObject will move in.")]
    public DirectionIndicator indicator;
    public Vector3 pos;

    private Vector3 indicatorPos;   //< To make this Vector accessible for the Update function

    private const float MOVE_INTERVAL = 1;

    private void Start()
    {
        pos = this.transform.position;

        if (indicator == null)
        {
            Debug.LogError("DirectionIndicator is not assigned in the Editor, can't set it's anchor.", this);
        }
        else
        {
            indicator.SetAnchor(this);
        }
        StartCoroutine(MoveEvery(MOVE_INTERVAL));
    }

    #region Archived
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
    #endregion

    private void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z)), Time.deltaTime * 3);
    }

    private IEnumerator MoveEvery(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        MoveTowardsIndicator();

        StartCoroutine(MoveEvery(seconds));
    }

    private void MoveTowardsIndicator()
    {
        if (this.indicator == null)    // Guard clause
        {
            Debug.LogError("DirectionIndicator is still null.", this);
            return;
        }

        indicatorPos = this.indicator.transform.position;

        Vector3 indicatorDir = indicatorPos - this.pos;  //< Required to calculate the magnitude of the distance between the box and the directionIndicator
        Vector3 indicatorDirMagnitude = new Vector3(Mathf.Abs(indicatorDir.x), 0, Mathf.Abs(indicatorDir.z));   //< dirPos y is 0 anyways

        #region Archived
        //pos.x = Mathf.Round(dirPos.x);
        //pos.z = Mathf.Round(dirPos.z);
        #endregion

        //> The following switch statement restricts movement to only one axis per iteration
        switch (indicatorDirMagnitude.x > indicatorDirMagnitude.z)
        {
            case true:
                pos.x = Mathf.Round(indicatorPos.x);  //< Is called when magn.x is bigger that magn.z -> Player moves on x-axis
                //Debug.Log($"Moving on x", this);
                break;
            case false:
                pos.z = Mathf.Round(indicatorPos.z);  //< Is called when magn.z is bigger or equal magn.x -> Player moves in z-axis. This causes a bias in favour of movement on z-axis
                //Debug.Log($"Moving on z", this);
                break;
        }

        //> Apply all changes on pos to the actual transform of this GameObject 
        //this.transform.position = pos;    // THIS IS DEPRECATED: Replaced by the "Vector.MoveTowards" in Update

        indicator.ResetToAnchor();
    }
}
