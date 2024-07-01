using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScreensRoot
{
    public class HeaderView : MonoBehaviour
    {
        [field:SerializeField] public Button BackButton { get; private set; }
        [field:SerializeField] public TMP_Text Title { get; private set; }

        public event Action OnBackClick;

        private void Awake()
        {
            BackButton.onClick.AddListener(() => OnBackClick?.Invoke());
        }
    }
}