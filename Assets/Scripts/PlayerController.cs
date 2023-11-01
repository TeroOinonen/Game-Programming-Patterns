using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movespeed = 30f;
    private float jumpspeed = 30f;

    MoveCommand forwardCmd;
    MoveCommand backCmd;
    MoveCommand leftCmd;
    MoveCommand rightCmd;

    MoveCommand jumpCmd;

    StrategyMoveCommand strForwardCmd;
    StrategyMoveCommand strBackCmd;
    StrategyMoveCommand strLeftCmd;
    StrategyMoveCommand strRightCmd;

    Stack<StrategyMoveCommand> moves;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moves = new Stack<StrategyMoveCommand>();

        forwardCmd = new MoveCommand(Vector3.forward * movespeed, rb);
        backCmd = new MoveCommand(Vector3.back * movespeed, rb);
        leftCmd = new MoveCommand(Vector3.left * movespeed, rb);
        rightCmd = new MoveCommand(Vector3.right * movespeed, rb);
        jumpCmd = new MoveCommand(Vector3.up * jumpspeed, rb);

        strForwardCmd = new StrategyMoveCommand(Vector3.forward, transform, ref moves);
        strBackCmd = new StrategyMoveCommand(Vector3.back, transform, ref moves);
        strLeftCmd = new StrategyMoveCommand(Vector3.left, transform, ref moves);
        strRightCmd = new StrategyMoveCommand(Vector3.right, transform, ref moves);
    }

    private void SwapCommands(ref MoveCommand A, ref MoveCommand B)
    {
        MoveCommand tmp = A;
        A = B;
        B = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement using rigidbody WASD + Space
        if (Input.GetKey(KeyCode.W))
        {
            forwardCmd.Execute();
        }

        if (Input.GetKey(KeyCode.S))
        {
            backCmd.Execute();
        }

        if (Input.GetKey(KeyCode.D))
        {
            rightCmd.Execute();
        }

        if (Input.GetKey(KeyCode.A))
        {
            leftCmd.Execute();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jumpCmd.Execute();
        }

        // Reverse A D keys
        if (Input.GetKeyDown(KeyCode.O))
        {
            SwapCommands(ref leftCmd, ref rightCmd);
        }
        
        // Strategy Game movement using transform UJHK
        if (Input.GetKey(KeyCode.U))
        {
            forwardCmd.Execute();
        }

        if (Input.GetKey(KeyCode.J))
        {
            backCmd.Execute();
        }

        if (Input.GetKey(KeyCode.K))
        {
            rightCmd.Execute();
        }

        if (Input.GetKey(KeyCode.H))
        {
            leftCmd.Execute();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            StrategyMoveCommand smc = moves.Pop();
            smc.Undo();
        }
    }
}
