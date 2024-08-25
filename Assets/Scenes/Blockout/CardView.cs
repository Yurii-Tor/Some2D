using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CardView : MonoBehaviour {
	private const float REVEAL_TIME = .7f;

	private Transform m_cachedTransform;
	private Coroutine m_animationCoroutine;

	private Sprite m_backSideSprite;
	private Sprite m_frontSideSprite;

	private SpriteRenderer m_spriteRenderer;
	
	private void Awake() {
		m_cachedTransform = transform;
		m_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Initialize(Sprite backSideSpite, Sprite frontSideSprite) {
		m_backSideSprite = backSideSpite;
		m_frontSideSprite = frontSideSprite;
		
		SetBackSprite();
	}

	public void Reveal(Action callback) {
		if (m_animationCoroutine is { }) {
			StopCoroutine(m_animationCoroutine);
		}
		m_animationCoroutine = StartCoroutine(RevealCoroutine(callback));
	}

	private IEnumerator RevealCoroutine(Action callback) {
		var elapsedTime = 0f;
		var halfTime = REVEAL_TIME * .5f;
		transform.rotation = quaternion.identity;
		SetBackSprite();

		var previousAngle = 0f;
		while (elapsedTime < halfTime) { // flip 90 degrees for hiding the content
			var angle = 90f * elapsedTime / halfTime; // interpolate the angle
			m_cachedTransform.Rotate(Vector3.up, angle - previousAngle);
			previousAngle = angle;
			elapsedTime += Time.deltaTime;
			yield return null; // wait for the next frame without allocating an extra memory
		}
		
		SetFrontSprite();

		previousAngle = 0f;
		while (elapsedTime < REVEAL_TIME) { // flip 90 degrees for showing the content
			var angle = 90f * (1f - elapsedTime / halfTime); // interpolate the angle
			m_cachedTransform.Rotate(Vector3.up, angle - previousAngle);
			previousAngle = angle;
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.rotation = quaternion.identity;
	}

	private void SetFrontSprite() {
		m_spriteRenderer.sprite = m_frontSideSprite;
	}
	
	private void SetBackSprite() {
		m_spriteRenderer.sprite = m_backSideSprite;
	}
}
