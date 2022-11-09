using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fireAction;
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 6f;
    [SerializeField] float yRange = 4f;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;
    private float _horizontalThrow;
    private float _verticalThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        movement.Enable();
        fireAction.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fireAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
        Firing();
    }

    private void UpdatePosition() 
    {
        _horizontalThrow = movement.ReadValue<Vector2>().x;
        _verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = _horizontalThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

        float yOffset = _verticalThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void UpdateRotation() 
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = _verticalThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = _horizontalThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void Firing()
    {
        if (fireAction.triggered)
        print("dbg: shooting");
    }
}
