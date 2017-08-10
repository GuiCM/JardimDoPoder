using UnityEngine;

public class StarFromMonster : MonoBehaviour {

    private Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
        Destroy(gameObject, 2f);
	}

    public void AddStarToPlayer()
    {
        player.AddStars(5);
    }
}
