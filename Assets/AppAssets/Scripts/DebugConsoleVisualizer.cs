using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ARMuseum
{
    public class DebugConsoleVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform _container;

        [SerializeField] private TextMeshProUGUI _textPrefab;

        private List<TextMeshProUGUI> _objs = new List<TextMeshProUGUI>();

        private int _counter = 0;
        private int _maxCount = 4; 
        
        
        public void CreateMessage(string message)
        {
            if (_objs.Count != _maxCount)
            {
                var t = Instantiate(_textPrefab, _container);
                t.text = message;
            
                _objs.Add(t);
            }
            else
            {
                _objs[_counter].text = message;
                _counter++;
                if (_counter > _maxCount)
                {
                    _counter = 0;
                }
            }
        }
    }
}