using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Transform targetTrans;//目标位置

    public float attackDist;//攻击距离
    public float attackAngle;//攻击角度

    private void Start()
    {
        DrawAttackArea(transform, attackAngle, attackDist);
    }

    void Update()
    {
        //DrawAttackArea(transform, attackAngle, attackDist);
        //移动
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        //transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 30);
        //if (Input.GetKey(KeyCode.O))
        //{
        //    transform.Rotate(Vector3.up * Time.deltaTime * -300);
        //}
        //if (Input.GetKey(KeyCode.P))
        //{
        //    transform.Rotate(Vector3.up * Time.deltaTime * 300);
        //}

        //判断
        float dist = Vector3.Distance(transform.position, targetTrans.position);
        float angle = Vector3.Angle(transform.forward, targetTrans.position - transform.position);
        if (dist <= attackDist && angle <= attackAngle / 2)
        {
            Debug.Log("进入视线");
        }
        else
        {
            Debug.Log("离开视线");
        }
    }

    /// <summary>
    /// 绘制攻击区域
    /// </summary>
    public void DrawAttackArea(Transform t, float angle, float radius)
    {
        int segments = 100;
        float deltaAngle = angle / segments;
        Vector3 forward = t.forward;

        Vector3[] vertices = new Vector3[segments + 2];
        vertices[0] = t.position;
        for (int i = 1; i < vertices.Length; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + deltaAngle * (i - 1), 0f) * forward * radius + t.position;
            vertices[i] = pos;
        }
        int trianglesAmount = segments;
        int[] triangles = new int[segments * 3];
        for (int i = 0; i < trianglesAmount; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }

        GameObject go = new GameObject("AttackArea");
        go.transform.position = new Vector3(0, 0.1f, 0);
        go.transform.SetParent(transform);
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        Mesh mesh = new Mesh();
        mr.material.shader = Shader.Find("Unlit/Color");
        mr.material.color = Color.red;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mf.mesh = mesh;
    }
}
