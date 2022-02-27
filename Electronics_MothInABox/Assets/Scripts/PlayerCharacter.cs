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
    [Tooltip("The GameObject that will indicate which direction this GameObject will move in.")]
    public DirectionIndicator indicator;

    [SerializeField]
    private const float MOVE_INTERVAL = 1;

    private Vector3 pos;

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

        Vector3 dirPos = this.indicator.transform.position;

        pos.x = Mathf.Round(dirPos.x);
        pos.z = Mathf.Round(dirPos.z);
        
        //> Apply all changes on pos to the actual transform of this GameObject
        this.transform.position = pos;
        //this.transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Round(dirPos.x), transform.position.y, Mathf.Round(dirPos.z)), float.MaxValue);

        indicator.ResetToAnchor();
    }
}
