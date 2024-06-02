using Engine.Views;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameplayView : ViewBase
{
		[SerializeField] private Game m_Game;
		[SerializeField] private Text m_ScoresField;
		private void Start()
		{
				
				
		}

		protected override void OnEnable()
		{
				m_Game = Instantiate(m_Game);
				m_Game.OnComplete.AddListener(CompleteHandler);
				m_Game.OnScoresChanged.AddListener(ScoresHandler);
				base.OnEnable();
		}

		protected override void OnDisable()
		{
				m_Game.OnComplete.RemoveListener(CompleteHandler);
				m_Game.OnScoresChanged.RemoveListener(ScoresHandler);
				Destroy(m_Game.gameObject);
				base.OnDisable();
				
		}

		protected void CompleteHandler(int scores)
		{
				viewManager.Push("CompleteView", new Dictionary<string, object>() { { "scores", scores } }, true);
		}
		protected void ScoresHandler(int scores)
		{
				m_ScoresField.text = scores.ToString();
		}
}
