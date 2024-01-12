using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
	public static float wrap(float value, float min, float max)
	{
		if (value > max) { return min; }
		else if (value < min) { return max; }

		return value;
	}

	public static Vector3 wrap(Vector3 value, Vector3 min, Vector3 max)
	{
		value.x = wrap(value.x, min.x, max.x);
		value.y = wrap(value.y, min.y, max.y);
		value.z = wrap(value.z, min.z, max.z);

		return value;
	}
}
