using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Drawer : CoreFeatures
{
    [Header("Drawer Configuration")]
    [SerializeField]
    private Transform drawerSlide;

    [SerializeField]
    private FeatureDirection featureDirection = FeatureDirection.Forward;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    // Update is called once per frame
    void Start()
    {
        
    }
}
