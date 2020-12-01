using UnityEngine;

public static class AnimationExtensions
{
    /// <summary>
    /// Permet de modifier la vitesse de l'animation
    /// </summary>
    /// <param name="anim"></param>
    /// <param name="newSpeed"></param>
    /// <returns></returns>
	public static Animation SetSpeed (this Animation anim, float newSpeed)
	{
		anim [anim.clip.name].speed = newSpeed; 
		return anim;
	}
}