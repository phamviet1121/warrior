using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class monsters_control : MonoBehaviour
{
    public bool is_MonsterAttack;
    public float time;
    public float times;
    public GameObject collider_;
    public bool left_rihgt;
    public float mover;
    public Animator anim;


    // Bán kính phát hiện để bắt đầu tấn công
    public float attackRadius = 7f;

    // Bán kính ngoài cùng để dừng tấn công và quay lại vị trí ban đầu
    public float disengageRadius = 7f;


    public Transform initialPosition;

    public bool die;
    public float value_die;

    private Transform player;

    public UnityEvent enent_attack;
    void Start()
    {
        die = false;
        collider_.SetActive(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {


        if (!die)
        {
            enent_attack.Invoke();
        }

        //on_monster_attack();
    }
    private void OnDrawGizmosSelected()
    {
        if (initialPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(initialPosition.position, disengageRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(initialPosition.position, attackRadius);
        }
    }

    public void on_slime_attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(initialPosition.position, disengageRadius);


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
            player = foundPlayer;

            // Nếu player nằm trong bán kính tấn công
            if (minDistance <= attackRadius && !is_MonsterAttack)
            {
                // Gán hướng trái/phải dựa vào vị trí player so với quái
                left_rihgt = player.position.x >= transform.position.x;

                // Quay mặt quái về phía player
                //Vector3 dir = (player.position - transform.position).normalized;
                //transform.forward = new Vector3(dir.x, 0, dir.z);
                if (left_rihgt)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                Debug.Log("tan cong player");
                // Gọi hàm tấn công nếu chưa tấn công
                on_monster_attack();
            }
            // Nếu player nằm ngoài bán kính dừng tấn công
            else if (minDistance > disengageRadius)
            {
                if (is_MonsterAttack == false)
                {
                    Debug.Log(" player nam ben ngoai");


                    float distance = Vector3.Distance(transform.position, initialPosition.position);
                    if (distance >= 3)
                    {
                        left_rihgt = initialPosition.position.x >= transform.position.x;
                        if (left_rihgt)
                        {
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        on_monster_attack();
                    }
                    else
                    {
                        left_rihgt = Random.Range(0, 2) == 0 ? true : false;
                        if (left_rihgt)
                        {
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        on_monster_attack();
                    }

                }

                // Disengage();
            }
        }
        else
        {


            if (is_MonsterAttack == false)
            {
                Debug.Log("ko tim thay player");

                float distance = Vector3.Distance(transform.position, initialPosition.position);
                if (distance >= 3)
                {
                    left_rihgt = initialPosition.position.x >= transform.position.x;
                    if (left_rihgt)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    on_monster_attack();
                }
                else
                {
                    left_rihgt = Random.Range(0, 2) == 0 ? true : false;
                    if (left_rihgt)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    on_monster_attack();
                }
            }
        }

    }


    public void on_monster_attack()
    {
        if (is_MonsterAttack == false)
        {
            StartCoroutine(is_Attack(times));
        }

    }

    private IEnumerator is_Attack(float times)
    {
        //trang thai dang tan cong 
        is_MonsterAttack = true;
        //anim chạy
        anim.SetTrigger("attack");
        //bât collider để gây dame
        collider_.SetActive(true);
        // di chuyển 
        StartCoroutine(Monster_mover(left_rihgt, time, mover));

        yield return new WaitForSeconds(times);
        is_MonsterAttack = false;
        collider_.SetActive(false);
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
        StartCoroutine(is_die());
    }

    private IEnumerator is_die()
    {

        yield return new WaitForSeconds(value_die);
        Destroy(gameObject);
    }

}
