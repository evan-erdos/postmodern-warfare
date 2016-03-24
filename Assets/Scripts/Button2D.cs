
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Button2D : MonoBehaviour, IDamageable {

	bool wait;

	float delay = 0.5f;

	public bool isOn;

	public GameObject explosion;

	public UnityEvent m_ClickEvent;

	public void Click() {
		isOn = !isOn;
		StartCoroutine(Clicking());
	}

	IEnumerator Clicking() {
		if (wait) yield break;
		wait = true;
		if (!explosion) yield break;
		Object.Instantiate(
			explosion,
			transform.position,
			Quaternion.identity);
		yield return new WaitForSeconds(delay);
		wait = false;
	}


	public void Apply(float damage) {
		if (damage>0) m_ClickEvent.Invoke();
	}

	void Start() {
		if (m_ClickEvent==null)
			m_ClickEvent = new UnityEvent();
		m_ClickEvent.AddListener(Click);
	}
}
