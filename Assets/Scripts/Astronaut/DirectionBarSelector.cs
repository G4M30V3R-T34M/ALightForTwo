using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionBarSelector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float angularVelocity;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -angularVelocity * Time.deltaTime);
    }

    public void ResetRotation()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
