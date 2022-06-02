using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    Vector2 orgPos, orgSize;

    private void Start()
    {
        orgPos = transform.position;
        orgSize = transform.localScale;
    }

    public void UIPositionX(float targetX)
    {
        Vector2 targetPos = new Vector2(transform.position.x + targetX, transform.position.y);

        transform.DOMove(targetPos, 0.25f).SetUpdate(true);
    }

    public void UIPositionY(float targetY)
    {
        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y + targetY);

        transform.DOMove(targetPos, 0.25f).SetUpdate(true);
    }

    public void UIScale(float targetScale)
    {
        Vector2 target = new Vector2(targetScale, targetScale);

        transform.DOScale(target, 0.25f).SetUpdate(true);
    }

    public void UIRotate(float targetRotation)
    {
        Vector3 target = new Vector3(0, 0, targetRotation);

        transform.DORotate(target, 0.25f).SetUpdate(true);
    }

    public void ResetPosition()
    {
        transform.DOMove(orgPos, 0.25f).SetUpdate(true);
    }

    public void ResetScale()
    {
        transform.DOScale(orgSize, 0.25f).SetUpdate(true);
    }
}
