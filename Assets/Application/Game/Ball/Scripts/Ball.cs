using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ball : MonoBehaviour
{
		[SerializeField] private ParticleSystem m_ScoresEffect;
		[SerializeField] private int m_Score;
		public int Score => m_Score;

		private Rigidbody2D m_Rigidbody;
		private SpriteRenderer m_SpriteRenderer;
		public Color Color => m_SpriteRenderer.color;
		public Vector2 Velocity
		{
				get => m_Rigidbody.velocity;
				set => m_Rigidbody.velocity = value;
		}
		// Start is called before the first frame update
		void Awake()
		{
				m_Rigidbody = GetComponent<Rigidbody2D>();
				m_SpriteRenderer = GetComponent<SpriteRenderer>();
		}
		public void Destory()
		{
				Instantiate(m_ScoresEffect, transform.position, m_ScoresEffect.transform.rotation, null);
				Destroy(gameObject);
		}

		public void Force(Vector3 forward)
		{
				m_Rigidbody.velocity = forward * Mathf.Sqrt(Mathf.Abs(Physics2D.gravity.y));
		}
		public void Deactivate()
		{
				m_Rigidbody.isKinematic = true;
				m_Rigidbody.velocity = Vector2.zero;
				m_Rigidbody.angularVelocity =  0;
		}
		public void Activate()
		{
				m_Rigidbody.isKinematic = false;
		}
		private void OnBecameInvisible()
		{
				Destroy(gameObject);
		}
}
