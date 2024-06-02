using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BallPoint : MonoBehaviour
{
		[SerializeField] private int m_Id;

		private Area m_Area;
		private Ball m_Ball;
		private SpriteRenderer m_SpriteRenderer;
		private Vector2 m_TempVelocity;

		private void Start()
		{
				m_Area = GetComponentInParent<Area>();
				m_SpriteRenderer = GetComponent<SpriteRenderer>();
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
				if (m_Ball == null && collision.TryGetComponent(out Ball ball))
				{
						m_Area.SetBall(ball, m_Id);
						m_Ball = ball;
						m_SpriteRenderer.color = Color.red;
						m_BallCentretedCoroutine = BallCentretedRoutine();
						StartCoroutine(m_BallCentretedCoroutine);
				}
		}

		private IEnumerator m_BallCentretedCoroutine;
		private IEnumerator BallCentretedRoutine()
		{
				while (m_Ball)
				{
						yield return new WaitForFixedUpdate();
						if (m_Ball)
						{
								m_TempVelocity = m_Ball.Velocity;
								m_TempVelocity.x = (transform.position.x - m_Ball.transform.position.x) * 10;
								m_Ball.Velocity = m_TempVelocity;
						}

				}
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
				if (collision.TryGetComponent(out Ball ball) && m_Ball)
				{
						m_Area.SetBall(null, m_Id);
						m_Ball = null;
						m_SpriteRenderer.color = Color.white;
						StopCoroutine(m_BallCentretedCoroutine);
				}
		}
}
