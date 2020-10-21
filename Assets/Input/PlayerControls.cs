using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    PlayerInput input;

    public PlayerTestScript player;
    public ObjectGrabber grabber;

    private void Awake()
    {
        input = new PlayerInput();

        //Movement
        input.Gameplay.Move.performed += ctx => player.movement = ctx.ReadValue<Vector2>();
        input.Gameplay.Move.canceled += ctx => player.movement = Vector2.zero;

        //Jump
        input.Gameplay.Jump.performed += ctx => player.Jump();

        //Right axis - Grabbing
        input.Gameplay.Grab.performed += ctx => grabber.rtAxis = ctx.ReadValue<float>();
        input.Gameplay.Grab.canceled += ctx => grabber.rtAxis = 0f;

        //Use object
        input.Gameplay.Use.performed += ctx => grabber.UseObject();

        //ThrowObject
        input.Gameplay.Throw.performed += ctx => grabber.ChargeThrow();
        input.Gameplay.Throw.canceled += ctx => grabber.ThrowObject();

    }


    private void OnEnable()
    {
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }
}
