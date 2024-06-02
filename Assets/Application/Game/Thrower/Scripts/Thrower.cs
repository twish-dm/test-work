using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Thrower : MonoBehaviour
{
		[SerializeField] private float m_DelayBetweenThrow;
		[SerializeField] private Ball[] m_Balls;
		[SerializeField] private GameObject[] m_SwingObjects;
		[SerializeField] private Transform m_SpawnPoint;

		private Animator m_Animator;
		private IController m_Controller;

		private void Awake()
		{
				m_Animator = GetComponentInChildren<Animator>();
				m_Controller = GameObject.FindObjectOfType<TapController>();
				m_Controller.OnAction += DoThrow;
				LoadBall();
		}

		private Ball m_Ball;
		public Ball Ball
		{
				get
				{
						return m_Ball;
				}
				set
				{
						if (value)
						{
								value.Deactivate();
								SwingObjectsEnable = true;
								m_Animator.enabled = true;
						}
						else if(m_Ball)
						{
								m_Animator.enabled = false;
								SwingObjectsEnable = false;
								m_Ball.transform.parent = transform.parent;
								m_Ball.Activate();
						}

						m_Ball = value;
				}
		}

		private bool m_SwingObjectsEnable;
		protected bool SwingObjectsEnable
		{
				get
				{
						return m_SwingObjectsEnable;
				}
				set
				{
						m_SwingObjectsEnable = value;
						for (int i = 0; i < m_SwingObjects.Length; i++)
						{
								m_SwingObjects[i].SetActive(m_SwingObjectsEnable);
						}
				}
		}
		public void Stop()
		{
				m_Animator.enabled = false;
				SwingObjectsEnable = false;
				if (Ball)
				{
						Destroy(Ball.gameObject);
						Ball = null;
				}
				StopCoroutine(m_ReloadCoroutine);
		}
		public void DoThrow()
		{
				if (Ball == null) return;
				//Ball.Force(-transform.up);
				Ball = null;
				m_ReloadCoroutine = ReloadRoutine();
				StartCoroutine(m_ReloadCoroutine);
		}

		private IEnumerator m_ReloadCoroutine;
		private IEnumerator ReloadRoutine()
		{
				yield return new WaitForSeconds(m_DelayBetweenThrow);
				LoadBall();
		}

		public void LoadBall()
		{
				Ball = Instantiate(m_Balls[UnityEngine.Random.Range(0, m_Balls.Length)], m_SpawnPoint.position, Quaternion.identity, transform);
		}


}
