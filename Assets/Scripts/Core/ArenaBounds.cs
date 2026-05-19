using UnityEngine;

// คลาสช่วยคำนวณ "พื้นที่ต่อสู้" = พื้นที่ที่กล้อง (main camera) มองเห็น
// ใช้ static เพราะไม่มี state ภายใน เรียกใช้ได้เลยไม่ต้องสร้าง instance
// - PlayerController ใช้ Clamp() เพื่อไม่ให้ player เดินออกนอกจอ
// - AutoCombat ใช้ Contains() เพื่อเลือกศัตรูในจอเท่านั้น
// - EnemySpawner ใช้ Clamp() เพื่อสุ่มตำแหน่งเกิดในจอ
public static class ArenaBounds
{
  // คืนกรอบสี่เหลี่ยมของพื้นที่กล้อง (หน่วย world)
  // margin = ระยะหักเข้าจากขอบจอ (เช่น 0.5 จะหดเข้ามาครึ่งหน่วย)
  public static Rect GetRect(float margin = 0f)
  {
    // Camera.main = กล้องที่มี tag "MainCamera"
    Camera cam = Camera.main;

    // ถ้าไม่มีกล้องหรือกล้องไม่ใช่ orthographic ให้คืนพื้นที่ใหญ่มาก (เท่ากับไม่จำกัด)
    if (cam == null || !cam.orthographic)
    {
      return new Rect(-50f, -50f, 100f, 100f);
    }

    // orthographicSize คือครึ่งหนึ่งของความสูงกล้อง
    float halfHeight = cam.orthographicSize;
    // ความกว้างคิดจากอัตราส่วนจอ
    float halfWidth = halfHeight * cam.aspect;

    // ตำแหน่งกล้อง (จุดกึ่งกลาง)
    Vector3 center = cam.transform.position;

    // มุมซ้ายล่างของกรอบ (บวก margin เพื่อหดเข้ามา)
    float x = center.x - halfWidth + margin;
    float y = center.y - halfHeight + margin;
    // ขนาดของกรอบ (ลบ margin ทั้ง 2 ข้าง)
    float width = halfWidth * 2f - margin * 2f;
    float height = halfHeight * 2f - margin * 2f;

    return new Rect(x, y, width, height);
  }

  // เช็คว่าตำแหน่งอยู่ในกรอบมั้ย
  public static bool Contains(Vector2 worldPos, float margin = 0f)
  {
    return GetRect(margin).Contains(worldPos);
  }

  // บีบตำแหน่งให้อยู่ในกรอบ (ถ้าล้นออกจะดึงกลับเข้าขอบ)
  public static Vector2 Clamp(Vector2 worldPos, float margin = 0f)
  {
    Rect rect = GetRect(margin);
    float x = Mathf.Clamp(worldPos.x, rect.xMin, rect.xMax);
    float y = Mathf.Clamp(worldPos.y, rect.yMin, rect.yMax);
    return new Vector2(x, y);
  }
}