using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class PlayerTestScript : MonoBehaviour
{
    public LayerMask collisionMask;
    public AnimationCurve jumpCurve;
    public ArmAnimation armAnimation;

    public Transform cam;
    public Vector2 movement;

    public float speed = 6;
    public float turnSmoothTime = 0.1f;
    
    CharacterController controller;

    float turnSmoothVel;
    float _currentSpeed;
    Vector3 _moveDir;
    float _jumpTime = 0;
    bool _jumping;

    float _gravity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (_jumping)
        {
            //Kind of a weird way to do jumps
            //controller.Move(Vector3.up * jumpCurve.Evaluate(_jumpTime) * Time.deltaTime);
            _jumpTime += Time.deltaTime;
            if (_jumpTime >= jumpCurve[jumpCurve.length - 1].time)
            {
                _jumping = false;
                _jumpTime = 0;
            }
        }

        Vector3 direction = new Vector3(movement.x, 0, movement.y);
        

        if (direction.magnitude > 0)
        {

            //Player Rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float targetAngleSmoothed = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, targetAngleSmoothed, 0);

            _moveDir = Quaternion.Euler(0f, targetAngle, 0) * Vector3.forward;

            armAnimation.moving = true;
        }
        else
        {
            armAnimation.moving = false;
        }

        if (!_grounded() && !_jumping)
        {
            _gravity -= 20f * Time.deltaTime;
        }
        else
        {
            _gravity = 0;
        }

        _moveDir = _moveDir * direction.magnitude * speed * Time.deltaTime + Vector3.up * _gravity * Time.deltaTime;

        if (_jumping)
            _moveDir = _moveDir + Vector3.up * jumpCurve.Evaluate(_jumpTime) * Time.deltaTime;

        controller.Move(_moveDir);

    }

    float VelocityY()
    {
        float velY = 0;

        return velY;
    }

    public void Jump()
    {
        if (_grounded())
        {
            Debug.Log("jump");
            _jumping = true;
        }
    }

    bool _grounded()
    {
        float sphereRadius = controller.height/2;
        float maxDistance = 0.1f;

        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        return Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, collisionMask, QueryTriggerInteraction.UseGlobal);
    }
}
