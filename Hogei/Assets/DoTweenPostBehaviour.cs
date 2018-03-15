using UnityEngine;
using UnityEngine.PostProcessing;
using DG.Tweening;

public class DoTweenPostBehaviour : MonoBehaviour
{
	PostProcessingProfile m_Profile;

	void Awake ()
	{
		Init ();
	}

	/// <summary>
	/// Initialize the post profile and make a runtime duplicate.
	/// </summary>
	void Init ()
	{
		var behaviour = Camera.main.GetComponent<PostProcessingBehaviour>();

		if (behaviour.profile == null)
		{
			enabled = false;
			return;
		}

		m_Profile = Instantiate(behaviour.profile);
		behaviour.profile = m_Profile;
	}

	public float MyFloat = 1f;

	void TweenTheThing ()
	{
		float floatValue = 5f;
		float duration = 1f;

		MyFloat = 0f;

		DOTween.To ( () => MyFloat, value => MyFloat = value, floatValue, duration )
			.SetEase(Ease.InOutSine);
	}

	void Update()
	{
		// push space to tween the value.
		if (Input.GetKeyDown (KeyCode.Space))
			TweenTheThing ();

//		var vignette = m_Profile.vignette.settings;
//		vignette.smoothness = Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup) * 0.99f) + 0.01f;
//		m_Profile.vignette.settings = vignette;
	}
}