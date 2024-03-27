using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTagBehavior : MonoBehaviour
{
    [SerializeField]
    private bool _isTagged = false;

    [SerializeField]
    private bool _canBeTagged = true;

    [SerializeField]
    private ParticleSystem _taggedParticles;
    public bool IsTagged { get => _isTagged; }

    public bool Tag()
    {
        //checks if can be tagged
        if (!_canBeTagged) return false;

        //set that we're tagged
        _isTagged = true;
        _canBeTagged = false;


        _taggedParticles.Play();

        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
            trail.enabled = true;

        return true;
    }

    private void SetCanBeTagged()
    {
        _canBeTagged = true;
    }

    private void Start()
    {
        //get the trail renderer
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail == null) return;

        //if i am tagged, tehn trun trail on, otherwise keep it off
        if (trail != null)
            trail.enabled = true;
        else
            trail.enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if not it, do nothing
        if (!IsTagged) return;
        //Debug.Log("1");

        //get the player tag behavior from what we hit
        PlayerTagBehavior tagBehavior = collision.gameObject.GetComponent<PlayerTagBehavior>();
        //Debug.Log("2");

        //if it didn't have one, return;
        if (tagBehavior == null) return;
        //Debug.Log("3");
        //tag other player
        if(!tagBehavior.Tag())  return;

        //set yourslef as not it
        _isTagged = false;
        _canBeTagged = false;
        //Debug.Log("4");

        //turn off our trail renderer
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }



        //if (TryGetComponent(out TrailRenderer trail))
        //{
        //    Debug.Log("5");

        //    trail.enabled = false;
        //}
        //GetComponent<TrailRenderer>().enabled = false;


        
    }

    private void OnCollisionExit(Collision collision)
    {
        Invoke("SetCanBeTagged", 0.5f);
    }
}
