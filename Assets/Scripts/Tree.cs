using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tree : Unit
{
	public int inHeat;
	public int superHeat;
	public int death;
	public int superdeath;

	public Tree(Vector2 sample) : base(sample)
	{
		setType("tree");
		setAge (0);
		inHeat = Random.Range (500, 750);
		superHeat = Random.Range (20, 40);
		death = Random.Range (4000, 6000);
		superdeath = Random.Range (400, 600);
	}
		
	public void ageIncrement()
	{
		setAge(getAge() + 1);
		if (getAge() >= superdeath)
			{
			this.demodelize (0.0f);
			}
	}

	public void treegasm(List<Tree> unitList, Game loop)
	{
		if (getAge () % superHeat == 0) {
				//Debug.Log ("is valid");
				for (int i = 0; i < 3; i++) {
					Vector2 currentPos = this.getV ();
					Vector2	newPos = this.findPosition (currentPos, 3.0f, 4.5f);

				//debug line drawing
				//Color color = new Color(1.0F, 0.543F, 0.6102941F, 1.0F);

				//Debug.DrawLine (currentPos, newPos, color, death);

					Tree newTree = new Tree (newPos);
					if (newPos != currentPos) {
						unitList.Add (newTree);
						newTree.modelize (loop.getBody (newTree.getType ()));
					}
				}
			}
		}
}