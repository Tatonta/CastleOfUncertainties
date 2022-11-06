using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{

    public static void StopMoving(this Rigidbody2D rigidbody2D)
    {
        rigidbody2D.velocity = new Vector2(0, 0);
    }

    public static void ResetTransformation(this Transform trans)
    {
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector2(0.7f, 0.7f);
    }

}
