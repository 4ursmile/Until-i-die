using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    static public PlayerController Instance;
    ///Move
    public float moveSpeed;
    public float sprintSpeed = 1;
    //Input
    InputManager input;
    CharacterController charControl;
    [SerializeField]
    LayerMask groundLayer;
    // Animation ID;
    Animator animator;
    int AddresDirX;
    int AddresDirY;
    int DeathID;
    GunShoot gunShoot;
    BulletPooling bulletPoll;
    private void AnimationIdAddresAssign()
    {
        AddresDirX = Animator.StringToHash("DirX");
        AddresDirY = Animator.StringToHash("DirY");
        DeathID = Animator.StringToHash("PlayerDeath");
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
     
        input = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        charControl = GetComponent<CharacterController>();
        gunShoot = GetComponent<GunShoot>();
        bulletPoll = GetComponent<BulletPooling>();
        AnimationIdAddresAssign();
        timeCountDown = Player.Instance.FireRate;
        footsound = GetComponent<FootStepSound>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.isDie)
        {
            if(Player.Instance.canMove) PlayerMove();
            Rotation();
            Shoot();
            GravitySimulation();
        }
    }
    [SerializeField]
    bool readyToShoot = true;
    [SerializeField]
    public float timeCountDown = 0.5f;
    void Shoot()
    {
        if (input.shoot && readyToShoot && !(input.sprint&&moveDir.z >0.9f))
        {
            readyToShoot = false;
            gunShoot.Shoot();
            bulletPoll.Shoot();
            Invoke("ShootCountDown", timeCountDown);
        }
    }
    [SerializeField]float verticalVerlocity;
    float Gravity = -9.81f;
    void GravitySimulation()
    {
        if (charControl.isGrounded)
        {
            verticalVerlocity = -2f;
        } else
        {
            verticalVerlocity += Time.deltaTime*Gravity;
        }
    }
    void ShootCountDown()
    {
        readyToShoot = true;
        gunShoot.StopShoot();
    }
    Ray ray;
    RaycastHit hitinfo;
    [SerializeField]
    float targetRotation = 0;
    float rotationVelocity;
    [SerializeField] float rotationSmoothTime = 0.2f;
    void Rotation()
    {
        ray = Camera.main.ScreenPointToRay(input.look);
        Physics.Raycast(ray,out hitinfo,Mathf.Infinity,groundLayer);
        Vector3 roVector = hitinfo.point - transform.position;
        roVector.Normalize();
        targetRotation = Mathf.Atan2(roVector.x,roVector.z)*Mathf.Rad2Deg;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetRotation,ref rotationVelocity,rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0,rotation,0);
    }
    [SerializeField]
    float realSpeed = 0;
    [SerializeField]
    float changeStateTime = 0.1f;
    Vector3 moveDir;
    float playerSpeeddelta = 1;
    void PlayerMove()
    {
        moveDir = new Vector3(input.move.x,0,input.move.y);
        moveDir.Normalize();
        float speed = moveDir.magnitude;
        sprintSpeed = (input.sprint && moveDir.z>0.9f)?1.6f:0.9f;
        sprintSpeed = moveDir.magnitude>0?sprintSpeed:0;
        if (Mathf.Abs(moveDir.x) > 0.5f && Mathf.Abs(moveDir.z) > 0.5f)
            sprintSpeed = sprintSpeed / 2;
       
        realSpeed = Mathf.Lerp(realSpeed,sprintSpeed,changeStateTime);
        Vector3 targetDirection = Quaternion.Euler(0,targetRotation,0)*moveDir;
        targetDirection.Normalize();
        animator.SetFloat(AddresDirX,moveDir.x*sprintSpeed);
        animator.SetFloat(AddresDirY,moveDir.z*sprintSpeed);
        charControl.Move(targetDirection*moveSpeed*sprintSpeed*playerSpeeddelta*Time.deltaTime + new Vector3(0,verticalVerlocity*Time.deltaTime,0));
    }
    public void HitReaction()
    {
        animator.SetLayerWeight(2, 0.7f);
        Invoke("ReWeight", 0.6f);
        playerSpeeddelta = 0.6f;
        HitSound();
    }
    void ReWeight()
    {
        animator.SetLayerWeight(2, 0f);
        playerSpeeddelta = 1;
    }
    FootStepSound footsound;
    public void HitSound()
    {
        footsound.playHitRaction();
    }
    public void DieAction()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetBool(DeathID,true);
    }
}
