using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
	[SerializeField] private Image healthBarImage = null;
	[SerializeField] private Text livesText = null;
	[SerializeField] private Text chamberAmmoText = null;
	[SerializeField] private Text ammoText = null;
	[SerializeField] private Text granadesText = null;

	public Image GetHealthBarImage => healthBarImage;
	public Text GetLivesText => livesText;
	public Text GetChamberAmmoText => chamberAmmoText;
	public Text GetAmmoText => ammoText;
	public Text GetGranadesText => granadesText;
}
