using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HeavyAnimation : WeaponsAnimation
{
    [SerializeField] private Transform playerTransfrom = null;

    public override void ShootAnimation()
    {
        float position = transform.localPosition.z;
        float targetPosition = position + 0.1f;
        Vector3 originalPosition = transform.localPosition;

        DOTween.To(() => position, x => position = x, targetPosition, .1f)
            .OnUpdate(() => transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, position));


        DOTween.To(() => targetPosition, x => targetPosition = x, position, .1f)
            .OnUpdate(() => transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, targetPosition));
    }
}
