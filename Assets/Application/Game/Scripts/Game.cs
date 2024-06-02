using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
		public UnityEvent<int> OnScoresChanged;
		private int m_Scores;
		public int Scores
		{
				get
				{
						return m_Scores;
				}

				set
				{
						m_Scores = value;
						OnScoresChanged?.Invoke(m_Scores);
				}
		}

		public UnityEvent<int> OnComplete;
		public void Complete()
		{
				OnComplete?.Invoke(m_Scores);
				GetComponentInChildren<Thrower>().Stop();
				Debug.Log("Complete");
		}
}
