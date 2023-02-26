using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCharacter : Character
{
    private PlayerController controller;
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private FoxyAnimate animator;


    [Header("Variables")]
    [SerializeField] private float jumpHeight = 300f;
    [SerializeField] private Transform headForPlace;

    [Header("Rope System")]
    [SerializeField] private DistanceJoint2D rope;
    [SerializeField] private LineRenderer ropeVisual;
    [SerializeField] private GameObject ropeVisualArea;
    [SerializeField] private LayerMask jointLayer;
    private Rigidbody2D joint;
    private RaycastHit2D hit;
    private bool isHooked = false;
    [SerializeField] private float ropeLength;
    [SerializeField] private float ropeMaxLength = 15f;
    [SerializeField] private float thrustPower = 500f;
    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        controller.OnSpacebar += Jump;
        OnHurt += movement.ShortFreeze;
        controller.OnLeftClick += GetJoint;
        controller.OnLeftClick += ShowRopeArea;
        controller.OnLeftUp += RemoveJoint;
        controller.OnLeftUp += HideRopeArea;
        controller.OnRightClick += Thrust;
        movement.SetSpeed(this.currentSpeed);
        RaiseHealthChanged();
    }

    private void ShowRopeArea()
    {
        ropeVisualArea.SetActive(true);
    }

    private void HideRopeArea()
    {
        ropeVisualArea.SetActive(false);
    }

    private void Update()
    {
        float currentX = controller.Axis.x;
        movement.Move(currentX);
        if(currentX > 0)
        {
            animator.FlipSprite(1);
        }
        else if(currentX < 0)
        {
            animator.FlipSprite(-1);
        }
    }

    private void LateUpdate()
    {
        animator.UpdateMove(movement.VelocityX);
        Swing();
    }

    private void Jump()
    {
        movement.HandleJump(jumpHeight);
        if (movement.CanJump())
        {
            animator.TriggerJump();
        }
    }

    public override void TakeDamage(int dmg)
    {
        if (currentHp == 0)
        {
            Died();
        }
        base.TakeDamage(dmg);
        animator.TriggerHurt();
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        if (SoundManager.Instance == null) return;
        SoundManager.Instance.PlaySFX(SoundManager.Sfx_Type.hit);
    }

    private void UseCherry()
    {
        currentHp--;
        RaiseHealthChanged();
    }

    protected override void Died()
    {
        base.Died();
        GameController.Instance.ResetPosition(transform);
        if (headForPlace.childCount > 0)
        {
            if (headForPlace.GetChild(0) != null)
            {
                GameController.Instance.ResetPosition(headForPlace.GetChild(0));
            }
        }
        Heal(2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mystic"))
        {
            Transform col = collision.transform;
            col.parent = headForPlace;
            col.position = headForPlace.position;
        }
    }

    #region RopeSystem
    private void GetJoint()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - movement.Rigidbody.position;
        hit = Physics2D.Raycast(transform.position, direction, ropeMaxLength,jointLayer);
        if(hit.collider != null)
        {
            movement.OverrideGround(true);
            joint = hit.rigidbody;
            ropeLength = (hit.rigidbody.position - movement.Rigidbody.position).magnitude;
            rope.enabled = true;
            rope.connectedBody = joint;
            ropeVisual.enabled = true;
            isHooked = true;
            //print("Hooked");
        }
        else
        {
            movement.OverrideGround(false);
            //print("Hooking Point Not Found!");
        }
    }

    private void RemoveJoint()
    {
        movement.OverrideGround(false);
        joint = null;
        rope.enabled = false;
        rope.connectedBody = null;
        ropeVisual.enabled = false;
        isHooked = false;
    }

    private void Swing()
    {
        if (!isHooked) return;
        Vector3[] twoPoint = new Vector3[2];
        twoPoint[0] = movement.Rigidbody.position;
        twoPoint[1] = joint.position;
        ropeVisual.SetPositions(twoPoint);
        rope.distance = ropeLength;
    }

    private void Thrust()
    {
        if (!isHooked) return;
        if (currentHp == 0) return;
        UseCherry();
        Vector2 direction = joint.position - movement.Rigidbody.position;
        movement.AddForce(direction.normalized * thrustPower);
        RemoveJoint();
    }
    #endregion
}