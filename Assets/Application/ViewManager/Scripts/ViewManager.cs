using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Engine.Views
{
		public class ViewManager : MonoBehaviour
		{
				public Stack<string> m_ViewsStack = new Stack<string>();
				private Dictionary<string, ViewBase> m_ViewsMap = new Dictionary<string, ViewBase>();
				public ViewBase CurrentView { get; protected set; }
				public ViewBase OverlayView { get; protected set; }
				async virtual public void Push(string name, Dictionary<string, object> data, bool popAll)
				{
						if (popAll)
								PopAll();

						if (CurrentView && !CurrentView.IsOverlay)
						{
								
								await CurrentView.FocusOut();
								if(CurrentView)
										Remove(CurrentView.name);
						}


						CurrentView = Add(name);
						if (CurrentView.IsOverlay)
						{

								OverlayView = CurrentView;
						}
						m_ViewsStack.Push(name);
						CurrentView.ApplyData(data);
						await CurrentView.FocusIn();
				}
				async virtual public void Pop(Dictionary<string, object> data)
				{
						if (CurrentView && !CurrentView.IsOverlay)
						{

								await CurrentView.FocusOut();
								Remove(CurrentView.name);
								m_ViewsStack.Pop();
								CurrentView = null;
						}
						if (!CurrentView && m_ViewsStack.TryPeek(out string peek))
						{
								CurrentView = Add(peek);
								CurrentView.ApplyData(data);
								await CurrentView.FocusIn();
						}
				}
				virtual protected void Remove(string name)
				{
						Destroy(m_ViewsMap[name].gameObject);
						m_ViewsMap.Remove(name);
				}
				virtual protected ViewBase Add(string name)
				{
						ViewBase view = null;
						if (!m_ViewsMap.ContainsKey(name))
						{
								view = LoadView(name);
								m_ViewsMap.Add(name, view);
						}
						else
						{
								view = m_ViewsMap[name];
						}
						return view;
				}
				virtual public void Push(string name, bool popAll)
				{
						Push(name, null, popAll);
				}
				virtual public void Push(string name)
				{
						Push(name, null, true);
				}
				async virtual public void PopAll()
				{
						if (CurrentView && !CurrentView.IsOverlay)
						{
								await CurrentView.FocusOut();
								Remove(CurrentView.name);
								m_ViewsStack.Clear();
						}
				}
				
				virtual public void Pop()
				{
						Pop(null);
				}
				virtual protected ViewBase LoadView(string name)
				{
						Debug.Log(name);
						ViewBase view = Instantiate(Resources.Load<ViewBase>($"Views/{name}"), transform);
						view.name = view.name.Replace("(Clone)", "");
						return view;
				}
		}
}