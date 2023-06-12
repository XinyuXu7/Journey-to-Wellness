using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; // Ŀ�����ͨ������Ϊ��ɫ����
    public float distance = 5.0f; // ����������ɫ�ľ���
    public float height = 2.0f; // ���������ڽ�ɫ�ĸ߶�
    public float heightDamping = 2.0f; // �߶ȵ����ᣬ����ƽ��������ĸ߶ȱ仯
    public float rotationDamping = 1.0f; // ��ת�����ᣬ����ƽ�����������ת�仯

    void LateUpdate()
    {
        // ��֤Ŀ���Ƿ����
        if (!target) return;

        // �����������������ת�ǶȺ͸߶�
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        // ��ȡ��ǰ���������ת�ǶȺ͸߶�
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // ͨ���������ƽ����ת�ǶȺ͸߶ȵĹ���
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // ���Ƕ�ת��Ϊ��ת��ʹ�����ʼ�ն�׼��ɫ�ı���
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // �����������λ��ΪĿ��λ�ü�ȥ��ת�ķ�����Ծ���
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // ����������ĸ߶�
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // ʹ�����ʼ�ճ����ɫ
        transform.LookAt(target);
    }
}
