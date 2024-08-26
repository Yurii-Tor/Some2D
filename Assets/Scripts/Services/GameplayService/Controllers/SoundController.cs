using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {

	[SerializeField] private AudioClip m_MatchSound;
	[SerializeField] private AudioClip m_DismatchSound;
	[SerializeField] private AudioClip m_FlippingSound;
	[SerializeField] private AudioClip m_FinishSound;

	private AudioSource m_source;
	private void Awake() {
		m_source = GetComponent<AudioSource>();
	}
	
	public void PlayMatch() {
		m_source.PlayOneShot(m_MatchSound);
	}
	
	public void PlayDismatch() {
		m_source.PlayOneShot(m_DismatchSound);
	}
	
	public void PlayFlipping() {
		m_source.PlayOneShot(m_FlippingSound);
	}
	
	public void PlayFinish() {
		m_source.PlayOneShot(m_FinishSound);
	}
}
