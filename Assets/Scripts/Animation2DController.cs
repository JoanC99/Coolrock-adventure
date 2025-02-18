using UnityEngine;

public class Animation2DController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            Debug.Log("Idle");
            anim.SetTrigger("Idle");
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log("Run");
            anim.SetTrigger("Run");
        }
    }
}