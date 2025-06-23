using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class monster_dragon : MonoBehaviour
{

    public bool die;
    public bool left_right;

    public Transform pointA;
    public Transform pointB;
    public Transform point;

    public float value_die;


    public float speed;


    public GameObject collider_;
    public GameObject collider_1;
    public Animator anim;


    [SerializeField] float detectionRadius = 7f;
    [SerializeField] float attackRadius = 1f;

    public int attack1Count;


    public float phamvi;

    public bool isattack_fire;


    public GameObject fire_obj;

    public UnityEvent<Transform, int > event_spam_items_one;
    public UnityEvent<Transform> event_spam_items;
    void Start()
    {

        die = false;
        collider_.SetActive(false);
        collider_1.SetActive(false);
        StartCoroutine(MoveLoopAB());
        attack1Count = 0;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius + 4f);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(point.position, phamvi);
    }

    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;

        while (true)
        {
            if (die)
            {
                yield break;
            }



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
            //if (foundPlayer != null)
            //{
            //    float phamvi_ = Vector3.Distance(foundPlayer.position, point.position);
            //}


            if (foundPlayer != null && Vector3.Distance(foundPlayer.position, point.position) <= phamvi)
            {
                while (foundPlayer != null && Vector3.Distance(transform.position, foundPlayer.position) < detectionRadius)
                {
                    if (die)
                    {
                        yield break;
                    }

                    left_right = foundPlayer.position.x >= transform.position.x;
                    transform.rotation = left_right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                    float distanceToPlayer = Vector3.Distance(transform.position, foundPlayer.position);

                    if (distanceToPlayer < detectionRadius && distanceToPlayer >= attackRadius + 3 && isattack_fire)
                    {
                        isattack_fire = false;
                        Attack(2);
                        yield return new WaitForSeconds(2f);
                    }
                    else if (distanceToPlayer <= attackRadius + 4f)
                    {
                        if (distanceToPlayer > attackRadius + 2f && attack1Count >= 3)
                        {

                            attack1Count = 0;
                            Attack(0);
                            yield return new WaitForSeconds(2f);
                        }

                        if (distanceToPlayer <= attackRadius)
                        {

                            Attack(1);
                            attack1Count++;
                            yield return new WaitForSeconds(1f);
                        }
                        else
                        {
                            //  Vector3 nextPosition = Vector3.MoveTowards(transform.position, foundPlayer.position, speed * Time.deltaTime);
                            anim.SetBool("run", true);
                            Vector3 nextPosition = new Vector3(Mathf.MoveTowards(transform.position.x, foundPlayer.position.x, speed * Time.deltaTime), transform.position.y, transform.position.z);
                            float distanceFromPoint = Vector3.Distance(point.position, nextPosition);
                            if (distanceFromPoint <= phamvi - 0.2f)
                            {
                                transform.position = nextPosition;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        anim.SetBool("run", true);
                        Vector3 nextPosition = new Vector3(Mathf.MoveTowards(transform.position.x, foundPlayer.position.x, speed * Time.deltaTime), transform.position.y, transform.position.z);
                        // Vector3 nextPosition = Vector3.MoveTowards(transform.position, foundPlayer.position, speed * Time.deltaTime);
                        float distanceFromPoint = Vector3.Distance(point.position, nextPosition);
                        if (distanceFromPoint <= phamvi - 0.2f)
                        {
                            transform.position = nextPosition;
                        }
                        else
                        {
                            break;
                        }
                    }

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

                target = GetClosestPatrolPoint().position;
                // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, target.x, speed * Time.deltaTime), transform.position.y, transform.position.z);
                anim.SetBool("run", true);
            }
            else
            {
                attack1Count = 0;
                left_right = target.x >= transform.position.x;
                transform.rotation = left_right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);

                while (Vector3.Distance(transform.position, target) > 0.1f)
                {
                    if (die)
                    {
                        yield break;
                    }
                    anim.SetBool("run", true);
                    transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, target.x, speed * Time.deltaTime), transform.position.y, transform.position.z);
                    //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

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
        anim.SetBool("run", false);
        if (randomValue == 2)
        {
            anim.SetTrigger("attack");
            StartCoroutine(isAttack_fire(5f));

        }
        else if (randomValue == 0)
        {
            StartCoroutine(Monster_mover(left_right, 0.3f, 5f));
            anim.SetTrigger("strike");
        }
        else
        {
            anim.SetTrigger("flykick");
        }
    }
    public void on_strike()
    {
        collider_.SetActive(true);
    }
    public void off_strike()
    {
        collider_.SetActive(false);
    }
    public void on_flykick()
    {
        collider_1.SetActive(true);
    }
    public void off_flykick()
    {
        collider_1.SetActive(false);
    }

    public void mover_fire()
    {
        // Tạo fire tại vị trí phía trước quái
        Vector3 spawnPos = new Vector3(
            transform.position.x + (left_right ? 1.5f : -1.5f),
            transform.position.y-0.25f,
            transform.position.z
        );

        // Tạo ra fire với góc quay mặc định
        GameObject fire = Instantiate(fire_obj, spawnPos, left_right ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0));

        // Di chuyển fire theo hướng (nếu bạn muốn nó bay ngay khi tạo)
        float moveSpeed = 5f;
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(left_right ? moveSpeed : -moveSpeed, 0);
        }
    }



    IEnumerator isAttack_fire(float time)
    {

        yield return new WaitForSeconds(time);
        isattack_fire = true;

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
        event_spam_items.Invoke(transform);
        event_spam_items_one.Invoke(transform,0);
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
