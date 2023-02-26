using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxyAnimate : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private readonly string PARAM_MOVE = "Speed";
    private readonly string PARAM_JUMP = "Jump";
    private readonly string PARAM_HURT = "Hurt";

    public void UpdateMove(float value)
    {
        anim.SetFloat(PARAM_MOVE, value);
    }

    public void TriggerHurt()
    {
        anim.SetTrigger(PARAM_HURT);
    }

    public void TriggerJump()
    {
        anim.SetTrigger(PARAM_JUMP);
    }

    public void FlipSprite(int side)
    {
        transform.localScale = new Vector3(side, 1, 1);
    }
}
