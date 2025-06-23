using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class monster_eagle : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public float speed = 3f;             // Tốc độ bình thường
    public float attackSpeed = 6f;       // Tốc độ khi tấn công
    public bool is_attack = false;

    private Vector3 returnPosition;
    private Coroutine currentRoutine;
    private bool isAttackingNow = false; // Theo dõi trạng thái đang tấn công


    public float attackRadius = 7f;
    private Vector3 originPosition;
    public bool left_right;

    public Animator anim;
    public GameObject collider_;
    public bool die;
    public UnityEvent<Transform> event_spam_items;

    void Start()
    {
        die = false;
        collider_.SetActive(false);
        currentRoutine = StartCoroutine(MoveLoopAB());
        originPosition = transform.position;
    }

    void Update()
    {
        //    if (is_attack && !isAttackingNow)
        //    {
        //        is_attack = false; // Reset trigger
        //        if (currentRoutine != null)
        //        {
        //            StopCoroutine(currentRoutine);
        //        }
        //        currentRoutine = StartCoroutine(AttackSequence());
        //    }
        if (!die)
        {
            Attack_eagle();
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (originPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(originPosition, attackRadius);

          
        }
    }



    public void Attack_eagle()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(originPosition, attackRadius);


       // Debug.Log($"Số collider phát hiện: {hits.Length}");
        Transform foundPlayer = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
          //  Debug.Log($"Phát hiện: {hit.name}, Tag: {hit.tag}");
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
        if (isAttackingNow)
        {
            // Đang tấn công rồi, giữ nguyên hành vi tấn công (không làm gì ở đây)
            return;
        }

        if (foundPlayer != null)
        {
            left_right = foundPlayer.position.x >= transform.position.x;
            if (left_right)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }



        if (foundPlayer != null && minDistance <= attackRadius)
        {
            is_attack = true;
            if (is_attack && !isAttackingNow)
            {
                is_attack = false; // Reset trigger
                if (currentRoutine != null)
                {
                    StopCoroutine(currentRoutine);
                }
                Vector3 foundPlayer_ = foundPlayer.position;

                currentRoutine = StartCoroutine(AttackSequence(foundPlayer_));
            }
        }
        else
        {
            // Nếu không phát hiện player trong phạm vi hoặc không thỏa điều kiện tấn công
            // Và nếu không đang chạy MoveLoopAB rồi thì chạy lại
            if (currentRoutine == null)
            {
                currentRoutine = StartCoroutine(MoveLoopAB());
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
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            target = (target == pointA.position) ? pointB.position : pointA.position;
            yield return null;
        }
    }

    IEnumerator AttackSequence(Vector3 pointC)
    {
        isAttackingNow = true;
        returnPosition = transform.position;

        anim.SetBool("attack", true);
        collider_.SetActive(true);
        // Di chuyển nhanh đến điểm C
        yield return StartCoroutine(MoveToTarget(pointC, attackSpeed));
        collider_.SetActive(false);
        anim.SetBool("attack", false);

        // Quay lại vị trí ban đầu với tốc độ thường
        yield return StartCoroutine(MoveToTarget(returnPosition, attackSpeed / 4));
        currentRoutine = StartCoroutine(MoveLoopAB());
        yield return new WaitForSeconds(1f);
        isAttackingNow = false;

    }

    IEnumerator MoveToTarget(Vector3 target, float moveSpeed)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void on_die()
    {


        event_spam_items.Invoke(transform);
        die = true;
        anim.SetBool("die", true);
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

