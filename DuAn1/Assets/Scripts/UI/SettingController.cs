﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    public GameObject settingsPanel;

    void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false); // Đảm bảo bảng cài đặt ban đầu bị ẩn
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel != null)
            {
                bool isActive = settingsPanel.activeSelf;
                settingsPanel.SetActive(!isActive); // Đảo ngược trạng thái hiển thị của bảng cài đặt
            }
        }
    }
}