/*
	SmoothOverheadCamera
	by David Koenig
	https://focusedgamedev.com
	Released under MIT license.
*/

using UnityEngine;

public class SmoothOverheadCamera : MonoBehaviour
{
	[SerializeField] private Transform m_target;
	[SerializeField] private float m_lerpSpeed = 1.0f;


	private void Update()
	{
		Vector3 targetPosition = m_target.transform.position;
		Vector3 cameraPosition = transform.position;

		float y = transform.position.y - targetPosition.y;
		float angle = 90.0f - transform.rotation.eulerAngles.x;
		float tangent = Mathf.Tan( Mathf.Deg2Rad * angle );
		float x = y * tangent;

		//hypotenuse
		float c = Mathf.Sqrt( (x*x) + (y*y) );

		Vector3 center_world_position = transform.position + (transform.forward * c);
		Vector3 difference = (targetPosition - center_world_position);
		Vector3 moveToPosition = cameraPosition + difference;
		moveToPosition.y = transform.position.y;
		transform.position = Vector3.Lerp( transform.position, moveToPosition, Time.deltaTime * m_lerpSpeed );
	}

	private void OnDrawGizmos()
	{
		Vector3 targetPosition = m_target.transform.position;
		Vector3 cameraPosition = transform.position;

		float y = transform.position.y - targetPosition.y;
		float angle = 90.0f - transform.rotation.eulerAngles.x;
		float tangent = Mathf.Tan( Mathf.Deg2Rad * angle );
		float x = y * tangent;
		float c = Mathf.Sqrt( (x*x) + (y*y) );
		Vector3 center_world_position = transform.position + (transform.forward * c);

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere( center_world_position, 0.5f );

		//Where we are
		Gizmos.DrawLine(transform.position, center_world_position);
		Vector3 positionY = new Vector3( cameraPosition.x, cameraPosition.y - y, cameraPosition.z );
		Gizmos.DrawLine( transform.position, positionY );
		Gizmos.DrawLine( positionY, center_world_position );

		//Current ground point
		Gizmos.DrawWireSphere( positionY, 0.5f );

		Vector3 difference = (targetPosition - center_world_position);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere( center_world_position + difference, 1.0f );

		//Where we want to go -- lerping to
		Gizmos.color = Color.red;
		Gizmos.DrawLine( cameraPosition + difference, positionY + difference );
		Gizmos.DrawLine( cameraPosition + difference, targetPosition );
		Gizmos.DrawLine( positionY + difference, targetPosition );
	}
}