using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer arrow;

    private void LateUpdate()
    {
        arrow.size = new Vector2(transform.position.magnitude, arrow.size.y);
        arrow.transform.rotation = Quaternion.FromToRotation(Vector3.right, transform.position);
    }
}
