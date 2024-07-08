using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WorldTime
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimeLine : MonoBehaviour
    {
        [SerializeField]
        private WorldTime _WorldTime;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _WorldTime.WorldTimeChanged += OnWorldTimeChanged;
        }

        private void OnDestroy()
        {
            _WorldTime.WorldTimeChanged -= OnWorldTimeChanged;
        }

        private void OnWorldTimeChanged(object sender, TimeSpan e)
        {
            _text.SetText(e.ToString(@"hh\:mm"));
        }

    }


}
