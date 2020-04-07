using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private WeaponManager weaponManager;
    private Gun playerGun;
    private Role playerRole;
    [SerializeField] private PauseMenu PauseMenuUI = null;

    private bool isCrouching = false;
    private bool isRunning = false;
    private bool isShooting = false;
    private bool isUsingMainSkill = false;
    private bool startedShooting = false;
    private Vector2 previousInput = Vector2.zero;

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

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weaponManager = GetComponent<WeaponManager>();
        playerGun = weaponManager.GetActiveWeapon;
        playerRole = GetComponent<Role>();

        weaponManager.OnWeaponChanged += HandleWeaponSwap;

        Controls.Player.Jump.performed += _ => playerMovement.Jump();

        Controls.Player.Crouch.performed += _ => isCrouching = !isCrouching;

        Controls.Player.Movement.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Movement.canceled += _ => ResetMovement();

        Controls.Player.Fire.performed += _ => IsShooting();
        Controls.Player.Fire.canceled += _ => isShooting = false;

        Controls.Player.Running.performed += _ => isRunning = !isRunning;

        Controls.Player.Reload.performed += _ => Reload();

        Controls.Player.WeaponSwap.started += ctx => WeaponSwap();

        Controls.Player.MainSkill.performed += _ => MainSkill();

        Controls.Menu.PauseMenu.performed += _ => HandlePause();
    }

    void Update()
    {
        if (isCrouching)
            playerMovement.Crouch();
        else {
            playerMovement.ResetHeight();
            playerMovement.ResetSpeedMultiplier();
        }

        if (isShooting)
        {
            if (playerGun.GetFireMode == Gun.FireModes.Single)
                isShooting = false;

            playerGun.ManageShoot(startedShooting);
            startedShooting = false;
        }
    }

    private void FixedUpdate()
    {
        playerMovement.Move(previousInput);
        playerMovement.IsRunning(isRunning);
    }

    private void SetMovement(Vector2 movement) => previousInput = movement;
    private void ResetMovement() => previousInput = Vector2.zero;

    private void IsShooting()
    {
        isShooting = !isShooting;

        if (isUsingMainSkill && isShooting)
        {
            playerRole.MainSkill.UseSkill();
            playerGun.gameObject.SetActive(true);
            isUsingMainSkill = false;
            return;
        }

        if (isShooting)
            startedShooting = true;
    }

    private void Reload() => playerGun.Reload();

    private void WeaponSwap() => weaponManager.StartWeaponSwap();

    private void MainSkill()
    {
        if (!isUsingMainSkill && playerRole.MainSkill.CanBeActivated())
        {
            isUsingMainSkill = true;
            playerGun.gameObject.SetActive(false);
            playerRole.MainSkill.UseSkill();
        }
    }

    private void HandleWeaponSwap(Gun gun) => playerGun = gun;

    public void HandlePause() => PauseMenuUI.HandlePauseMenu(Controls);
}
