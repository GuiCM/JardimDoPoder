using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField]
	protected Image bgText;
	[SerializeField]
	protected Sprite textSprite;
	[SerializeField]
	protected Sprite textSpriteHover;

	//Mouse enter 
	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (!this.GetComponent<Animation> ().isPlaying) {
			bgText.enabled = true;
			this.GetComponent<Image> ().sprite = textSpriteHover;
		}
	}

	//Mouse leave
	public virtual void OnPointerExit(PointerEventData eventData)
	{
		bgText.enabled = false;
		this.GetComponent<Image> ().sprite = textSprite;
	}
}
