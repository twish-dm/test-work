
using UnityEngine;
using UnityEngine.Events;

public class TapController : MonoBehaviour, IController
{
		public event UnityAction OnAction;

		public void Action()
		{
				OnAction?.Invoke();
		}
		private void Update()
		{
				if(Input.GetMouseButtonDown(0))
				{
						Action();
				}
		}
}