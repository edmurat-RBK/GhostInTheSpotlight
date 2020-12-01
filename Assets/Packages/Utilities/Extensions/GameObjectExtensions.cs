using UnityEngine;
using System.Collections.Generic;

public static class GameObjectExtensions
{
    /// <summary>
    /// Returns true if the gameObject has a rigidBody.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
	public static bool HasRigidbody(this GameObject go)
    {
        return (go.GetComponent<Rigidbody>() != null);
    }

    /// <summary>
    /// Returns true if the gameObject has an animation.
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
	public static bool HasAnimation(this GameObject go)
    {
        return (go.GetComponent<Animation>() != null);
    }

    /// <summary>
    /// Changes the layer of the gameObject and all their children.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="layer"></param>
    public static void SetLayerRecursively(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform t in gameObject.transform)
            t.gameObject.SetLayerRecursively(layer);
    }

    /// <summary>
    /// Get An Object’s Collision Mask.
    /// </summary>
    /// <returns>The collision mask.</returns>
    /// <param name="gameObject">Game object.</param>
    /// <param name="layer">Layer.</param>
    public static int GetCollisionMask(this GameObject gameObject, int layer = -1)
    {
        if (layer == -1)
            layer = gameObject.layer;

        int mask = 0;
        for (int i = 0; i < 32; i++)
            mask |= (Physics.GetIgnoreLayerCollision(layer, i) ? 0 : 1) << i;

        return mask;
    }

    /// <summary>
    /// Returns true if the object's layer is in the specified layermask.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    public static bool IsInLayerMask(this GameObject gameObject, LayerMask mask)
    {
        return ((mask.value & (1 << gameObject.layer)) > 0);
    }
}