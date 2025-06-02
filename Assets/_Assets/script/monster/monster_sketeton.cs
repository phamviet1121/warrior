using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_sketeton : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    private Coroutine currentRoutine;
    public bool left_right;
    public Animator anim;

    public float attackRadius = 3f;

    private bool isAttacking = false;




    void Start()
    {
        anim.SetBool("run", true);
        currentRoutine = StartCoroutine(MoveLoopAB());
    }


    private void Update()
    {
        attack();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public void attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius);


       // Debug.Log($"Số collider phát hiện: {hits.Length}");
        Transform foundPlayer = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
           // Debug.Log($"Phát hiện: {hit.name}, Tag: {hit.tag}");
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

        if (foundPlayer != null && !isAttacking)
        {
            anim.SetBool("run", false);
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = StartCoroutine(Attack_sketeton());
            isAttacking = true;
        }
        else if (foundPlayer == null && isAttacking)
        {
            anim.SetBool("run", true);
            Debug.Log("Không thấy player -> quay lại tuần tra");
            if (currentRoutine != null) StopCoroutine(currentRoutine);
            currentRoutine = StartCoroutine(MoveLoopAB());
            isAttacking = false;
        }





    }


    IEnumerator Attack_sketeton()
    {
        anim.SetTrigger("attack");
        Debug.Log("Phát hiện player -> tấn công");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }


    // Update is called once per frame

    //IEnumerator MoveLoopAB()
    //{
    //    Vector3 target = pointA.position;


    //    while (true)
    //    {
    //        left_right = target.x >= transform.position.x;

    //        if (!left_right)
    //        {
    //            transform.rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //        else
    //        {
    //            transform.rotation = Quaternion.Euler(0, 0, 0);
    //        }

    //        while (Vector3.Distance(transform.position, target) > 0.1f)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    //            yield return null;
    //        }

    //        target = (target == pointA.position) ? pointB.position : pointA.position;

    //        yield return null;
    //    }
    //}

    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;

        while (true)
        {
            // Nếu đang tấn công thì không di chuyển, chờ tới khi kết thúc tấn công
            while (isAttacking)
            {
                yield return null;
            }

            left_right = target.x >= transform.position.x;

            if (!left_right)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                // Nếu trong lúc di chuyển mà bắt đầu tấn công thì dừng di chuyển luôn
                if (isAttacking)
                {
                    break;
                }

                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            // Nếu đang tấn công thì không đổi target, chờ tới khi kết thúc
            if (isAttacking)
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



        // die = true;
        //anim.SetBool("die", true);
        // StartCoroutine(is_die());
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
