using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SlimeManager : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] float health;
    [SerializeField] Slider healthBar;


    public bool isDead;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private SlimeMovement slimeMovement;
    private NavMeshAgent agent;
    void Start()
    {
        health = 100;
        if (healthBar != null)
        {
            healthBar.value = health;
            isDead = false;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        slimeMovement = GetComponent<SlimeMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private bool isTakingDamage = false;
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        if (isTakingDamage) return;
        isTakingDamage = true;
        health -= damage;
        StartCoroutine(HurtEffect());

        if (health <= 0)
        {
            animator.SetTrigger("Death");
            healthBar.value = 0;
            StartCoroutine(DestroyAfterDeathAnimation());
        }
        else
        {
            //animator.SetTrigger("Hurt");
            healthBar.value = health;
        }
        isTakingDamage = false;
    }

    IEnumerator HurtEffect()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, Mathf.PingPong(elapsed * 2, 1));
            elapsed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = Color.white;
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        //slimeMovement.enabled = false;
        agent.enabled = false;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Drop something");
    }
}
