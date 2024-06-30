using UnityEngine;
using System.Collections;
using PanelsNavigationModule.Animations;

public class ScreenAnimationController
{
    private readonly AbstractPanelMono _closingScreen;
    private readonly AbstractPanelMono _openingScreen;
    private readonly PanelTransitionDirection _direction;

    public ScreenAnimationController(AbstractPanelMono closingScreen, AbstractPanelMono openingScreen, PanelTransitionDirection direction)
    {
        _closingScreen = closingScreen;
        _openingScreen = openingScreen;
        _direction = direction;
    }

    public void PlayAnimation()
    {
        _closingScreen.StartCoroutine(PlayAnimationCoroutine());
    }

    private IEnumerator PlayAnimationCoroutine()
    {
        DisableInteraction();
        SetInitialPositions(out Vector3 startPos, out Vector3 endPos, out Vector3 closingStartPos, out Vector3 closingEndPos);

        _openingScreen.transform.position = startPos;
        _openingScreen.gameObject.SetActive(true);

        float duration = 0.2f;
        float startTime = Time.time;

        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            AnimatePanels(startPos, endPos, closingStartPos, closingEndPos, t);
            yield return null;
        }

        FinalizeAnimation(endPos, closingEndPos, closingStartPos);
    }

    private void DisableInteraction()
    {
        _closingScreen.DisableInteraction();
        _openingScreen.DisableInteraction();
    }

    private void SetInitialPositions(out Vector3 startPos, out Vector3 endPos, out Vector3 closingStartPos, out Vector3 closingEndPos)
    {
        Vector3 screenWidth = new Vector3(Screen.width, 0, 0);
        startPos = _openingScreen.transform.position;
        endPos = _openingScreen.transform.position;
        closingStartPos = _closingScreen.transform.position;
        closingEndPos = closingStartPos;

        switch (_direction)
        {
            case PanelTransitionDirection.LeftToRight:
                startPos = closingStartPos - screenWidth;
                closingEndPos = closingStartPos + screenWidth;
                break;
            case PanelTransitionDirection.RightToLeft:
                startPos = closingStartPos + screenWidth;
                closingEndPos = closingStartPos - screenWidth;
                break;
        }
    }

    private void AnimatePanels(Vector3 startPos, Vector3 endPos, Vector3 closingStartPos, Vector3 closingEndPos, float t)
    {
        _openingScreen.transform.position = Vector3.Lerp(startPos, endPos, t);
        _closingScreen.transform.position = Vector3.Lerp(closingStartPos, closingEndPos, t);
    }

    private void FinalizeAnimation(Vector3 endPos, Vector3 closingEndPos, Vector3 closingStartPos)
    {
        _openingScreen.transform.position = endPos;
        _closingScreen.transform.position = closingEndPos;
        _openingScreen.EnableInteraction();
        _closingScreen.gameObject.SetActive(false);
        ResetPosition(closingStartPos, endPos);
    }

    private void ResetPosition(Vector3 closingStartPos, Vector3 endPos)
    {
        _closingScreen.transform.position = closingStartPos;
        _openingScreen.transform.position = endPos;
    }
}
