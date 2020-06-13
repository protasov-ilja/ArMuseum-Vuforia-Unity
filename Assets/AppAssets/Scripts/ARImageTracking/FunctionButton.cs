using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FunctionButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _unactiveColor;
    [SerializeField] private TMP_Text _buttonText;

    public bool IsActive { get; private set; }

    void Start()
    {
        _button.onClick.AddListener( ActivateButton );
        _buttonText.color = _unactiveColor;
        IsActive = false;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener( ActivateButton );
    }

    public void ActivateButton()
    {
        IsActive = !IsActive;
        _buttonText.color = IsActive ? _activeColor : _unactiveColor;
    }
}
