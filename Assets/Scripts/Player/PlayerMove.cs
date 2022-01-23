using UnityEngine;

public enum UnitType
{
    Default = 0,
    Player = 1,
    Npc = 2,
}

public class PlayerInfo
{
    public float gravityScale;
    public float forceScale;
    public float maxRotateSpeed;
    public float maxMoveSpeed;
    public float drag;
    public float angularDrag;

    public PlayerInfo(float gravityScale, float forceScale, float maxRotateSpeed, float maxMoveSpeed, float drag, float angularDrag)
    {
        this.gravityScale = gravityScale;
        this.forceScale = forceScale;
        this.maxRotateSpeed = maxRotateSpeed;
        this.maxMoveSpeed = maxMoveSpeed;
        this.drag = drag;
        this.angularDrag = angularDrag;
    }
}

public class PlayerMove : MonoBehaviour
{
    [Header("旋转的扭矩力")]
    public float forceScale;
    private Rigidbody2D rigid;
    [Header("最大旋转速度")]
    public float maxRotateSpeed;
    [Header("最大移动速度")]
    public float maxMoveSpeed;
    private bool inputAble = true;
    [Header("翻滚停顿的CD时间")]
    public float triggerGroundCd = 0.5f;
    [Header("单位类型")]
    public UnitType unitType = UnitType.Npc;
    private bool npcStartMove = false;
    private float npcMoveInput = 0;
    private static bool playerControlAble = true;
    private PlayerInfo normalInfo;
    [Header("是否能浮起来")]
    public bool floatAble = false;
    private bool isInWater = false;
    [Header("是否忽视水")]
    public bool ignoreWater = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        normalInfo = new PlayerInfo(rigid.gravityScale, forceScale, maxRotateSpeed, maxMoveSpeed, rigid.drag, rigid.angularDrag);
    }

    private void FixedUpdate()
    {
        if (unitType == UnitType.Player && !playerControlAble)
        {
            return;
        }
        if (!inputAble)
        {
            return;
        }
        float input = GetInput();
        if (input == 0)
        {
            return;
        }
        if (rigid.velocity.magnitude < maxMoveSpeed)
        {
            rigid.AddForce(new Vector2(input, -Mathf.Abs(input)) * 10);
        }
        if (rigid.angularVelocity < maxRotateSpeed && rigid.angularVelocity > -maxRotateSpeed)
        {
            //rigid.AddTorque(-input * forceScale);
            rigid.angularVelocity -= input * forceScale;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTrigGround();
    }

    private void OnTrigGround()
    {
        inputAble = false;
        MyTimer.instance.WaitTimer(triggerGroundCd, () =>
        {
            inputAble = true;
        });
    }

    private float GetInput()
    {
        if (unitType == UnitType.Player)
        {
            return Input.GetAxis("Horizontal");
        }
        if (unitType == UnitType.Npc && npcStartMove)
        {
            return npcMoveInput;
        }
        return 0;
    }

    public void SetNpcMove(float input)
    {
        npcMoveInput = input;
        npcStartMove = true;
    }

    public void SetNpcStop()
    {
        npcStartMove = false;
    }

    public static void SetControlAble(bool able)
    {
        playerControlAble = able;
    }

    public void SetWaterState(bool isOpen)
    {
        isInWater = isOpen;
        if (isOpen)
        {
            SetWaterFloat(floatAble);
            forceScale = normalInfo.forceScale * 0.2f;
            maxRotateSpeed = normalInfo.maxRotateSpeed * 0.2f;
            maxMoveSpeed = normalInfo.maxMoveSpeed = 0.2f;
            rigid.drag = normalInfo.drag * 5;
            rigid.angularDrag = normalInfo.angularDrag * 5;
        }
        else
        {
            rigid.gravityScale = normalInfo.gravityScale;
            forceScale = normalInfo.forceScale;
            maxRotateSpeed = normalInfo.maxRotateSpeed;
            maxMoveSpeed = normalInfo.maxMoveSpeed;
            rigid.drag = normalInfo.drag;
            rigid.angularDrag = normalInfo.angularDrag;
        }
    }

    public void SetWaterFloat(bool isUp)
    {
        if (isUp && floatAble)
        {
            rigid.gravityScale = normalInfo.gravityScale * -0.5f;
            return;
        }
        else
        {
            rigid.gravityScale = normalInfo.gravityScale * 0.15f;
        }
    }

    public void SetRigidMove(bool able)
    {
        if (able)
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            rigid.bodyType = RigidbodyType2D.Kinematic;
        }
    }

}