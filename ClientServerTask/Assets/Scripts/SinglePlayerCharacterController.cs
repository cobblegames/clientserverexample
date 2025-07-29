using UnityEngine;

public class SinglePlayerCharacterController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            anim.SetBool("Walk", true);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Walk", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            anim.SetBool("Walk", true);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Walk", true);
            transform.rotation = Quaternion.Euler(0, 90, 0);

        } else if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Walk", false);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Walk", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Walk", false);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Walk", false);
        }



    }

}
