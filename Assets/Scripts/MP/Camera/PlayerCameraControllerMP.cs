using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerCameraControllerMP : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField] private Vector2 cameraVelocity = new Vector2(4f, 0.25f);
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private Transform weaponTransform = null;
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private Controls controls;
    private Controls Controls
    {
        get
        {
            if (controls != null)
                return controls;

            return controls = new Controls();
        }
    }
    private CinemachineTransposer transposer;

    public override void OnStartAuthority()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);

        enabled = true;

        Controls.Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();

    private void Look(Vector2 lookAxis)
    {
        float followOffset = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * cameraVelocity.y * Time.deltaTime),
            maxFollowOffset.x,
            maxFollowOffset.y);

        transposer.m_FollowOffset.y = followOffset;

        //float weaponRotation = Mathf.Clamp(
        //    weaponTransform.rotation.x - (lookAxis.y * cameraVelocity.y * Time.deltaTime),
        //    maxFollowOffset.x,
        //    maxFollowOffset.y
        //);

        //weaponTransform.Rotate(weaponTransform.rotation.x - (lookAxis.y * cameraVelocity.y * Time.deltaTime), 0f, 0f);

        //Debug.Log(weaponTransform.rotation);

        playerTransform.Rotate(0f, lookAxis.x * cameraVelocity.x * Time.deltaTime, 0f);
    }
}
