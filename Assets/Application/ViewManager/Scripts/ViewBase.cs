using System.Collections.Generic;
using System.Threading.Tasks;

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Views
{
		public class ViewBase : MonoBehaviour
		{
				[field: SerializeField] public bool IsOverlay { get; set; }
				[SerializeField] private Image m_Background;
				[SerializeField] private RectTransform m_Content;
				protected ViewManager viewManager;
				private float m_Alpha;
				public bool IsFocused { get; protected set; }
				private void Awake()
				{
						viewManager = GetComponentInParent<ViewManager>();
				}
				virtual protected void OnEnable()
				{
						if (m_Background)
						{
								m_Alpha = m_Background.color.a;
								Color color = m_Background.color;
								color.a = 0;
								m_Background.color = color;
						}
						if (m_Content)
						{
								m_Content.anchoredPosition = Vector2.up * 1920;
						}
						
						
				}
				virtual protected void OnDisable()
				{
						if (m_Background)
						{
								Color color = m_Background.color;
								color.a = 0;
								m_Background.color = color;
						}
						if (m_Content)
						{
								m_Content.anchoredPosition = Vector2.up * 1920;
						}
				}
				async virtual public Task FocusIn()
				{
						IsFocused = true;
						if (m_Background)
						{
								m_Background.DOKill();
								m_Background.raycastTarget = true;
								m_Background.DOFade(m_Alpha, .25f);
						}
						gameObject.SetActive(true);
						transform.SetAsLastSibling();
						if (m_Content)
						{
								m_Content.DOKill();
								await m_Content.DOAnchorPosY(0, .5f).AsyncWaitForCompletion();
						}
				}
				async virtual public Task FocusOut()
				{
						IsFocused = false;
						if (m_Background)
						{
								m_Background.DOKill();
								m_Background.DOFade(0, 0.25f).OnComplete(() =>
								{
										m_Background.raycastTarget = false;
								});
						}

						if (m_Content)
						{
								m_Content.DOKill();
								await m_Content.DOAnchorPosY(-1920, 0.5f).OnComplete(() =>
								{
										gameObject.SetActive(false);

								}).AsyncWaitForCompletion();
						}
						else
						{
								gameObject.SetActive(false);
						}
						
				}
				protected Dictionary<string, object> data;

				virtual public void ApplyData(Dictionary<string, object> data)
				{
						this.data = data;
				}
		}
}