using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_vultuer : MonoBehaviour
{
    public bool is_attack = true;

    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;

    private Vector3 originPosition;
    private Coroutine moveCoroutine;
    private bool isRunningAttackLoop = false;
    private bool left_right;


    //public Transform initialPosition;
    public float attackRadius = 7f;
    // private Transform player;
    public bool die;
    public Animator anim;


    void Start()
    {
        die = false;
        originPosition = transform.position;
        moveCoroutine = StartCoroutine(IdleRoutine());
    }

    void Update()
    {
        if (!die)
        {
            controller();
        }

    }


    public void controller()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(originPosition, attackRadius);


        Debug.Log($"Số collider phát hiện: {hits.Length}");
        Transform foundPlayer = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            Debug.Log($"Phát hiện: {hit.name}, Tag: {hit.tag}");
            // Nếu đối tượng có tag "Player"
            if (hit.CompareTag("Player"))
            {

                float distance = Vector3.Distance(transform.position, hit.transform.position);

                // Ưu tiên player gần nhất nếu có nhiều player
                if (distance < minDistance)
                {
                    foundPlayer = hit.transform;
                    minDistance = distance;
                }
            }
        }

        if (foundPlayer != null)
        {
            if (foundPlayer != null && minDistance <= attackRadius)
            {
                // player = foundPlayer;

                if (!isRunningAttackLoop && is_attack)
                {
                    anim.SetBool("attack", true);
                    if (moveCoroutine != null) StopCoroutine(moveCoroutine);
                    moveCoroutine = StartCoroutine(MoveLoopAB());
                    isRunningAttackLoop = true;
                }
            }
            else
            {
                if (isRunningAttackLoop)
                {
                    is_attack = false;
                    if (moveCoroutine != null) StopCoroutine(moveCoroutine);
                    moveCoroutine = StartCoroutine(MoveToOrigin());
                    isRunningAttackLoop = false;
                }
            }
        }
        else
        {
            if (isRunningAttackLoop)
            {
                is_attack = false;
                if (moveCoroutine != null) StopCoroutine(moveCoroutine);
                moveCoroutine = StartCoroutine(MoveToOrigin());
                isRunningAttackLoop = false;
            }
        }
    }

    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;
        while (true)
        {
            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                SetFacingDirection(target);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            // Đổi hướng
            target = (target == pointA.position) ? pointB.position : pointA.position;

            yield return null;
        }
    }

    IEnumerator MoveToOrigin()
    {
        SetFacingDirection(originPosition);
        while (Vector3.Distance(transform.position, originPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originPosition, speed * Time.deltaTime);
            yield return null;
        }

        // Giữ tại chỗ
        moveCoroutine = StartCoroutine(IdleRoutine());
        anim.SetBool("attack", false);
        yield return new WaitForSeconds(1f);
        is_attack = true;

        //StartCoroutine(ResetAttackAfterDelay(2f));
    }

    IEnumerator IdleRoutine()
    {
        while (true) yield return null;
    }

    void SetFacingDirection(Vector3 target)
    {
        if (target.x > transform.position.x)
        {
            left_right = true; // phải
            transform.rotation = Quaternion.Euler(0, 0, 0); // quay sang phải
        }
        else
        {
            left_right = false; // trái
            transform.rotation = Quaternion.Euler(0, 180, 0); // quay sang trái (lật trục X)
        }
    }

    public void on_die()
    {
        die = true;
        //  anim.SetBool("die", true);
        StartCoroutine(is_die());
    }

    private IEnumerator is_die()
    {

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
