using Engine.Views;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Main : MonoBehaviour
{
		private ViewManager m_ViewManager;
		void Start()
		{
				m_ViewManager = GetComponentInChildren<ViewManager>();
				m_ViewManager.Push("StartView");
		}
}
