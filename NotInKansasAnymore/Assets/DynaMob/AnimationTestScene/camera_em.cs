using UnityEngine;
using System.Collections;

public class camera_em : MonoBehaviour {

    public Transform Tesla;
    public Transform Target;
    public float zOffset = 0;
    public float Distance = 4.5f;
    public float ZoomStep = 1.0f;
    public float MoveSpeed = 5f;
    public float Pitch = 30f;
    public float yaw = 45f;
    public float TargetMoveSpeed = 3f;
    public float RotateSpeed = 60f;

    public Material[] skins;
    private int MatInd = 0;

    public Animator[] animator;

    private Vector3 TargetPos;
    private Vector3 LookPoint;

    void Start()
    {
        transform.position = GetPosition();
        LookPoint = Target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       

        // --
        if (Input.GetKey("a"))
        {
            yaw -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("d"))
        {
            yaw += Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("w") && Distance > 2f)
        {
            Distance -= Time.deltaTime * ZoomStep;
        }

        if (Input.GetKey("s") && Distance < 14f)
        {
            Distance += Time.deltaTime * ZoomStep;
        }
        if (Input.GetKey("q") && Pitch > 10f)
        {
            Pitch -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("e") && Pitch < 75f)
        {
            Pitch += Time.deltaTime * RotateSpeed;
        }

        LookPoint = Vector3.MoveTowards(LookPoint, Target.position + Vector3.up * zOffset, TargetMoveSpeed * Time.deltaTime);

        TargetPos = GetPosition();
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);
        transform.LookAt(LookPoint);
    }

    Vector3 GetPosition()
    {
        float y = Distance * Mathf.Sin(Pitch * Mathf.Deg2Rad);
        float r = Distance * Mathf.Cos(Pitch * Mathf.Deg2Rad);
        float x = r * Mathf.Cos(yaw * Mathf.Deg2Rad);
        float z = r * Mathf.Sin(yaw * Mathf.Deg2Rad);

        return (Target.position + new Vector3(x, y, z));
    }

    public void SetAngle(float angle)
    {
        foreach (Animator a in animator)
            a.SetFloat("angle", angle);
    }

    public void SetTurnAngle(float angle)
    {
        foreach (Animator a in animator)
            a.SetFloat("turnangle", angle);
    }

    public void SetTrigger(string str)
    {
        foreach (Animator a in animator)
            a.SetTrigger(str);
    }

    public void Death(int dir)
    {
        foreach (Animator a in animator)
        {
            if (dir == 0)
                a.SetTrigger("death1");
            else
                a.SetTrigger("death2");
        }
    }

    public void WalkStop()
    {
        foreach (Animator a in animator)
            a.SetBool("walk", !a.GetBool("walk"));
    }

    public void FireStopFire()
    {
        foreach (Animator a in animator)
            a.SetBool("fire", !a.GetBool("fire"));
    }

    public void ChangeSkin()
    {
        GameObject m1 = Tesla.Find("mesh_head").gameObject;
        GameObject m2 = Tesla.Find("mesh_head_helmet").gameObject;
        GameObject m3 = Tesla.Find("mesh_electro_man").gameObject;

        m1.SetActive(!m1.activeInHierarchy);
        m2.SetActive(!m2.activeInHierarchy);

        MatInd += 1;
        if (MatInd >= skins.Length) MatInd = 0;
        m3.GetComponent<Renderer>().material = skins[MatInd];
    }
}