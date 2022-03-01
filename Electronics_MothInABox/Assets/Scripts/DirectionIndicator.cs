//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 01-03-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DirectionIndicator : MonoBehaviour
{
    public PlayerCharacter anchor { set; get; }

    private const float MaxDistFromAnchor = 1;

    private Vector3 pos;

    private void Update()
    {
        if (anchor == null)
            return;

        ClampToAnchor(anchor.pos);
    }

    //> This function is called by LightSources to modify the position of this DirectionIndicator
    public void MoveTo(Vector3 lightPosition, float _attractionForce)
    {
        //Rigidbody rigidbody = GetComponent<Rigidbody>();
        //Debug.Log($"{name} will be moved to {_direction * _attractionForce}", this);  //< This log message severely rounds the result to a point where it's not representable anymore.
        //Debug.Log($"X: {_direction.x * _attractionForce * Time.deltaTime}", this);

        //rigidbody.AddForce(_direction * _attractionForce * Time.deltaTime, ForceMode.VelocityChange);
        Vector3 relativeLightPosition = lightPosition - pos;
        relativeLightPosition.y = 0;
        pos += relativeLightPosition * _attractionForce * Time.deltaTime;

        //> Write new position to pos
        //pos = this.transform.position;
    }

    //> This function is called by the PlayerCharacter on start
    public void SetAnchor(PlayerCharacter _anchor)
    {
        this.anchor = _anchor;
        Debug.Log($"\"{name}\" is now anchored to \"{anchor.name}\".", this);

        ResetToAnchor();
    }

    public void ResetToAnchor()
    {
        pos = anchor.pos;
        this.transform.position = pos;
        //Debug.Log($"\"{name}\" is reset to {anchor.pos}.", this);
    }

    private void ClampToAnchor(Vector3 anchorPos)
    {
        //> Locks this object to a perimeter around the anchor object
        pos.x = Mathf.Clamp(pos.x, anchorPos.x - MaxDistFromAnchor, anchorPos.x + MaxDistFromAnchor);
        pos.y = Mathf.Clamp(pos.y, anchorPos.y - MaxDistFromAnchor, anchorPos.y + MaxDistFromAnchor);
        pos.z = Mathf.Clamp(pos.z, anchorPos.z - MaxDistFromAnchor, anchorPos.z + MaxDistFromAnchor);

        //> Apply all changes on pos to the actual transform of this GameObject
        this.transform.position = pos;
    }
}
