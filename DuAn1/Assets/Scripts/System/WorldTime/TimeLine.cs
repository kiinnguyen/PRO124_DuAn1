using System;
using UnityEngine;
using UnityEngine.UI;

namespace WorldTime
{
    [RequireComponent(typeof(Text))]
    public class TimeLine : MonoBehaviour
    {
        [SerializeField]
        private WorldTime _WorldTime;

        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _WorldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            _WorldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan e)
        {
            _text.text = e.ToString(@"hh\:mm");
        }

    }


}
