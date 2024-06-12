using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public Transform cameraRoot;
    public Transform itemsRoot;
    public Rigidbody body;

    public void IdleAnimation()
    {
        animator.Play("idle2");
    }
    public void RunAnimation()
    {
        animator.Play("run2");
    }
}
