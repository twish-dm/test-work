using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
		[SerializeField] private Ball[] m_Balls;
		[SerializeField] private Transform m_SpawnPoint;
		[SerializeField] private float m_DelayBetweenSpawn;
		// Start is called before the first frame update
		void Start()
		{
				StartCoroutine(Spawn());
		}

		protected IEnumerator Spawn()
		{
				while(true)
				{
						yield return new WaitForSeconds(m_DelayBetweenSpawn);
						Instantiate(m_Balls[Random.Range(0, m_Balls.Length)], m_SpawnPoint.position - Vector3.right * Random.Range(-m_SpawnPoint.localScale.x/2, m_SpawnPoint.localScale.x/2), Quaternion.identity, transform);
				}
		}
}
