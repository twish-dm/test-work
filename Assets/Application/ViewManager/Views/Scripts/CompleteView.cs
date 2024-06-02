using Engine.Views;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteView : ViewBase
{
		[SerializeField] private Text m_ScoresField;

		public override void ApplyData(Dictionary<string, object> data)
		{
				base.ApplyData(data);
				m_ScoresField.text = data["scores"].ToString();
		}

		public void Restart()
		{
				viewManager.Push("GameplayView");
		}

		public void MainMenu()
		{
				viewManager.Push("StartView");
		}
}
