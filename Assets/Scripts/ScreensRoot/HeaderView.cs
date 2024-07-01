using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreensRoot
{
    public class HeaderView : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        [SerializeField] private TMP_Text title;

        public event Action<ScreenName> OnBackClick;

        public void SetView(ScreenConfiguration screenConfiguration)
        {
            if (screenConfiguration.backToScreen != ScreenName.None)
                backButton.onClick.AddListener(() => OnBackClick?.Invoke(screenConfiguration.backToScreen));
            
            title.text = screenConfiguration.headerTitle;
        }
    }
}