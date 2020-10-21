using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulserTest : MonoBehaviour
{
    public static PulserTest instance;
    public TextureRenderer groundTest;


    [Range(-24,24)]
    public float pulseSpeed;

    [FMODUnity.EventRef]
    public string pulserEvent = "";

    public FMOD.Studio.EventInstance pulser;


    public PlantControl plantController;
    public BerryControl berryController;


    private void Awake()
    {
        instance = this;

        pulser = FMODUnity.RuntimeManager.CreateInstance(pulserEvent);
        pulser.start();
    }

    private void Update()
    {
        pulser.setParameterByName("TempoShift", pulseSpeed);
    }

    public void PulseOn(string markerName)
    {
        groundTest.RenderTexture();

        plantController.Grow();
        berryController.RotPulse();
        //Debug.Log(markerName);
    }
}
