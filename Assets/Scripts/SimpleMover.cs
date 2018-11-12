using UnityEngine;

public class SimpleMover : MonoBehaviour
{
	[SerializeField] private float m_speed = 10.0f;

	private void Update()
	{
		float x = Input.GetAxis( "Horizontal" );
		float z = Input.GetAxis( "Vertical" );

		Vector3 position = transform.localPosition;
		position.x += x * Time.deltaTime * m_speed;
		position.z += z * Time.deltaTime * m_speed;

		transform.localPosition = position;
	}
}