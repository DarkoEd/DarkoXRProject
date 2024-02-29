using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;


// inherit corefeatures class
public class DoorFeatures : CoreFeatures
{
    //Door Configuration - pivot information, open door state, max angle, Reverse, Speed
    [Header("Door Configurations")]
    [SerializeField]
    private Transform doorPivot; //controls pivot

    [SerializeField]
    private float maxAngle = 90.0f; //Maybe <90 will be wanted

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float doorSpeed = 1.0f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;



    //Interaction Features for Socket Interactor, Simple Interactor
    [Header("Door Configurations")]
    [SerializeField]
    private XRSocketInteractor socketInteractor;

    private XRSimpleInteractable simpleInteractor;

    private void Start()
    {
        socketInteractor?.selectEntered.AddListener((s) =>
        {
            //OpenDoor();
        });

        socketInteractor?.selectEntered.AddListener((s) =>
        {

            PlayOnEnd();
            //When we're done exiting, we dont want to resue the socket
            socketInteractor.socketActive = featureUsage == FeatureUsage.Once ? false : true;

        });

        simpleInteractor?.selectEntered?.AddListener((s) =>
        {
            OpenDoor();
        });

        //remove this
        OpenDoor();

         
    }

    public void OpenDoor()
    {

        //openDoor ? false : true;
        if (!open)
        {
            PlayOnStart();
            open = true;
            StartCoroutine(ProcessMotion());
        }
    }
    private IEnumerator ProcessMotion() {


        var angle = doorPivot.localEulerAngles.y < 100 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y - 360;
        angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;
        if(angle <= maxAngle)
        {
            doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
        }

        else
        {
            open = false;
            var featureRigidBody = GetComponent<Rigidbody>();
            if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true;
        }

    }
}
