using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Area : MonoBehaviour
{
		private Game m_Game;
		private void Start()
		{
				m_Game = GetComponentInParent<Game>();
		}


		private Ball[] m_Balls = new Ball[9];
		public void SetBall(Ball ball, int id)
		{
				m_Balls[id] = ball;
				CancelInvoke("ComputeMatch");
				Invoke("ComputeMatch", .5f);
		}

		public void ComputeMatch()
		{
				
				ComputeMatch(0, 1, 2);
				ComputeMatch(3, 4, 5);
				ComputeMatch(6, 7, 8);

				ComputeMatch(6, 4, 2);
				ComputeMatch(0, 4, 8);

				ComputeMatch(0, 3, 6);
				ComputeMatch(1, 4, 7);
				ComputeMatch(2, 5, 8);

				ComputeEndGame();
		}

		private void ComputeEndGame()
		{
				int count = m_Balls.Length;
				for (int i = 0; i < m_Balls.Length; i++)
				{
						if(m_Balls[i])
						{
								count--;
						}
				}
				if(count == 0)
				{
						m_Game.Complete();
				}
		}

		public void ComputeMatch(params int[] ids)
		{
				int count = ids.Length;
				for (int i = 0; i < ids.Length; i++)
				{

						if (m_Balls[ids[i]] && m_Balls[ids[0]] && Color.Equals(m_Balls[ids[0]].Color, m_Balls[ids[i]].Color))
						{
								count--;
						}
				}
				if (count == 0)
				{
						for (int i = 0; i < ids.Length; i++)
						{
								if (m_Balls[ids[i]])
								{
										m_Game.Scores += m_Balls[ids[i]].Score;
										m_Balls[ids[i]].Destory();
								}
						}
				}
		}
}
