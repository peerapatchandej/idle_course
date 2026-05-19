using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float moveSpeed = 5f;
  public float screenMargin = 0.5f;
  public MobileJoystick joystick;
  public Vector2 facingDirection = Vector2.right;
  public bool isMoving;

  private Rigidbody2D rb;
  private Vector2 manualDirection;

  //=====
  private Vector2 autoDirection;
  private bool hasAutoRequest;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  private void Update()
  {
    if (joystick.isPressed)
    {
      manualDirection = joystick.direction;
    }
    else
    {
      manualDirection = Vector2.zero;
    }

    if (manualDirection.sqrMagnitude > 0.01f)
    {
      facingDirection = manualDirection.normalized; //x -1 0 1
    }
  }

  private void FixedUpdate()
  {
    //
    Vector2 moveDir = manualDirection;    //0.0f
    if (moveDir.sqrMagnitude < 0.01f && hasAutoRequest) //true && true
    {
      moveDir = autoDirection; //-1/1
      if (moveDir.sqrMagnitude > 0.01f)
      {
        facingDirection = moveDir;
      }
    }

    Vector2 targetPos = rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime);
    targetPos = ArenaBounds.Clamp(targetPos, screenMargin);
    rb.MovePosition(targetPos);

    isMoving = moveDir.sqrMagnitude > 0.01f;

    //
    hasAutoRequest = false;
    autoDirection = Vector2.zero;
  }

  //
  public bool HasManualInput()
  {
    return manualDirection.sqrMagnitude > 0.01f;
  }

  //
  public void RequestAutoMove(Vector2 direction)
  {
    autoDirection = direction;  //-1 0 1
    hasAutoRequest = true;
  }

  //float time = 0;

  //// Start is called once before the first execution of Update after the MonoBehaviour is created
  //void Start()
  //{
  //  Application.targetFrameRate = 10;
  //}

  //// Update is called once per frame
  //void Update()
  //{
  //  if (time < 1)
  //  {
  //    time = time + Time.deltaTime;
  //    transform.position = transform.position + new Vector3(1f * Time.deltaTime, 0, 0);
  //  }
  //}
}
