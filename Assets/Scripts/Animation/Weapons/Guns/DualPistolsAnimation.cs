using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DualPistolsAnimation : WeaponsAnimation
{
    private bool first = true;
    private bool second = false;
    [SerializeField] private Transform firstPistol = null;
    [SerializeField] private Transform secondPistol = null;
    [SerializeField] private Transform playerTransfrom = null;

    public override void ShootAnimation()
    {
        float rotation = 0f;
        float targetRotation = 10f;

        Transform targetPistol;

        if (first)
            targetPistol = firstPistol;
        else
            targetPistol = secondPistol;

        first = !first;

        var tweener = DOTween.To(() => rotation, x => rotation = x, targetRotation, .5f)
            .OnUpdate(() => targetPistol.eulerAngles = new Vector3(rotation + Camera.main.transform.eulerAngles.x, playerTransfrom.eulerAngles.y, 0f));
    }
}
