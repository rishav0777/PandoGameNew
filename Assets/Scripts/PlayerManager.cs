using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public static event Action<bool> OnNoPlayerLeft;
    public static event Action<bool> OnFightingBoss;


    public bool MoveByTouch, gameState, attackToTheBoss;
    private Vector3 Direction;
    public List<Rigidbody>  Rblst = new List<Rigidbody>();
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    public Transform road;
    public static PlayerManager PlayerManagerCls;

    public AudiController audio;
    void Start()
    {
        audio= GameObject.FindGameObjectWithTag("reward").GetComponent<AudiController>();

        PlayerManagerCls = this;
        Rblst.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        if (Application.platform == RuntimePlatform.Android) roadSpeed /= 1.5f;
    }


    
    void Update()
    {

        if (attackToTheBoss)
        {
            MoveByTouch = false;
            OnFightingBoss?.Invoke(true);
        }

        if (gameState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("moveby touch");
                if (PlayerPrefs.GetInt("sound") == 1) audio.Onrunning();
                MoveByTouch = true;
            }
        
            if (Input.GetMouseButtonUp(0))
            {
                audio.CloseAudio();
                MoveByTouch = false;
            }
        
            if (MoveByTouch)
            {
                
                Direction.x = Mathf.Lerp(Direction.x,Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed);
           
                Direction = Vector3.ClampMagnitude(Direction,1f);
           
                road.position = new Vector3(0f,0f,Mathf.SmoothStep(road.position.z,-100f,Time.deltaTime * roadSpeed));

                foreach (var stickman_Anim in Rblst)
                    stickman_Anim.GetComponent<Animator>().SetFloat("run",1f);
            }
        
            else
            {
                  
                foreach (var stickman_Anim in Rblst)
                    stickman_Anim.GetComponent<Animator>().SetFloat("run",0f);
            }

            foreach (var stickman_rb in Rblst)
            {
                if (stickman_rb.velocity.magnitude > 0.5f)
                {
                    stickman_rb.rotation = Quaternion.Slerp(stickman_rb.rotation,Quaternion.LookRotation(stickman_rb.velocity), Time.deltaTime * velocity );
                }
                else
                {
                    stickman_rb.rotation = Quaternion.Slerp(stickman_rb.rotation,Quaternion.identity, Time.deltaTime * velocity );
                }
            }
        }
        else
        {
            if (!bossManager.BossManagerCls.BossIsAlive)
            {
                foreach (var stickMan in Rblst)
                {
                    stickMan.GetComponent<Animator>().SetFloat("attackmode",4);
                }
            }
        }

        if (attackToTheBoss)
        {
            if(PlayerPrefs.GetInt("sound")==1) audio.OnFighting();
            
            MoveByTouch = false;
        }

        activePlayerCheck();  
        
    }


    private void OnEnable()
    {
        bossManager.OnLevelEnd += BossManager_OnLevelEnd;
    }

    private void BossManager_OnLevelEnd(bool obj)
    {
        if(obj || !obj)
        {
            MoveByTouch = false;
        }
    }

    private void OnDisable()
    {
        bossManager.OnLevelEnd -= BossManager_OnLevelEnd;
    }


    [SerializeField] private GameObject nextLevel;

    void activePlayerCheck()
    {
        if (Rblst.Count <= 0)
        {
            Debug.Log("reset only");
            nextLevel.SetActive(false);
            OnNoPlayerLeft?.Invoke(true);
            MoveByTouch = false;
        }

        if(Rblst.Count > 0)
        {
            for(int i = 0; i < Rblst.Count; i++)
            {
                if (!Rblst[i].gameObject.activeSelf)
                {
                   
                    Rblst.Remove(Rblst[i]);
                }
            }
        }



        
    }

    
    private void FixedUpdate()
    {
        if (gameState)
        {
            if (MoveByTouch)
            {
                Vector3 displacement = new Vector3(Direction.x,0f,0f) * Time.fixedDeltaTime;
            
                foreach (var stickman_rb in Rblst)
                    stickman_rb.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed,0f,0f) + displacement;
            }
            else
            {
                foreach (var stickman_rb in Rblst)
                    stickman_rb.velocity = Vector3.zero;
            }
        }
        
    }
}
