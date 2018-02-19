using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
public class RaycastController : MonoBehaviour {

	public LayerMask collisionMask;					// Indicates which Layer these rays are going to detect

	public const float skinWidth = .003f;
	const float dstBetweenRays = .05f;				// !!! Has to be correct

	protected int horizontalRayCount;				// Number of horizontal ray
	protected int verticalRayCount;					// Number of vertical ray

	protected float horizontalRaySpacing;
	protected float verticalRaySpacing;
	[HideInInspector]
	public BoxCollider2D collider;
	public RaycastOrigins raycastOrigins;

	public virtual void Awake() {		// Awake in order to get the BoxCollider2D component before CameraFollow
		collider = GetComponent<BoxCollider2D> ();

		// Adjusting the BoxCollider2D to the size of the sprite
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		if (spriteRenderer) {
			Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
			collider.size = spriteSize;
		}
	}

	public virtual void Start() {
		CalculateRaySpacing ();
	}

	public void UpdateRaycastOrigins() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);		// Reduce the bounds

		raycastOrigins.bottomLeft = new Vector2 (bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2 (bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2 (bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2 (bounds.max.x, bounds.max.y);
	}
		
	public void CalculateRaySpacing() {
		Bounds bounds = collider.bounds;
		bounds.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.RoundToInt (bounds.size.y / dstBetweenRays);
		verticalRayCount = Mathf.RoundToInt (bounds.size.x / dstBetweenRays);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}


	public struct RaycastOrigins {
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
}
