using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTweens : MonoBehaviour
{
    [SerializeField] private float buttonHoverScaleInAmount = 0.9f;
    [SerializeField] private float buttonClickScaleInAmount = 0.8f;

    [SerializeField] private float timeToScaleInHover = 0.15f;
    [SerializeField] private float timeToScaleInClick = 0.15f;

    public void HoverTween()
    {
        LeanTween.scaleX(gameObject, buttonHoverScaleInAmount, timeToScaleInHover).setIgnoreTimeScale(true);
        LeanTween.scaleY(gameObject, buttonHoverScaleInAmount, timeToScaleInHover).setIgnoreTimeScale(true);
        AudioManager.Instance.PlayAudio("ButtonHover");
    }

    public void UnhoverTween()
    {
        LeanTween.scaleX(gameObject, 1, timeToScaleInHover).setIgnoreTimeScale(true);
        LeanTween.scaleY(gameObject, 1, timeToScaleInHover).setIgnoreTimeScale(true);
    }

    public void ClickTween()
    {
        LeanTween.scaleX(gameObject, buttonClickScaleInAmount, timeToScaleInClick).setIgnoreTimeScale(true);
        LeanTween.scaleY(gameObject, buttonClickScaleInAmount, timeToScaleInClick).setIgnoreTimeScale(true);
        AudioManager.Instance.PlayAudio("ButtonClick");
    }

    public void UnclickTween()
    {
        LeanTween.scaleX(gameObject, 1, timeToScaleInClick).setIgnoreTimeScale(true);
        LeanTween.scaleY(gameObject, 1, timeToScaleInClick).setIgnoreTimeScale(true);
    }
}
