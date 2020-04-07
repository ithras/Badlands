using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    [SerializeField] private float MouseSensitivity = 100f;
    [SerializeField] private float smoothing = 0f;

    [SerializeField] private Transform playerTransform = null;
    private Vector2 smoothedVelocity;
    private Vector2 currentLookingPos;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(!PauseMenu.GameIsPaused)
            RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 inputValues = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        inputValues = Vector2.Scale(inputValues, new Vector2(MouseSensitivity * smoothing, MouseSensitivity*smoothing));

        smoothedVelocity.x = Mathf.Lerp(smoothedVelocity.x, inputValues.x, 1f / smoothing);
        smoothedVelocity.y = Mathf.Lerp(smoothedVelocity.y, inputValues.y, 1f / smoothing);

        currentLookingPos.x += smoothedVelocity.x;
        currentLookingPos.y += smoothedVelocity.y;

        //GIRA LA CAMARA VERTICALMENTE. ES NEGATIVO PARA PODER INVERTIR EL EJE DEL MOUSE
        transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);

        //GIRA EL PERSONAJE HORIZONTALMENTE
        playerTransform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, Vector3.up);     

    
    }
}
