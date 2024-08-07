using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpiderManager : MonoBehaviour
{
    public GameObject webPrefab;
    public Transform webShootPoint;
    public float shootingRange = 10f;

    private GameObject player;
    private Animator anim;

    public bool onAttack = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= shootingRange && !onAttack)
        {
            StartCoroutine(ShootWebAtTarget());
        }
    }

    IEnumerator ShootWebAtTarget()
    {
        if (webPrefab != null && webShootPoint != null && player != null)
        {
            onAttack = true;
            Vector3 attackToPOS = player.transform.position;
            GameObject webInstance = Instantiate(webPrefab, webShootPoint.position, Quaternion.identity);
            Vector2 direction = (attackToPOS - webShootPoint.position).normalized;
            webInstance.GetComponent<Rigidbody2D>().velocity = direction * 2f;

            yield return new WaitForSeconds(webInstance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length / 2); // Đợi đến khi animation hoàn tất

            webInstance.gameObject.SendMessage("AttackAnim");

            yield return new WaitForSeconds(webInstance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); // Đợi đến khi animation hoàn tất

            onAttack = false;
        }
    }
}
