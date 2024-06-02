using Engine.Views;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartView : ViewBase
{
		[SerializeField] private SpawnBalls m_SpawnBalls;

		protected override void OnEnable()
		{
				m_SpawnBalls = Instantiate(m_SpawnBalls);
				base.OnEnable();
		}
		protected override void OnDisable()
		{
				Destroy(m_SpawnBalls.gameObject);
				base.OnDisable();
		}

		public void PlayGame()
		{
				viewManager.Push("GameplayView");
		}
}
