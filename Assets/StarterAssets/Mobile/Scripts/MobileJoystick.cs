using UnityEngine;
using UnityEngine.EventSystems;

// ========================================================================
// Joystick สำหรับมือถือ — เป็น "library" สำเร็จรูป ไม่ต้องอ่าน logic ข้างใน
// ก็ใช้งานได้ แค่รู้วิธีเรียกใช้ 2 ค่าด้านล่างก็พอ
//
// ----- วิธีเรียกใช้ (สำหรับผู้เรียน) -----
//   1) ลาก MobileJoystick จากฉาก ใส่ public field ในสคริปต์ของเรา
//        public MobileJoystick joystick;
//
//   2) อ่านค่า 2 ตัวนี้ได้เลย:
//        joystick.isPressed   -> bool   กำลังแตะนิ้วอยู่ไหม
//        joystick.direction   -> Vector2 ทิศทางนิ้วลาก (ยาว 0..1)
//
//   ตัวอย่าง:
//        if (joystick.isPressed)
//        {
//            Vector2 dir = joystick.direction;
//            // เอา dir ไปขยับตัวละครได้เลย
//        }
//
// ----- วิธีวางใน Canvas (ครั้งเดียวตอนตั้ง scene) -----
//   สคริปต์นี้ต้องอยู่บน Image ที่กินพื้นที่เต็มจอ (anchorMin=0,0 anchorMax=1,1)
//   และ Image นั้นต้องอยู่ "ล่างสุด" ใน hierarchy ของ Canvas เพื่อให้ปุ่ม HUD อื่น ๆ
//   รับคลิกก่อน (joystick จะ active เฉพาะเมื่อแตะที่ว่าง)
//
// ----- การทำงาน (อ่านเพิ่มได้ถ้าสนใจ) -----
//   - แตะที่ว่าง ๆ บนหน้าจอ -> joystick เกิดตรงที่แตะ (floating joystick)
//   - ลากนิ้ว             -> ออกทิศทาง (เวกเตอร์ยาว 0..1)
//   - ปล่อยนิ้ว           -> joystick หาย, ทิศกลับเป็นศูนย์
// ========================================================================
public class MobileJoystick : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
  // RectTransform ของกลุ่ม "ฐาน+หัว" joystick (ลากจาก prefab มาใส่)
  // - ต้องเป็นลูกของ GameObject นี้ (Image เต็มจอ)
  // - จะถูกย้ายตำแหน่งไปตรงที่นิ้วแตะ และ active/deactive
  public RectTransform joystickRoot;

  // RectTransform ของหัว joystick (วงเล็กที่ขยับตามนิ้ว)
  // ต้องเป็นลูกภายใน joystickRoot
  public RectTransform handleRect;

  // ระยะสูงสุดที่หัว joystick เลื่อนได้จากจุดกลาง (pixel)
  public float maxRadius = 80f;

  // ผลลัพธ์: ทิศทางที่ผู้เล่นสั่ง (ยาว 0..1) — PlayerController อ่านค่านี้
  public Vector2 direction;

  // กำลังแตะนิ้วอยู่มั้ย (PlayerController ใช้เช็คว่าผู้เล่นกำลังบังคับเองรึเปล่า)
  public bool isPressed;

  // RectTransform ของตัว Image เต็มจอ (เอาไว้แปลงตำแหน่งนิ้ว)
  private RectTransform selfRect;

  private void Awake()
  {
    selfRect = (RectTransform)transform;

    // เริ่มต้น joystick ยังไม่โชว์
    if (joystickRoot != null)
    {
      joystickRoot.gameObject.SetActive(false);
    }
  }

  // เรียกเมื่อผู้เล่นเริ่มแตะนิ้ว
  public void OnPointerDown(PointerEventData eventData)
  {
    if (joystickRoot == null)
    {
      return;
    }

    // แปลงตำแหน่งนิ้ว (pixel บนจอ) -> ตำแหน่งภายใน RectTransform ของ Image เต็มจอ
    // หมายเหตุ: Unity API ตัวนี้คืนค่าผ่าน "out parameter" (ใส่ค่ากลับเข้าตัวแปร localPoint)
    // ทำแบบนี้เพราะตัว method มันคืน bool บอกว่าแปลงสำเร็จไหม เลยใช้ out ส่งค่าจริงออกมา
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        selfRect, eventData.position, eventData.pressEventCamera, out localPoint);

    // ย้าย joystick ทั้งก้อนไปตรงที่นิ้วแตะ
    joystickRoot.anchoredPosition = localPoint;

    // รีเซ็ตหัวให้อยู่กลาง
    if (handleRect != null)
    {
      handleRect.anchoredPosition = Vector2.zero;
    }

    // แสดง joystick ขึ้นมา
    joystickRoot.gameObject.SetActive(true);
    isPressed = true;
    direction = Vector2.zero;
  }

  // เรียกเมื่อผู้เล่นลากนิ้ว
  public void OnDrag(PointerEventData eventData)
  {
    if (!isPressed || joystickRoot == null)
    {
      return;
    }

    // ตำแหน่งนิ้วในพื้นที่ของ Image เต็มจอ
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        selfRect, eventData.position, eventData.pressEventCamera, out localPoint);

    // เวกเตอร์จากจุดกลาง joystick ไปยังนิ้ว
    Vector2 delta = localPoint - joystickRoot.anchoredPosition;

    // จำกัดให้ไม่เกิน maxRadius
    if (delta.magnitude > maxRadius)
    {
      delta = delta.normalized * maxRadius;
    }

    // ขยับหัว joystick ตามเวกเตอร์ (relative กับ joystickRoot)
    if (handleRect != null)
    {
      handleRect.anchoredPosition = delta;
    }

    // ทิศทางผลลัพธ์ = เวกเตอร์ delta หารด้วย maxRadius
    // ทำไมหาร? เพราะ delta ยาวสุดแค่ maxRadius (เราจำกัดไว้แล้วข้างบน)
    // เลยได้ค่ายาว 0..1: 0 = นิ้วอยู่กลาง, 1 = นิ้วลากสุดขอบ
    direction = delta / maxRadius;
  }

  // เรียกเมื่อผู้เล่นปล่อยนิ้ว
  public void OnPointerUp(PointerEventData eventData)
  {
    if (joystickRoot != null)
    {
      joystickRoot.gameObject.SetActive(false);
    }
    if (handleRect != null)
    {
      handleRect.anchoredPosition = Vector2.zero;
    }
    direction = Vector2.zero;
    isPressed = false;
  }
}