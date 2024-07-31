    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Hole : MonoBehaviour
    {
        [SerializeField] Player player;
        private void Start()
        {
            player = FindObjectOfType<Player>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Transform parentTransform = collision.transform.parent;
                if (parentTransform != null)
                {
                    // Lấy tọa độ của parent
                    Vector3 parentPosition = parentTransform.position;

                    // Dịch chuyển player đến tọa độ của parent
                    collision.transform.position = parentPosition;

                    // Gây sát thương cho player
                    collision.SendMessage("TakeDamage", 30);

                }
                else
                {
                    Debug.LogWarning("Player does not have a parent transform!");
                }
            }
        }
    }
