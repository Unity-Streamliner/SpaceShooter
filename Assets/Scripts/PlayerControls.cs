using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{   
    [Header("Input Controls")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fireAction;

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")][SerializeField] float controlSpeed = 30f;
    [Tooltip("How far player moves horizontally")][SerializeField] float xRange = 6f;
    [Tooltip("How far player moves vertically")][SerializeField] float yRange = 4f;

    [Header("Laser gun array")]
    [Tooltip("Add all player laser here")]
    [SerializeField] ParticleSystem[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
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
        if (fireAction.ReadValue<float>() > 0.5)
        {
            SetLasersActive(true);
        } else {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive) 
    {
        foreach (var laser in lasers)
        {
            var emissionModule = laser.emission;
            emissionModule.enabled = isActive;
        }
    }
}
