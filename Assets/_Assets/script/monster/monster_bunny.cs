using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_bunny : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    private Coroutine currentRoutine;
    public bool left_right;
    void Start()
    {
        currentRoutine = StartCoroutine(MoveLoopAB());
    }

    // Update is called once per frame

    IEnumerator MoveLoopAB()
    {
        Vector3 target = pointA.position;

        while (true)
        {
            left_right = target.x >= transform.position.x;

            // Quay mặt quái về phía player
            //Vector3 dir = (player.position - transform.position).normalized;
            //transform.forward = new Vector3(dir.x, 0, dir.z);
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
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
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
