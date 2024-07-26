using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        public event EventHandler<TimeSpan> WorldTimeChanged;

        [SerializeField]
        private float _dayLength = 1440f;

        [SerializeField]
        RawImage rawImage;
        [SerializeField]
        Texture _sunSprite;
        [SerializeField]
        Texture _moonSprite;

        private TimeSpan _currentTime;

        private float _minuteLength => _dayLength / WorldTimeConstants.MinutesInDay;

        private void Start()
        {
            _currentTime = TimeSpan.FromHours(7);
            StartCoroutine(AddMinute());
        }

        private IEnumerator AddMinute()
        {
            UpdateSprite();
            // nếu thời gian thuộc buổi sáng thì thay đổi ảnh bằng đường dẫn tên file sun.png, ngược lại moon.png
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChanged?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength);

            StartCoroutine(AddMinute());
        }

        private void UpdateSprite()
        {
            if (_currentTime.Hours >= 6 && _currentTime.Hours < 18)
            {
                rawImage.texture = _sunSprite;
            }
            else
            {
                rawImage.texture = _moonSprite;
            }
        }
    }

}