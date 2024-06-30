using UnityEngine;

[RequireComponent(typeof(CanvasGroup))] 
public abstract class AbstractPanelMono : MonoBehaviour
{
    public abstract PanelType PanelType { get; }

    private CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void DisableInteraction()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    public void EnableInteraction()
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
}