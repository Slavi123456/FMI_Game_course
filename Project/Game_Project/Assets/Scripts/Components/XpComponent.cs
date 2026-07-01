using UnityEngine;

public class XpComponent: MonoBehaviour
{
    private int level = 0;
    public int currentXP = 0;
    public int currentXPthreshold = 20;

    public void AddXP(int xp)
    {
        this.currentXP += xp;
        Debug.Log($"Player current xp: {currentXP}");

        if (this.currentXP >= this.currentXPthreshold)
        {
            this.currentXP -= this.currentXPthreshold;
            this.LevelUp();
        }
    }

    public void LevelUp() {
        this.level ++;

        this.currentXPthreshold = Mathf.RoundToInt(this.currentXPthreshold * 1.5f);
        Debug.Log($"Player LEVEL UP: {this.level}");
    }
}
