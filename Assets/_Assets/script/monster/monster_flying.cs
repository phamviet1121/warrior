using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class monster_flying : MonoBehaviour
{
    public bool is_MonsterAttack;
    public bool is_disengageRadius;
    public bool die;
    public bool left_right;
    private bool isPatrolling = false;
    public Transform pointA;
    public Transform pointB;

    public float time;
    public float times;

    public float value_die;

    public float mover;
    public float speed;

    // Bán kính phát hiện để bắt đầu tấn công
    public float attackRadius = 7f;

    // Bán kính ngoài cùng để dừng tấn công và quay lại vị trí ban đầu
    public float disengageRadius = 10f;

    private Transform player;

    public GameObject collider_;
    public GameObject collider_1;
    public Animator anim;
    public Transform initialPosition;

    public UnityEvent enent_attack;
    private Coroutine moveCoroutine = null;

    void Start()
    {

        die = false;
        collider_.SetActive(false);
        collider_1.SetActive(false);
        moveCoroutine = StartCoroutine(MoveLoopAB());
    }


    // Update is called once per frame
    void FixedUpdate()
    {


        if (!die)
        {

        }

        //on_monster_attack();
    }

    // di chuyuen 2 ben   VV
    // khi player trong pham vi thi  di chuyeenr den vi tri va tan cong ,attack cachs 2f,attack1 cachs 0.75f
    //khi player nam ngoai pham vi tan cong nhung nam trong vung tan cong thi van toan cong 
    // neu plaer nam ngoai vung tan cong thi quay lai trang thai di chuyen 2 ben 
    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;

        while (true)
        {

            left_right = target.x >= transform.position.x;
            transform.rotation = left_right
                ? Quaternion.Euler(0, 0, 0)
                : Quaternion.Euler(0, 180, 0);

            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                

                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            

            target = (target == pointA.position) ? pointB.position : pointA.position;
            yield return null;
        }
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
