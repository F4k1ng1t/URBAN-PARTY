using Unity.VisualScripting;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    [SerializeField]
    PlayerMovement player;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
            player.Move(Direction.left);
        else if (Input.GetKey(KeyCode.D))
            player.Move(Direction.right);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            player.TryJump();
    }
}
