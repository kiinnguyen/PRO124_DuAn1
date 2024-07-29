using System.Collections;
using UnityEngine;

public class BossGoblinSkills : MonoBehaviour
{
    Animator anim;
    BossGoblinMovement movement;
    bool isPerformingSkill = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<BossGoblinMovement>();
    }

    public void PerformMeleeAttack()
    {
        if (isPerformingSkill) return;
        StartCoroutine(MeleeAttack());
    }

    IEnumerator MeleeAttack()
    {
        isPerformingSkill = true;
        movement.SetAttacking(true);

        // Xác định hướng của player
        float x = transform.position.x - movement.transform.position.x;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        isPerformingSkill = false;
        movement.SetAttacking(false);
    }

    public void PerformSkill1()
    {
        if (isPerformingSkill) return;
        StartCoroutine(Skill1());
    }

    IEnumerator Skill1()
    {
        isPerformingSkill = true;
        movement.SetUsingSkill(true);

        // Chạy animation skill 1
        anim.SetTrigger("Skill1");

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // Lấy vị trí của player
        Vector3 targetPosition = movement.transform.position;

        // Lướt đến vị trí của player trong 1 giây
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (elapsedTime < 1f)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Kiểm tra va chạm với player
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Player dính x2 sát thương
            // Gọi hàm xử lý sát thương tại đây
        }

        isPerformingSkill = false;
        movement.SetUsingSkill(false);
    }
}
