using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public Transform cameraRoot;

    private string currentAnimation = "idle2";

    public void IdleAnimation()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle2")
            return;

        animator.Play("idle2");
        currentAnimation = "idle2";
    }

    public void RunAnimation()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "run2")
            return;

        animator.Play("run2");
        currentAnimation = "run2";
    }
}
