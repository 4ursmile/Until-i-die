using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    [SerializeField] Image EnemyHealthBar;
    [SerializeField] GameObject OhitFuckingPlayer;
    [HideInInspector] public NavMeshAgent nav;
    [SerializeField] float stopDistance;
    [SerializeField] float distacceForSkill;
    Animator animator;
    [SerializeField] int tyLeSkill;
    public Enemy enemy;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        enemy = GetComponent<Enemy>();
        nav = GetComponent<NavMeshAgent>();
        OhitFuckingPlayer = GameObject.FindGameObjectWithTag("Player");
        nav.stoppingDistance = stopDistance;
        animator = GetComponent<Animator>();
        AnimationClipIDAssign();
        DOTween.SetTweensCapacity(500, 125);
        StartOnActive();
    }
    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        enemy = GetComponent<Enemy>();
        nav = GetComponent<NavMeshAgent>();
        OhitFuckingPlayer = GameObject.FindGameObjectWithTag("Player");
        nav.stoppingDistance = stopDistance;
        animator = GetComponent<Animator>();
        AnimationClipIDAssign();
        DOTween.SetTweensCapacity(500, 125);
        jumpTarget = OhitFuckingPlayer.transform;
        StartOnActive();
    }
    public void StartOnActive()
    {
        if (Player.Instance.isDie) return;

        nav.destination = OhitFuckingPlayer.transform.position;
        nav.speed = enemy.speed;
        animator.SetBool(moveID, true);
        gameObject.GetComponent<Collider>().enabled = true;
        enemy.Refresh();
    }
    int moveID;
    int deathID;
    int skillID;
    int attackID;
    void AnimationClipIDAssign()
    {
        moveID = Animator.StringToHash("Move");
        deathID = Animator.StringToHash("Death");
        skillID = Animator.StringToHash("Skill");
        attackID = Animator.StringToHash("Attack");
    }
    // Update is called once per frame
    [SerializeField] InputManager input;
    void Update()
    {
        if (Player.Instance.isDie) return;

        if (enemy.isDie)
        {
            return;
        }
        if (enemy.isMove)
        {
            nav.destination = OhitFuckingPlayer.transform.position;
        } else if (enemy.isAttack)
        {
            Vector3 targetVector = jumpTarget.position - transform.position;
            targetVector.Normalize();
            float targetY = Mathf.Atan2(targetVector.x, targetVector.z) * Mathf.Rad2Deg;
            Vector3 targetRotation = new Vector3(0, targetY, 0);
            transform.DORotate(targetRotation, 0.1f);
            TrytoAttack();
        }

    }
    public void AudioAttackPlay()
    {
    }
    [SerializeField] float attackCoolTime;
    public void TrytoAttack()
    {
        if (enemy.isDie) return;
        if (enemy.canAttack)
        {
            enemy.canAttack = false;
            AttackAction();
            Invoke("AttackCoolDown", attackCoolTime);
        }
    }
    void AttackCoolDown()
    {
        enemy.canAttack = true;
    }
    [SerializeField] Transform colappOffset;
    [SerializeField] LayerMask playerLayer;
    void AttackAction()
    {
        if (enemy.isDie) return;

        animator.SetBool(attackID, true);
        Invoke("ResetIdle", 2);
    }
    [SerializeField]float  CaptureRadius;
    public void AttackCapturePlayer()
    {
        if (enemy.isDie) return;

        Collider[] PlayeColider = Physics.OverlapSphere(colappOffset.position, CaptureRadius, playerLayer);
        if (PlayeColider.Length > 0)
        {
            Player.Instance.TakeDamage(enemy.damage);
        }
    }
    void ResetIdle()
    {
        if (enemy.isDie) return;

        animator.SetBool(attackID, false);
    }
    public void StartAttack()
    {
        if (enemy.isDie) return;
        audioSource.Pause();
        nav.isStopped = true;
        enemy.isAttack = true;
        animator.SetBool(moveID, false);

        enemy.isMove = false;
    }
    public void EndAttack()
    {
        if (enemy.isDie) return;
        audioSource.Play();
        enemy.isAttack = false;
        enemy.isMove = true;
        Invoke("EndAttackDelay", 0.2f);
    }
    void EndAttackDelay()
    {
        animator.SetBool(moveID, true);
        animator.SetBool(attackID, false);
        nav.Resume();
        nav.destination = OhitFuckingPlayer.transform.position;
    }
    [SerializeField] Transform jumpTarget;
    [SerializeField] float changeDuration;
    public void ChangeHealthBar(float value)
    {
        if (Player.Instance.isDie) return;

        //EnemyHealthBar.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
        EnemyHealthBar.rectTransform.DOScaleX(value,changeDuration);
    }
    public void TakeDamgeAction()
    {
        if (enemy.isDie) return;
    }
    public void DieAction()
    {
        audioSource.Pause();
        gameObject.GetComponent<Collider>().enabled = false;
        nav.Stop();
        animator.SetTrigger(deathID);
    }
}
