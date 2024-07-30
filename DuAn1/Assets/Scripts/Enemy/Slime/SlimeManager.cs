using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SlimeManager : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] float health;

    public bool isDead;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private SlimeMovement slimeMovement;
    private NavMeshAgent agent;
    private Rigidbody2D rb;

    [Header("Items when drop")]
    [SerializeField]
    List<GameObject> listObject;
    [SerializeField] private float percentToDropItem;


    private float knockbackForce = 10f;
    private float knockbackDuration = 0.2f;

    void Start()
    {
        health = 100;

        isDead = false;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        slimeMovement = GetComponent<SlimeMovement>();
    }
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
            StartCoroutine(DestroyAfterDeathAnimation());
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
        agent.enabled = false;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 1f);

        if (randomNumber <= percentToDropItem)
        {
            if (listObject.Count > 0)
            {
                GameObject newObject = listObject[UnityEngine.Random.Range(0, listObject.Count)];

                Instantiate(newObject, transform.position, Quaternion.identity);
            }
        }
    }
}
