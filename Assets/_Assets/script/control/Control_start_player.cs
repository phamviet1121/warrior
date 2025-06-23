using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Control_start_player : MonoBehaviour
{
    public GameObject player_obj;
    public GameObject player;
    public GameObject location;
    public Transform location_replayer;
    public Control_attribute control_Attribute;
    public CinemachineVirtualCamera virtualCamera;
    public void Awake()
    {
        GameObject newPlayer = Instantiate(player);

        newPlayer.transform.SetParent(player_obj.transform);
        newPlayer.transform.SetSiblingIndex(0);

        newPlayer.transform.position = location.transform.position;

        Mover mover = player_obj.GetComponent<Mover>();
        Control control = player_obj.GetComponent<Control>();
        if (control != null && mover != null && control_Attribute != null)
        {
            control.input_start();
            mover.inputStart();
            control_Attribute.input_start();
        }

        virtualCamera.Follow = newPlayer.transform;

        
        
            location_replayer = location.transform;
            location_replayer.position = location.transform.position;
       


    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void On_replay()
    {
        float maxHealth = control_Attribute.healthSystem.maxHealth;
        float maxEnergy = control_Attribute.energySystem.maxEnergy;

        float damageAmount = control_Attribute.attach.collider_attack.damageAmount;
        float damageAmount_ = control_Attribute.attach.collider_attack.damageAmount_;
        float _damageAmount = control_Attribute.attach.collider_attack._damageAmount;

        GameObject child = player_obj.transform.GetChild(0).gameObject;
        Mover mover_player = player_obj.GetComponent<Mover>();


        GameObject newPlayer = Instantiate(player);
        newPlayer.transform.SetParent(player_obj.transform);
        newPlayer.transform.SetSiblingIndex(0);

        if (mover_player.onjump == true && mover_player != null)
        {
            newPlayer.transform.position = child.transform.position;
        }
        else
        {
            if (location_replayer != null)
            {
                newPlayer.transform.position = location_replayer.position;
            }
            else
            {
                newPlayer.transform.position = location.transform.position;
            }

        }

        Mover mover = player_obj.GetComponent<Mover>();
        Control control = player_obj.GetComponent<Control>();
        if (control != null && mover != null && control_Attribute != null)
        {
            control.input_start();
            mover.inputStart();
            control_Attribute.input_start();
        }
        virtualCamera.Follow = newPlayer.transform;

        control_Attribute.healthSystem.maxHealth = maxHealth;
        control_Attribute.energySystem.maxEnergy = maxEnergy;
        control_Attribute.attach.collider_attack.damageAmount = damageAmount;
        control_Attribute.attach.collider_attack.damageAmount_ = damageAmount_;
        control_Attribute.attach.collider_attack._damageAmount = _damageAmount;

        // newPlayer.transform.position = child.transform.position;


        child.SetActive(false);
        Destroy(child, 3f);

    }
    public void saver_location_replay(Transform point)
    {
        location_replayer = point;
        location_replayer.position = point.position;
    }

}
