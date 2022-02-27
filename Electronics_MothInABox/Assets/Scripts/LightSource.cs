//----------------------------------------------------------------------------------------------
// University:   Darmstadt University of Applied Sciences, Expanded Realities
// Course:       Introduction to Electronics and Physical Interfaces by Prof. Dr. Frank Gabler
// Script by:    Daniel Heilmann (771144)
// Last changed: 26-02-22
//----------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LightSource : MonoBehaviour
{
    [Range(0f, 100f)]
    public float strength = 5;

    [SerializeField] protected float attractionForce = 0;

    protected SphereCollider lightCollider;

    protected PlayerCharacter player;

    //public static bool debugLightArea;

    protected void Start()
    {
        // Make sure that the sphere collider only triggers for the player
        this.gameObject.layer = 7;

        //> Sphere Collider Setup
        lightCollider = this.GetComponent<SphereCollider>();
        lightCollider.isTrigger = true;

        lightCollider.radius = strength;


        //> Establish connection to player, replaced by the "CollideOnlyWithPlayer" collision layer
        //player = FindObjectOfType<PlayerCharacter>();   //< Is actually slower and more performance ineffective than GameObject.Find, but makes sure that it has the required component.
        //if (player == null) Debug.LogWarning($"{this.name} could not find object of type \"PlayerCharacter\".");
    }

    private void Update()
    {
        if (lightCollider.radius != strength)   // This is not necessary, but nice for live debugging
            lightCollider.radius = strength;
    }

    private void PullDirectionIndicator(DirectionIndicator _directionIndicator, Vector3 _dirToPlayer)
    {
        Rigidbody DIrigidbody = _directionIndicator.gameObject.GetComponent<Rigidbody>();
        Debug.Log($"{DIrigidbody.name} will be shoved in {_dirToPlayer * attractionForce * Time.deltaTime}", this);
        //DIrigidbody.AddForce(_dirToPlayer * attractionForce);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (player == null)
            player = other.GetComponent<PlayerCharacter>();     //< Does not have to be cleared after player leaves the trigger, because there is only one player anyways.
        //Debug.Log($"\"{other.gameObject.name}\" entered collider of {this.name}.", this); 
    }

    protected void OnTriggerStay(Collider other)
    {
        Vector3 dirToPlayer = this.transform.position - other.transform.position;   //< This is the directional vector from box to lightsource
                dirToPlayer = new Vector3(dirToPlayer.x, 0f, dirToPlayer.z);        //< Removes the y-axis from the equation, literally -> Attracts only on the xz-plane
        float  distToPlayer = Vector3.Distance(Vector3.zero, dirToPlayer);          //< Calculates the distance solely based on dirToPlayer
        //Debug.Log($"dist from {other.gameObject.name} to {this.name}: {distToPlayer}.", this);

        CalculateAttractionForce(distToPlayer);

        if (attractionForce == 0 || dirToPlayer == Vector3.zero)   //< Guard clause. There is no need to run the MoveTo function if the vector will be (0,0,0) anyways.
            return;

        DirectionIndicator directionIndicator = player.indicator;    //< This is very not nice.
        directionIndicator.MoveTo(dirToPlayer, attractionForce);
    }

    protected void OnTriggerExit(Collider other)
    {
        attractionForce = 0;    //< Reset attractionForce once the player leaves the light area
        //Debug.Log($"\"{other.gameObject.name}\" exited collider of {this.name}.", this);
    }

    private void CalculateAttractionForce(float _distToPlayer)
    {
        //> attractionForce can reach a maximum of {strength} and a minimum of 0
        attractionForce = Mathf.Clamp(Mathf.Clamp(strength, 1, float.MaxValue) / Mathf.Clamp(_distToPlayer, 1, strength), 0, strength);
        //Debug.Log($"{this.name} has an attraction force of {attractionForce}", this);

        //float temp = Mathf.Round(strength * 100f) / 100f;   //< This calculation truncates the float value to 2 decimal points
        //Debug.Log($"{this.name} has a light strength of {temp}", this);
    }
}
