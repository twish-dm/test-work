using UnityEngine.Events;

public interface IController
{
		public void Action();
		event UnityAction OnAction;
}