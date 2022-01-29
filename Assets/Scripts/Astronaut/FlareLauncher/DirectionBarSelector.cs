using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBarSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DirectionBarScriptable _bar;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -_bar.angularVelocity * Time.deltaTime);
    }

    public void ResetRotation()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public Vector3 GetNormalizedDirection()
    {
        float radians = ToRadians(transform.rotation.eulerAngles.z);
        return new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0);
    }

    private float ToRadians(float degrees)
    {
        return degrees * Mathf.PI / 180f;
    }
}
