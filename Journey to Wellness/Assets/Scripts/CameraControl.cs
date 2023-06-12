using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; // 目标对象，通常设置为角色对象
    public float distance = 5.0f; // 摄像机距离角色的距离
    public float height = 2.0f; // 摄像机相对于角色的高度
    public float heightDamping = 2.0f; // 高度的阻尼，用于平滑摄像机的高度变化
    public float rotationDamping = 1.0f; // 旋转的阻尼，用于平滑摄像机的旋转变化

    void LateUpdate()
    {
        // 验证目标是否存在
        if (!target) return;

        // 计算期望的摄像机旋转角度和高度
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        // 获取当前的摄像机旋转角度和高度
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // 通过阻尼进行平滑旋转角度和高度的过渡
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // 将角度转换为旋转，使摄像机始终对准角色的背部
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // 设置摄像机的位置为目标位置减去旋转的方向乘以距离
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // 调整摄像机的高度
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // 使摄像机始终朝向角色
        transform.LookAt(target);
    }
}
