//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DirectionIndicator : MonoBehaviour
{

    [Tooltip("The GameObject that will be moved by this, usually the player character.")]
    public PlayerCharacter player;

    private const float MaxDistFromAnchor = 1;

    private Vector3 pos;

    private void Start()
    {
        player.SetDirectionIndicator(this);
        pos = player.transform.position;
    }

   
    private void Update()
    {
        //MoveByInput();

        //> Locks this object to a perimeter around the anchor object
        pos.x = Mathf.Clamp(pos.x, player.transform.position.x - MaxDistFromAnchor, player.transform.position.x + MaxDistFromAnchor);
        pos.y = Mathf.Clamp(pos.y, player.transform.position.y - MaxDistFromAnchor, player.transform.position.y + MaxDistFromAnchor);
        pos.z = Mathf.Clamp(pos.z, player.transform.position.z - MaxDistFromAnchor, player.transform.position.z + MaxDistFromAnchor);

        //> Applies all changes to pos to the actual transform of this GameObject
        this.transform.position = pos;
    }

    void MoveByInput()
    {
        //> Quick workaround

        //Debug.Log($"MouseButtonDown(0) is {Input.GetMouseButtonDown(0)}");

        float movementSpeed = Time.deltaTime;
        Vector3 movement = Vector3.zero;

        if (Input.GetAxis("Horizontal") > 0)
        {
            movement.x = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            movement.x = -1;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            movement.z = 1;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            movement.z = -1;
        }
        movement *= movementSpeed;

        pos += movement;

        if (Input.GetMouseButtonDown(0))
        {
            PullPlayerToIndicator();
        }
    }

    public void MoveTo(Vector3 _direction, float _attractionForce)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        //Debug.Log($"{name} will be moved to {_direction * _attractionForce}", this);  //< This log message severely rounds the result to a point where it's not representable anymore.
        Debug.Log($"X: {_direction.x * _attractionForce * Time.deltaTime}", this);
        //rigidbody.AddForce(_direction * _attractionForce * Time.deltaTime, ForceMode.VelocityChange);
        
        this.transform.position += _direction * _attractionForce * Time.deltaTime;

        // Write new position to pos
        pos = this.transform.position;
    }

    public void PullPlayerToIndicator()
    {
        pos = player.MoveTowards(pos);    //< Is called too early
        //< Gives own pos to player, which then returns their new (modified) version of the input pos and that one is applied to this classes pos again
    }
}
