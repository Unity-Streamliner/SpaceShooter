using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() 
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void UpdatePosition() 
    {
        float horizontalThrow = movement.ReadValue<Vector2>().x;
        float verticalThrow = movement.ReadValue<Vector2>().y;

        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, -xRange, xRange);

        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void UpdateRotation() 
    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
    }
}
