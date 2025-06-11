using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class monster_flying : MonoBehaviour
{
    // public bool is_MonsterAttack;
    //  public bool is_disengageRadius;
    public bool die;
    public bool left_right;
    // private bool isPatrolling = false;
    public Transform pointA;
    public Transform pointB;

    // public float time;
    // public float times;

    public float value_die;

    //public float mover;
    public float speed;

    // Bán kính phát hiện để bắt đầu tấn công
    //  public float attackRadius = 7f;

    // Bán kính ngoài cùng để dừng tấn công và quay lại vị trí ban đầu
    // public float disengageRadius = 10f;

    // private Transform player;

    public GameObject collider_;
    public GameObject collider_1;
    public Animator anim;
    // public Transform initialPosition;

    //public UnityEvent enent_attack;
    // private Coroutine moveCoroutine = null;


    [SerializeField] float detectionRadius = 7f; // Phạm vi phát hiện player
    [SerializeField] float attackRadius = 1f;     // Phạm vi tấn công

    public int attack1Count;
    void Start()
    {

        die = false;
        collider_.SetActive(false);
        collider_1.SetActive(false);
        StartCoroutine(MoveLoopAB());
        attack1Count = 0;
    }


    // Update is called once per frame
    //void FixedUpdate()
    //{


    //    if (!die)
    //    {

    //    }

    //    //on_monster_attack();
    //}

    public void on_collider_()
    {
        collider_.SetActive(true);
    }
    public void off_collider_()
    {
        collider_.SetActive(false);
    }
    public void on_collider_1()
    {
        collider_1.SetActive(true);
    }
    public void off_collider_1()
    {
        collider_1.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius + 2f);
    }



    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;

        while (true)
        {
            if (die)
            {
                yield break; // Dừng coroutine hoàn toàn
            }
            // Tìm player trong vùng detection bằng OverlapCircleAll
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
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
                // Player trong vùng phát hiện, đuổi theo
                while (foundPlayer != null && Vector3.Distance(transform.position, foundPlayer.position) < detectionRadius)
                {
                    if (die)
                    {
                        yield break; // Dừng coroutine hoàn toàn
                    }
                    left_right = foundPlayer.position.x >= transform.position.x;
                    transform.rotation = left_right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    float distanceToPlayer = Vector3.Distance(transform.position, foundPlayer.position);

                    if (distanceToPlayer <= attackRadius + 2)
                    {
                        // Attack(0): khi player trong vùng giữa attackRadius và attackRadius + 2
                        if (distanceToPlayer > attackRadius && attack1Count >= 3)
                        {
                            attack1Count = 0;
                            Attack(0);
                            yield return new WaitForSeconds(2f);
                        }

                        // Attack(1): khi player trong vùng attackRadius trở xuống
                        if (distanceToPlayer <= attackRadius)
                        {
                            Attack(1);
                            attack1Count++;
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            // Di chuyển nếu không tấn công gần
                            transform.position = Vector3.MoveTowards(transform.position, foundPlayer.position, speed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        // Di chuyển nếu quá xa
                        transform.position = Vector3.MoveTowards(transform.position, foundPlayer.position, speed * Time.deltaTime);
                    }



                    // Cập nhật lại player trong vùng (tránh player mất khỏi vùng detection)
                    hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
                    foundPlayer = null;
                    minDistance = float.MaxValue;
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

                    yield return null;
                }

                target = GetClosestPatrolPoint().position; // Quay lại patrol khi player rời đi
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            }
            else
            {

                attack1Count = 0;
                // Patrol bình thường
                left_right = target.x >= transform.position.x;
                transform.rotation = left_right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                while (Vector3.Distance(transform.position, target) > 0.1f)
                {

                    if (die)
                    {
                        yield break; // Dừng coroutine hoàn toàn
                    }
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

                    // Kiểm tra lại xem có player trong detection không
                    hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius - 0.5f);
                    bool playerFound = false;
                    foreach (Collider2D hit in hits)
                    {
                        if (hit.CompareTag("Player"))
                        {
                            playerFound = true;
                            break;
                        }
                    }
                    if (playerFound)
                        break;

                    yield return null;
                }

                if (!hits.Any(hit => hit.CompareTag("Player")))
                {
                    target = (target == pointA.position) ? pointB.position : pointA.position;
                }
            }

            yield return null;
        }
    }
    Transform GetClosestPatrolPoint()
    {
        float distToA = Vector3.Distance(transform.position, pointA.position);
        float distToB = Vector3.Distance(transform.position, pointB.position);
        return (distToA < distToB) ? pointA : pointB;
    }

    void Attack(int randomValue)
    {
        Debug.Log("Monster tấn công!");
        // int randomValue = Random.Range(0, 2);

        if (randomValue == 0)
        {
            StartCoroutine(Monster_mover(left_right, 0.3f, 2f));
            anim.SetTrigger("attack");
        }
        else
        {
            anim.SetTrigger("attack1");
        }
    }

    private IEnumerator Monster_mover(bool left_rihgt, float time, float mover)
    {
        // canSlideAttack = false;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector3(transform.position.x + (left_rihgt ? mover : -mover), transform.position.y);

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
        collider_.SetActive(false);
        //yield return new WaitForSeconds(1f);

    }


    public void on_die()
    {
        die = true;
        anim.SetBool("die", true);
        StopCoroutine(MoveLoopAB());
        StartCoroutine(is_die());
    }

    private IEnumerator is_die()
    {

        yield return new WaitForSeconds(value_die);
        Destroy(gameObject);
    }

}
