using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioGuidButton : MonoBehaviour
{
    [SerializeField] private GameObject _activeIcon = default;
    [SerializeField] private GameObject _deactiveIcon = default;
    [SerializeField] private Button _button = default;

    private bool _isActive = false;

    private void Start()
    {
        _button.onClick.AddListener( ChangeIcon );
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener( ChangeIcon );
    }

    private void ChangeIcon()
    {
        IsActive = !IsActive;
    }

    public bool IsActive 
    {
        get
        {
            return _isActive;
        }

        set
        {
            if ( value )
            {
                _activeIcon.SetActive( false );
                _deactiveIcon.SetActive( true );
            }
            else
            {
                _activeIcon.SetActive( true );
                _deactiveIcon.SetActive( false );
            }

            _isActive = value;
        }
    }

    private void OnEnable()
    {
        _isActive = false;
        _activeIcon.SetActive( true );
        _deactiveIcon.SetActive( false );
    }
}
