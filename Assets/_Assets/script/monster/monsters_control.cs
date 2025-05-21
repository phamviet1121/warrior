using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsters_control : MonoBehaviour
{
    public bool is_MonsterAttack;
    public float time;
    public float times;
    public GameObject collider_;
    public bool left_rihgt;
    public float mover;
    public Animator anim;

    void Start()
    {
        collider_.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        on_monster_attack();
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

        //yield return new WaitForSeconds(1f);

    }
}
