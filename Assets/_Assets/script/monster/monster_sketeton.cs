using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class monster_sketeton : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    private Coroutine currentRoutine;
    public bool left_right;
    public Animator anim;

    public float attackRadius = 3f;

    public bool isAttacking = false;
    public bool isAttacking_;
    public bool isReturningToPatrol = false;
    public bool die;
    public bool ishurt;
    public GameObject collider_;
    public UnityEvent<Transform> event_spam_items;

    void Start()
    {
        collider_.SetActive(false);
        ishurt = false;
        die = false;
        isAttacking_ = false;
        anim.SetBool("run", true);
        currentRoutine = StartCoroutine(MoveLoopAB());
    }

    private void Update()
    {
        if (!die)
        {
            if (!ishurt)
            {
                attack();
            }

        }


    }

    public void on_hurt()
    {
        if (die ) return; // Đã chết hoặc đang hurt thì bỏ qua

        ishurt = true;
        anim.SetBool("run", false);
        anim.SetTrigger("hurt");
        
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(hurt());
    }

    IEnumerator hurt()
    {
        isAttacking = false;
        isAttacking_ = false;
        isReturningToPatrol = false;

        yield return new WaitForSeconds(1f);

        ishurt = false;
       // anim.SetBool("run", true);

        // Sau khi hurt xong thì tiếp tục tuần tra
       // currentRoutine = StartCoroutine(MoveLoopAB());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        Transform foundPlayer = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    foundPlayer = hit.transform;
                    minDistance = distance;
                }
            }
        }

        if (foundPlayer != null)
        {
            if (!isAttacking)
            {
                RotateToPlayer(foundPlayer);
                isAttacking = true;
                anim.SetBool("run", false);
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(Attack_skeleton());
            }
        }
        else
        {
            if (!isAttacking && !isReturningToPatrol)
            {
                isReturningToPatrol = true;
                if (currentRoutine != null) StopCoroutine(currentRoutine);
                currentRoutine = StartCoroutine(ReturnToPatrolAfterAttack());
            }
        }
    }

    void RotateToPlayer(Transform player)
    {
        left_right = player.position.x >= transform.position.x;
        transform.rotation = left_right
            ? Quaternion.Euler(0, 0, 0)
            : Quaternion.Euler(0, 180, 0);
    }

    public void on_collider_attack()
    {
        collider_.SetActive(true);
    }
    public void off_collider_attack()
    {
        collider_.SetActive(false);
    }

    IEnumerator Attack_skeleton()
    {
        isAttacking_ = true;

        while (true)
        {
            if (ishurt)
                yield break;
            // Xác định lại Player gần nhất để xoay trước khi tấn công
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);
            Transform foundPlayer = null;
            float minDistance = float.MaxValue;

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    float distance = Vector3.Distance(transform.position, hit.transform.position);
                    if (distance < minDistance)
                    {
                        foundPlayer = hit.transform;
                        minDistance = distance;
                    }
                }
            }

            if (foundPlayer == null)
            {
                break;
            }

            // Xoay trước khi đánh
            RotateToPlayer(foundPlayer);

            anim.SetTrigger("attack");
            Debug.Log("Phát hiện player -> tấn công");
            yield return new WaitForSeconds(1f);
        }

        // Không còn player
        isAttacking = false;
        isAttacking_ = false;
        Debug.Log("Không còn player, kết thúc tấn công -> chờ 1s rồi tuần tra");
        yield return new WaitForSeconds(1f);

        isReturningToPatrol = false;
        anim.SetBool("run", true);
        currentRoutine = StartCoroutine(MoveLoopAB());
    }

    IEnumerator ReturnToPatrolAfterAttack()
    {
        yield return new WaitUntil(() => !isAttacking_);
        anim.SetBool("run", true);
        Debug.Log("Không thấy player -> quay lại tuần tra sau khi tấn công xong");
        currentRoutine = StartCoroutine(MoveLoopAB());
    }

    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;

        while (true)
        {
            while (isAttacking || ishurt)
                yield return null;

            left_right = target.x >= transform.position.x;
            transform.rotation = left_right
                ? Quaternion.Euler(0, 0, 0)
                : Quaternion.Euler(0, 180, 0);

            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                if (isAttacking || ishurt)
                    break;

                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            if (isAttacking || ishurt)
            {
                yield return null;
                continue;
            }

            target = (target == pointA.position) ? pointB.position : pointA.position;
            yield return null;
        }
    }

    public void on_die()
    {
        event_spam_items.Invoke(transform);
        die = true;
        anim.SetBool("die", true);
        anim.SetBool("run", false);
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(is_die());
    }

    private IEnumerator is_die()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
