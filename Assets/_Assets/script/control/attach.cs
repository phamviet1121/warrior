﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class attach : MonoBehaviour
{
    public Animator anim;
    public bool allow_Attach_bool;
    public bool Croush_Attach_bool;
    private bool canSlideAttack = true;

    public Collider2D Collider2D_1;
    public Collider2D Collider2D_2;
    public collider_attack collider_attack;

    public float moveDistance = 2f;

    public bool is_Death;
    public bool is_durt;

    public float increase_speed;
    public float pushed_back;
    //  bool allow_Dash_Attach_bool;

    void Start()
    {
        Collider2D_1.enabled = true;
        Collider2D_2.enabled = false;
        allow_Attach_bool = true;
        is_Death = false;
        is_durt = false;
    }






    public void on_attach()
    {
        if (allow_Attach_bool && Croush_Attach_bool)
        {
            anim.SetTrigger("attach");

            allow_Attach_bool = false;
            collider_attack.isattacking = true;
        }

    }
    public void on_Dash_attach(bool left_rihgt)
    {
        if (allow_Attach_bool && Croush_Attach_bool)
        {
            anim.SetTrigger("dash_attack");

            allow_Attach_bool = false;
            collider_attack.is_attacking = true;
            StartCoroutine(SlideAttackCooldown(left_rihgt, moveDistance, increase_speed));
        }

    }
    public void on_slide_attack(bool left_rihgt)
    {


        if (canSlideAttack)
        {
            canSlideAttack = false;
            collider_attack.isattacking_ = true;
            anim.SetTrigger("slide_attack");
            StartCoroutine(SlideAttackCooldown(left_rihgt, moveDistance, increase_speed));
        }

    }
    private IEnumerator SlideAttackCooldown(bool left_rihgt, float moveDistance, float mover)
    {
        // canSlideAttack = false;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector3(transform.position.x + (left_rihgt ? mover : -mover), transform.position.y);

        float elapsedTime = 0f;
        while (elapsedTime < moveDistance)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        canSlideAttack = true;
        collider_attack.isattacking_ = false;
    }




    public void on_Croush()
    {

        anim.SetBool("onCroush", true);
        Collider2D_1.enabled = false;
        Collider2D_2.enabled = true;
        Croush_Attach_bool = false;
    }
    public void off_Croush()
    {
        anim.SetBool("onCroush", false);
        Collider2D_1.enabled = true;
        Collider2D_2.enabled = false;
        Croush_Attach_bool = true;
    }

    public void allow_Attach()
    {
        collider_attack.is_attacking = false;
        collider_attack.isattacking = false;
        allow_Attach_bool = true;
    }
    public void on_dame()
    {
        collider_attack.on_isattacking();
    }
    public void off_dame()
    {
        collider_attack.off_isattacking();
    }
    public void on_dame_()
    {
        collider_attack.on_isattacking_();
    }
    public void off_dame_()
    {
        collider_attack.off_isattacking_();
    }

    public void on_death()
    {
        anim.SetBool("death", true);
        is_Death = true;
    }
    public void on_hurt(bool left_rihgt)
    {
        if (allow_Attach_bool && canSlideAttack)
        {
          //  Debug.Log("2");

            anim.SetTrigger("hurt");
            is_durt = true;

            bool is_pushed = left_rihgt == true ? false : true;
            StartCoroutine(shocked(is_pushed, 0.3f, pushed_back));
        }
    }

    private IEnumerator shocked(bool left_rihgt, float moveDistance, float mover)
    {
        // canSlideAttack = false;
        Vector2 startPosition = transform.position;
        Vector2 targetPosition = new Vector3(transform.position.x + (left_rihgt ? mover : -mover), transform.position.y);

        float elapsedTime = 0f;
        while (elapsedTime < moveDistance)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, elapsedTime / moveDistance);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        is_durt = false;
    }
}
