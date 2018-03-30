using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A class containing many annex stuff...
 * 
 */
public class Annex {

	public static float Linear(float A, float f_A, float B, float f_B, float x) {
		/*
		 * Return f(x), where f is the line passing through (A, f_A) and (B, f_B)
		 */
		float slope = (f_B - f_A) / (B - A);
		return slope * (x - A) + f_A;
	}


	public static Vector3 toVector3(Color color) {
		/* Convert a Color into a Vector3 (alpha is lost) */
		return new Vector3(color.r, color.g, color.b);
	}

	public static Color toColor(Vector3 vector3) {
		/* Convert a Vector3 into a Color (alpha set to 1) */
		return new Color (vector3.x, vector3.y, vector3.z);
	}

}
