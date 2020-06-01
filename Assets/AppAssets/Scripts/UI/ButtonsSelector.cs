using System;
using UnityEngine;
using UnityEngine.UI;

namespace ARMuseum.UI
{
    public class ButtonsSelector : MonoBehaviour
    {
        [SerializeField] private SelectedButton[] _buttons = default;

        private void OnEnable()
        {
            var id = 0;
            foreach (var button in _buttons)
            {
                button.Subscribe(this, id);
                id++;
            }
        }
        
        public void DeactivateOthers(int selectedButtonId)
        {
            foreach (var t in _buttons)
            {
                if (t.Id != selectedButtonId)
                {
                    t.DeactivateButton();
                }
            }
        }
    }
}